using Newtonsoft.Json;
using Shopping.Core.Constants;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Core
{
    public class CustomerClient : ClienteHttp<User>, IHttpCliente
    {

        public CustomerClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            Check.NotNull(httpClientFactory, nameof(httpClientFactory));
            ClientName = nameof(IHttpCliente);

        }
        public async  Task<string> Get() 
        {
          return await Task.FromResult("Ok");
        }

        public async Task<string> GetCliente()
        {
            //var cliente = _httpClientFactory.CreateClient("cliente");
            //var request = new HttpRequestMessage();
            //request.RequestUri = new Uri("https://60b97a2380400f00177b6813.mockapi.io/api/v1/users");
            //var response = await cliente.SendAsync(request);
            //if (!response.IsSuccessStatusCode)
            //    return default;

            //var result = await response.Content.ReadAsStringAsync();

            //Console.Write(result);

            var cliente = _httpClientFactory.CreateClient(ClientName);            
            var response = await cliente.GetAsync(ClientConstant.USERS_ENDPOINT);
            if (!response.IsSuccessStatusCode)
                return default;

            var result = await response.Content.ReadAsStringAsync();
            Console.Write(result);

            return result;


        }
    }
}
