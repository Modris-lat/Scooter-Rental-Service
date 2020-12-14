using System;
using ScooterRental.Library.Company;
using ScooterRental.Library.Interfaces;
using Xunit;

namespace Unit.Tests
{
    public class RentCalculatorTest
    {
        private readonly IRentCalculator _calculator;

        public RentCalculatorTest()
        {
            _calculator = new RentCalculator();
        }
        [Fact]
        public void RentCalculatorIncomeFor0PricePerMinuteReturns0()
        {
            var pricePerMinute = 0;
            var startRentDate = new DateTime(2020, 12, 11, 16, 0, 0);
            var endRentDate = new DateTime(2020, 12, 12, 20, 0, 0);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(income == 0);
        }
        [Fact]
        public void RentCalculatorReturnsIncomeForFirstDay()
        {
            var pricePerMinute = 0.15M;
            var startRentDate = new DateTime(2020, 12, 11, 16, 0, 0);
            var endRentDate = new DateTime(2020, 12, 11, 20, 0, 0);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(Math.Ceiling(income) == 20.00M);
        }
        [Fact]
        public void RentCalculatorReturnsIncomeForFirstDayLessThan20()
        {
            var pricePerMinute = 0.05M;
            var startRentDate = new DateTime(2020, 12, 11, 16, 0, 0);
            var endRentDate = new DateTime(2020, 12, 11, 20, 0, 0);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(income == 12.00M);
        }
        [Fact]
        public void RentCalculatorReturnsIncomeForFirstDayWithEndDateBeforeMidnight()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 11, 23, 0, 0);
            var endRentDate = new DateTime(2020, 12, 11, 23, 59, 59);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(Math.Ceiling(income) == 6.00M);
        }
        [Fact]
        public void RentCalculatorReturnsIncomeForFirstDayWithEndDateInTheMidnight()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 11, 23, 0, 0);
            var endRentDate = new DateTime(2020, 12, 12, 0, 0, 0);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(Math.Ceiling(income) == 6.00M);
        }
        [Fact]
        public void RentCalculatorForMoreThanOneDayReturnsMaxIncomeForTwoDays()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 11, 16, 0, 0);
            var endRentDate = new DateTime(2020, 12, 12, 20, 0, 0);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(income == 40.00M);
        }
        [Fact]
        public void RentCalculatorIncomeForLessThanOneDayReturnsLessThanMaxIncomePerDay()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 9, 18, 0, 0);
            var endRentDate = new DateTime(2020, 12, 9, 20, 0, 0);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(income == 12.00M);
        }
        [Fact]
        public void RentCalculatorThroughMidnightReturnsLessThan20()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 9, 23, 15, 0);
            var endRentDate = new DateTime(2020, 12, 10, 1, 0, 0);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(Math.Ceiling(income) == 11.00M);
        }
        [Fact]
        public void RentCalculatorIncomeForHigherPriceThroughMidnightReturnsMaxPriceForTwoDays()
        {
            var pricePerMinute = 1.10M;
            var startRentDate = new DateTime(2020, 12, 09, 21, 15, 12);
            var endRentDate = new DateTime(2020, 12, 10, 03, 45, 30);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(income == 40.00M);
        }
        [Fact]
        public void RentCalculatorIncomeForMoreThanTwoDays()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 08, 22, 00, 00);
            var endRentDate = new DateTime(2020, 12, 15, 22, 00, 00);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(Math.Ceiling(income) == 152.00M);
        }
        [Fact]
        public void RentCalculatorIncomeForOneMonth()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 10, 22, 00, 00);
            var endRentDate = new DateTime(2021, 01, 10, 22, 00, 00);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(Math.Ceiling(income) == 632.00M);
        }
    }
}
