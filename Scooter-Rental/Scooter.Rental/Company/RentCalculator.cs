using System;
using ScooterRental.Library.Interfaces;

namespace ScooterRental.Library.Company
{
    public class RentCalculator: IRentCalculator
    {
        public decimal CalculateIncome(decimal pricePerMinute, DateTime startRentDate, DateTime endRentDate)
        {
            decimal totalIncome = 0.00M;
            decimal firstDayIncome = 0.00M;
            decimal lastDayIncome = 0.00M;
            decimal timeBetweenFirstAndLastDayIncome = 0.00M;
            if (pricePerMinute == 0)
            {
                return totalIncome;
            }
            TimeSpan spanTotal = endRentDate.Subtract(startRentDate);
            TimeSpan firstDayTime = GetFirstDayTime(startRentDate);
            if (firstDayTime.TotalMinutes >= spanTotal.TotalMinutes)
            {
                firstDayIncome = GetDayIncome(spanTotal.TotalMinutes, pricePerMinute);
                totalIncome += firstDayIncome;
                return totalIncome;
            }
            TimeSpan lastDayTime = GetLastDayTime(endRentDate);
            TimeSpan timeBetweenFirstAndLastDay = GetTimeBetweenFirstAndLastDayTime(startRentDate, endRentDate);
            if (timeBetweenFirstAndLastDay.TotalMinutes < 1)
            {
                firstDayIncome = GetDayIncome(firstDayTime.TotalMinutes, pricePerMinute);
                lastDayIncome = GetDayIncome(lastDayTime.TotalMinutes, pricePerMinute);
                totalIncome += firstDayIncome + lastDayIncome;
                return totalIncome;
            }
            firstDayIncome = GetDayIncome(firstDayTime.TotalMinutes, pricePerMinute);
            lastDayIncome = GetDayIncome(lastDayTime.TotalMinutes, pricePerMinute);
            totalIncome += firstDayIncome + lastDayIncome;
            if (timeBetweenFirstAndLastDay.Days >= 1)
            {
                var avg = (decimal)timeBetweenFirstAndLastDay.TotalMinutes * pricePerMinute / timeBetweenFirstAndLastDay.Days;
                if (avg > 20)
                {
                    timeBetweenFirstAndLastDayIncome = 20.00M * timeBetweenFirstAndLastDay.Days;
                    totalIncome += timeBetweenFirstAndLastDayIncome;
                }
            }
            else
            {
                timeBetweenFirstAndLastDayIncome =
                    GetDayIncome(timeBetweenFirstAndLastDay.TotalMinutes, pricePerMinute);
                totalIncome += timeBetweenFirstAndLastDayIncome;
            }
            return totalIncome;
        }

        private TimeSpan GetFirstDayTime(DateTime startDate)
        {
            TimeSpan firstDay = new DateTime(
                    startDate.Year, startDate.Month, startDate.Day, 23, 59, 59)
                .Subtract(startDate);
            return firstDay;
        }
        private TimeSpan GetLastDayTime(DateTime endDate)
        {
            TimeSpan lastDay = endDate.Subtract(new DateTime(
                endDate.Year, endDate.Month, endDate.Day, 00, 00, 00));
            return lastDay;
        }

        private TimeSpan GetTimeBetweenFirstAndLastDayTime(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeBetweenFirstAndLastDay = new DateTime(
                    endDate.Year, endDate.Month, endDate.Day, 00, 00, 00)
                .Subtract(new DateTime(
                    startDate.Year, startDate.Month, startDate.Day, 23, 59, 59));
            return timeBetweenFirstAndLastDay;
        }
        private decimal GetDayIncome(double totalMinutes, decimal pricePerMinute)
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
