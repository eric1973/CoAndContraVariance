using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoAndContraVariance.Repository
{
    public interface IRepository<T> : IReadOnlyRepository<T>, IWriteOnlyRepository<T>
    {
    }
}
