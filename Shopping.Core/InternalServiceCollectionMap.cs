using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace Shopping.Core
{
    public class InternalServiceCollectionMap : IInternalServiceCollectionMap
    {

        private readonly IDictionary<Type, IList<int>> _serviceMap = new Dictionary<Type, IList<int>>();

        private static readonly ResourceManager _resourceManager
            = new ResourceManager("Shopping.Core.Properties.CoreStrings", typeof(InternalServiceCollectionMap).Assembly);
        public InternalServiceCollectionMap(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
            var index = 0;
            foreach (var descriptor in serviceCollection)
            {
                // ReSharper disable once VirtualMemberCallInConstructor
                GetOrCreateDescriptorIndexes(descriptor.ServiceType).Add(index++);
            }
        }
        
        public virtual IServiceCollection ServiceCollection { get; }
        public object CoreStrings { get; private set; }
        
        public virtual IList<int> GetOrCreateDescriptorIndexes(Type serviceType)
        {
            if (!_serviceMap.TryGetValue(serviceType, out var indexes))
            {
                indexes = new List<int>();
                _serviceMap[serviceType] = indexes;
            }

            return indexes;
        }
        public virtual void AddNewDescriptor(IList<int> indexes, ServiceDescriptor newDescriptor)
        {
            indexes.Add(ServiceCollection.Count);
            ServiceCollection.Add(newDescriptor);
        }
        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IInternalServiceCollectionMap AddDependencySingleton<TDependencies>()
            => AddDependency(typeof(TDependencies), ServiceLifetime.Singleton);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IInternalServiceCollectionMap AddDependencyScoped<TDependencies>()
            => AddDependency(typeof(TDependencies), ServiceLifetime.Scoped);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IInternalServiceCollectionMap AddDependency(Type serviceType, ServiceLifetime lifetime)
        {
            var indexes = GetOrCreateDescriptorIndexes(serviceType);
            if (!indexes.Any())
            {
                AddNewDescriptor(indexes, new ServiceDescriptor(serviceType, serviceType, lifetime));
            }
            else if (indexes.Count > 1
                || ServiceCollection[indexes[0]].ImplementationType != serviceType)
            {
                throw new InvalidOperationException(BadDependencyRegistration(serviceType.Name));
            }

            return this;
        }

        public static string BadDependencyRegistration(object? dependenciesType)
            => string.Format(
                GetString("BadDependencyRegistration", nameof(dependenciesType)),
                dependenciesType);

        private static string GetString(string name, params string[] formatterNames)
        {
            var value = _resourceManager.GetString(name)!;
            for (var i = 0; i < formatterNames.Length; i++)
            {
                value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
            }

            return value;
        }

      
    }
}