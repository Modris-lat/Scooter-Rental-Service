using System;
using ScooterRental.Library.Interfaces;

namespace ScooterRental.Library.Company
{
    public class RentCalculator: IRentCalculator
    {
        public decimal CalculateIncome(decimal pricePerMinute, DateTime startRentDate, DateTime endRentDate)
        {
            decimal lastDayEarnings = 0.00M;
            decimal otherEarnings = 0.00M;
            TimeSpan spanTotal = endRentDate.Subtract(startRentDate);
            double totalMinutes = spanTotal.TotalMinutes;
            TimeSpan firstDay = new DateTime(
                startRentDate.Year, startRentDate.Month, startRentDate.Day, 23, 59, 59).Subtract(
                startRentDate);

            double restMinutes = 0;
            decimal firstDayEarnings = GetDayEarnings(totalMinutes, pricePerMinute);
            if (totalMinutes > firstDay.TotalMinutes)
            {
                firstDayEarnings = (decimal)firstDay.TotalMinutes * pricePerMinute;
                if (firstDayEarnings > 20)
                {
                    firstDayEarnings = 20.00M;
                }
                TimeSpan lastDay = endRentDate.Subtract(new DateTime(
                    endRentDate.Year, endRentDate.Month, endRentDate.Day, 00, 00, 00));
                lastDayEarnings = GetDayEarnings(lastDay.TotalMinutes, pricePerMinute);
                restMinutes = totalMinutes - (firstDay.TotalMinutes + lastDay.TotalMinutes);
            }

            if (restMinutes >= 1)
            {
                TimeSpan timeBetweenFirstAndLastDay = new DateTime(
                        endRentDate.Year, endRentDate.Month, endRentDate.Day, 00, 00, 00)
                    .Subtract(new DateTime(
                        startRentDate.Year, startRentDate.Month, startRentDate.Day, 23, 59, 59));
                otherEarnings = GetDayEarnings(restMinutes, pricePerMinute);
                if (timeBetweenFirstAndLastDay.Days >= 1)
                {
                    var avg = (decimal)restMinutes * pricePerMinute / timeBetweenFirstAndLastDay.Days;
                    if (avg > 20)
                    {
                        otherEarnings = 20.00M * timeBetweenFirstAndLastDay.Days;
                    }
                }
                else
                {
                    if (otherEarnings > 20)
                    {
                        otherEarnings = 20.00M;
                    }
                }

            }
            var earnings = otherEarnings + firstDayEarnings + lastDayEarnings;
            return earnings;
        }

        private decimal GetDayEarnings(double totalMinutes, decimal pricePerMinute)
        {
            decimal sum = (decimal)totalMinutes * pricePerMinute;
            if (sum > 20)
            {
                sum = 20.00M;
            }
            return sum;
        }
    }
}
