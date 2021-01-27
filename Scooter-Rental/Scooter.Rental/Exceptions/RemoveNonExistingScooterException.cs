using System;

namespace ScooterRental.Library.Exceptions
{
    public class RemoveNonExistingScooterException: Exception
    {
        public RemoveNonExistingScooterException(): base(){}
        public RemoveNonExistingScooterException(string message):base(message){}
        public RemoveNonExistingScooterException(string message, Exception inner) : base(message, inner) { }
    }
}
