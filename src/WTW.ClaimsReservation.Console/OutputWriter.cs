using ClaimsReservation.Core.Models;
using System.IO;
using System.Linq;
using System.Text;

namespace ClaimsReservation.ConsoleApp
{
    public class OutputWriter
    {
        /// <summary>
        /// Given a Triangle set, write the data to a file.
        /// </summary>
        /// <param name="output">The stream to write data to. Will be closed when method completed</param>
        /// <param name="data">The <TriangleSet> to output </param>
        public void WriteOutput(Stream output, TriangleSet data)
        {
            using (var sw = new StreamWriter(output))
            {
                sw.WriteLine($"{data.EarliestOriginYear},{data.NumberOfDevelopmentYears}");

                foreach (var t in data.Triangles)
                {
                    var stringBuilder = new StringBuilder(t.ProductName);

                    foreach (var year in t.Matrix.Keys)
                    {
                        stringBuilder.Append(",").Append(string.Join(",", t.Matrix[year].Select(x => x.ToString("#,##0.##"))));
                    }

                    sw.WriteLine(stringBuilder.ToString());
                }
            }
        }
    }
}
