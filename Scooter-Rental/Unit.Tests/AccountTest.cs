using System;
using System.Collections.Generic;
using System.Text;
using ScooterRental.Library.Company;
using ScooterRental.Library.Interfaces;
using Xunit;

namespace Unit.Tests
{
    public class AccountTest
    {
        private readonly IAccount _account;

        public AccountTest()
        {
            _account = new Account();
        }

        [Fact]
        public void GetIncomeWithYearNullReturns0()
        {
            var expectedIncome = 0;
            decimal actualIncome = _account.GetIncome(null);
            Assert.Equal(expectedIncome, actualIncome);
        }
        [Fact]
        public void GetIncomeReturnsMoreThan0ForSpecificYear()
        {
            var year = 2020;
            var notRequestedYear = 2017;
            var expectedIncome = 100.20M;
            _account.AddIncome(notRequestedYear, 50.60M);
            _account.AddIncome(year, expectedIncome);
            decimal actualIncome = _account.GetIncome(year);
            Assert.Equal(expectedIncome, actualIncome);
        }
        [Fact]
        public void GetIncomeReturns0ForSpecificYearIfNoIncomeForThatYear()
        {
            var year = 2020;
            var expectedIncome = 0;
            decimal actualIncome = _account.GetIncome(year);
            Assert.Equal(expectedIncome, actualIncome);
        }
        [Fact]
        public void GetIncomeWithYearNullReturnsTotalIncomeForAllYears()
        {
            var expectedIncome = 300.90M;
            _account.AddIncome(2018, 100.30M);
            _account.AddIncome(2019, 100.30M);
            _account.AddIncome(2020, 100.30M);
            decimal actualIncome = _account.GetIncome(null);
            Assert.Equal(expectedIncome, actualIncome);
        }
    }
}
