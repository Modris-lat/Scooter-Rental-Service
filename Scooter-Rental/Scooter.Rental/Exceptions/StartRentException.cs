using System;

namespace ScooterRental.Library.Exceptions
{
    public class StartRentException: Exception
    {
        public StartRentException(string message):base(message){}
    }
}
