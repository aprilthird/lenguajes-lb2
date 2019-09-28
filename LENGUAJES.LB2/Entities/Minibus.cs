using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LENGUAJES.LB2.Entities
{
    public class Minibus : Vehicle
    {
        public int MaxWeightCapacity { get; set; }

        public int PassengersCapacity { get; set; }

        public List<Person> Passengers { get; set; }

        public Minibus(int maxWeightCapacity, int passengersCapacity, double tankCapacity, double fuelConsumption, double fuelAmount)
            : base(tankCapacity, fuelConsumption, fuelAmount)
        {
            MaxWeightCapacity = maxWeightCapacity;
            PassengersCapacity = passengersCapacity;
            TankCapacity = tankCapacity;
            Passengers = new List<Person>();
        }

        public void AddPassenger(Person p)
        {
            Passengers.Add(p);
        }

        public bool RemovePassenger(bool isKid)
        {
            if (!Passengers.Any(p => isKid ? p is Kid : p is Adult))
                return false;
            Passengers.Remove(Passengers.First(p => isKid ? p is Kid : p is Adult));
            return true;
        }

        public override void Drive(int distance)
        {
            if (!Passengers.Any())
            {
                Console.WriteLine("Microbus no puede realizar viaje por falta de pasajeros.");
                return;
            }
            if (Passengers.Count > PassengersCapacity)
            {
                Console.WriteLine("Microbus tiene exceso de pasajeros.");
                return;
            }
            var totalCompsumption = distance * FuelConsumption;
            if (Passengers.Sum(p => p.Weight) > MaxWeightCapacity)
            {
                var weightOffset = Passengers.Sum(p => p.Weight) - MaxWeightCapacity;
                var perWeight = 0.5 * weightOffset;
                Console.WriteLine($"Advertencia, Microbus tiene {weightOffset} kilos en exceso, el consumo aumenta en {perWeight}% por km que es el 0.5% por kilo en exceso.");

                totalCompsumption *= (1 + perWeight / 100);
            }
            if (totalCompsumption > FuelAmount)
            {
                Console.WriteLine("Microbus necesita reabastecimiento de combustible.");
                return;
            }
            Console.WriteLine($"Microbus empieza con {FuelAmount} de gasolina.");
            FuelAmount -= totalCompsumption;
            Console.WriteLine($"Microbus consumió {totalCompsumption} de gasolina en el recorrido.");
            Console.WriteLine($"Microbus llega al destino recorriendo {distance}km. Acaba con {FuelAmount} de gasolina.");
            return;
        }

        public override void Refuel(double load)
        {
            FuelAmount = FuelAmount + load > TankCapacity ?  TankCapacity : FuelAmount + load;
            Console.WriteLine($"Microbus recargó {load} de gasolina. Su cantidad de gasolina actual es {FuelAmount}.");
        }
    }
}
