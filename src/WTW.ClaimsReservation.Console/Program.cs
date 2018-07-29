using System;
using System.Diagnostics;
using System.IO;
using ClaimsReservation;
using ClaimsReservation.Clients;

namespace WTW.ClaimsReservation.ConsoleApp
{
    class Program
    {
        private static string BuildOutputPath(string inputFilePath)
        {
            return Path.Combine(Path.GetDirectoryName(inputFilePath), Path.GetFileNameWithoutExtension(inputFilePath) + "_output" + Path.GetExtension(inputFilePath));
        }

        private static Stream Open(string path)
        {
            Stream fileStream = null;

            while (fileStream == null)
            {

                try
                {
                    fileStream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
                }
                catch (Exception ex)
                {
                    ConsoleEx.DisplayMessage($"There was a problem opening the specifed file.{System.Environment.NewLine}{ex.Message}", ConsoleColor.Red);
                    switch (ConsoleEx.Prompt("[E]nter another file path, [Q]uit or any other key to quit: ", ConsoleColor.Yellow))
                    {
                        case ConsoleKey.E:
                            path = Utilities.GetFileNameFromConsole();
                            break;

                        case ConsoleKey.Q:
                            return null;

                        default:
                            break;
                    }
                }
            }

            return fileStream;

        }

        static void DisplayOutputDetails(string outputFilePath)
        {
            ConsoleEx.DisplayMessage($"Output File Created: {outputFilePath}", ConsoleColor.Green);

            var response = ConsoleEx.Prompt("[O]pen file | [S]how in folder | any other key to continue: ", ConsoleColor.Yellow);

            switch (response)
            {
                case ConsoleKey.O:
                    Process.Start("explorer.exe", outputFilePath);
                    break;

                case ConsoleKey.S:
                    Process.Start("explorer.exe", Path.GetDirectoryName(outputFilePath));
                    break;

                default:
                    break;
            }
        }

        static void Main(string[] args)
        {
            try
            {
                var inputFilePath = Utilities.GetFileNameFromArgs(args) ?? Utilities.GetFileNameFromConsole();

                if (inputFilePath == null)
                {
                    ConsoleEx.DisplayMessage("No file was specified", ConsoleColor.Red);
                }
                else
                {
                    using (Stream inputFileStream = Open(inputFilePath))
                    {

                        if (inputFileStream != null)
                        {
                            var outputFilePath = BuildOutputPath(inputFilePath);

                            using (var outputStream = File.Create(outputFilePath))
                            {
                                var triangleBuilder = new TriangleBuilder {
                                    Source = new StreamParser(inputFileStream)
                                };
                                var outputWriter = new OutputWriter();
                                outputWriter.WriteOutput(outputStream, triangleBuilder.Create());
                            }

                            DisplayOutputDetails(outputFilePath);
                            ConsoleEx.Prompt("Execution complete. Press any key to exit : ", ConsoleColor.Yellow);
                            ConsoleEx.DisplayMessage("Closing", ConsoleColor.Green);
                            
                        }
                    }
                }
   
            }
            catch (Exception ex)
            {
                ConsoleEx.DisplayMessage($"An error occured : {ex.Message}", ConsoleColor.Red);
                ConsoleEx.Prompt("Press any key to exit: ", ConsoleColor.Yellow);

            }


        }
    }
}
