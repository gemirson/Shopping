using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Shopping.Core
{
    public interface IUserService
    {
        public Client GetOneUser(string id);
        public  IEnumerable<Client> GetAll();
    }
}