using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Core
{
    public class PersonCliente: ClienteHttp<Client>,IHttpPerson
    {
        public PersonCliente (IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            Check.NotNull(httpClientFactory,nameof(httpClientFactory));
            ClientName = nameof(PersonCliente);
            
        }
        public Task Get()
        {
            throw new NotImplementedException();
        }
    }
}
