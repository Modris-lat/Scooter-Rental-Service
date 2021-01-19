using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Library.Company;
using ScooterRental.Library.Exceptions;
using ScooterRental.Library.Interfaces;
using ScooterRental.Library.Models;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Xunit;

namespace Unit.Tests
{
    public class RentalCompanyTest
    {
        private readonly IRentalCompany _rentalCompany;
        private readonly IScooterService _scooterService;
        private readonly IRentedScootersList _rentedScooters;

        public RentalCompanyTest()
        {
            _scooterService = Mock.Create<IScooterService>();
            _rentedScooters = Mock.Create<IRentedScootersList>();
            _rentalCompany = new RentalCompany("Company", _scooterService, _rentedScooters, new RentCalculator());
        }

        [Fact]
        public void CompanyNameValid()
        {
            Assert.True(_rentalCompany.Name == "Company");
        }
        [Fact]
        public void CompanyNameInvalid()
        {
            Assert.False(_rentalCompany.Name == "some");
        }
        [Fact]
        public void StartRentForNonExistingScooterThrowsException()
        {
            Scooter scooter = null;
            Mock.Arrange(() => _scooterService.GetScooterById("01"))
                .Returns(scooter)
                .MustBeCalled();
            Assert.Throws<StartRentNonExistingScooterException>(() => _rentalCompany.StartRent("01"));
            Mock.Assert(_scooterService);
        }
        [Fact]
        public void StartRentScooterIsAlreadyRentedThrowsException()
        {
            var id = "01";
            var pricePerMinute = 0.10M;
            var scooter = new Scooter(id, pricePerMinute) {IsRented = true};
            Mock.Arrange(() => _scooterService.GetScooterById(id))
                .Returns(scooter)
                .MustBeCalled();
            Assert.Throws<StartRentForRentedScooterException>(() => _rentalCompany.StartRent(id));
            Mock.Assert(_scooterService);
        }
        [Fact]
        public void StartRentSetScooterToRentedGetScooterByIdReturnsScooterIsRented()
        {
            var id = "01";
            var pricePerMinute = 0.10M;
            var scooter = new Scooter(id, pricePerMinute){IsRented = false};
            Mock.Arrange(() => _scooterService.GetScooterById(id))
                .Returns(scooter)
                .MustBeCalled();
            _rentalCompany.StartRent(id);
            Assert.True(scooter.IsRented);
        }
        [Fact]
        public void StartRentAddRentedScooterToRentedScootersList()
        {
            var id = "01";
            _rentalCompany.StartRent(id);
            Mock.Assert(() =>
                _rentedScooters.AddScooter(Arg.IsAny<Scooter>(), Arg.IsAny<DateTime>()), Occurs.Once());
        }
        [Fact]
        public void EndRentForNotRentedScooterThrowsException()
        {
            Assert.Throws<EndRentForNotRentedScooterException>(() => _rentalCompany.EndRent("01"));
        }
        [Fact]
        public void EndRentRemovesRentedScooterReturnsEmptyRentedScooterList()
        {
            _scooterService.AddScooter("02", 0.10M);
            _rentalCompany.StartRent("02");
            _rentalCompany.EndRent("02");
            IList<RentedScooter> rentedScooters = _rentedScooters.GetScooters();
            Assert.False(rentedScooters.Any());
        }
        [Fact]
        public void EndRentSetScooterIsRentedToFalseScooterServiceReturnsScooterWithPropertyIsRentedFalse()
        {
            _scooterService.AddScooter("02", 0.10M);
            _rentalCompany.StartRent("02");
            _rentalCompany.EndRent("02");
            Scooter scooter = _scooterService.GetScooterById("02");
            Assert.False(scooter.IsRented);
        }
        [Fact]
        public void EndRentReturnsIncomeMoreThan0()
        {
            _scooterService.AddScooter("02", 0.10M);
            _rentalCompany.StartRent("02");
            Assert.True(_rentalCompany.EndRent("02") > 0);
        }
        [Fact]
        public void CalculateIncomeWithNoPreviousIncomeReturns0()
        {
            Assert.True(_rentalCompany.CalculateIncome(null, false) == 0);
        }
        [Fact]
        public void CalculateIncomeIsNot0EurWithYearNullAndIncludeRentedFalseReturnsMoreThan0()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            _rentalCompany.EndRent("01");
            Assert.True(_rentalCompany.CalculateIncome(null, false) > 0);
        }
        [Fact]
        public void CalculateIncomeIsNot0EurWithYearNullAndIncludeRentedTrueReturnsMoreThan0()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            Assert.True(_rentalCompany.CalculateIncome(null, true) > 0);
        }
        [Fact]
        public void CalculateIncomeIs0EurWithYearAndIncludeRentedFalseReturns0()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            Assert.True(_rentalCompany.CalculateIncome(2020, false) == 0);
        }
        [Fact]
        public void CalculateIncomeInvalidYearThrowsException()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            Assert.Throws<CalculateIncomeInvalidYearException>(
                ()=>_rentalCompany.CalculateIncome(2030, false));
        }
        [Fact]
        public void CalculateIncomeIsNot0EurWithYearAndIncludeRentalsTrueReturnsMoreThan0()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            Assert.True(_rentalCompany.CalculateIncome(2020, true) > 0);
        }
        [Fact]
        public void CalculateIncomeIs0EurWithYearAndIncludeRentalsTrueReturns0()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            Assert.True(_rentalCompany.CalculateIncome(2015, true) == 0);
        }
        [Fact]
        public void CalculateIncomeIs0EurWithCurrentYearAndIncludeRentalsTrueReturns0()
        {
            Assert.True(_rentalCompany.CalculateIncome(2020, true) == 0);
        }
        [Fact]
        public void CalculateIncomeIs0EurWithPassedYearAndIncludeNotCompletedRentalsIsFalse()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            Assert.True(_rentalCompany.CalculateIncome(2019, false) == 0);
        }
    }
}
