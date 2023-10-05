using Capstone.FileIO;
using System;

namespace Capstone.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine machine = new VendingMachine(VendingMachineFileIO.ReadFile());
            machine.Display.MainMenu();

        }
    }
}
