using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScooterRental.Library.Interfaces;

namespace ScooterRental.Library.Company
{
    public class Account: IAccount
    {
        private readonly IDictionary<int, decimal> _account;

        public Account()
        {
            _account = new Dictionary<int, decimal>{};
        }

        public void AddIncome(int year, decimal income)
        {
            if (ContainsIncomeForYear(year))
            {
                _account[year] += income;
            }
            else
            {
                _account.Add(year, income);
            }
        }
        public decimal GetIncome(int? year)
        {
            if (year != null)
            {
                if (ContainsIncomeForYear(year.Value))
                {
                    return _account[year.Value];
                }

                return 0;
            }
            return SumAllIncome();
        }

        bool ContainsIncomeForYear(int year)
        {
            bool contains = _account.ContainsKey(year);
            if (contains)
            {
                return true;
            }
            return false;
        }

        decimal SumAllIncome()
        {
            return _account.Sum(year => year.Value);
        }
    }
}
