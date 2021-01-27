using System;

namespace ScooterRental.Library.Exceptions
{
    public class EndRentForNotRentedScooterException: Exception
    {
        public EndRentForNotRentedScooterException(): base(){}
        public EndRentForNotRentedScooterException(string message):base(message){}
        public EndRentForNotRentedScooterException(string message, Exception inner) : base(message, inner) { }
    }
}
