using System;

namespace ScooterRental.Library.Exceptions
{
    public class AddNotRentedScooterException: Exception
    {
        public AddNotRentedScooterException(){}
        public AddNotRentedScooterException(string message):base(message){}
        public AddNotRentedScooterException(string message, Exception inner):base(message, inner){}
    }
}
