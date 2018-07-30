using System;
using System.Collections.Generic;
using System.Text;

namespace ClaimsReservation.ConsoleApp
{
    public static class ConsoleEx
    {

        public static void DisplayMessage(String message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static ConsoleKey Prompt(String message, ConsoleColor color)
        {
            Console.WriteLine();
            Console.ForegroundColor = color;
            var key = Prompt(message);
            Console.ResetColor();
            Console.WriteLine();
            return key;
        }

        public static ConsoleKey Prompt(String message)
        {
            Console.Write(message);
            var key = Console.ReadKey(true);
            Console.WriteLine();
            return key.Key;
        }



    }
}
