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
                throw new AddScooterWithNegativePriceException($"Cannot add scooter with negative price {pricePerMinute}.");
            }

            if (ScooterExists(id))
            {
                throw new AddScooterWithExistingId($"Scooter {id} already exists.");
            }
            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            if (!ScooterExists(id))
            {
                throw new RemoveNonExistingScooterException($"Scooter {id} does not exist.");
            }
            var scooter = GetScooterById(id);
            if (scooter.IsRented)
            {
                throw new RemoveRentedScooterException($"Scooter {id} is rented.");
            }
            _scooters.Remove(scooter);
        }

        public IList<Scooter> GetScooters()
        {
            return _scooters.ToList();
        }

        public Scooter GetScooterById(string scooterId)
        {
            return _scooters.SingleOrDefault(sc => sc.Id == scooterId);
        }

        bool ScooterExists(string id)
        {
            Scooter scooter = GetScooterById(id);
            if (scooter == null)
            {
                return false;
            }
            return true;
        }
    }
}
