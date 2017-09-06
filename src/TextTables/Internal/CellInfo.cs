using System.Collections.Generic;

namespace TextTables.Internal
{
    internal class CellInfo
    {
        public CellInfo(TextTableCell cell)
        {
            Cell = cell;
            Rows = new HashSet<int>();
            Columns = new HashSet<int>();
        }

        public TextTableCell Cell { get; }
        public IList<string> Lines { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public HashSet<int> Rows { get; }
        public HashSet<int> Columns { get; }
        public Size Position { get; set; }
        public Size ContentSize { get; set; }
        public Size CellSize { get; set; }
    }
}