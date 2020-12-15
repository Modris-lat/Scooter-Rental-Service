using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Library.Exceptions
{
    public class AddScooterWithExistingId: Exception
    {
        public AddScooterWithExistingId(string message):base(message){}
    }
}
