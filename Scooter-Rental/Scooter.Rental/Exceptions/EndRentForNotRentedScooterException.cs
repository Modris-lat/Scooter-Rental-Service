using System;

namespace ScooterRental.Library.Exceptions
{
    public class EndRentForNotRentedScooterException: Exception
    {
        public EndRentForNotRentedScooterException(string message):base(message){}
    }
}
