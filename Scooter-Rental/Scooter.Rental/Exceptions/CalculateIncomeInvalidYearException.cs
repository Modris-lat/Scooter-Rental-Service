using System;

namespace ScooterRental.Library.Exceptions
{
    public class CalculateIncomeInvalidYearException: Exception
    {
        public CalculateIncomeInvalidYearException(string message):base(message){}
    }
}
