using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;
namespace BankTests
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act
            account.Debit(debitAmount);
            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act and assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() =>
            account.Debit(debitAmount));
        }
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("The expected exception was not thrown.");
        }
        //Тест: Дебетирование до половины баланса
        [TestMethod]
        public void Debit_HalfBalance_ShouldUpdateCorrectly()
        {
            double initialBalance = 200.0;
            double debitAmount = 100.0;
            double expectedBalance = 100.0;
            BankAccount account = new BankAccount("Test User", initialBalance);

            account.Debit(debitAmount);

            Assert.AreEqual(expectedBalance, account.Balance, "Balance is not updated correctly after debit.");
        }
        //Тест: Кредит с положительным значением
        [TestMethod]
        public void Credit_WithPositiveAmount_ShouldUpdateBalance()
        {
            double initialBalance = 50.0;
            double creditAmount = 30.0;
            double expectedBalance = 80.0;
            BankAccount account = new BankAccount("Test User", initialBalance);

            account.Credit(creditAmount);

            Assert.AreEqual(expectedBalance, account.Balance, "Balance is not updated correctly after credit.");
        }
        //Тест: Попытка снять весь баланс и потом пополнить счёт
        [TestMethod]
        public void Debit_AllBalance_ThenCredit_ShouldUpdateBalanceCorrectly()
        {
            double initialBalance = 200.0;
            double debitAmount = 200.0;
            double creditAmount = 50.0;
            double expectedBalance = 50.0;
            BankAccount account = new BankAccount("Test User", initialBalance);

            account.Debit(debitAmount);
            account.Credit(creditAmount);

            Assert.AreEqual(expectedBalance, account.Balance, "Balance is not updated correctly after debit and credit.");
        }
    }
}