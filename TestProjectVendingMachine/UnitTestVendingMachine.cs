using BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProjectVendingMachine
{
    [TestClass]
    public class UnitTestVendingMachine
    {
        [TestMethod]
        public void TestCheckIncorrectAmount()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            decimal insertedAmount = 0.02m;
            bool expectedResult = false;

            // Action
            bool actualResult = vm.checkAddedAmount(insertedAmount);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCheckCorrectAmount()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            decimal insertedAmount = 0.2m;
            bool expectedResult = true;

            // Action
            bool actualResult = vm.checkAddedAmount(insertedAmount);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestInsertIncorrectAmount()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            vm.resetInsertedMoney();
            decimal insertedAmount = 0.3m;
            bool expectedResult = false;

            // Action
            bool actualResult = vm.checkAddedAmount(insertedAmount);
            vm.addMoney(insertedAmount);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(0, vm.getInsertedMoney());
        }

        [TestMethod]
        public void TestInsertCorrectAmount()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            vm.resetInsertedMoney();
            decimal insertedAmount = 0.2m;

            // Action
            vm.addMoney(insertedAmount);

            // Assert
            Assert.AreEqual(insertedAmount, vm.getInsertedMoney());
        }

        [TestMethod]
        public void TestProductNotExist()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            int searchedProduct = 5;
            int expectedResult = -1;

            // Action
            int actualResult = vm.checkProduct(searchedProduct);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestProductExists()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            int searchedProduct = 2;
            int expectedResult = 1;

            // Action
            int actualResult = vm.checkProduct(searchedProduct);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestNotEnoughMoney()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            int searchedProduct = 2;
            bool expectedResult = false;

            // Action
            bool actualResult = vm.checkPrice(searchedProduct);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestEnoughMoney()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            int searchedProduct = 2;
            decimal money = 1;
            bool expectedResult = true;

            // Action
            vm.addMoney(money);
            bool actualResult = vm.checkPrice(searchedProduct);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestSoldout()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            int searchedProduct = 3;
            decimal money = 1;
            bool expectedResult = false;

            // Action
            vm.addMoney(money);
            bool actualResult = vm.checkQtty(searchedProduct);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestEnoughQtty()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            int searchedProduct = 2;
            decimal money = 1;
            bool expectedResult = true;

            // Action
            vm.addMoney(money);
            bool actualResult = vm.checkQtty(searchedProduct);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestNotEnoughChange()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            int searchedProduct = 2;
            decimal money = 1;
            bool expectedResult = false;

            // Action
            vm.addMoney(money);
            vm.buyProduct(searchedProduct);
            bool actualResult = vm.checkChange(searchedProduct);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestEnoughChange()
        {
            // Arrangement
            VendingMachine vm = new VendingMachine();
            int searchedProduct = 2;
            decimal money = 1;
            bool expectedResult = true;

            // Action
            vm.addMoney(money);
            bool actualResult = vm.checkChange(searchedProduct);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
