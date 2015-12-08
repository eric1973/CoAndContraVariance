using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoAndContraVariance.Repository
{
    public interface IReadOnlyRepository<out T>
    {
        IEnumerable<T> GetAll();
        T FindById(int id);
    }
}
