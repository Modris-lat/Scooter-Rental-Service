using System;

namespace ScooterRental.Library.Exceptions
{
    public class AddRentedScooterException: Exception
    {
        public AddRentedScooterException(string message):base(message){}
    }
}
