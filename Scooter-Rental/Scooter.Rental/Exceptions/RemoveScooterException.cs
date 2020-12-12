using System;

namespace ScooterRental.Library.Exceptions
{
    public class RemoveScooterException: Exception
    {
        public RemoveScooterException(string message):base(message){}
    }
}
