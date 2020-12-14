using System.Collections.Generic;
using System.Linq;
using ScooterRental.Library.Exceptions;
using ScooterRental.Library.Interfaces;
using ScooterRental.Library.Service;
using Xunit;

namespace Unit.Tests
{
    public class ScooterServiceTest
    {
        private readonly IScooterService _scooterService;

        public ScooterServiceTest()
        {
            _scooterService = new ScooterService();
        }

        [Fact]
        public void GetScootersEmptyList()
        {
            int scootersCount = _scooterService.GetScooters().Count;
            Assert.True(scootersCount == 0);
        }
        [Fact]
        public void GetScooterByIdNonExistingScooterReturnNull()
        {
            Scooter scooter = _scooterService.GetScooterById("01");
            Assert.True(scooter == null);
        }
        [Fact]
        public void AddScooterListCountIs1WithValidScooterProperties()
        {
            _scooterService.AddScooter("01", 0.10M);
            IList<Scooter> scooters = _scooterService.GetScooters();
            Scooter scooter = _scooterService.GetScooterById("01");
            Assert.True(scooters.Count == 1 && 
                        scooter.Id == "01" &&
                        scooter.PricePerMinute == 0.10M &&
                        scooter.IsRented == false);
        }
        [Fact]
        public void AddScooterAdd4ScootersAndGetScootersReturns4Items()
        {
            _scooterService.AddScooter("01", 0.10M);
            _scooterService.AddScooter("02", 0.10M);
            _scooterService.AddScooter("03", 0.15M);
            _scooterService.AddScooter("04", 0.20M);
            IList<Scooter> scooters = _scooterService.GetScooters();
            Assert.True(scooters.Count == 4);
        }
        [Fact]
        public void GetExistingScooterById()
        {
            _scooterService.AddScooter("01", 0.10M);
            Scooter scooter = _scooterService.GetScooterById("01");
            Assert.True(scooter.Id == "01" && scooter.PricePerMinute == 0.10M && scooter.IsRented == false);
        }
        [Fact]
        public void AddScooterAddingExistingScooterThrowsException()
        {
            _scooterService.AddScooter("01", 0.10M);
            Assert.Throws<AddScooterWithExistingId>(()=> _scooterService.AddScooter("01", 0.10M));
        }
        [Fact]
        public void AddScooterWithNegativePriceThrowsException()
        {
            Assert.Throws<AddScooterWithNegativePriceException>(() => _scooterService.AddScooter("01", -0.10M));
        }
        [Fact]
        public void RemoveScooterGetScootersReturnsEmptyList()
        {
            _scooterService.AddScooter("01", 0.20M);
            _scooterService.RemoveScooter("01");
            IList<Scooter> scooters = _scooterService.GetScooters();
            Assert.False(scooters.Any());
        }
        [Fact]
        public void RemoveNonExistingScooterThrowsException()
        {
            Assert.Throws<RemoveNonExistingScooterException>(() => _scooterService.RemoveScooter("01"));
        }
        [Fact]
        public void RemoveScooterIsRentedThrowsException()
        {
            _scooterService.AddScooter("01", 0.20M);
            _scooterService.GetScooterById("01").IsRented = true;
            Assert.Throws<RemoveRentedScooterException>(() => _scooterService.RemoveScooter("01"));
        }
    }
}
