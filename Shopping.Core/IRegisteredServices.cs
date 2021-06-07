using System;
using System.Collections.Generic;

namespace Shopping.Core
{
    public interface IRegisteredServices
    {
        ISet<Type> Services { get; }
    }
}