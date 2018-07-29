using System;
using System.IO;

namespace ClaimsReservation.ConsoleApp
{
    /// <summary>
    /// A set of utilites for reading filenames etc.
    /// </summary>
    static class Utilities
    {
        /// <summary>
        /// Get a file path from the passed in args.
        /// assumes file path is the last arg.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>The filepath</returns>
        public static string GetFileNameFromArgs(string[] args)
        {
            string filePath = null;

            if (args.Length >= 1)
            {
                filePath = args[args.Length - 1];
                ConsoleEx.DisplayMessage($"Importing file: {filePath}", ConsoleColor.Green);
            }

            return filePath;
        }

        /// <summary>
        /// Get a file path from the console
        /// Will repeat until the user enters a none-whitespace value 
        /// </summary>
        /// <returns>The filepath</returns>
        public static string GetFileNameFromConsole()
        {
            string path = null;

            while (String.IsNullOrWhiteSpace(path))
            {
                Console.Write("Enter Input File path : ");
                path = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(path))
                {
                    ConsoleEx.DisplayMessage("Invalid Path", ConsoleColor.Red);
                }
            }

            return path;
        }

        /// <summary>
        /// Inserts '_output' before the extesion of any filepath passed in
        /// </summary>
        /// <example>
        /// Passing "c:\dir\file.csv" will return "c:\dir\file_output.csv"
        /// </example>
        /// <param name="inputFilePath">the original file path</param>
        /// <returns>The new file path </returns>

        public static string BuildOutputPath(string inputFilePath)
        {
            return Path.Combine(Path.GetDirectoryName(inputFilePath), Path.GetFileNameWithoutExtension(inputFilePath) + "_output" + Path.GetExtension(inputFilePath));
        }
    }
}
