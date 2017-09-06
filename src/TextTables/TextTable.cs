using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using TextTables.Internal;

namespace TextTables
{
    public class TextTable
    {
        public IList<TextTableRow> Rows { get; protected set; }

        public TextTableOptions Options { get; protected set; }
        public TextTable(TextTableOptions options = null)
        {
            Options = options ?? new TextTableOptions();
            Rows = new List<TextTableRow>();
        }

        public TextTableRow AddRow(params object[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            var cells = values.Select(x => x as TextTableCell ?? new TextTableCell(x)).ToList();
            var row = new TextTableRow(cells);
            Rows.Add(row);
            return row;
        }

        public string Build()
        {
            var calc = new TableInfoCalculator();
            var info = calc.CalculateTableInfo(this);
            var sb = new StringBuilder();
            var writer = new TableWriter(sb, info.TableSize, Options.Charset);
            var builder = new TableBuilder();
            builder.Build(info, writer);
            var str = sb.ToString();
            return str;
        }

        public override string ToString()
        {
            return Build();
        }
    }
}
