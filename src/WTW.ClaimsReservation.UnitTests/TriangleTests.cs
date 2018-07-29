using ClaimsReservation.Core.Models;
using ClaimsReservation.Models;
using System;
using System.Linq;
using Xunit;

namespace ClaimsReservation.Tests.Unit
{
    public class TriangleTests
    {
        [Fact]
        public void Triangles_Should_Have_Correct_Number_Of_Elements()
        {
            var developmentYears = 20;

            ClaimTriangle t = new ClaimTriangle("TestProduct", 1900, developmentYears);

            var numberOfElements = t.Matrix.Sum(year => year.Value.Length);

            var expectedNumberOfElements = (Math.Pow(developmentYears, 2) / 2) + developmentYears / 2;

            Assert.Equal(expectedNumberOfElements, numberOfElements);
        }

        [Fact]
        public void Triangles_Should_Have_Same_Number_Of_Origin_And_Dev_YEars()
        {
            var developmentYears = 20;

            var t = new ClaimTriangle("TestProduct", 1900, developmentYears);   
            Assert.Equal(t.Matrix.Keys.Count, t.Matrix[1900].Length);
        }


        [Fact]
        public void Triangles_Should_Have_ProductName()
        {
            var productName = "TestProduct";
            var t = new ClaimTriangle(productName, 1900, 1);
            Assert.Equal(productName, t.ProductName);
        }

    }
}
