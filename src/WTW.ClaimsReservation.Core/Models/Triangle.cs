using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClaimsReservation.Core.Models
{
    /// <summary>
    /// Represents a claim triangle.
    /// A Claim Triangle has a productname, earliest orgin year, and development years.
    /// Matrix is a Dictionary indexed by origin year, the value of each origin
    /// year is an array whose legnth is equal to the number of development years valid
    /// for the origin year.
    /// </summary>
    /// <example>
    /// 
    /// <code>New Triangle("prodname", 1990, 4) </code>
    /// creates the following triangle
    ///         1   2   3   4
    /// 1990    0   0   0   0
    /// 1991    0   0   0
    /// 1992    0   0
    /// 1993    0
    /// </example>
    /// 
    public class ClaimTriangle
    {
        public String ProductName { get; set; }

        public int EarliestOriginYear { get; set; }

        public int DevelopmentYears { get; set;  }

        public Dictionary<int, decimal[]> Matrix { get; set; }

        public ClaimTriangle(string productName, int minOrigin, int devYears)
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
    }

}
