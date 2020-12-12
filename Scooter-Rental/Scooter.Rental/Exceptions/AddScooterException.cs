using System;

namespace ScooterRental.Library.Exceptions
{
    public class AddScooterException: Exception
    {
        public AddScooterException(string message):base(message){}
    }
}
