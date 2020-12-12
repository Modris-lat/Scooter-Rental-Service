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
        public void IncomeFor0EurPricePerMinute()
        {
            var pricePerMinute = 0;
            var startRentDate = new DateTime(2020, 12, 11, 16, 00, 00);
            var endRentDate = new DateTime(2020, 12, 12, 20, 00, 00);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(income == 0);
        }
        [Fact]
        public void IncomeForMoreThanOneDay()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 11, 16, 00, 00);
            var endRentDate = new DateTime(2020, 12, 12, 20, 00, 00);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(income == 40.00M);
        }
        [Fact]
        public void IncomeForLessThanOneDay()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 09, 18, 00, 00);
            var endRentDate = new DateTime(2020, 12, 09, 20, 00, 00);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(income == 12.00M);
        }
        [Fact]
        public void IncomeForLessThan20EurThroughMidnight()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 09, 23, 15, 00);
            var endRentDate = new DateTime(2020, 12, 10, 01, 00, 00);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(Math.Ceiling(income) > 10.50M);
        }
        [Fact]
        public void IncomeForHigherPriceThroughMidnight()
        {
            var pricePerMinute = 1.10M;
            var startRentDate = new DateTime(2020, 12, 09, 21, 15, 12);
            var endRentDate = new DateTime(2020, 12, 10, 03, 45, 30);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(income == 40.00M);
        }
        [Fact]
        public void IncomeForMoreThanTwoDays()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 08, 22, 00, 00);
            var endRentDate = new DateTime(2020, 12, 15, 22, 00, 00);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(Math.Ceiling(income) == 152.00M);
        }
        [Fact]
        public void IncomeForOneMonth()
        {
            var pricePerMinute = 0.10M;
            var startRentDate = new DateTime(2020, 12, 10, 22, 00, 00);
            var endRentDate = new DateTime(2021, 01, 10, 22, 00, 00);
            decimal income = _calculator.CalculateIncome(pricePerMinute, startRentDate, endRentDate);
            Assert.True(Math.Ceiling(income) == 632.00M);
        }
    }
}
