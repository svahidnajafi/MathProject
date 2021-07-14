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
            
            // Validating user's input
            if (!String.IsNullOrEmpty(userInput))
            {
                if (ExpressionCalculator.ValidateUserInput(userInput))
                {
                    var result = ExpressionCalculator.CalculateExpression(userInput);
                    // Print result as output
                    Console.WriteLine(result);
                }
            }
            
            Console.ReadLine();
        }
    }
}
