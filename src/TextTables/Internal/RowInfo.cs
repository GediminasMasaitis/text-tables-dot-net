using System.Collections.Generic;

namespace TextTables.Internal
{
    internal class RowInfo
    {
        public IList<CellInfo> Cells { get; }
        public int Height { get; set; }
        public int Index { get; set; }

        public RowInfo()
        {
            Cells = new List<CellInfo>();
        }
    }
}