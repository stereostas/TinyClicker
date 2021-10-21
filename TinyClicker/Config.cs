﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace TinyClicker
{
    internal class Config
    {
        private bool _vipPackage;
        private float _elevatorSpeed;
        private int _floorsNumber;
        private int _coins;

        public bool VipPackage { get => _vipPackage; set => _vipPackage = value; }
        public float ElevatorSpeed { get => _elevatorSpeed; set => _elevatorSpeed = value; }
        public int FloorsNumber { get => _floorsNumber; set => _floorsNumber = value; }
        public int Coins { get => _coins; set => _coins = value; }

        public Config() : this(true, 10f, 0) { }
        public Config(bool vip, float elevatorSpeed, int floorsNumber)
        {
            VipPackage = vip;
            ElevatorSpeed = elevatorSpeed;
            FloorsNumber = floorsNumber;
        }
    }

    internal class ConfigManager
    {
        static readonly string configPath = Environment.CurrentDirectory + @"\Config.txt";

        public static void CreateNewConfig()
        {
            float elevatorSpeed;
            bool vipPackage;
            int floors;
            try
            {
                Console.WriteLine("New config. Enter the value and press enter.\nDo you have the VIP package? (!) true or false");
                bool.TryParse(Console.ReadLine(), out vipPackage);

                Console.WriteLine("Provide value for elevator speed (e.g: 10 or 9.25), leave empty for the default value (10)");
                string input = Console.ReadLine();
                if (input.Length == 0)
                {
                    elevatorSpeed = 10f;
                }
                else
                {
                    elevatorSpeed = float.Parse(input);
                    if (elevatorSpeed > 10 || elevatorSpeed < 0)
                    {
                        Console.WriteLine("Error: elevator speed cannot be lower than zero or greater than 10");
                        CreateNewConfig();
                    }
                }

                Console.WriteLine("Provide the current number of floors (e.g: 15)");
                floors = Convert.ToInt32(Console.ReadLine());

                Config config = new Config(vipPackage, elevatorSpeed, floors);
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(config, options);
                File.WriteAllText(configPath, json);
                Console.WriteLine("Success! Restart the app");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Incorrect input, make sure you use correct values");
                CreateNewConfig();
            }
        }

        public static void AddNewFloor()
        {
            var config = Clicker.currentConfig;
            config.FloorsNumber += 1;
            SaveConfig(config);
        }

        public static Config GetConfig()
        {
            string json = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<Config>(json);
            return config;
        }

        static void SaveConfig(Config config)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(config, options);
            File.WriteAllText(configPath, json);
        }
    }
}
