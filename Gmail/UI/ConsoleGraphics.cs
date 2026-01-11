namespace Gmail.UI
{
    public class ConsoleGraphics
    {
        public const ConsoleColor AccentColor = ConsoleColor.Blue;
        public const ConsoleColor DefaultColor = ConsoleColor.Gray;

        public int CurrRow { get; set; } = 0;

        public void Print(string text, ConsoleColor color = DefaultColor)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(GetXFromCol(0), CurrRow);

            Console.WriteLine(text);

            CurrRow++;
        }

        public void Clear()
        {
            Console.Clear();
            CurrRow = 0;
        }

        public void EmptyLine()
        {
            CurrRow++;
        }

        public int GetXFromCol(int col)
        {
            return col * 10;
        }
    }
}
