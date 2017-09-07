using System;
using System.Text;

namespace TextTables.Internal
{
    internal class TableBuilder
    {
        public void Build(TableInfo info, TableWriter writer)
        {
            foreach (var row in info.Rows)
            {
                foreach (var cell in row.Cells)
                {
                    BuildCellWalls(writer, cell);
                }
            }
            foreach (var row in info.Rows)
            {
                foreach (var cell in row.Cells)
                {
                    BuildCellCorners(writer, cell);
                }
            }
            foreach (var row in info.Rows)
            {
                foreach (var cell in row.Cells)
                {
                    BuildCellContent(writer, cell);
                }
            }
        }

        private void BuildCellWalls(TableWriter tb, CellInfo cell)
        {
            var pos = cell.Position;
            var size = cell.CellSize;
            for (var i = 1; i < size.Height - 1; i++)
            {
                tb.AddConnection(i + pos.Height, pos.Width, BorderConnection.Vertical);
                tb.AddConnection(i + pos.Height, pos.Width + size.Width - 1, BorderConnection.Vertical);
            }
            for (var i = 1; i < size.Width - 1; i++)
            {
                tb.AddConnection(pos.Height, i + pos.Width, BorderConnection.Horizontal);
                tb.AddConnection(pos.Height + size.Height - 1, i + pos.Width, BorderConnection.Horizontal);
            }
        }

        private void BuildCellCorners(TableWriter tb, CellInfo cell)
        {
            var pos = cell.Position;
            var size = cell.CellSize;
            tb.AddConnection(pos.Height, pos.Width, BorderConnection.Up | BorderConnection.Left);
            tb.AddConnection(pos.Height, pos.Width + size.Width - 1, BorderConnection.Up | BorderConnection.Right);
            tb.AddConnection(pos.Height + size.Height - 1, pos.Width, BorderConnection.Down | BorderConnection.Left);
            tb.AddConnection(pos.Height + size.Height - 1, pos.Width + size.Width - 1, BorderConnection.Down | BorderConnection.Right);
        }

        private void BuildCellContent(TableWriter tb, CellInfo cell)
        {
            var pos = cell.Position;
            var padTop = (cell.CellSize.Height - cell.Lines.Count) / 2;
            for (var i = 0; i < cell.Lines.Count; i++)
            {
                var line = cell.Lines[i];
                var padLeft = (cell.CellSize.Width - line.Length) / 2;
                tb.WriteAt(pos.Height + i + padTop, pos.Width + padLeft, line);
            }
        }
    }

    internal class TableWriter
    {
        public BorderConnection[,] Connections { get; }
        public StringBuilder InnerBuilder { get; }
        public Size TableSize { get; }
        public TextTableCharset Charset { get; }

        public TableWriter(StringBuilder innerBuilder, Size tableSize, TextTableCharset charset)
        {
            InnerBuilder = innerBuilder;
            TableSize = tableSize;
            Charset = charset;
            // Use InnerBuilder.Clear() for .NET 4.0+
            InnerBuilder.Length = 0;
            InnerBuilder.Capacity = tableSize.Height * (tableSize.Width + 1);
            for (var i = 0; i < tableSize.Height; i++)
            {
                for (var j = 0; j < tableSize.Width; j++)
                {
                    InnerBuilder.Append(' ');
                }
                InnerBuilder.Append('\n');
            }
            Connections = new BorderConnection[tableSize.Height, tableSize.Width];
        }

        public void AddConnection(int row, int col, BorderConnection connection)
        {
            Connections[row, col] |= connection;
            var newConnection = Connections[row, col];
            var ch = ConnectionToChar(newConnection);
            SetChar(row, col, ch);
        }

        private char ConnectionToChar(BorderConnection connection)
        {
            switch (connection)
            {
                case BorderConnection.None: return Charset.None;
                case BorderConnection.Up: return Charset.Vertical;
                case BorderConnection.Down: return Charset.Vertical;
                case BorderConnection.Left: return Charset.Horizontal;
                case BorderConnection.Right: return Charset.Horizontal;
                case BorderConnection.Vertical: return Charset.Vertical;
                case BorderConnection.Horizontal: return Charset.Horizontal;
                case BorderConnection.Cross: return Charset.Cross;
                case BorderConnection.CornerUpLeft: return Charset.CornerUpLeft;
                case BorderConnection.CornerUpRight: return Charset.CornerUpRight;
                case BorderConnection.CornerDownLeft: return Charset.CornerDownLeft;
                case BorderConnection.CornerDownRight: return Charset.CornerDownRight;
                case BorderConnection.VerticalLeft: return Charset.VerticalLeft;
                case BorderConnection.VerticalRight: return Charset.VerticalRight;
                case BorderConnection.HorizontalUp: return Charset.HorizontalUp;
                case BorderConnection.HorizontalDown: return Charset.HorizontalDown;
                default:
                    throw new ArgumentOutOfRangeException(nameof(connection), connection, null);
            }
        }

        public void SetChar(int row, int col, char ch)
        {
            InnerBuilder[row * (TableSize.Width + 1) + col] = ch;
        }

        public void WriteAt(int row, int col, string msg)
        {
            for (var i = 0; i < msg.Length; i++)
            {
                SetChar(row, col + i, msg[i]);
            }
        }

        /*public char this[int i, int j]
        {
            get => InnerBuilder[i * (TableSize.Width + 1) + j];
            set => InnerBuilder[i * (TableSize.Width + 1) + j] = value;
        }*/
    }
}