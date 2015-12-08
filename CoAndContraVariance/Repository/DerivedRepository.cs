using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoAndContraVariance.Repository
{
    public class DerivedRepository : IRepository<Derived>
    {
        private readonly List<Derived> items;

        public DerivedRepository()
        {
            items = new List<Derived>
            {
                new Derived { Id = 1, Name = "item1" }
            };            
        }

        public void Add(Derived item)
        {
            items.Add(item);
        }

        public Derived FindById(int id)
        {
            return items.FirstOrDefault(item => item.Id == id);
        }

        public IEnumerable<Derived> GetAll()
        {
            return items.ToList();
        }
    }
}
