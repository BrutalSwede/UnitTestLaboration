using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp
{
    interface IAccount
    {
        void Deposit(double amount);
        void Withdraw(double amount);
        bool Transfer(Account target, double amount);
        double CalculateIntrest();
    }
}
