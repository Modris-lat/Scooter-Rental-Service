﻿using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Library.Exceptions;
using ScooterRental.Library.Interfaces;
using ScooterRental.Library.Models;

namespace ScooterRental.Library.Company
{
    public class RentalCompany: IRentalCompany
    {
        private readonly IScooterService _scooterService;
        private readonly IRentedScootersList _rentedScooters;
        private readonly IRentCalculator _calculator;
        private readonly IDictionary<int, decimal> _totalIncome;
        public RentalCompany(
            string name, IScooterService scooterService, 
            IRentedScootersList rentedScooters, IRentCalculator calculator)
        {
            Name = name;
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
                throw new StartRentException($"Scooter {id} does not exist!");
            }

            if (scooter.IsRented)
            {
                throw new StartRentException($"Scooter {id} is already rented!");
            }
            _scooterService.GetScooterById(id).IsRented = true;
            _rentedScooters.AddScooter(scooter, DateTime.Now);
        }

        public decimal EndRent(string id)
        {
            RentedScooter rentedScooter = _rentedScooters.GetScooterById(id);
            if (rentedScooter == null)
            {
                throw new EndRentException($"Scooter {id} is not rented!");
            }
            _rentedScooters.RemoveScooter(id);
            _scooterService.GetScooterById(id).IsRented = false;
            var endRentDate = DateTime.Now;
            decimal income = _calculator
                .CalculateIncome(rentedScooter.PricePerMinute, rentedScooter.StartRentDateTime, endRentDate);
            var year = endRentDate.Year;
            if (!_totalIncome.ContainsKey(year))
            {
                _totalIncome.Add(year, income);
                return income;
            }

            _totalIncome[year] += income;
            return income;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            if (year == null)
            {
                if (includeNotCompletedRentals)
                {
                    GetIncomeFromRentedScooters();
                }
                return _totalIncome.Sum(y => y.Value);
            }
            int key = year ?? default(int);
            if (DateTime.Now.Year < key)
            {
                throw new CalculateIncomeException($"Invalid year {year}!");
            }
            if (key == DateTime.Now.Year && includeNotCompletedRentals)
            {
                GetIncomeFromRentedScooters();
                return _totalIncome[key];
            }
            decimal income =
                _totalIncome.SingleOrDefault(y => y.Key == key).Value;
            return income;
        }

        void GetIncomeFromRentedScooters()
        {
            IList<RentedScooter> rentedScooters = _rentedScooters.GetScooters();
            int count = rentedScooters.Count;
            for (int i = 0; i < count; i++)
            {
                string id = rentedScooters[i].Id;
                EndRent(id);
                StartRent(id);
            }
        }
    }
}