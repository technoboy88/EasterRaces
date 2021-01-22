using System.Collections.Generic;
using System.Linq;

using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : IRepository<ICar>
    {
        private readonly ICollection<ICar> cars;

        public CarRepository()
        {
            this.cars = new List<ICar>();
        }

        public void Add(ICar model)
        {
            this.cars.Add(model);
        }

        public bool Remove(ICar model)
        {
            return this.cars.Remove(model);
        }

        public ICar GetByName(string name)
        {
            ICar car = this.cars.FirstOrDefault(c => c.Model == name);

            return car;
        }

        public IReadOnlyCollection<ICar> GetAll()
        {
            return this.cars.ToArray();
        }
    }
}

