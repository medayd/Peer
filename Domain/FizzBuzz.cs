using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Domain
{
    public class FizzBuzz : IFizzBuzz
    {
        private readonly IEnumerable<IFizzBuzzCalculator> _fizzBuzzCalculators;
        private readonly IHttpClientFactory _httpClientFactory;
        public FizzBuzz(IEnumerable<IFizzBuzzCalculator> fizzBuzzCalculators, IHttpClientFactory httpClientFactory)
        {
            _fizzBuzzCalculators = fizzBuzzCalculators;
            _httpClientFactory = httpClientFactory;
        }
        // httpmessagehandler
        public string CalculateResult()
        {
            var client = _httpClientFactory.CreateClient("Peer");
            var values = client.GetAsync("/Peer").Result.Content.ReadAsStringAsync().Result.Split(":").ToList();
            var calculator = _fizzBuzzCalculators.Single(x => x.CanApply(values));
            return calculator.Calcul(values);
        }
    }
}
