namespace TextTables
{
    public class TextTableCell
    {
        public int ColSpan { get; set; }
        public int RowSpan { get; set; }
        public object Content { get; set; }
        public int? MaxRowLength { get; set; }

        public TextTableCell(object content) : this()
        {
            Content = content;
        }

        public TextTableCell()
        {
            ColSpan = 1;
            RowSpan = 1;
        }

        public TextTableCell WithColSpan(int colSpan)
        {
            ColSpan = colSpan;
            return this;
        }

        public TextTableCell WithRowSpan(int rowSpan)
        {
            RowSpan = rowSpan;
            return this;
        }
    }
}