﻿using System;

namespace ScooterRental.Library.Exceptions
{
    public class CalculateIncomeInvalidYearException: Exception
    {
        public CalculateIncomeInvalidYearException(){}
        public CalculateIncomeInvalidYearException(string message):base(message){}
        public CalculateIncomeInvalidYearException(string message, Exception inner):base(message, inner){}
    }
}
