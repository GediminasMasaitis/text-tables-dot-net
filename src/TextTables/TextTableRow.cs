using System.Collections.Generic;

namespace TextTables
{
    public class TextTableRow
    {
        public TextTableRow()
        {
            Cells = new List<TextTableCell>();
        }

        public TextTableRow(IList<TextTableCell> cells)
        {
            Cells = cells;
        }

        public IList<TextTableCell> Cells { get; set; }

        public TextTableRow WithColSpan(int cell, int colSpan)
        {
            Cells[cell].ColSpan = colSpan;
            return this;
        }

        public TextTableRow WithRowSpan(int cell, int rowSpan)
        {
            Cells[cell].RowSpan = rowSpan;
            return this;
        }

        public TextTableCell AddValue(TextTableCell cell)
        {
            Cells.Add(cell);
            return cell;
        }

        public TextTableCell AddValue(object value)
        {
            var cell = new TextTableCell(value);
            Cells.Add(cell);
            return cell;
        }
    }
}