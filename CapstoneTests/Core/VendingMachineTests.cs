using Capstone.Core;
using Capstone.Sellable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests.Core
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void FeedMoney_HandlePositive()
        {
            VendingMachine vendingMachine = new VendingMachine(null);

            decimal inputAmount = 5;
            decimal balance = vendingMachine.FeedMoney(inputAmount);

            Assert.AreEqual(inputAmount, balance);
        }

        [TestMethod]
        public void FeedMoney_HandleNegative()
        {
            VendingMachine vendingMachine = new VendingMachine(null);

            decimal inputAmount = -5;
            decimal balance = vendingMachine.FeedMoney(inputAmount);
            decimal expected = 0;

            Assert.AreEqual(expected, balance);
        }

        [TestMethod]
        public void FeedMoney_AccumulateBalance()
        {
            VendingMachine vendingMachine = new VendingMachine(null);

            decimal inputAmount = 5;
            vendingMachine.FeedMoney(inputAmount);
            decimal balance = vendingMachine.FeedMoney(inputAmount);
            decimal expected = 10;

            Assert.AreEqual(expected, balance);
        }

        [TestMethod]
        public void SellItem_AvailableItem()
        {
            Dictionary<string, InventorySlot> testSlots = new Dictionary<string, InventorySlot>()
            {
                {"A1", new InventorySlot("A1", new Duck(1, "TestItem")) }
            };

            VendingMachine vendingMachine = new VendingMachine(testSlots);

            decimal inputAmount = 5;
            vendingMachine.FeedMoney(inputAmount);

            string output = vendingMachine.SellItem("A1");
            string expected = "Quack, Quack, Splash!";

            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void SellItem_AvailableItem_CaseInsensitive()
        {
            Dictionary<string, InventorySlot> testSlots = new Dictionary<string, InventorySlot>()
            {
                {"A1", new InventorySlot("A1", new Duck(1, "TestItem")) }
            };

            VendingMachine vendingMachine = new VendingMachine(testSlots);

            decimal inputAmount = 5;
            vendingMachine.FeedMoney(inputAmount);

            string output = vendingMachine.SellItem("a1");
            string expected = "Quack, Quack, Splash!";

            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void SellItem_HandleInvalidSlot()
        {
            Dictionary<string, InventorySlot> testSlots = new Dictionary<string, InventorySlot>()
            {
                {"A1", new InventorySlot("A1", new Duck(1, "TestItem")) }
            };

            VendingMachine vendingMachine = new VendingMachine(testSlots);

            decimal inputAmount = 5;
            vendingMachine.FeedMoney(inputAmount);

            string output = vendingMachine.SellItem("B1");
            string expected = "INVALID SLOT";

            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void SellItem_HandleSoldOut()
        {
            Dictionary<string, InventorySlot> testSlots = new Dictionary<string, InventorySlot>()
            {
                {"A1", new InventorySlot("A1", new Duck(1, "TestItem")) }
            };

            VendingMachine vendingMachine = new VendingMachine(testSlots);

            decimal inputAmount = 10;
            vendingMachine.FeedMoney(inputAmount);
            vendingMachine.SellItem("A1");
            vendingMachine.SellItem("A1");
            vendingMachine.SellItem("A1");
            vendingMachine.SellItem("A1");
            vendingMachine.SellItem("A1");

            //6th item should be sold out
            string output = vendingMachine.SellItem("A1");
            string expected = "SOLD OUT";

            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void GiveChange_Dollar()
        {
            VendingMachine vendingMachine = new VendingMachine(null);

            decimal inputAmount = 1;
            vendingMachine.FeedMoney(inputAmount);

            Dictionary<string, int> change =  vendingMachine.GiveChange();
            Dictionary<string, int> expected = new Dictionary<string, int>()
            {
                {"quarter", 4 },
                {"dime", 0 },
                {"nickel", 0 },
                {"penny", 0 }
            };

            CollectionAssert.AreEqual(expected, change);
        }

        [TestMethod]
        public void GiveChange_90Cents()
        {
            VendingMachine vendingMachine = new VendingMachine(null);

            decimal inputAmount = 0.90m;
            vendingMachine.FeedMoney(inputAmount);

            Dictionary<string, int> change = vendingMachine.GiveChange();
            Dictionary<string, int> expected = new Dictionary<string, int>()
            {
                {"quarter", 3 },
                {"dime", 1 },
                {"nickel", 1 },
                {"penny", 0 }
            };

            CollectionAssert.AreEqual(expected, change);
        }

        [TestMethod]
        public void GiveChange_7Cents()
        {
            VendingMachine vendingMachine = new VendingMachine(null);

            decimal inputAmount = 0.07m;
            vendingMachine.FeedMoney(inputAmount);

            Dictionary<string, int> change = vendingMachine.GiveChange();
            Dictionary<string, int> expected = new Dictionary<string, int>()
            {
                {"quarter", 0 },
                {"dime", 0 },
                {"nickel", 1 },
                {"penny", 2 }
            };

            CollectionAssert.AreEqual(expected, change);
        }
    }
}
