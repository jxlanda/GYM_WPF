using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        bool Login(string user, string pin);
    }
}
