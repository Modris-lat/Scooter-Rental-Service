using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScooterRental.Library.Company;
using ScooterRental.Library.Exceptions;
using ScooterRental.Library.Interfaces;
using ScooterRental.Library.Models;
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
        public void CompanyName()
        {
            Assert.True(_rentalCompany.Name == "Company");
        }
        [Fact]
        public void StartRentInvalidId()
        {
            Assert.Throws<StartRentException>(() => _rentalCompany.StartRent("01"));
        }
        [Fact]
        public void StartRentScooterIsAlreadyRented()
        {
            _scooterService.AddScooter("01", 0.10M);
            _scooterService.GetScooterById("01").IsRented = true;
            Assert.Throws<StartRentException>(() => _rentalCompany.StartRent("01"));
        }
        [Fact]
        public void StartRentSetScooterToRented()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            Scooter scooter = _scooterService.GetScooterById("01");
            Assert.True(scooter.IsRented);
        }
        [Fact]
        public void StartRentAddScooterToRentedScootersList()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            RentedScooter scooter = _rentedScooters.GetScooterById("01");
            Assert.True(scooter != null);
        }
        [Fact]
        public void EndRentInvalidId()
        {
            Assert.Throws<EndRentException>(() => _rentalCompany.EndRent("01"));
        }
        [Fact]
        public void EndRentRemoveRentedScooter()
        {
            _scooterService.AddScooter("02", 0.10M);
            _rentalCompany.StartRent("02");
            _rentalCompany.EndRent("02");
            IList<RentedScooter> rentedScooters = _rentedScooters.GetScooters();
            Assert.False(rentedScooters.Any());
        }
        [Fact]
        public void EndRentSetScooterIsRentedToFalse()
        {
            _scooterService.AddScooter("02", 0.10M);
            _rentalCompany.StartRent("02");
            _rentalCompany.EndRent("02");
            Scooter scooter = _scooterService.GetScooterById("02");
            Assert.False(scooter.IsRented);
        }
        [Fact]
        public async Task EndRentIncome()
        {
            _scooterService.AddScooter("02", 0.10M);
            _rentalCompany.StartRent("02");
            await Task.Delay(1000);
                Assert.True(_rentalCompany.EndRent("02") > 0);
        }
        [Fact]
        public void CalculateIncomeIs0Eur()
        {
            Assert.True(_rentalCompany.CalculateIncome(null, false) == 0);
        }
        [Fact]
        public async Task CalculateIncomeIsNot0EurWithYearNullAndIncludeRentedFalse()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            await Task.Delay(1000);
            _rentalCompany.EndRent("01");
            Assert.True(_rentalCompany.CalculateIncome(null, false) > 0);
        }
        [Fact]
        public async Task CalculateIncomeIsNot0EurWithYearNullAndIncludeRentedTrue()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            await Task.Delay(1000);
            Assert.True(_rentalCompany.CalculateIncome(null, true) > 0);
        }
        [Fact]
        public async Task CalculateIncomeIs0EurWithYearAndIncludeRentedFalse()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            await Task.Delay(1000);
            Assert.True(_rentalCompany.CalculateIncome(2020, false) == 0);
        }
        [Fact]
        public void CalculateIncomeInvalidYear()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            Assert.Throws<CalculateIncomeException>(
                ()=>_rentalCompany.CalculateIncome(2030, false));
        }
        [Fact]
        public async Task CalculateIncomeIsNot0EurWithYearAndIncludeRentalsTrue()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            await Task.Delay(1000);
            Assert.True(_rentalCompany.CalculateIncome(2020, true) > 0);
        }
        [Fact]
        public async Task CalculateIncomeIs0EurWithYearAndIncludeRentalsTrue()
        {
            _scooterService.AddScooter("01", 0.10M);
            _rentalCompany.StartRent("01");
            await Task.Delay(1000);
            Assert.True(_rentalCompany.CalculateIncome(2015, true) == 0);
        }
    }
}
