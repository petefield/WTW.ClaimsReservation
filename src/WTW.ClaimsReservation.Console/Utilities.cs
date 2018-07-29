using ClaimsReservation.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace WTW.ClaimsReservation.ConsoleApp
{
    static class Utilities
    {
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

    }
}
