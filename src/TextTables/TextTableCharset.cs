namespace TextTables
{
    public class TextTableCharset
    {
        public char None { get; set; }
        public char CornerUpLeft { get; set; }
        public char CornerUpRight { get; set; }
        public char CornerDownLeft { get; set; }
        public char CornerDownRight { get; set; }
        public char Cross { get; set; }
        public char VerticalLeft { get; set; }
        public char VerticalRight { get; set; }
        public char HorizontalUp { get; set; }
        public char HorizontalDown { get; set; }
        public char Vertical { get; set; }
        public char Horizontal { get; set; }

        public static TextTableCharset GetStandardCharset()
        {
            var set = new TextTableCharset();
            set.None = ' ';
            set.CornerUpLeft = '+';
            set.CornerUpRight = '+';
            set.CornerDownLeft = '+';
            set.CornerDownRight = '+';
            set.Cross = '+';
            set.VerticalLeft = '+';
            set.VerticalRight = '+';
            set.HorizontalUp = '+';
            set.HorizontalDown = '+';
            set.Vertical = '|';
            set.Horizontal = '-';
            return set;
        }

        public static TextTableCharset GetBoxCharset()
        {
            var set = new TextTableCharset();
            set.None = ' ';
            set.CornerUpLeft = (char)0x250C;
            set.CornerUpRight = (char)0x2510;
            set.CornerDownLeft = (char)0x2514;
            set.CornerDownRight = (char)0x2518;
            set.Cross = (char)0x253C;
            set.VerticalLeft = (char)0x251C;
            set.VerticalRight = (char)0x2524;
            set.HorizontalUp = (char)0x252C;
            set.HorizontalDown = (char)0x2534;
            set.Vertical = (char)0x2502;
            set.Horizontal = (char)0x2500;
            return set;
        }
    }
}