using System;

namespace ScooterRental.Library.Interfaces
{
    public interface IRentCalculator
    {
        decimal CalculateIncome(decimal pricePerMinute, DateTime startRentDate, DateTime endRentDate);
    }
}
