using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IFizzBuzz
    {
        string CalculateResult();
        string FizzBuzzCalculator(string[] input);
        bool IsBuzz(int item);
        bool IsFizz(int item);
        string CalculatetFizzBuzz(int number);

    }
}
