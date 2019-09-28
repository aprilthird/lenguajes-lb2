using System;
using System.Collections.Generic;
using System.Text;

namespace LENGUAJES.LB2.Entities
{
    public class Vehicle
    {
        public double TankCapacity { get; set; }

        public double FuelConsumption { get; set; }

        public double FuelAmount { get; set; }

        public Vehicle(double tankCapacity, double fuelConsumption, double fuelAmount)
        {
            TankCapacity = tankCapacity;
            FuelConsumption = fuelConsumption;
            FuelAmount = fuelAmount;
        }

        public virtual void Drive(int distance)
        {

        }

        public virtual void Refuel(double load)
        {

        }
    }
}
