using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Library.Exceptions
{
    public class StartRentForRentedScooterException:Exception
    {
        public StartRentForRentedScooterException(string message):base(message){}
    }
}
