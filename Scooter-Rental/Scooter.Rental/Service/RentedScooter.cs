using System;

namespace ScooterRental.Library.Service
{
    public class RentedScooter
    {
        public RentedScooter(string id, decimal pricePerMinute, DateTime dateTime)
        {
            Id = id;
            PricePerMinute = pricePerMinute;
            StartRentDateTime = dateTime;
        }
        public string Id { get; }
        public decimal PricePerMinute { get; }
        public DateTime StartRentDateTime { get; }
    }
}
