using ClaimsReservation.Core;
using ClaimsReservation.Models;
using System.Linq;
using Xunit;

namespace ClaimsReservation.Tests.Unit
{
    public class TriangleBuilderTests
    {
        [Fact]
        public void Should_Calculate_Correct_Number_Of_dev_Years()
        {
            //Taking into account that some years might be missing if no claims.

            var inputData = new DataRow[] {
                new DataRow("Test Product 1", 1990, 1990, 1),
                new DataRow("Test Product 1", 1990, 1991, 2),
                new DataRow("Test Product 1", 1991, 1991, 1),
                new DataRow("Test Product 1", 1991, 1992, 2),
                new DataRow("Test Product 1", 1991, 1993, 3),
            };

            var triangleSetBuilder = new TriangleSetFactory();
            var result = triangleSetBuilder.Create(inputData);

            Assert.Equal(3,result.NumberOfDevelopmentYears);

        }

        [Fact]
        public void Should_Calculate_Correct_Ealiest_OriginYear()
        {

            var inputData = new DataRow[] {
                new DataRow("Test Product 1", 1990, 1990, 1),
                new DataRow("Test Product 1", 1990, 1991, 2),
                new DataRow("Test Product 1", 1991, 1991, 1),
                new DataRow("Test Product 1", 1991, 1992, 2),
                new DataRow("Test Product 1", 1991, 1993, 3),
            };

            var tsf = new TriangleSetFactory();

            var result = tsf.Create(inputData);

            Assert.Equal(1990, result.EarliestOriginYear);

        }

        [Fact]
        public void Should_Calculate_Cummulative_Values()
        {
            //The last value of each origin year should equal the sum of all
            //incremental claims data for that year.

            var inputData = new DataRow[] {
                new DataRow("Test Product 1", 1990, 1990, 2),
                new DataRow("Test Product 1", 1990, 1991, 3),
                new DataRow("Test Product 1", 1990, 1992, 3),
                new DataRow("Test Product 1", 1991, 1991, 1),
                new DataRow("Test Product 1", 1991, 1992, 9),
            };

            var tsf = new TriangleSetFactory();
            var result = tsf.Create(inputData);

            Assert.Equal(8, result.Triangles.First().Matrix[1990].Last());
            Assert.Equal(10, result.Triangles.First().Matrix[1991].Last());

        }

        [Fact]
        public void Should_Return_As_Many_Triangles_As_Products()
        {
          
            var inputData = new DataRow[] {
                new DataRow("Test Product 1", 1990, 1990, 2),
                new DataRow("Test Product 1", 1990, 1991, 3),
                new DataRow("Test Product 1", 1991, 1991, 1),
                new DataRow("Test Product 1", 1991, 1992, 9),
                new DataRow("Test Product 2", 1990, 1990, 2),
                new DataRow("Test Product 2", 1990, 1991, 3),
                new DataRow("Test Product 2", 1991, 1991, 1),
                new DataRow("Test Product 2", 1991, 1992, 9),
            };

            var tsf = new TriangleSetFactory();
            var result = tsf.Create(inputData);

            Assert.Equal(2, result.Triangles.Count());
        }

        [Fact]
        public void Should_Include_All_Origin_Years_For_All_Products()
        {
            //Even if no claim data for the specified year has been included

            var inputData = new DataRow[] {
                new DataRow("Test Product 1", 1990, 1990, 2),
                new DataRow("Test Product 1", 1990, 1991, 3),
                new DataRow("Test Product 1", 1991, 1991, 1),
                new DataRow("Test Product 1", 1991, 1992, 9),
                new DataRow("Test Product 2", 1991, 1991, 1),
                new DataRow("Test Product 2", 1991, 1992, 9),
            };

            var tsf = new TriangleSetFactory();
            var result = tsf.Create(inputData);

            Assert.Equal(0, result.Triangles
                .Single(triangle => triangle.ProductName == "Test Product 2")
                .Matrix[1990]
                .Sum(y => y ));
        }


    }
}
