﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Library.Exceptions
{
    public class StartRentForRentedScooterException:Exception
    {
        public StartRentForRentedScooterException(){}
        public StartRentForRentedScooterException(string message):base(message){}
        public StartRentForRentedScooterException(string message, Exception inner):base(message, inner){}
    }
}
