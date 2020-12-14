using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Library.Exceptions
{
    public class RemoveNonExistingScooterException: Exception
    {
        public RemoveNonExistingScooterException(){}
        public RemoveNonExistingScooterException(string message):base(message){}
        public RemoveNonExistingScooterException(string message, Exception inner):base(message, inner){}
    }
}
