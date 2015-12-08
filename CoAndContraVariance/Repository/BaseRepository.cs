using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoAndContraVariance.Repository
{
    public class BaseRepository : IRepository<Base>
    {
        public void Add(Base item)
        {
            throw new NotImplementedException();
        }

        public Base FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Base> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
