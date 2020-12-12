using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Library.Exceptions;
using ScooterRental.Library.Interfaces;
using ScooterRental.Library.Models;
using ScooterRental.Library.Service;
using Xunit;

namespace Unit.Tests
{
    public class RentedScootersTest
    {
        private readonly IRentedScootersList _rentedScooters;

        public RentedScootersTest()
        {
            _rentedScooters = new RentedScootersList();
        }
        [Fact]
        public void GetScootersEmptyList()
        {
            int scootersCount = _rentedScooters.GetScooters().Count;
            Assert.True(scootersCount == 0);
        }
        [Fact]
        public void GetNonExistingScooterById()
        {
            RentedScooter scooter = _rentedScooters.GetScooterById("01");
            Assert.True(scooter == null);
        }
        [Fact]
        public void AddScooterTrue()
        {
            var time = new DateTime(2020, 12, 10, 14, 00, 00);
            _rentedScooters.AddScooter(new Scooter("01", 0.10M){IsRented = true}, time);
            IList<RentedScooter> rentedScooters = _rentedScooters.GetScooters();
            RentedScooter rentedScooter = _rentedScooters.GetScooterById("01");
            Assert.True(rentedScooters.Count == 1 &&
                        rentedScooter.Id == "01" &&
                        rentedScooter.PricePerMinute == 0.10M &&
                        rentedScooter.StartRentDateTime == time);
        }
        [Fact]
        public void AddScooterIsNotRented()
        {
            var time = new DateTime(2020, 12, 10, 14, 00, 00);
            Assert.Throws<AddRentedScooterException>(
                () => _rentedScooters.AddScooter(new Scooter("01", 0.10M), time));
        }
        [Fact]
        public void RemoveExistingScooter()
        {
            var time = new DateTime(2020, 12, 10, 14, 00, 00);
            _rentedScooters.AddScooter(new Scooter("01", 0.20M){IsRented = true}, time);
            _rentedScooters.RemoveScooter("01");
            IList<RentedScooter> scooters = _rentedScooters.GetScooters();
            Assert.False(scooters.Any());
        }
        [Fact]
        public void RemoveNonExistingScooter()
        {
            Assert.Throws<RemoveRentedScooterException>(() => _rentedScooters.RemoveScooter("01"));
        }
    }
}
