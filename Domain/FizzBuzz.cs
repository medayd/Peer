using System;
using System.Net.Http;

namespace Domain
{
    public class FizzBuzz : IFizzBuzz
    {
        private const string separator = " ";
        public string FizzBuzzCalculator(string[] value)
        {
            string result = string.Empty;
            foreach (var item in value)
            {
                if (!int.TryParse(item, out int iItem))
                    result += item + " ";

                if (iItem == 0) { result += "0 "; }
                else if (iItem % 2 == 0 && iItem % 3 == 0)
                    result += "FIZZBUZZ ";
                else if (iItem % 2 == 0)
                    result += "FIZZ ";
                else if (iItem % 3 == 0)
                    result += "BUZZ ";
                else
                    result += item + " ";
            }
            return result;
        }
        public string CalculateResult()
        {
            using (var client = new HttpClient())
            {
                string input = client.GetAsync("https://localhost:5001/Peer").Result.Content.ReadAsStringAsync().Result;
                string[] value = input.Split(":");
                string result = $" \r\nrandom values : {input}  \r\n";
                foreach (var item in value)
                {
                    // couldn't parse the string to int
                    if (!int.TryParse(item, out int iItem))
                    {
                        result += item + separator;
                        continue;
                    }

                    result += CalculatetFizzBuzz(iItem) + separator;
                }

                return result;
            }
        }

        public bool IsFizz(int item)
        {
            return item % 2 == 0;
        }
        public bool IsBuzz(int item)
        {
            return item % 3 == 0;
        }
        public string CalculatetFizzBuzz(int number)
        {
            return ((IsFizz(number) ? "FIZZ" : string.Empty)
                + (IsBuzz(number) ? "BUZZ" : string.Empty)
                + ((!IsBuzz(number) && !IsFizz(number)) ? number.ToString() : string.Empty));
        }
    }
}
