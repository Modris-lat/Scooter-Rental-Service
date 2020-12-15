using System;

namespace ScooterRental.Library.Exceptions
{
    public class AddNotRentedScooterException: Exception
    {
        public AddNotRentedScooterException(string message):base(message){}
    }
}
