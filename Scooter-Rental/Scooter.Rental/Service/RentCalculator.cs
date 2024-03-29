﻿using System;
using ScooterRental.Library.Interfaces;
using ScooterRental.Library.Static;

namespace ScooterRental.Library.Company
{
    public class RentCalculator: IRentCalculator
    {
        private readonly decimal _maxDailyIncome;

        public RentCalculator()
        {
            _maxDailyIncome = StaticValues.MaxDailyIncome;
        }
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
                return decimal.Round(totalIncome, 2);
            }
            TimeSpan lastDayTime = GetLastDayTime(endRentDate);
            TimeSpan timeBetweenFirstAndLastDay = GetTimeBetweenFirstAndLastDayTime(startRentDate, endRentDate);
            if (timeBetweenFirstAndLastDay.TotalMinutes < 1)
            {
                firstDayIncome = GetDayIncome(firstDayTime.TotalMinutes, pricePerMinute);
                lastDayIncome = GetDayIncome(lastDayTime.TotalMinutes, pricePerMinute);
                totalIncome += firstDayIncome + lastDayIncome;
                return decimal.Round(totalIncome, 2);
            }
            firstDayIncome = GetDayIncome(firstDayTime.TotalMinutes, pricePerMinute);
            lastDayIncome = GetDayIncome(lastDayTime.TotalMinutes, pricePerMinute);
            totalIncome += firstDayIncome + lastDayIncome;
            if (timeBetweenFirstAndLastDay.Days >= 1)
            {
                var avg = (decimal)timeBetweenFirstAndLastDay.TotalMinutes * pricePerMinute / timeBetweenFirstAndLastDay.Days;
                if (avg > _maxDailyIncome)
                {
                    timeBetweenFirstAndLastDayIncome = _maxDailyIncome * timeBetweenFirstAndLastDay.Days;
                    totalIncome += timeBetweenFirstAndLastDayIncome;
                }
            }
            else
            {
                timeBetweenFirstAndLastDayIncome =
                    GetDayIncome(timeBetweenFirstAndLastDay.TotalMinutes, pricePerMinute);
                totalIncome += timeBetweenFirstAndLastDayIncome;
            }
            return decimal.Round(totalIncome, 2);
        }

        private TimeSpan GetFirstDayTime(DateTime startDate)
        {
            DateTime nextDay = startDate.AddDays(1);
            TimeSpan firstDay = new DateTime(
                    nextDay.Year, nextDay.Month, nextDay.Day, 00, 00, 00)
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
            if (sum > _maxDailyIncome)
            {
                sum = _maxDailyIncome;
            }
            return sum;
        }
    }
}
