using System;

namespace BankApp
{
    public class Account : IAccount
    {

        public Account(double initialBalance, double interest)
        {
            if (initialBalance < 0d)
                throw new ArgumentOutOfRangeException("Balance cannot be negative", nameof(initialBalance));
            if (interest < 0d)
                throw new ArgumentOutOfRangeException("Interest cannot be negative", nameof(interest));
            if (double.IsNaN(initialBalance))
                throw new ArgumentException("Balance must be a valid number", nameof(initialBalance));
            if (double.IsNaN(interest))
                throw new ArgumentException("Interest must be a valid number", nameof(interest));
            if (double.IsPositiveInfinity(initialBalance))
                throw new ArgumentOutOfRangeException("Balance cannot be infinite", nameof(initialBalance));
            if (double.IsPositiveInfinity(interest))
                throw new ArgumentOutOfRangeException("Interest cannot be infinite", nameof(interest));

            Balance = initialBalance;
            Interest = interest;
        }

        public double Balance { get; private set; }

        public double Interest { get; private set; }


        public double CalculateIntrest()
        {
            var calculatedIntrest = Balance * (Interest / 100d);

            Balance += calculatedIntrest;

            return calculatedIntrest;
        }

        public void Deposit(double amount)
        {
            if (double.IsNaN(amount))
                throw new ArgumentException("Amount must be a valid number", nameof(amount));
            if (double.IsPositiveInfinity(amount))
                throw new ArgumentOutOfRangeException("Amount cannot be infinite", nameof(amount));
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Value cannot be less than zero", nameof(amount));

            Balance += amount;
        }

        public bool Transfer(Account target, double amount)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target), "Target cannon be null");

            Withdraw(amount);
            target.Deposit(amount);

            return true;
        }

        public void Withdraw(double amount)
        {
            if (double.IsNaN(amount))
                throw new ArgumentException("Amount must be a valid number", nameof(amount));
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Value cannot be less than zero", nameof(amount));
            if (amount > Balance)
                throw new ArgumentException("Value cannot be more than balance", nameof(amount));

            Balance -= amount;
        }
    }
}
