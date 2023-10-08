using Capstone.FileIO;
using System;

namespace Capstone.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachineDisplay display = new VendingMachineDisplay(new VendingMachine(VendingMachineFileIO.ReadFile()));
            display.DisplayTitleScreen();
            display.MainMenu();
        }
    }
}
