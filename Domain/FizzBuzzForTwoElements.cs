using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class FizzBuzzForTwoElements : IFizzBuzzCalculator
    {
        public bool CanApply(List<string> values) => values.Count == 2;

        public string Calcul(IList<string> values)
        {
            return $"{int.Parse(values[0]) * int.Parse(values[1])}";
        }
    }
}
