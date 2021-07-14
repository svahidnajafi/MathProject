using System;
using System.Collections.Generic;
using System.Linq;

namespace MathProject
{
    public static class ExpressionCalculator
    {
        /// <summary>
        /// Validates User's Input
        /// </summary>
        /// <param name="value">User's Input</param>
        /// <returns>Result Of Validation</returns>
        public static bool ValidateUserInput(string value)
        {
            // allowed charachters
            var allowedChars = new [] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '*', '/', '(', ')'};

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
        /// Calculates result using digits in right order and operation in right order both as arrays
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
        /// <param name="consideredOperators">Considered Operators</param>
        /// <returns>Extraction result containing single digits and result of higher order expressions</returns>
        static List<float> ExtractQueue(string value, char[] consideredOperators)
        {
            var queue = new List<float>();
            
            var splitResult = value.Split(consideredOperators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var splited in splitResult)
            {
                var parseResult = float.TryParse(splited, out float number);
                if (!parseResult)
                {
                    // its a expression
                    queue.Add(ResolveExpression(splited));
                }
                else
                {
                    // its a digit
                    queue.Add(number);
                }
            }
            
            return queue;
        }

        /// <summary>
        /// Extract the operators in right order
        /// </summary>
        /// <param name="value">Math Expression</param>
        /// <param name="consideredOperators">Considered operators</param>
        /// <returns>List of operators in right order</returns>
        static List<string> ExtractOpertations(string value, char[] consideredOperators)
        {
            var operations = new List<string>();

            for (var i = 0; i < value.Length; i++)
            {
                if (consideredOperators.ToList().Contains(value[i]))
                {
                    operations.Add(value[i].ToString());
                }
            }
            
            return operations;
        }

        /// <summary>
        /// Resolves an expression containing one or multiple operators by considering the order of operators
        /// </summary>
        /// <param name="value">Math Expression</param>
        /// <returns>Calculated result of a simple math expression</returns>
        static float ResolveExpression(string value)
        {
            var operators = new[] {'*', '/'};
            if (value.Contains('+') || value.Contains('-'))
            {
                operators = new[] {'+', '-'};
            }
            // Seperating digits from string in right order
            var queue = ExtractQueue(value, operators);
            // Seperating operators in right order
            var operationList = ExtractOpertations(value, operators);
                
            // Calculate Result Using Queue And Operations
            return CalculateExpressionResult(queue.ToArray(), operationList.ToArray());
        }

        /// <summary>
        /// Calculates expression inside a paranthesis
        /// </summary>
        /// <param name="value">Math Expression</param>
        /// <returns>Expression Result</returns>
        static float CalculateParanthesis(string value)
        {
            var startIndex = value.LastIndexOf('(');
            var endIndex = value.IndexOf(')');
            var expression = value.Substring(startIndex + 1, endIndex - startIndex - 1);
            var expressionResult = ResolveExpression(expression);
            var finalResult = value.Replace("(" + expression + ")", expressionResult.ToString());

            return CalculateExpression(finalResult);
        }

        /// <summary>
        /// Checks the expression if it has paranthesis in it will resolve them first
        /// then resolves the final expression and returns the answer
        /// </summary>
        /// <param name="value">User's Input</param>
        /// <returns>Final result</returns>
        public static float CalculateExpression(string value)
        {
            if (value.Contains("(") && value.Contains(")"))
            {
                return CalculateParanthesis(value);
            }
            else
            {
                return ResolveExpression(value);
            }
        }
    }
}