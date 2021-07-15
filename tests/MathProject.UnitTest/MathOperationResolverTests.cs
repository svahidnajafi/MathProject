using System;
using System.Collections.Generic;
using Xunit;

namespace MathProject.UnitTest
{
    public class MathOperationResolverTests
    {
        public static IEnumerable<object[]> TestData =>
            new List<object[]>()
            {
                new object[]
                {
                    new MathOperationResolverTestModel
                    {
                        Operation = "4+5/2-1",
                        ExpectedAnswer = (float)5.5
                    }
                },
                new object[]
                {
                    new MathOperationResolverTestModel
                    {
                        Operation = "4+(3+(6-3*2))-3",
                        ExpectedAnswer = (float)4
                    }
                },
                new object[]
                {
                    new MathOperationResolverTestModel
                    {
                        Operation = "4+(2+5*2)/3-1",
                        ExpectedAnswer = (float)7
                    }
                },
                new object[]
                {
                    new MathOperationResolverTestModel
                    {
                        Operation = "5*6+2",
                        ExpectedAnswer = (float)32
                    }
                }
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void ResolveOperationsTest(MathOperationResolverTestModel input)
        {
            try
            {
                var result = MathOperationResolver.Solve(input.Operation);
                Assert.Equal(input.ExpectedAnswer, result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.True(false);
            }
        }
    }
}