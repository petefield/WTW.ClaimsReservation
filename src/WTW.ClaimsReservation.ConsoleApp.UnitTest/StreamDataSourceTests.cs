using ClaimsReservation.DataSources;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace ClaimsReservation.Tests.Unit
{
    public class StreamDataSourceTests
    {
        [Fact]
        public void Should_Throw_ArgumentNullException_When_Stream_Null()
        {

            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new StreamDataSource(null);
            });
        }

        [Fact]
        public void Should_Report_Correct_Row_and_Column_Number_For_invalid_data()
        {
            var content =
                @"Product, Origin Year, Development Year, Incremental Value
                LongHistory,1990,1990,22
                LongHistory,1990,1991,33
                LongHistory,1990,1992,beef
                LongHistory,1990,1993,170
                LongHistory,1990,1994,36";

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            var StreamParser = new DataSources.StreamDataSource(stream);

            var ex = Assert.Throws<InvalidDataException>(() =>
            {
                StreamParser.ParsedData.ToList();
            });

            Assert.Equal(3, ex.Data["row"]);
            Assert.Equal("Incremental Value", ex.Data["column"]);
        }

        [Fact]
        public void Should_Report_Row_Column_When_Product_Invalid()
        {
            var content = @"Product, Origin Year, Development Year, Incremental Value
                            LongHistory,1990,1990,22
                              ,1990,1991,33
                            LongHistory,1990,1992,44
                            LongHistory,1990,1993,170
                            LongHistory,1990,1994,36";

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            var fileStreamData = new DataSources.StreamDataSource(stream);
            var ex = Assert.Throws<InvalidDataException>(() =>
            {
                fileStreamData.ParsedData.ToList();
            });
            Assert.Equal(2, ex.Data["row"]);
            Assert.Equal("Product", ex.Data["column"]);
        }

        [Fact]
        public void Should_Report_Row_Column_When_File_Row_Has_Missing_Data()
        {
            var content = @"Product, Origin Year, Development Year, Incremental Value
                            LongHistory,1990,1990,22
                            LongHistory,1990,1991,33
                            LongHistory,1990,00
                            LongHistory,1990,1993,170
                            LongHistory,1990,1994,36";

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            var fileStreamData = new DataSources.StreamDataSource(stream);

            var ex = Assert.Throws<InvalidDataException>(() =>
            {
                fileStreamData.ParsedData.ToList();
            });

            Assert.Equal(3, ex.Data["row"]);
        }

        [Fact]
        public void Should_Create_Correct_Number_Of_DataItems_When_File_Is_Valid()
        {
            var content = @"Product, Origin Year, Development Year, Incremental Value
                            LongHistory,1990,1990,22
                            LongHistory,1990,1991,33
                            LongHistory,1990,1991,00
                            LongHistory,1990,1993,170
                            LongHistory,1990,1994,36";

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var fileDataSource = new DataSources.StreamDataSource(stream);
            var dataItems = fileDataSource.ParsedData.ToList();
            Assert.Equal(5, dataItems.Count);
        }

        [Fact]
        public void Should_Create_Correct_Total_Value_When_File_Valid()
        {
            var content = @"Product, Origin Year, Development Year, Incremental Value
                            LongHistory,1990,1990,1
                            LongHistory,1990,1991,2
                            LongHistory,1990,1991,3
                            LongHistory,1990,1993,4
                            LongHistory,1990,1994,5";

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            var fileStreamDataSource = new DataSources.StreamDataSource(stream);

            var dataItems = fileStreamDataSource.ParsedData.ToList();

            Assert.Equal(15, dataItems.Sum(item => item.IncrementalValue));
        }
    }
}
