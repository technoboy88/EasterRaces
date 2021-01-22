using System.Collections.Generic;
using System.Linq;

using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Repositories.Contracts;

namespace EasterRaces.Repositories.Entities
{
    public class DriverRepository : IRepository<IDriver>
    {
        private readonly ICollection<IDriver> drivers;

        public DriverRepository()
        {
            this.drivers = new List<IDriver>();
        }

        public void Add(IDriver model)
        {
            this.drivers.Add(model);
        }

        public bool Remove(IDriver model)
        {
            return this.drivers.Remove(model);
        }
        public IDriver GetByName(string name)
        {
            IDriver driver = this.drivers.FirstOrDefault(d => d.Name == name);

            return driver;
        }

        public IReadOnlyCollection<IDriver> GetAll()
        {
            return this.drivers.ToArray();
        }
    }
}
