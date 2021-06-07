using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Shopping.Core
{
    public static class ServiceLocator
    {
        public static IEnumerable<IHttpCliente> GetAllServices(IServiceCollection services)
        {
            return services.BuildServiceProvider().GetServices<IHttpCliente>();
                
        }

        public static T GetService<T>(IServiceCollection services)
        {
            return services.BuildServiceProvider().GetService<T>();

        }
    }
}
