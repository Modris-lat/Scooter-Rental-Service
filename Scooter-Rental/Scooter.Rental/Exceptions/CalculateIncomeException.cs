using System;

namespace ScooterRental.Library.Exceptions
{
    public class CalculateIncomeException: Exception
    {
        public CalculateIncomeException(string message):base(message){}
    }
}
