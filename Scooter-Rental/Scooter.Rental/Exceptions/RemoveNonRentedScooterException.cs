﻿using System;

namespace ScooterRental.Library.Exceptions
{
    public class RemoveNonRentedScooterException:Exception
    {
        public RemoveNonRentedScooterException(): base(){}
        public RemoveNonRentedScooterException(string message) : base(message){}
        public RemoveNonRentedScooterException(string message, Exception inner) : base(message, inner) { }
    }
}
