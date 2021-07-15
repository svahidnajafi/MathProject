using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MathProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your input:");
            
            // accepting math expression from user
            var userInput = Console.ReadLine();
            // Solve input operation
            var result = MathOperationResolver.Solve(userInput);
            // Print result as output
            Console.WriteLine(result);
            
            Console.ReadLine();
        }
    }
}
