using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Shopping.Core
{
    public class ShoppingServicesBuilder
    {
        
        public static readonly IDictionary<Type, ServiceCharacteristics> CoreServices
          = new Dictionary<Type, ServiceCharacteristics>
          {
                { typeof(IUserService),    new ServiceCharacteristics(ServiceLifetime.Scoped) },
                { typeof(IClientService),  new ServiceCharacteristics(ServiceLifetime.Scoped) },
                { typeof(IHttpCliente),    new ServiceCharacteristics(ServiceLifetime.Scoped) },
                { typeof(IHttpPerson),     new ServiceCharacteristics(ServiceLifetime.Scoped) }

          };

          public static   readonly IEnumerable<string> CoreServicesHttpInstanted
            = new List<string>
            {
                { nameof(IHttpCliente)},
                { nameof(IHttpPerson)},
              
            };


        public ShoppingServicesBuilder(IServiceCollection serviceCollection)
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));
            ServiceCollectionMap = new ServiceCollectionMap(serviceCollection);
        }


        protected virtual ServiceCollectionMap ServiceCollectionMap { get; }



        protected virtual ServiceCharacteristics GetServiceCharacteristics(Type serviceType)
        {
            if (!CoreServices.TryGetValue(serviceType, out var characteristics))
            {
                throw new InvalidOperationException(CoreStrings.NotAnEFService(serviceType.Name));
            }

            return characteristics;
        }

        public virtual IEnumerable<string> GetAllHttpServicesInstanted { get; private set; }

        public virtual ShoppingServicesBuilder TryAddProviderSpecificServices(Action<ServiceCollectionMap> serviceMap)
        {
            Check.NotNull(serviceMap, nameof(serviceMap));
            serviceMap(ServiceCollectionMap);
            return this;
        }


        public virtual ShoppingServicesBuilder TryAddCoreServices()
        {
            TryAdd<IUserService,   UserService>();
            TryAdd<IClientService, ClientService>();
            TryAdd<IHttpCliente,   CustomerClient>();
            TryAdd<IHttpPerson,    PersonCliente>();

            GetAllHttpServicesInstanted = CoreServicesHttpInstanted;

            return this;
        }

        public virtual ShoppingServicesBuilder TryAdd<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => TryAdd(typeof(TService), typeof(TImplementation));

        /// <summary>
        ///     Adds an implementation of an Entity Framework service only if one has not already been registered.
        ///     The scope of the service is automatically defined by Entity Framework.
        /// </summary>
        /// <param name="serviceType"> The contract for the service. </param>
        /// <param name="implementationType"> The concrete type that implements the service. </param>
        /// <returns> This builder, such that further calls can be chained. </returns>
        public virtual ShoppingServicesBuilder TryAdd(Type serviceType, Type implementationType)
        {
            Check.NotNull(serviceType, nameof(serviceType));
            Check.NotNull(implementationType, nameof(implementationType));

            var characteristics = GetServiceCharacteristics(serviceType);

            if (characteristics.MultipleRegistrations)
            {
                ServiceCollectionMap.TryAddEnumerable(serviceType, implementationType, characteristics.Lifetime);
            }
            else
            {
                ServiceCollectionMap.TryAdd(serviceType, implementationType, characteristics.Lifetime);
            }

            return this;
        }


    }
}