using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClaimsReservation.Models
{
    public class Triangle
    {
        public String ProductName { get; set; }
        public int EarliestOriginYear { get; set; }
        public int DevelopmentYears { get; set;  }
        public Dictionary<int, decimal[]> Matrix { get; set; }

     


        public Triangle(string productName, int minOrigin, int devYears)
        {
            ProductName = productName;
            EarliestOriginYear = minOrigin;
            DevelopmentYears = devYears;

            Matrix = new Dictionary<int, decimal[]>();

            for (int year = 0; year < devYears; year++)
            {
                Matrix.Add(minOrigin + year, new decimal[devYears - year]);
            }
        }

        public override string ToString()
        {

            var stringBuilder = new StringBuilder(ProductName);

            foreach (var year in Matrix.Keys)
            {
                stringBuilder.Append(",").Append(string.Join(",", Matrix[year].Select(x => x.ToString("#,##0.##"))));
            }

            return stringBuilder.ToString();
        }


    }

}
