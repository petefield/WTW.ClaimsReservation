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

        private const string samplesDir = "../../../../../documents/sampledata";

        [Fact]
        public void RunSampleData()
        {
            string inputFilePath = System.IO.Path.Combine(samplesDir, "example/input.csv");
            string outputFilePath = System.IO.Path.Combine(samplesDir, "example/output.csv");
            string expectedResultsFilePath = System.IO.Path.Combine(samplesDir, "example/expected.csv");

            using (var inputStream = File.Open(inputFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var outputStream = File.Create(outputFilePath))
                {
                    var outputWriter = new OutputWriter();

                    outputWriter.WriteOutput(outputStream, new TriangleSetFactory(new StreamDataSource(inputStream)).Create());
                }
            }

            Assert.Equal(GetHashOfFile(expectedResultsFilePath), GetHashOfFile(outputFilePath));

        }

        [Fact]
        public void RunLongData()
        {
            string inputFilePath = System.IO.Path.Combine(samplesDir, "long/input.csv");
            string outputFilePath = System.IO.Path.Combine(samplesDir, "long/output.csv");
            string expectedResultsFilePath = System.IO.Path.Combine(samplesDir, "long/expected.csv");

            using (var inputStream = File.Open(inputFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var outputStream = File.Create(outputFilePath))
                {
                    var outputWriter = new OutputWriter();

                    outputWriter.WriteOutput(outputStream, new TriangleSetFactory(new StreamDataSource(inputStream)).Create());
                }
            }

            Assert.Equal(GetHashOfFile(expectedResultsFilePath), GetHashOfFile(outputFilePath));

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
