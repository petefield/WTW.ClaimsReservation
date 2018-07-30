using System.Collections.Generic;

namespace ClaimsReservation.Core.Models
{
    /// <summary>
    /// Represents a collection of Triangles , along with
    /// Earliest Origin Year and number of development years
    /// </summary>
    public class TriangleSet
    {
        public TriangleSet()
        {

        }

        public TriangleSet(int earliestOriginYear, int numberOfDevlopementYears, IEnumerable<ClaimTriangle> triangles)
        {
            EarliestOriginYear = earliestOriginYear;
            NumberOfDevelopmentYears = numberOfDevlopementYears;
            Triangles = triangles;
        }

        public IEnumerable<ClaimTriangle> Triangles { get; }

        public int EarliestOriginYear { get; }

        public int NumberOfDevelopmentYears { get; }

    }
}

