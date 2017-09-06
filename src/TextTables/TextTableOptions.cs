namespace TextTables
{
    public class TextTableOptions
    {
        public TextTableCharset Charset { get; set; }
        public TextTablePadding Padding { get; set; }
        public TextTableHorizontalAlignment HorizontalAlignment { get; set; }
        public TextTableVerticalAlignment VerticalAlignment { get; set; }

        public TextTableOptions()
        {
            Charset = TextTableCharset.GetStandardCharset();
            Padding = new TextTablePadding();
            HorizontalAlignment = TextTableHorizontalAlignment.Center;
            VerticalAlignment = TextTableVerticalAlignment.Center;
        }
    }
}