using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Library.Exceptions;
using ScooterRental.Library.Interfaces;

namespace ScooterRental.Library.Service
{
    public class RentedScootersList: IRentedScootersList
    {
        private readonly IList<RentedScooter> _rentedScooters;

        public RentedScootersList()
        {
            _rentedScooters = new List<RentedScooter>{};
        }
        public void AddScooter(Scooter scooter, DateTime time)
        {
            if (!scooter.IsRented)
            {
                throw new AddNotRentedScooterException($"Scooter {scooter.Id} is not rented.");
            }
            _rentedScooters.Add(new RentedScooter(scooter.Id, scooter.PricePerMinute, time));
        }

        public void RemoveScooter(string id)
        {
            RentedScooter scooter = GetScooterById(id);
            if (scooter == null)
            {
                throw new RemoveNonRentedScooterException($"Scooter {id} is not rented.");
            }
            _rentedScooters.Remove(scooter);
        }

        public IList<RentedScooter> GetScooters()
        {
            return _rentedScooters.ToList();
        }

        public RentedScooter GetScooterById(string scooterId)
        {
            return _rentedScooters.SingleOrDefault(sc => sc.Id == scooterId);
        }
    }
}
