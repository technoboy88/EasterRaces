using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private CarRepository cars;
        private DriverRepository drivers;
        private RaceRepository races;

        public ChampionshipController()
        {
            this.cars = new CarRepository();
            this.drivers = new DriverRepository();
            this.races = new RaceRepository();
        }

        public string CreateDriver(string driverName)
        {
            IDriver driver = this.drivers.GetByName(driverName);

            if (driver != null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.DriversExists, driverName));
            }

            driver = new Driver(driverName);
            this.drivers.Add(driver);
            return String.Format(OutputMessages.DriverCreated, driverName);
        }
        public string CreateCar(string type, string model, int horsePower)
        {
            ICar car = this.cars.GetByName(model);

            if (car != null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.CarExists, model));
            }

            if (type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);
            }
            else if (type == "Sports")
            {
                car = new SportsCar(model, horsePower);
            }

            this.cars.Add(car);
            return String.Format(OutputMessages.CarCreated, car.GetType().Name, model);
        }

        public string AddCarToDriver(string driverName, string carModel)
        {
            IDriver driver = this.drivers.GetByName(driverName);
            ICar car = this.cars.GetByName(carModel);

            if (driver == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.DriverNotFound, driverName));
            }
            if (car == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.CarNotFound, carModel));
            }

            driver.AddCar(car);
            return String.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = this.races.GetByName(raceName);
            IDriver driver = this.drivers.GetByName(driverName);

            if (race == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceNotFound, raceName));
            }
            if (driver == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            race.AddDriver(driver);
            return String.Format(OutputMessages.DriverAdded, driverName, raceName);        
        }
     
        public string CreateRace(string name, int laps)
        {
            IRace race = this.races.GetByName(name);

            if (race != null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceExists, name));
            }

            race = new Race(name, laps);
            this.races.Add(race);
            return String.Format(OutputMessages.RaceCreated, name);
        }

        public string StartRace(string raceName)
        {
            IRace race = this.races.GetByName(raceName);

            if (race == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (race.Drivers.Count < 3)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceInvalid, raceName, 3));
            }

            List<IDriver> participants = race.Drivers.OrderByDescending(d => d.Car.CalculateRacePoints(race.Laps)).ToList();
            IDriver first = participants[0];
            IDriver second = participants[1];
            IDriver third = participants[2];

            this.races.Remove(race);

            StringBuilder sb = new StringBuilder();

            sb
                .AppendLine($"Driver {first.Name} wins {raceName} race.")
                .AppendLine($"Driver {second.Name} is second in {raceName} race.")
                .AppendLine($"Driver {third.Name} is third in {raceName} race.");

            return sb.ToString().TrimEnd();
        }
    }
}
