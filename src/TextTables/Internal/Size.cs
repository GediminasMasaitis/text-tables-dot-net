namespace TextTables.Internal
{
    internal class Size
    {
        public int Width { get; set; }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }

        public int Height { get; set; }
    }
}