using ClaimsReservation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClaimsReservation
{
    public class OutputWriter
    {
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
