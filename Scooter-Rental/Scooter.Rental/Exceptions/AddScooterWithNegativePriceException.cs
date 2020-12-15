using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Library.Exceptions
{
    public class AddScooterWithNegativePriceException: Exception
    {
        public AddScooterWithNegativePriceException(string message):base(message){}
    }
}
