using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Library.Interfaces
{
    public interface IAccount
    {
        void AddIncome(int year, decimal income);
        decimal GetIncome(int? year);
    }
}
