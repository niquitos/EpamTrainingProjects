using Microsoft.VisualBasic.FileIO;
using System.Data;

namespace SqlProject.Services.Data
{
    public class CsvDataService : IDataService
    {
        public DataTable GetData(string connectionString)
        {
            using (TextFieldParser parser = new(connectionString))
            {
                DataTable dt = new();

                parser.CommentTokens = new string[] { "#" };
                parser.SetDelimiters(new string[] { "," });
                parser.HasFieldsEnclosedInQuotes = true;

                string[]? headers = parser.ReadFields();
                if (headers == null) return null;

                foreach (string h in headers)
                {
                    dt.Columns.Add(h);
                }

                while (!parser.EndOfData)
                {
                    string[]? cells = parser.ReadFields();
                    if (cells != null)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            string value = i > cells.Length - 1 ? "" : cells[i];
                            dr[i] = value;
                        }
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
        }
    }
}
