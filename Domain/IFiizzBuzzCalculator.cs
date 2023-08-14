using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IFizzBuzzCalculator
    {
        bool CanApply(List<string> values);
        string Calcul(IList<string> Values);
    }
}
