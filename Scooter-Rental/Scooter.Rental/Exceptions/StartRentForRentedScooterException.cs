using System;

namespace ScooterRental.Library.Exceptions
{
    public class StartRentForRentedScooterException:Exception
    {
        public StartRentForRentedScooterException(): base(){}
        public StartRentForRentedScooterException(string message):base(message){}
        public StartRentForRentedScooterException(string message, Exception inner) : base(message, inner) { }
    }
}
