using Capstone.Core;
using Capstone.FileIO;
using Capstone.Sellable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace CapstoneTests.FileIO
{
    [TestClass]
    public class VendingMachineFileIOTests
    {
        private static string testSourceFile;

        [ClassInitialize]
        static public void Initialize(TestContext testContext)
        {
            testSourceFile = $"{Environment.CurrentDirectory}/TestStock.csv";
            Assert.IsTrue(File.Exists(testSourceFile));
        }

        [TestMethod]
        public void ReadFileTest()
        {
            Dictionary<string, InventorySlot> testDict = new Dictionary<string, InventorySlot>()
            {
                {"A1", new InventorySlot("A1", new Duck(0.90M, "Yellow Duck")) },
                {"B1", new InventorySlot("B1", new Penguin(2.80M, "Emperor Penguin")) },
                {"C1", new InventorySlot("C1", new Cat(2.25M, "Black Cat")) },
                {"D1", new InventorySlot("D1", new Pony(1.95M, "Unicorn Pony")) }
            };

            Dictionary<string, InventorySlot> result = VendingMachineFileIO.ReadFile(testSourceFile);

            CollectionAssert.AreEqual(result.Keys, testDict.Keys);
            CollectionAssert.AreEqual(result.Values, testDict.Values);
        }

        [TestMethod]
        public void CreateSalesReportTest_NoSales()
        {
            VendingMachine machine = new VendingMachine(VendingMachineFileIO.ReadFile(testSourceFile));

            string result = VendingMachineFileIO.CreateSalesReport(machine);

            Assert.AreEqual("\n0", result);
        }

        [TestMethod]
        public void CreateSalesReportTest_WIthSales()
        {

            VendingMachine machine = new VendingMachine(VendingMachineFileIO.ReadFile(testSourceFile));
            machine.FeedMoney(10);
            machine.SellItem("A1");
            machine.SellItem("B1");
            machine.SellItem("C1");
            machine.SellItem("D1");

            string result = VendingMachineFileIO.CreateSalesReport(machine);

            Assert.AreEqual("Yellow Duck|1\nEmperor Penguin|1\nBlack Cat|1\nUnicorn Pony|1\n\n7.90", result);
        }
    }
}
