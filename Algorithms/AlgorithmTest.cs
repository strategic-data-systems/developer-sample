using System;
using Xunit;

namespace DeveloperSample.Algorithms
{
    public class AlgorithmTest
    {
        [Fact()]
        public void CanGetFactorial()
        {
            //Negative tests
            Assert.Throws<ArgumentException>(() => Algorithms.GetFactorial(-1));
            Assert.Equal(1, Algorithms.GetFactorial(0));
            Assert.Equal(1, Algorithms.GetFactorial(1));

            //Positive tests
            Assert.Equal(24, Algorithms.GetFactorial(4));
        }


        [Fact()]
        public void CanFormatSeparators()
        {
            //-ve testcase
            Assert.Equal(string.Empty, Algorithms.FormatSeparators(null));
            Assert.Equal(string.Empty, Algorithms.FormatSeparators(string.Empty));
            Assert.Equal(" ", Algorithms.FormatSeparators(" "));
            Assert.Equal(string.Empty, Algorithms.FormatSeparators());
            Assert.Equal("a", Algorithms.FormatSeparators("a"));
            Assert.Equal("a,  and b", Algorithms.FormatSeparators("a", null, "b"));

            //+ve test case
            Assert.Equal("a, b and c", Algorithms.FormatSeparators("a", "b", "c"));
            Assert.Equal("a1, b2, c3 and d4", Algorithms.FormatSeparators("a1", "b2", "c3", "d4"));
        }
    }
}