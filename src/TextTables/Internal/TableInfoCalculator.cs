using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextTables.Internal
{
    internal class TableInfoCalculator
    {
        public TableInfo CalculateTableInfo(TextTable table)
        {
            var tableInfo = new TableInfo();
            var rowSpans = new Dictionary<int, SpanInfo>();
            var columnCount = 0;
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var rowInfo = new RowInfo();
                rowInfo.Index = i;
                var row = table.Rows[i];
                var col = 0;
                for (var j = 0; j < row.Cells.Count; j++)
                {
                    var cell = row.Cells[j];
                    var info = new CellInfo(cell);
                    info.Lines = cell.Content.ToString().Replace("\r", string.Empty).Split('\n');
                    if (cell.MaxRowLength.HasValue)
                    {
                        info.Lines = info.Lines.SelectMany(x => ChunksUpto(x, cell.MaxRowLength.Value)).ToArray();
                    }
                    info.ContentSize = CalculateCellSize(info.Lines);
                    var span = rowSpans.AddOrCreate(col);
                    if (span.RowSpan > 0)
                    {
                        col += span.ColSpan;
                        span.RowSpan--;
                    }
                    info.Column = col;
                    info.Row = i;
                    if (info.Cell.RowSpan > 1)
                    {
                        span = rowSpans.AddOrCreate(col);
                        span.RowSpan = cell.RowSpan - 1;
                        span.ColSpan = cell.ColSpan;
                    }
                    for (var k = 0; k < cell.ColSpan; k++)
                    {
                        info.Columns.Add(col);
                        col++;
                    }
                    var endRow = i + cell.RowSpan;
                    for (var k = i; k < endRow; k++)
                    {
                        info.Rows.Add(k);
                    }
                    rowInfo.Cells.Add(info);
                }
                if (col > columnCount)
                {
                    columnCount = col;
                }
                tableInfo.Rows.Add(rowInfo);
            }
            CalculateRowColSizes(tableInfo, table);
            var currentHeight = 0;
            foreach (var rowInfo in tableInfo.Rows)
            {
                //var currentWidth = 0;
                foreach (var cellInfo in rowInfo.Cells)
                {
                    var cellSize = new Size();
                    cellSize.Height = cellInfo.Rows.Sum(x => tableInfo.Rows[x].Height) - cellInfo.Rows.Count + 1;
                    cellSize.Width = cellInfo.Columns.Sum(x => tableInfo.Columns[x].Width) - cellInfo.Columns.Count + 1;
                    cellInfo.CellSize = cellSize;

                    var position = new Size();
                    position.Height = currentHeight;
                    position.Width = tableInfo.Columns.Take(cellInfo.Column).Sum(x => x.Width) - cellInfo.Column;
                    cellInfo.Position = position;

                    //currentWidth += cellSize.Width-1;
                }
                currentHeight += rowInfo.Height - 1;
            }
            var tableSize = new Size();
            tableSize.Width = tableInfo.Columns.Sum(x => x.Width) - tableInfo.Columns.Count + 1;
            tableSize.Height = tableInfo.Rows.Sum(x => x.Height) - tableInfo.Rows.Count + 1;
            tableInfo.TableSize = tableSize;
            return tableInfo;
        }

        private IEnumerable<string> ChunksUpto(string str, int maxChunkSize)
        {
            if (str.Length == 0)
                yield return str;
            for (int i = 0; i < str.Length; i += maxChunkSize)
                yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
        }

        private void CalculateRowColSizes(TableInfo tableInfo, TextTable table)
        {
            var byRow = new Dictionary<int, IList<CellInfo>>();
            var byCol = new Dictionary<int, IList<CellInfo>>();
            for (var i = 0; i < tableInfo.Rows.Count; i++)
            {
                var rowInfo = tableInfo.Rows[i];
                for (var j = 0; j < rowInfo.Cells.Count; j++)
                {
                    var cellInfo = rowInfo.Cells[j];
                    foreach (var rowIndex in cellInfo.Rows)
                    {
                        var cells = byRow.AddOrCreate(rowIndex, () => new List<CellInfo>());
                        cells.Add(cellInfo);
                    }
                    foreach (var colIndex in cellInfo.Columns)
                    {
                        var cells = byRow.AddOrCreate(colIndex, () => new List<CellInfo>());
                        cells.Add(cellInfo);
                    }
                }
            }
            var horizontalPadding = table.Options.Padding.Left + table.Options.Padding.Right;
            var colSizes = GetMinSizeValue(byCol, horizontalPadding, x => x.ContentSize.Width, x => x.Cell.ColSpan);
            for (var i = 0; i < colSizes.Count; i++)
            {
                tableInfo.Columns.Add(new ColumnInfo());
            }
            foreach (var colSize in colSizes)
            {
                tableInfo.Columns[colSize.Key].Width = colSize.Value;
            }

            var verticalPadding = table.Options.Padding.Top + table.Options.Padding.Bottom;
            var rowSizes = GetMinSizeValue(byRow, verticalPadding, x => x.ContentSize.Height, x => x.Cell.RowSpan);
            foreach (var rowSize in rowSizes)
            {
                tableInfo.Rows[rowSize.Key].Height = rowSize.Value;
            }
        }

        private IDictionary<int, int> GetMinSizeValue(IDictionary<int, IList<CellInfo>> data, int padding, Func<CellInfo, int> sizeSelector, Func<CellInfo, int> spanSelector)
        {
            var sizes = new Dictionary<int, int>();
            foreach (var pair in data)
            {
                var max = 0;
                foreach (var info in pair.Value)
                {
                    var cellSize = sizeSelector(info);
                    var span = spanSelector(info);
                    var size = (cellSize / span) + padding;
                    if (size < 1)
                    {
                        size = 1;
                    }
                    if (size > max)
                    {
                        max = size;
                    }
                }
                sizes[pair.Key] = max + 2;
            }
            return sizes;
        }

        private Size CalculateCellSize(IList<string> lines)
        {
            var size = new Size();
            size.Height = lines.Count;
            size.Width = lines.Max(x => x.Length);
            return size;
        }
    }
}
