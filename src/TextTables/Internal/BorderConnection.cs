using System;

namespace TextTables.Internal
{
    [Flags]
    internal enum BorderConnection
    {
        None = 0,

        Up = 1 << 0,
        Down = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3,

        Vertical = Up | Down,
        Horizontal = Left | Right,
        Cross = Vertical | Horizontal,

        CornerUpLeft = Up | Left,
        CornerUpRight = Up | Right,
        CornerDownLeft = Down | Left,
        CornerDownRight = Down | Right,

        VerticalLeft = Vertical | Left,
        VerticalRight = Vertical | Right,
        HorizontalUp = Horizontal | Up,
        HorizontalDown = Horizontal | Down
    }
}