using System;
using System.Collections.Generic;
using ScooterRental.Library.Service;

namespace ScooterRental.Library.Interfaces
{
    public interface IRentedScootersList
    {
        void AddScooter(Scooter scooter, DateTime startRentDate);
        void RemoveScooter(string id);
        IList<RentedScooter> GetScooters();
        RentedScooter GetScooterById(string scooterId);
    }
}
