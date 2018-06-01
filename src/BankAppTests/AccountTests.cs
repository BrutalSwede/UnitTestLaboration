using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BankApp;

namespace BankAppTests
{
    public class AccountTests
    {
        [Fact]
        public void Constructor_ValidArguments_Constructs()
        {
            Account account = new Account(100d, 1d);

            double expectedBalance = 100d;
            double expectedInterest = 1d;

            Assert.Equal(expectedBalance, account.Balance);
            Assert.Equal(expectedInterest, account.Interest);
        }

        [Fact]
        public void Constructor_NegativeInterest_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Account(0d, -1d));
        }

        [Fact]
        public void Constructor_NegativeBalance_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Account(-1d, 0d));
        }

        [Fact]
        public void Constructor_PositiveInfinityBalance_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Account(double.PositiveInfinity, 0d));
        }

        [Fact]
        public void Constructor_NegativeInfinityBalance_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Account(double.NegativeInfinity, 0d));
        }

        [Fact]
        public void Constructor_PositiveInfinityInterest_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Account(0d, double.PositiveInfinity));
        }

        [Fact]
        public void Constructor_NegativeInfinityInterest_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Account(0d, double.NegativeInfinity));
        }

        [Fact]
        public void Constructor_NaNBalance_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Account(double.NaN, 0d));
        }

        [Fact]
        public void Constructor_NaNInterest_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Account(0d, double.NaN));
        }

        [Fact]
        public void Deposit_ValidArguments_IncreasesBalance()
        {
            Account account = new Account(100d, 1d);

            account.Deposit(100d);

            double expected = 200d;

            Assert.Equal(expected, account.Balance);
        }

        [Fact]
        public void Deposit_NegativeValue_ThrowsArgumentOutOfRangeException()
        {
            Account account = new Account(0d, 0d);

            Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(-100d));
        }

        [Fact]
        public void Deposit_PositiveInfinityAmount_ThrowsArgumentOutOfRangeException()
        {
            Account account = new Account(0d, 0d);

            Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(double.PositiveInfinity));
        }

        [Fact]
        public void Deposit_NegativeInfinityAmount_ThrowsArgumentOutOfRangeException()
        {
            Account account = new Account(0d, 0d);

            Assert.Throws<ArgumentOutOfRangeException>(() => account.Deposit(double.NegativeInfinity));
        }

        [Fact]
        public void Deposit_NaNAmount_ThrowsArgumentException()
        {
            Account account = new Account(0d, 0d);

            Assert.Throws<ArgumentException>(() => account.Deposit(double.NaN));
        }

        [Fact]
        public void Withdraw_ValidArguments_DecreasesBalance()
        {
            Account account = new Account(100d, 0d);

            account.Withdraw(50);

            double expectedBalance = 50d;

            Assert.Equal(expectedBalance, account.Balance);
        }

        [Fact]
        public void Withdraw_NegativeValue_ThrowsArgumentOutOfRangeException()
        {
            Account account = new Account(100d, 0d);

            Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw(-100d));
        }

        [Fact]
        public void Withdraw_NaNAmount_ThrowsArgumentException()
        {
            Account account = new Account(100d, 0d);

            Assert.Throws<ArgumentException>(() => account.Withdraw(double.NaN));
        }

        [Fact]
        public void Withdraw_InsufficientBalance_ThrowsArgumentException()
        {
            Account account = new Account(0d, 0d);

            Assert.Throws<ArgumentException>(() => account.Withdraw(100d));
        }

        [Fact]
        public void Transfer_InvalidAccount_ThrowsArgumentNullException()
        {
            Account account = new Account(0d, 0d);

            Assert.Throws<ArgumentNullException>(() => account.Transfer(null, 100));
        }

        [Fact]
        public void Transfer_NegativeAmount_ThrowsArgumentOutOfRangeException()
        {
            Account source = new Account(0d, 0d);
            Account target = new Account(0d, 0d);

            Assert.Throws<ArgumentOutOfRangeException>(() => source.Transfer(target, -50d));
        }

        [Fact]
        public void Transfer_InsufficientBalance_ThrowsArgumentException()
        {
            Account source = new Account(0d, 0d);
            Account target = new Account(0d, 0d);

            Assert.Throws<ArgumentException>(() => source.Transfer(target, 100d));
        }

        [Fact]
        public void Transfer_NaNAmount_ThrowsArgumentException()
        {
            Account source = new Account(100d, 0d);
            Account target = new Account(0d, 0d);

            Assert.Throws<ArgumentException>(() => source.Transfer(target, double.NaN));
        }

        [Fact]
        public void Transfer_ValidArguments_TransfersFunds()
        {
            Account source = new Account(100d, 0d);
            Account target = new Account(0d, 0d);

            double expectedBalance = 50d;

            source.Transfer(target, 50d);

            Assert.Equal(expectedBalance, source.Balance);
            Assert.Equal(expectedBalance, target.Balance);
        }

        [Fact]
        public void Transfer_Successful_ReturnsTrue()
        {
            Account source = new Account(100d, 0d);
            Account target = new Account(0d, 0d);

            bool result = source.Transfer(target, 50d);

            Assert.True(result);
        }

        [Fact]
        public void CalculateIntrest_Successful_ReturnsIntrest()
        {
            Account account = new Account(123d, 1d);

            double expected = 123d * (1d / 100d);

            double actual = account.CalculateIntrest();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateIntrest_Successful_IncreasesBalance()
        {
            double intrest = 1d;
            double amount = 1234d;

            Account account = new Account(amount, intrest);

            account.CalculateIntrest();

            double expected = amount *  (1d + (intrest / 100d));
            double actual = account.Balance;

            Assert.Equal(expected, actual);
        }
    }
}
