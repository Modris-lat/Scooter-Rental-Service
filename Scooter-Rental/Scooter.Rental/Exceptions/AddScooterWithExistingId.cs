using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Library.Exceptions
{
    public class AddScooterWithExistingId: Exception
    {
        public AddScooterWithExistingId(): base(){}
        public AddScooterWithExistingId(string message):base(message){}
        public AddScooterWithExistingId(string message, Exception inner): base(message, inner){}
    }
}
