using Capstone.FileIO;
using System;

namespace Capstone.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine machine = new VendingMachine(VendingMachineFileIO.ReadFile());
            machine.Display.DisplayAnimation(@"Animations\VendingMachineFrames", 5, true);
            machine.Display.MainMenu();

        }
    }
}
