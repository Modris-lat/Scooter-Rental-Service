using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Library.Exceptions;
using ScooterRental.Library.Interfaces;
using ScooterRental.Library.Models;

namespace ScooterRental.Library.Company
{
    public class RentalCompany: IRentalCompany
    {
        private readonly IAccount _account;
        private readonly IScooterService _scooterService;
        private readonly IRentedScootersList _rentedScooters;
        private readonly IRentCalculator _calculator;
        private readonly IDictionary<int, decimal> _totalIncome;
        public RentalCompany(
            string name, IAccount account, IScooterService scooterService, 
            IRentedScootersList rentedScooters, IRentCalculator calculator)
        {
            Name = name;
            _account = account;
            _rentedScooters = rentedScooters;
            _scooterService = scooterService;
            _calculator = calculator;
            _totalIncome = new Dictionary<int, decimal>{};
        }
        public string Name { get; }
        public void StartRent(string id)
        {
            Scooter scooter = _scooterService.GetScooterById(id);
            if (scooter == null)
            {
                throw new StartRentNonExistingScooterException($"Scooter {id} does not exist.");
            }

            if (scooter.IsRented)
            {
                throw new StartRentForRentedScooterException($"Scooter {id} is already rented.");
            }
            _scooterService.GetScooterById(id).IsRented = true;
            _rentedScooters.AddScooter(scooter, DateTime.Now);
        }

        public decimal EndRent(string id)
        {
            RentedScooter rentedScooter = _rentedScooters.GetScooterById(id);
            if (rentedScooter == null)
            {
                throw new EndRentForNotRentedScooterException($"Scooter {id} is not rented.");
            }
            _rentedScooters.RemoveScooter(id);
            _scooterService.GetScooterById(id).IsRented = false;
            var endRentDate = DateTime.Now;
            decimal income = _calculator
                .CalculateIncome(rentedScooter.PricePerMinute, rentedScooter.StartRentDateTime, endRentDate);
            _account.AddIncome(endRentDate.Year, income);
            return income;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            if (includeNotCompletedRentals)
            {
                var currentDate = DateTime.Now;
                decimal income = CalculateIncomeForNotCompletedRentals(currentDate);
                _account.AddIncome(currentDate.Year, income);
            }
            return _account.GetIncome(year);
        }

        decimal CalculateIncomeForNotCompletedRentals(DateTime currentDate)
        {
            decimal totalIncome = 0;
            IList<RentedScooter> rentedScooters = _rentedScooters.GetScooters();
            foreach (var rentedScooter in rentedScooters)
            {
                totalIncome += _calculator.CalculateIncome(
                    rentedScooter.PricePerMinute,
                    rentedScooter.StartRentDateTime,
                    currentDate
                );
            }

            return totalIncome;
        }
    }
}
