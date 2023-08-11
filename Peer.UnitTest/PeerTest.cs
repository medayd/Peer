using NUnit.Framework;
using Domain;

namespace Peer.UnitTest
{
    public class Tests
    {

        //[TestCase(new string[] { "0", "20", "21", "33", "60", "55" }, "0 FIZZ BUZZ BUZZ FIZZBUZZ 55 ")]
        [TestCase(33, true)]
        [TestCase(0, true)]
        [TestCase(55, false)]
        public void TestBuzz(int input, bool output)
        {
            IFizzBuzz fizzBuzz = new FizzBuzz();
            Assert.AreEqual(output, fizzBuzz.IsBuzz(input));
        }

        [TestCase(20, true)]
        [TestCase(0, true)]
        [TestCase(55, false)]
        public void TestFizz(int input, bool output)
        {
            IFizzBuzz fizzBuzz = new FizzBuzz();
            Assert.AreEqual(output, fizzBuzz.IsFizz(input));
        }

        [TestCase(0, "FIZZBUZZ")]
        [TestCase(20, "FIZZ")]
        [TestCase(21, "BUZZ")]
        [TestCase(30, "FIZZBUZZ")]
        [TestCase(55, "55")]
        public void TestFizzBuzz(int input, string output)
        {
            IFizzBuzz fizzBuzz = new FizzBuzz();
            Assert.AreEqual(output, fizzBuzz.CalculatetFizzBuzz(input));
        }

    }
}