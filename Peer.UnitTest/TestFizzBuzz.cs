using Domain;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Peer.UnitTest
{
    [TestFixture]
    public class TestFizzBuzz
    {
        private IEnumerable<IFizzBuzzCalculator> _fizzBuzzCalculators;
        private FizzBuzz service;

        [SetUp]
        public void SetUp()
        {
            _fizzBuzzCalculators = new List<IFizzBuzzCalculator> { new FizzBuzzForTwoElements(), new FizzBuzzForThreeElements() };
        }
        private void InitializeTest(string peerHttpCallResultMessage)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            HttpResponseMessage result = new HttpResponseMessage();
            result.Content = new StringContent(peerHttpCallResultMessage); // setting the result each time

            handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .Returns(Task.FromResult(result))
            .Verifiable()
            ;

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost:5001/peer")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient("Peer")).Returns(httpClient);

            this.service = new FizzBuzz(_fizzBuzzCalculators, mockHttpClientFactory.Object);

        }

        [TestCase(new string[] { "22", "2" }, "44")]
        [TestCase(new string[] { "22", "0" }, "0")]
        public void TestFizzBuzzForTwoElements(string[] input, string output)
        {
            IFizzBuzzCalculator fizzBuzzCalculator = new FizzBuzzForTwoElements();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(true, fizzBuzzCalculator.CanApply(input.ToList()));
                Assert.AreEqual(output, fizzBuzzCalculator.Calcul(input.ToList()));
            });
        }

        [TestCase(new string[] { "3", "2", "30" }, "FIZZBUZZ")] // 3eme valeur divisible par la 1ere et la 2eme
        [TestCase(new string[] { "3", "9", "30" }, "FIZZ")] // Seulement divisible par la 1ere
        [TestCase(new string[] { "13", "2", "30" }, "BUZZ")] // Seulement divisible par la 2eme
        [TestCase(new string[] { "3", "2", "0" }, "FIZZBUZZ")]
        [TestCase(new string[] { "22", "10", "5" }, "")]
        public void TestFizzBuzzForThreeElements(string[] input, string output)
        {
            IFizzBuzzCalculator fizzBuzzCalculator = new FizzBuzzForThreeElements();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(true, fizzBuzzCalculator.CanApply(input.ToList()));
                Assert.AreEqual(output, fizzBuzzCalculator.Calcul(input.ToList()));
            });
        }

        [TestCase("22:2", "44")]
        [TestCase("22:0", "0")]
        [TestCase("3:2:30", "FIZZBUZZ")]
        [TestCase("13:2:30", "BUZZ")]
        [TestCase("3:7:30", "FIZZ")]
        [TestCase("22:10:33", "")]
        [TestCase("3:2:0", "FIZZBUZZ")]
        public void TestFizzBuzzCalculator(string input, string output)
        {
            InitializeTest(input); // J'ai utilisé une methode normale au lieu de setup pour pouvoir changer le resultat du mock à chaque fois
            Assert.AreEqual(output, this.service.CalculateResult());
        }
    }
}