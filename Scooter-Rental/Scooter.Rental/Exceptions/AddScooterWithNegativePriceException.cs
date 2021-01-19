using System;

namespace ScooterRental.Library.Exceptions
{
    public class AddScooterWithNegativePriceException: Exception
    {
        public AddScooterWithNegativePriceException(): base(){}
        public AddScooterWithNegativePriceException(string message):base(message){}
        public AddScooterWithNegativePriceException(string message, Exception inner): base(message, inner){}
    }
}
