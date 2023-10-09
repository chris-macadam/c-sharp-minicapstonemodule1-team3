using Capstone.Core;
using Capstone.Sellable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.FileIO
{
    public static class VendingMachineFileIO
    {
        public const string InventoryFilePath = "vendingmachine.csv";
        public static string splitString = "|";

        public static Dictionary<string, InventorySlot> ReadFile(string filePath)
        {
            Dictionary<string, InventorySlot> output = new Dictionary<string, InventorySlot>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] lineSegments = line.Split(splitString);
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
                Debug.WriteLine(e.Message);
                //Console.WriteLine(e.Message);
            }
            return output;
        }

        public static void GenerateNewTransactionLog()
        {
            using (StreamWriter sw = new StreamWriter("TransactionLog.txt"))
            {
                sw.Write("");
            }
        }

        public static void UpdateTransactionLog(Transaction transaction)
        {
            using (StreamWriter sw = new StreamWriter("TransactionLog.txt",true))
            {
                sw.WriteLine(transaction.ToString());
            }
        }

        public static string CreateSalesReport(VendingMachine vendingMachine)
        {
            string output = "";
            try
            {
                using (StreamWriter sw = new StreamWriter("SalesReport.txt"))
                {
                    foreach (KeyValuePair<string, int> item in vendingMachine.ItemsSold)
                    {
                        output += $"{item.Key}|{item.Value}\n";
                    }

                    output += $"\n{vendingMachine.Profit}";
                    sw.Write(output);
                }
            }
            catch (IOException e)
            {
                Debug.WriteLine(e.Message);
            }
            return output;
        }

        public static List<string> ReadAsciiFromFile(string asciiFolderPath)
        {
            List<string> asciiFileList = new List<string>();
            List<string> frames = new List<string>();
            asciiFileList.AddRange(Directory.GetFiles(asciiFolderPath, "*.txt"));

            foreach (string filePath in asciiFileList)
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    frames.Add(sr.ReadToEnd().Replace(splitString, "\n"));
                }
            }
            return frames;
        }
    }
}
