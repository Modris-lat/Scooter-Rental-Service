using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Library.Exceptions
{
    public class RemoveNonExistingScooterException: Exception
    {
        public RemoveNonExistingScooterException(string message):base(message){}
    }
}
