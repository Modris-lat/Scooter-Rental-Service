using System;

namespace ScooterRental.Library.Exceptions
{
    public class RemoveRentedScooterException: Exception
    {
        public RemoveRentedScooterException(): base(){}
        public RemoveRentedScooterException(string message):base(message){}
        public RemoveRentedScooterException(string message, Exception inner) : base(message, inner) { }
    }
}
