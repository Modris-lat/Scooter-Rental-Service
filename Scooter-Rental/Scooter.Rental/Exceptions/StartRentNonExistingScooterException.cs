using System;

namespace ScooterRental.Library.Exceptions
{
    public class StartRentNonExistingScooterException: Exception
    {
        public StartRentNonExistingScooterException(){}
        public StartRentNonExistingScooterException(string message):base(message){}
        public StartRentNonExistingScooterException(string message, Exception inner):base(message, inner){}
    }
}
