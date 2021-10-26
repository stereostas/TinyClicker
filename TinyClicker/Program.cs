﻿using System;

namespace TinyClicker
{
    internal class Program
    {
        public static void Main()
        {
            Startup();
        }

        static void Startup()
        {
            Actions.PrintInfo();
            string input = Console.ReadLine();
            switch (input)
            {
                case "s":
                    Clicker.StartClicker();
                    break;

                case "q":
                    Environment.Exit(0);
                    break;

                case "ss":
                    Actions.SaveScreenshot();
                    Main();
                    break;

                case "cc":
                    ConfigManager.CreateNewConfigCommand();
                    break;

                default:
                    Main();
                    break;
            }
        }
    }
}
