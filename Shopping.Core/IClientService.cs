using System.Collections.Generic;

namespace Shopping.Core
{
    public interface IClientService
    {
        public Client GetOne(string id);
        public IEnumerable<Client> GetAll();

    }
}