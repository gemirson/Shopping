using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Core
{
    public interface IHttpCliente
    {
        Task<string> Get();

        Task<string> GetCliente();
    }
}
