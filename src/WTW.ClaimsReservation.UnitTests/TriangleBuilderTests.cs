using ClaimsReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ClaimsReservation.Tests.Unit
{
    public class TriangleBuilderTests
    {
        [Fact]
        public void Should_Calculate_Correct_Number_Of_dev_Years()
        {
            //Taking into account that some years might be missing if no claims.

            var inputData = new InputRow[] {
                new InputRow("Test Product 1", 1990, 1990, 1),
                new InputRow("Test Product 1", 1990, 1991, 2),
                new InputRow("Test Product 1", 1991, 1991, 1),
                new InputRow("Test Product 1", 1991, 1992, 2),
                new InputRow("Test Product 1", 1991, 1993, 3),
            };

            var triangleBuilder = new TriangleBuilder();
            var result = triangleBuilder.CreateFromInputRows(inputData);

            Assert.Equal(3,result.NumberOfDevelopmentYears);

        }

        [Fact]
        public void Should_Calculate_Correct_Ealiest_OriginYear()
        {

            var inputData = new InputRow[] {
                new InputRow("Test Product 1", 1990, 1990, 1),
                new InputRow("Test Product 1", 1990, 1991, 2),
                new InputRow("Test Product 1", 1991, 1991, 1),
                new InputRow("Test Product 1", 1991, 1992, 2),
                new InputRow("Test Product 1", 1991, 1993, 3),
            };

            var triangleBuilder = new TriangleBuilder();

            var result = triangleBuilder.CreateFromInputRows(inputData);

            Assert.Equal(1990, result.EarliestOriginYear);

        }

        [Fact]
        public void Should_Calculate_Cummulative_Values()
        {
            //The last value of each origin year should equal the sum of all
            //incremental claims data for that year.

            var inputData = new InputRow[] {
                new InputRow("Test Product 1", 1990, 1990, 2),
                new InputRow("Test Product 1", 1990, 1991, 3),
                new InputRow("Test Product 1", 1990, 1992, 3),
                new InputRow("Test Product 1", 1991, 1991, 1),
                new InputRow("Test Product 1", 1991, 1992, 9),
            };

            var triangleBuilder = new TriangleBuilder();
            var result = triangleBuilder.CreateFromInputRows( inputData);

            Assert.Equal(8, result.Triangles.First().Matrix[1990].Last());
            Assert.Equal(10, result.Triangles.First().Matrix[1991].Last());

        }

        [Fact]
        public void Should_Return_As_Many_Triangles_As_Products()
        {
          
            var inputData = new InputRow[] {
                new InputRow("Test Product 1", 1990, 1990, 2),
                new InputRow("Test Product 1", 1990, 1991, 3),
                new InputRow("Test Product 1", 1991, 1991, 1),
                new InputRow("Test Product 1", 1991, 1992, 9),
                new InputRow("Test Product 2", 1990, 1990, 2),
                new InputRow("Test Product 2", 1990, 1991, 3),
                new InputRow("Test Product 2", 1991, 1991, 1),
                new InputRow("Test Product 2", 1991, 1992, 9),
            };

            var triangleBuilder = new TriangleBuilder();
            var result = triangleBuilder.CreateFromInputRows(inputData);

            Assert.Equal(2, result.Triangles.Count());
        }

        [Fact]
        public void Should_Include_All_Origin_Years_For_All_Products()
        {
            //Even if no claim data for the specified year has been included

            var inputData = new InputRow[] {
                new InputRow("Test Product 1", 1990, 1990, 2),
                new InputRow("Test Product 1", 1990, 1991, 3),
                new InputRow("Test Product 1", 1991, 1991, 1),
                new InputRow("Test Product 1", 1991, 1992, 9),
                new InputRow("Test Product 2", 1991, 1991, 1),
                new InputRow("Test Product 2", 1991, 1992, 9),
            };

            var triangleBuilder = new TriangleBuilder();
            var result = triangleBuilder.CreateFromInputRows(inputData);

            Assert.Equal(0, result.Triangles[1].Matrix[1990].Sum(y => y ));
        }


    }
}
