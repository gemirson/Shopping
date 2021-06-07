using Shopping.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ShoppingCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddShoppingCore(this IServiceCollection serviceCollection)
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));
            serviceCollection.AddHttpSettingsCore(ShoppingServicesBuilder.CoreServicesHttpInstanted);
            CheckHttpServiceInstanted(serviceCollection);
            new ShoppingServicesBuilder(serviceCollection).TryAddCoreServices();
                       
            return serviceCollection;
        }

        public static void AddHttpSettingsCore(this IServiceCollection serviceCollection, IEnumerable<string> nameServices)
        {
            foreach (var clientHttpModel in HttpServicesinstanted(nameServices, new Uri("https://60b97a2380400f00177b6813.mockapi.io/api/v1/")))
            {
                serviceCollection.AddHttpClient(clientHttpModel.Name, endPoint => {
                    endPoint.BaseAddress = clientHttpModel.Url;
                });
            }
        }
        public static IEnumerable<ClientHttpModel> HttpServicesinstanted(IEnumerable<string> nameServices,Uri urlBase) {
                
                  return nameServices.Select(value => new ClientHttpModel (value ,urlBase))
            .ToList();
        }

        public static void CheckHttpServiceInstanted(IServiceCollection serviceCollection)
        {
            if (!ValidateServices.hasService<IHttpClientFactory>(serviceCollection))
            {
                throw new InvalidOperationException(
                    "The Http  Services is not in the pipeline. Add 'AddShoppingCore' after 'AddHttpSettingsCore' ");
            }

        }

        public static IServiceCollection Teste(this IServiceCollection serviceCollection)
        {
          var teste = serviceCollection.BuildServiceProvider().GetServices<object>();
          var t = ServiceLocator.GetService<Servers>(serviceCollection);
          return serviceCollection;
        }


    }
}