using System.Collections.Generic;
using System.Linq;

namespace Shopping.Core
{
    public class UserService:IUserService
    {
        private readonly IReadOnlyList<Client> list = new List<Client>()
        {
            new Client("João da Silva Moraes"),
            new Client("Paula da Silva Santos"),
            new Client("Jose da Silva"),
            new Client("Carla Moraes")
        };
        public Client GetOneUser(string name)
        {
            return list.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Client> GetAll()
        {
            return list;
        }
    }
}