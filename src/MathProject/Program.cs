using System;
using System.Collections.Generic;
using System.Linq;

namespace MathProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your input:");
            var userInput = Console.ReadLine();
            if (!String.IsNullOrEmpty(userInput))
            {
                if (CheckUserInput(userInput))
                {
                    // Seperating digits from string in right order
                    var queue = ExtractQueue(userInput);
                    // Seperating operators in right order
                    var operationList = ExtractOpertations(userInput, new [] {"+", "-"});
                    
                    // Calculate Result Using Queue And Operations
                    var finalResult = CalculateExpressionResult(queue.ToArray(), operationList.ToArray());
                    
                    // Print result as output
                    Console.WriteLine(finalResult);
                }
            }
            
            Console.ReadLine();
        }

        static bool CheckUserInput(string value)
        {
            var allowedChars = new [] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '*', '/'};

            for (int i = 0; i < allowedChars.Length; i++)
            {
                if (!allowedChars.ToList().Contains(value[i]))
                {
                    Console.WriteLine("Invalid charachter !");
                    return false;
                }
            }
            
            return true;
        }

        static float CalculateExpressionResult(float[] digits, string[] operations)
        {
            float result = 0;

            for (int i = 0; i < operations.Length; i++)
            {
                if (i == 0)
                {
                    result = Calculate(digits[i], digits[i + 1], operations[i]);
                }
                else
                {
                    result = Calculate(result, digits[i + 1], operations[i]);
                }
            }
            
            return result;
        }

        static float Calculate(float value1, float value2, string operation)
        {
            switch (operation)
            {
                case "+":
                    return value1 + value2;
                case "-":
                    return value1 - value2;
                case "*":
                    return value1 * value2;
                case "/":
                    return value1 / value2;
                default:
                    return 0;
            }
        }

        static List<float> ExtractQueue(string value)
        {
            var queue = new List<float>();
            
            var splitPlusResult = value.Split('+');

            foreach (var splitedFromPlus in splitPlusResult)
            {
                if (splitedFromPlus.Length > 1)
                {
                    //expression
                    if (splitedFromPlus.Contains("-"))
                    {
                        var splitMinusResult = splitedFromPlus.Split('-');

                        foreach (var splitedFromMinus in splitMinusResult)
                        {
                            if (splitedFromMinus.Length > 1)
                            {
                                // multiple or division resolver
                                queue.Add(MultipleOrDivisionResolver(splitedFromMinus));
                            }
                            else
                            {
                                // digit
                                var digit = Convert.ToInt32(splitedFromMinus);
                                queue.Add(digit);
                            }
                        }
                    }
                    else
                    {
                        // multiple or division resolver
                        queue.Add(MultipleOrDivisionResolver(splitedFromPlus));
                    }
                }
                else
                {
                    // digit
                    var digit = Convert.ToInt32(splitedFromPlus);
                    queue.Add(digit);
                }
            }
            
            return queue;
        }

        static List<float> ExtractDigits(string value, string[] consideredOperations)
        {
            var queue = new List<float>();
            
            for (var i = 0; i < value.Length; i++)
            {
                if (!consideredOperations.ToList().Contains(value[i].ToString()))
                {
                    queue.Add(Convert.ToInt32(value[i].ToString()));
                }
            }
            
            return queue;
        }

        static List<string> ExtractOpertations(string value, string[] consideredOperations)
        {
            var operations = new List<string>();

            for (var i = 0; i < value.Length; i++)
            {
                if (consideredOperations.ToList().Contains(value[i].ToString()))
                {
                    operations.Add(value[i].ToString());
                }
            }
            
            return operations;
        }

        static float MultipleOrDivisionResolver(string value)
        {
            // Seperating digits from string in right order
            var queue = ExtractDigits(value, new [] {"*", "/"});
            // Seperating operators in right order
            var operationList = ExtractOpertations(value, new [] {"*", "/"});
                
            // Calculate Result Using Queue And Operations
            return CalculateExpressionResult(queue.ToArray(), operationList.ToArray());
        }
    }
}
