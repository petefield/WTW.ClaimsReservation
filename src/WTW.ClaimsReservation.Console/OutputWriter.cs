using ClaimsReservation.Core.Models;
using System.IO;

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
                    sw.WriteLine(t.ToString());
                }
            }
        }
    }
}
