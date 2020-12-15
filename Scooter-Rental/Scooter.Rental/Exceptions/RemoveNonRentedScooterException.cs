using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Library.Exceptions
{
    public class RemoveNonRentedScooterException:Exception
    {
        public RemoveNonRentedScooterException(string message) : base(message){}
    }
}
