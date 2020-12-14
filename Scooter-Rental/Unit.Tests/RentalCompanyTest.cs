using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScooterRental.Library.Company;
using ScooterRental.Library.Exceptions;
using ScooterRental.Library.Interfaces;
using ScooterRental.Library.Service;
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
            _scooterService = new ScooterService();
            _rentedScooters = new RentedScootersList();
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
            Assert.Throws<StartRentNonExistingScooterException>(() => _rentalCompany.StartRent("01"));
        }
        [Fact]
        public void StartRentScooterIsAlreadyRentedThrowsException()
        {
            _scooterService.AddScooter("01", 0.10M);
            _scooterService.GetScooterById("01").IsRented = true;
            Assert.Throws<StartRentForRentedScooterException>(() => _rentalCompany.StartRent("01"));
        }
        [Fact]
        public void StartRentSetScooterToRentedGetScooterByIdReturnsScooterIsRented()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            Scooter scooter = _scooterService.GetScooterById("01");
            Assert.True(scooter.IsRented);
        }
        [Fact]
        public void StartRentAddScooterToRentedScootersListRentedScootersReturnsValidRentedScooterId()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            RentedScooter scooter = _rentedScooters.GetScooterById("01");
            Assert.True(scooter.Id == "01");
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
        public async Task EndRentReturnsIncomeMoreThan0()
        {
            _scooterService.AddScooter("02", 0.10M);
            _rentalCompany.StartRent("02");
            await Task.Delay(1000);
                Assert.True(_rentalCompany.EndRent("02") > 0);
        }
        [Fact]
        public void CalculateIncomeWithNoPreviousIncomeReturns0()
        {
            Assert.True(_rentalCompany.CalculateIncome(null, false) == 0);
        }
        [Fact]
        public async Task CalculateIncomeIsNot0EurWithYearNullAndIncludeRentedFalseReturnsMoreThan0()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            await Task.Delay(1000);
            _rentalCompany.EndRent("01");
            Assert.True(_rentalCompany.CalculateIncome(null, false) > 0);
        }
        [Fact]
        public async Task CalculateIncomeIsNot0EurWithYearNullAndIncludeRentedTrueReturnsMoreThan0()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            await Task.Delay(1000);
            Assert.True(_rentalCompany.CalculateIncome(null, true) > 0);
        }
        [Fact]
        public async Task CalculateIncomeIs0EurWithYearAndIncludeRentedFalseReturns0()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            await Task.Delay(1000);
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
        public async Task CalculateIncomeIsNot0EurWithYearAndIncludeRentalsTrueReturnsMoreThan0()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            await Task.Delay(1000);
            Assert.True(_rentalCompany.CalculateIncome(2020, true) > 0);
        }
        [Fact]
        public async Task CalculateIncomeIs0EurWithYearAndIncludeRentalsTrueReturns0()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            await Task.Delay(1000);
            Assert.True(_rentalCompany.CalculateIncome(2015, true) == 0);
        }
        [Fact]
        public void CalculateIncomeIs0EurWithCurrentYearAndIncludeRentalsTrueReturns0()
        {
            Assert.True(_rentalCompany.CalculateIncome(2020, true) == 0);
        }
    }
}
