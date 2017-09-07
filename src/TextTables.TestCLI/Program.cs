using System;

namespace TextTables.TestCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var table = new TextTable();
            table.AddRow(1, "2", 3.5);
            Console.WriteLine(table);
        }
    }
}
