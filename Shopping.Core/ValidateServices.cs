using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Shopping.Core
{
    public static class ValidateServices
    {

        public static bool hasService<T>(this IServiceCollection serviceCollection)
        {
            Check.NotNull( serviceCollection, nameof(serviceCollection));
            var service = serviceCollection.BuildServiceProvider().GetService<T>();
            if (service is null) return false;
            return true;
        }

        public static void CheckSettingsService(IServiceCollection serviceCollection)
        {
            if (!hasService<Servers>(serviceCollection))
            {
                throw new InvalidOperationException(
                    "The Settings Services is not in the pipeline. Add 'AddShoppingCore' after 'AddSettingsCore' ");
            }

        }
    }
}
