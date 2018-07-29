using System;
using System.Diagnostics;
using System.IO;
using ClaimsReservation.Core;
using ClaimsReservation.DataSources;

namespace ClaimsReservation.ConsoleApp
{
    static class Program
    {

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
                // Get a file path. First from the command line args.
                // If no args are specified, ask the user for a path.
                var inputFilePath = Utilities.GetFileNameFromArgs(args) ?? Utilities.GetFileNameFromConsole();

                if (inputFilePath == null) 
                {
                    // No file path was specified on either command 
                    // line args or by user so we wont do any more 
                    // work. We'll just allow the user to exit.

                    ConsoleEx.DisplayMessage("No file was specified", ConsoleColor.Red);
                }
                else
                {
                    using (Stream inputFileStream = Open(inputFilePath))
                    {
                        // If inputFileStream is not null, user has 
                        // entered a valid file path. Let's proceed.
                        // Other wise we'll just exit.
                  
                        if (inputFileStream != null) 
                        {
                            var outputFilePath = Utilities.BuildOutputPath(inputFilePath);

                            using (var outputStream = File.Create(outputFilePath))
                            {
                                var triangleBuilder = new TriangleSetFactory(new StreamDataSource(inputFileStream));

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
