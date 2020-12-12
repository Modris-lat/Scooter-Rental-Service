using System;

namespace ScooterRental.Library.Exceptions
{
    public class EndRentException: Exception
    {
        public EndRentException(string message):base(message){}
    }
}
