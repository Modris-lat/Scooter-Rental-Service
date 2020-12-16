using System;

namespace ScooterRental.Library.Exceptions
{
    public class RemoveRentedScooterException: Exception
    {
        public RemoveRentedScooterException(string message):base(message){}
    }
}
