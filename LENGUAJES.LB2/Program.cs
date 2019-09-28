using LENGUAJES.LB2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LENGUAJES.LB2
{
    class Program
    {
        static Minibus bus;

        static void Main(string[] args)
        {
            Console.SetWindowSize(200, 50);
            //RunMenu();
            Run();
            Console.Clear();
        }

        static void Run()
        {
            Console.Clear();
            Help();
            Console.WriteLine("\nEnter command (write \"help\" for help)");
            bus = new Minibus(460, 9, 1000, 10, 300);
            Eval();
        }

        static void Help()
        {
            Console.WriteLine("=== HELP ===");
            Console.WriteLine("drive [distance in km]");
            Console.WriteLine("refill [quantity]");
            Console.WriteLine("businfo");
            Console.WriteLine("passengers");
            Console.WriteLine("add kid");
            Console.WriteLine("add adult");
            Console.WriteLine("remove kid");
            Console.WriteLine("remove adult");
            Console.WriteLine("help");
            Console.WriteLine("exit");
        }

        static void Eval()
        {
            Console.Write(">");
            var exit = false;
            var str = Console.ReadLine();

            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("invalid command...");
                Eval();
            }

            var commandStr = str.Split(" ");
            if(!commandStr.Any())
            {
                Console.WriteLine("invalid command...");
                Eval();
            }

            switch (commandStr[0])
            {
                case "drive":
                    if (commandStr.Count() == 0)
                        Console.WriteLine("you must write \"distance\" param");
                    var distance = Int32.Parse(commandStr[1]);
                    bus.Drive(distance);
                    break;
                case "refill":
                    if (commandStr.Count() == 0)
                        Console.WriteLine("you must write \"quantity\" param");
                    var load = Convert.ToDouble(commandStr[1]);
                    bus.Refuel(load);
                    break;
                case "businfo":
                    if (bus is null)
                        Console.WriteLine("no bus initialized");
                    else
                    {
                        Console.WriteLine($"Max pasajeros: {bus.PassengersCapacity}");
                        Console.WriteLine($"Max peso: {bus.MaxWeightCapacity}kg");
                        Console.WriteLine($"Cap tanque: {bus.TankCapacity}kg");
                        Console.WriteLine($"Consumo de gasolina por km: {bus.FuelConsumption}");
                        Console.WriteLine($"Gasolina: {bus.FuelAmount}");
                    }
                    break;
                case "passengers":
                    if (!bus.Passengers.Any())
                        Console.WriteLine("No passengers added");
                    foreach (var pas in bus.Passengers)
                        Console.WriteLine($"{(pas is Adult ? "Adult" : "Kid")} ({pas.Weight}kg)");
                    Console.WriteLine($"Total ammount: {bus.Passengers.Count()} (Total Weight: {bus.Passengers.Sum(pa => pa.Weight)}kg)");
                    break;
                case "add":
                    if(commandStr.Count() == 0)
                        Console.WriteLine("you must specify \"kid\" or \"adult\"");
                    if (string.IsNullOrEmpty(commandStr[1]))
                        Console.WriteLine("you must specify \"kid\" or \"adult\"");
                    Person p = null;
                    switch(commandStr[1])
                    {
                        case "kid":
                            Console.WriteLine("Adding kid passenger...");
                            p = new Kid();
                            break;
                        case "adult":
                            p = new Adult();
                            Console.WriteLine("Adding adult passenger...");
                            break;
                    }
                    bus.AddPassenger(p);
                    Console.WriteLine("Added passenger");
                    break;
                case "remove":
                    if (commandStr.Count() == 0)
                        Console.WriteLine("you must specify \"kid\" or \"adult\"");
                    if (string.IsNullOrEmpty(commandStr[1]))
                        Console.WriteLine("you must specify \"kid\" or \"adult\"");
                    switch (commandStr[1])
                    {
                        case "kid":
                            Console.WriteLine("Removing kid passenger...");
                            var resultKid = bus.RemovePassenger(true);
                            if(resultKid)
                                Console.WriteLine("Removed kid passenger");
                            else
                                Console.WriteLine("No kid found");
                            break;
                        case "adult":
                            Console.WriteLine("Removing adult passenger...");
                            var resultAdult = bus.RemovePassenger(false);
                            if (resultAdult)
                                Console.WriteLine("Removed adult passenger");
                            else
                                Console.WriteLine("No adult found");
                            break;
                    }
                    break;
                case "exit":
                    exit = true;
                    break;
                default:
                    Console.WriteLine($"invalid \"{commandStr[0]}\" command...");
                    break;
            }

            if (!exit)
                Eval();
        }

        static void RunMenu()
        {
            Console.Clear();
            ShowMenu();
            var bus = new Minibus(460, 9, 1000, 10, 300);
            var nKids = 2;
            var nAdults = 7;
            var passengers = new List<Person>();
            for (var i = 0; i < nKids; ++i)
            {
                var k = new Kid();
                Console.WriteLine($"Se sube niño con peso de {k.Weight}kg.");
                passengers.Add(k);
            }
            for (var i = 0; i < nAdults; ++i)
            {
                var a = new Adult();
                Console.WriteLine($"Se sube adulto con peso de {a.Weight}kg.");
                passengers.Add(a);
            }
            Console.WriteLine($"El peso total es {passengers.Sum(p => p.Weight)}kg.");
            bus.Passengers = passengers;

            var key = ReadOption();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    Console.Write("\nDistancia (en km): ");
                    var distance = Convert.ToInt32(Console.ReadLine());
                    bus.Drive(distance);
                    break;
                case ConsoleKey.D2:
                    Console.Write("\nCarga: ");
                    var load = Convert.ToDouble(Console.ReadLine());
                    bus.Refuel(load);
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("Presione [ENTER] para salir...");
                    Console.ReadLine();
                    break;
                default:
                    RunMenu();
                    break;
            }

            Console.ReadLine();
        }

        static void ShowMenu()
        {
            Console.WriteLine("=== MENU ===");
            Console.WriteLine("[1] Manejar");
            Console.WriteLine("[2] Recargar");
            Console.WriteLine("[3] Salir");
        }

        static ConsoleKeyInfo ReadOption()
        {
            var key = Console.ReadKey();
            Console.WriteLine("\nSu opción es [" + key.KeyChar + "]...");
            return key;
        }
    }
}
