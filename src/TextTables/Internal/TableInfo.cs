using System.Collections.Generic;

namespace TextTables.Internal
{
    internal class TableInfo
    {
        public IList<RowInfo> Rows { get; }
        public IList<ColumnInfo> Columns { get; }
        public Size TableSize { get; set; }

        public TableInfo()
        {
            Rows = new List<RowInfo>();
            Columns = new List<ColumnInfo>();
        }
    }
}