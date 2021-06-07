using System.Collections.Generic;


namespace Shopping.Core
{
    public interface IServiceLocator
        {
            IEnumerable<T> GetServices<T>();
        }
    
}
