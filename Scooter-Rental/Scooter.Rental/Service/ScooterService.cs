using System.Collections.Generic;
using System.Linq;
using ScooterRental.Library.Exceptions;
using ScooterRental.Library.Interfaces;
using ScooterRental.Library.Models;

namespace ScooterRental.Library.Service
{
    public class ScooterService: IScooterService
    {
        private readonly IList<Scooter> _scooters;

        public ScooterService()
        {
            _scooters = new List<Scooter>{};
        }
        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (pricePerMinute < 0)
            {
                throw new AddScooterException($"Cannot add scooter with negative price {pricePerMinute}!");
            }

            if (GetScooterById(id) != null)
            {
                throw new AddScooterException($"Scooter {id} already exists!");
            }
            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            var scooter = GetScooterById(id);
            if (scooter == null)
            {
                throw new RemoveScooterException($"Scooter {id} does not exist!");
            }

            if (scooter.IsRented)
            {
                throw new RemoveScooterException($"Scooter {id} is rented");
            }
            _scooters.Remove(scooter);
        }

        public IList<Scooter> GetScooters()
        {
            return _scooters;
        }

        public Scooter GetScooterById(string scooterId)
        {
            return _scooters.SingleOrDefault(sc => sc.Id == scooterId);
        }
    }
}
