using System;

namespace ScooterRental.Library.Exceptions
{
    public class StartRentNonExistingScooterException: Exception
    {
        public StartRentNonExistingScooterException(string message):base(message){}
    }
}
