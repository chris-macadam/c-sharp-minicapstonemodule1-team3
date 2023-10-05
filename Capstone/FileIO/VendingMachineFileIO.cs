﻿using Capstone.Core;
using Capstone.Sellable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.FileIO
{
    public static class VendingMachineFileIO
    {
        public static Dictionary<string, InventorySlot> ReadFile()
        {
            Dictionary<string, InventorySlot> output = new Dictionary<string, InventorySlot>();
            try
            {
                using (StreamReader sr = new StreamReader("vendingmachine.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] lineSegments = line.Split('|');
                        ISellable sellable = null;
                        if (lineSegments[lineSegments.Length-1] == "Duck")
                        {
                            sellable = new Duck(decimal.Parse(lineSegments[2]), lineSegments[1]);
                        }
                        if (lineSegments[lineSegments.Length - 1] == "Pony")
                        {
                            sellable = new Pony(decimal.Parse(lineSegments[2]), lineSegments[1]);
                        }
                        if (lineSegments[lineSegments.Length - 1] == "Cat")
                        {
                            sellable = new Cat(decimal.Parse(lineSegments[2]), lineSegments[1]);
                        }
                        if (lineSegments[lineSegments.Length - 1] == "Penguin")
                        {
                            sellable = new Penguin(decimal.Parse(lineSegments[2]), lineSegments[1]);
                        }

                        InventorySlot currentSlot = new InventorySlot(lineSegments[0], sellable);

                        output[currentSlot.Name] = currentSlot;
                    }
                }
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch
            {

            }

            return output;
        }
        public static void UpdateTransactionLog(Transaction transaction)
        {
            using (StreamWriter sw = new StreamWriter("TransactionLog.txt",true))
            {
                sw.WriteLine(transaction.ToString());
            }
        }

        public static string CreateSalesReport(Dictionary<string,int> itemsSold)
        {
            string output = "";
            try
            {
                using (StreamWriter sw = new StreamWriter("SalesReport.txt"))
                {
                    foreach (KeyValuePair<string, int> item in itemsSold)
                    {
                        output += $"{item.Key}|{item.Value}\n";
                        sw.Write(output);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return output;
        }
    }
}