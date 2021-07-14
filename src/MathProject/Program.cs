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
            
            // accepting math expression from user
            var userInput = Console.ReadLine();
            
            // Validating user's input
            if (!String.IsNullOrEmpty(userInput))
            {
                if (ValidateUserInput(userInput))
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

        /// <summary>
        /// Validates User's Input
        /// </summary>
        /// <param name="value">User's Input</param>
        /// <returns>Result Of Validation</returns>
        static bool ValidateUserInput(string value)
        {
            // allowed charachters
            var allowedChars = new [] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '*', '/'};

            // validates each charachter one by one
            // if it finds a unallowed charachter the validation result will be false
            for (int i = 0; i < value.Length; i++)
            {
                if (!allowedChars.ToList().Contains(value[i]))
                {
                    Console.WriteLine("Invalid charachter !");
                    return false;
                }
            }
            
            return true;
        }

        /// <summary>
        /// Calculates result using digits in right order and operation in right order
        /// the operations array length should always be digits array length minus 1
        /// </summary>
        /// <param name="digits">Extracted Digits (Order Matters)</param>
        /// <param name="operations">Extracted Operations (Order Matters)</param>
        /// <returns>Result Of Calculation</returns>
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

        /// <summary>
        /// Calculates an expression with single operator
        /// </summary>
        /// <param name="value1">First Value</param>
        /// <param name="value2">Second Value</param>
        /// <param name="operator">Operator (+, -, *, /)</param>
        /// <returns>Result of calculation</returns>
        static float Calculate(float value1, float value2, string @operator)
        {
            switch (@operator)
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

        /// <summary>
        /// Extracts digits from a string and ignores operators
        /// expressions with higher precedence will be calculated separately then th result will be added to list in right order 
        /// </summary>
        /// <param name="value">User's Input</param>
        /// <returns>Extraction result containing single digits and result of higher order expressions</returns>
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

        /// <summary>
        /// Extracts only digits and ignores considered operators
        /// </summary>
        /// <param name="value">Math Expression</param>
        /// <param name="consideredOperations">array of operators</param>
        /// <returns>List of extracted digits from expression</returns>
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

        /// <summary>
        /// Extract the operators in right order
        /// </summary>
        /// <param name="value">Math Expression</param>
        /// <param name="consideredOperations">Considered operators</param>
        /// <returns>List of operators in right order</returns>
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

        /// <summary>
        /// Resolves an expression containing one or multiple higher order operators
        /// </summary>
        /// <param name="value">Math Expression</param>
        /// <returns>Calculated result of a simple math expression</returns>
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
