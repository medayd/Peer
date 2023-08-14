using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class FizzBuzzForThreeElements : IFizzBuzzCalculator
    {
        public bool CanApply(List<string> values) => values.Count == 3;

        public string Calcul(IList<string> values)
        {
            var firstItem = int.Parse(values[0]);
            var secondItem = int.Parse(values[1]);
            var thirdItem = int.Parse(values[2]);

            return $"{(thirdItem % firstItem == 0 ? "FIZZ" : string.Empty)}{(thirdItem % secondItem == 0 ? "BUZZ" : string.Empty)}";
        }
    }
}
