using System;

namespace TextTables.TestCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            TextTable table = new TextTable();
            table.AddRow("TextTables.NET demo").WithColSpan(0,4);
            table.AddRow("Multiple columns", "Or rows", 1.6180).WithColSpan(0,2).WithRowSpan(1,2);
            table.AddRow(3.14, "foo", "bar");
            Console.WriteLine(table);

            // +---------------------------------------+
            // |          TextTables.NET demo          |
            // +---------------------+---------+-------+
            // |  Multiple columns   |         | 1.618 |
            // +----------+----------+ Or rows +-------+
            // |   3.14   |   foo    |         |  bar  |
            // +----------+----------+---------+-------+

            table.Options.Charset = TextTableCharset.GetBoxCharset();
            Console.WriteLine(table);

            // ┌────────────────────────────────────────┐
            // │          TextTables.NET demo           │
            // ├──────────┬──────────┬─────────┬────────┤
            // │    1     │    2     │    3    │ 3.1415 │
            // ├──────────┴──────────┼─────────┼────────┤
            // │  Multiple columns   │         │ 1.618  │
            // ├──────────┬──────────┤ Or rows ├────────┤
            // │   foo    │   bar    │         │ 2.7182 │
            // └──────────┴──────────┴─────────┴────────┘

            Console.ReadLine();
        }
    }
}
