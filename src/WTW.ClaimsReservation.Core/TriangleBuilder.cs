using System;
using System.Collections.Generic;
using System.Linq;
using ClaimsReservation.Models;

namespace ClaimsReservation
{
    public class TriangleSet
    {
        public TriangleSet(int earliestOriginYear, int numberOfDevlopementYears, IEnumerable<Triangle> triangles)
        {
            EarliestOriginYear = earliestOriginYear;
            NumberOfDevelopmentYears = numberOfDevlopementYears;
            Triangles = triangles;
        }

        public IEnumerable<Triangle> Triangles { get; set; }

        public int EarliestOriginYear { get; set; }

        public int NumberOfDevelopmentYears { get; set; }

    }
    
    public class TriangleBuilder
    {
        public IDataSource Source { get; set; }

        private Triangle PopulateTriangle(Triangle triangle,  IEnumerable<DataRow> incrementalData)
        {
            if (triangle == null) throw new ArgumentNullException(nameof(triangle));
            if (incrementalData == null) throw new ArgumentNullException(nameof(incrementalData));


            for (int originYearIndex = 0; originYearIndex < triangle.DevelopmentYears; originYearIndex++)
            {
                var originYear = triangle.EarliestOriginYear + originYearIndex;

                for (int devYearIndex = 0; devYearIndex < triangle.DevelopmentYears - originYearIndex; devYearIndex++)
                {

                    var devYear = triangle.EarliestOriginYear + originYearIndex + devYearIndex;

                    var value = incrementalData
                        .Where(x => x.OriginYear == originYear && x.DevelopmentYear == devYear)
                        .Sum(x => x.IncrementalValue);

                    triangle.Matrix[originYear][devYearIndex] = value + (devYearIndex > 0 ? triangle.Matrix[originYear][devYearIndex - 1] : 0);
                }
            }

            return triangle;
        }

        private int CalculateNumberOfDevYears(IEnumerable<DataRow> incrementalData)
        {
            //Look for the largest difference between Origin Year and Dev year

            var rowsByOrigin = incrementalData.GroupBy(dataItem => dataItem.OriginYear);

            var spread = rowsByOrigin
                .Select(originYearRows => originYearRows.Max(x => x.DevelopmentYear) - originYearRows.Key);

            return spread.Max() + 1;
        }

        public TriangleSet Create() {
            if (Source == null) throw new InvalidOperationException("No Source specified");
            return CreateFromInput(Source);
        }

        public TriangleSet CreateFromInput(IDataSource parser)
            => CreateFromDataRows(parser.ParsedData);
 
        public TriangleSet CreateFromDataRows(IEnumerable<DataRow> incrementalData)
        {

            if (incrementalData == null) throw new ArgumentNullException(nameof(incrementalData));

            int minOriginYear = incrementalData.Min(row => row.OriginYear);
            int devYears = CalculateNumberOfDevYears(incrementalData);

            var triangles  = incrementalData
                .GroupBy(row => row.Product)
                .Select(productData => PopulateTriangle(new Triangle(productData.Key, minOriginYear, devYears), productData));

            return new TriangleSet(minOriginYear, devYears, triangles);

        }
    }
}
