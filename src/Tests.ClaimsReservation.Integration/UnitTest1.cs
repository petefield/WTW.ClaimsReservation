using ClaimsReservation.ConsoleApp;
using ClaimsReservation.Core;
using ClaimsReservation.DataSources;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace ClaimsReservation.Tests.Integration
{
    public class ParseSampleFiles
    {
        [Fact]
        public void RunSampleData()
        {
            using (var inputStream = File.Open("../../../../../documents/sampledata/example/input.csv", FileMode.Open, FileAccess.Read))
            {
                using (var outputStream = File.Create("../../../../../documents/sampledata/example/output.csv"))
                {
                    var outputWriter = new OutputWriter();

                    outputWriter.WriteOutput(outputStream,new TriangleSetFactory(new StreamDataSource(inputStream)).Create());
                }
            }

            Assert.Equal(GetHashOfFile("../../../../../documents/sampledata/example/expected.csv"), GetHashOfFile("../../../../../documents/sampledata/example/output.csv"));

        }


        private string GetHashOfFile(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }
    }
}
