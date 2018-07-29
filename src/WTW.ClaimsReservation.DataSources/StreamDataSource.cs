using ClaimsReservation.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClaimsReservation.DataSources
{
    public class StreamDataSource : IDataSource
    {
        public StreamDataSource(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            this.stream = stream;
        }

        private const string HEADERCONTENTS = "Product,Origin Year,Development Year,Incremental Value";
        private readonly Stream stream;
        private enum Columns
        {
            PRODUCTNAME = 0,
            ORIGINYEAR = 1,
            DEVLOPMENTYEAR = 2,
            AMOUNT = 3
        }

        private int GetColumnValueAsInt(string[] tokens, Columns col, int row)
        {
            if (!int.TryParse(tokens[(int)col], out int value))
            {
                var ex = new InvalidDataException("Invalid Data");
                ex.Data.Add("column", HEADERCONTENTS.Split(',')[(int)col]);
                ex.Data.Add("row", row);
                throw ex;
            }

            return value;
        }

        private string GetColumnValueAsString(string[] tokens, Columns col, int row)
        {
            string value = tokens[(int)col];

            if (String.IsNullOrWhiteSpace(value))
            {
                var ex = new InvalidDataException("Invalid Data");
                ex.Data.Add("column", HEADERCONTENTS.Split(',')[(int)col]);
                ex.Data.Add("row", row);
                throw ex;
            }

            return value.Trim();
        }

        private decimal GetColumnValueAsDecimal(string[] tokens, Columns col, int row)
        {
            if (!decimal.TryParse(tokens[(int)col], out decimal value))
            {
                var ex = new InvalidDataException("Invalid Data");
                ex.Data.Add("column", HEADERCONTENTS.Split(',')[(int)col]);
                ex.Data.Add("row", row);
                throw ex;
            }

            return value;
        }

        public IEnumerable<DataRow> ParsedData {
            get
            {
                stream.Position = 0;

                var reader = new StreamReader(stream);

                string header = reader.ReadLine();

                if (!header.Replace(", ", ",").Equals(HEADERCONTENTS, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidDataException($"Incorrect Header : Should be {HEADERCONTENTS}. Is {header}");
                }

                var rowIndex = 1;

                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine();
                    var tokens = row.Split(',');

                    if (tokens.Length != 4)
                    {
                        var ex = new InvalidDataException($"Incorrect number of columns at row {rowIndex}");
                        ex.Data.Add("row", rowIndex);
                        throw ex;
                    }

                    var productName = GetColumnValueAsString(tokens, Columns.PRODUCTNAME, rowIndex);
                    var originYear = GetColumnValueAsInt(tokens, Columns.ORIGINYEAR, rowIndex);
                    var developYear = GetColumnValueAsInt(tokens, Columns.DEVLOPMENTYEAR, rowIndex);
                    var amount = GetColumnValueAsDecimal(tokens, Columns.AMOUNT, rowIndex);
                    rowIndex++;
                    yield return new DataRow(productName, originYear, developYear, amount);

                }
            }
        }
    }

}