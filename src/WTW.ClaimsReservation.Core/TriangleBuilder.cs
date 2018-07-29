using System;
using System.Collections.Generic;
using System.Linq;
using ClaimsReservation.Core.Models;
using ClaimsReservation.DataSources;
using ClaimsReservation.Models;

namespace ClaimsReservation.Core
{  
    /// <summary>
    /// Turns a set of incremental data rows into 
    /// a set of cummulative claim triangles.
    /// </summary>
    public class TriangleSetFactory
    {
        public IDataSource Source { get; set; }

        public TriangleSetFactory() : this(null)
        {
        }

        public TriangleSetFactory(IDataSource source)
        {
            Source = source;
        }

        /// <summary>
        /// Populates a precreated triangle with data from a set 
        /// of incremental data.
        /// </summary>
        /// <param name="triangle">The initialised but empty triangle to populate</param>
        /// <param name="incrementalData">The data to insert into the triangle</param>
        /// <returns></returns>
        private ClaimTriangle PopulateTriangle(ClaimTriangle triangle,  IEnumerable<DataRow> incrementalData)
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

        /// <summary>
        /// Calculate the maximum number of development years from 
        /// a set of input data. 
        /// </summary>
        /// <param name="incrementalData">Input data</param>
        /// <returns>The maximum number of development years</returns>
        private int CalculateNumberOfDevYears(IEnumerable<DataRow> incrementalData)
        {
            //Look for the largest difference between Origin Year and Dev year

            var rowsByOrigin = incrementalData.GroupBy(dataItem => dataItem.OriginYear);

            var spread = rowsByOrigin
                .Select(originYearRows => originYearRows.Max(x => x.DevelopmentYear) - originYearRows.Key);

            return spread.Max() + 1;
        }

        /// <summary>
        /// Create a TriangleSet from the source already set
        /// </summary>
        /// <returns>A triangle set containing 1 cummulative triangle per product in the source</returns>
        public TriangleSet Create() {
            if (Source == null) throw new InvalidOperationException("No Source specified");
            return Create(Source);
        }

        /// <summary>
        /// Create a TriangleSet from the data returned by <paramref name="source"/>
        /// </summary>
        /// <param name="source">An IDataSource used to parse the data used to build up the triangles</param>
        /// <returns>A triangle set containing one cummulative triangle per product in the source</returns>
        public TriangleSet Create(IDataSource source)
            => Create(source.ParsedData);

        /// <summary>
        /// Create a TriangleSet from the data in <paramref name="incrementalData"/>
        /// </summary>
        /// <param name="incrementalData">The data used to build up the triangles</param>
        /// <returns>A triangle set containing one cummulative triangle per product in the incremental data</returns>
        public TriangleSet Create(IEnumerable<DataRow> incrementalData)
        {

            if (incrementalData == null) throw new ArgumentNullException(nameof(incrementalData));

            int minOriginYear = incrementalData.Min(row => row.OriginYear);
            int devYears = CalculateNumberOfDevYears(incrementalData);

            var triangles  = incrementalData
                .GroupBy(row => row.Product)
                .Select(productData => PopulateTriangle(new ClaimTriangle(productData.Key, minOriginYear, devYears), productData));

            return new TriangleSet(minOriginYear, devYears, triangles);

        }
    }
}
