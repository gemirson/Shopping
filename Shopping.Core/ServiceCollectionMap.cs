using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Shopping.Core
{
    public class ServiceCollectionMap: IInfrastructure<IInternalServiceCollectionMap>
    {
        private readonly InternalServiceCollectionMap _map;


        public ServiceCollectionMap(IServiceCollection serviceCollection)
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));

            _map = new InternalServiceCollectionMap(serviceCollection);
        }

        public virtual IServiceCollection ServiceCollection
            => _map.ServiceCollection;

        public virtual ServiceCollectionMap TryAddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => TryAdd(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient);

        public virtual ServiceCollectionMap TryAddScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
            => TryAdd(typeof(TService), typeof(TImplementation), ServiceLifetime.Scoped);

        public virtual ServiceCollectionMap TryAdd(
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime)
        {
            Check.NotNull(serviceType, nameof(serviceType));
            Check.NotNull(implementationType, nameof(implementationType));

            var indexes = _map.GetOrCreateDescriptorIndexes(serviceType);
            if (indexes.Count == 0)
            {
                _map.AddNewDescriptor(indexes, new ServiceDescriptor(serviceType, implementationType, lifetime));
            }

            return this;
        }

        public virtual ServiceCollectionMap TryAdd(
            Type serviceType,
            Func<IServiceProvider, object> factory,
            ServiceLifetime lifetime)
        {
            Check.NotNull(serviceType, nameof(serviceType));
            Check.NotNull(factory, nameof(factory));

            var indexes = _map.GetOrCreateDescriptorIndexes(serviceType);
            if (indexes.Count == 0)
            {
                _map.AddNewDescriptor(indexes, new ServiceDescriptor(serviceType, factory, lifetime));
            }

            return this;
        }

        public virtual ServiceCollectionMap TryAddEnumerable(
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime)
        {
            Check.NotNull(serviceType, nameof(serviceType));
            Check.NotNull(implementationType, nameof(implementationType));

            var indexes = _map.GetOrCreateDescriptorIndexes(serviceType);
            if (indexes.All(i => TryGetImplementationType(ServiceCollection[i]) != implementationType))
            {
                _map.AddNewDescriptor(indexes, new ServiceDescriptor(serviceType, implementationType, lifetime));
            }

            return this;
        }

        public virtual ServiceCollectionMap TryAddEnumerable(
            Type serviceType,
            Type implementationType,
            Func<IServiceProvider, object> factory,
            ServiceLifetime lifetime)
        {
            Check.NotNull(serviceType, nameof(serviceType));
            Check.NotNull(implementationType, nameof(implementationType));
            Check.NotNull(factory, nameof(factory));

            var indexes = _map.GetOrCreateDescriptorIndexes(serviceType);
            if (indexes.All(i => TryGetImplementationType(ServiceCollection[i]) != implementationType))
            {
                _map.AddNewDescriptor(indexes, new ServiceDescriptor(serviceType, factory, lifetime));
            }

            return this;
        }

        private static Type TryGetImplementationType(ServiceDescriptor descriptor)
            => descriptor.ImplementationType
               ?? descriptor.ImplementationInstance?.GetType()
               ?? descriptor.ImplementationFactory?.GetType().GenericTypeArguments[1]!;


        IInternalServiceCollectionMap IInfrastructure<IInternalServiceCollectionMap>.Instance
            => _map;
    }
}