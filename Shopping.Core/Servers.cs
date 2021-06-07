using System.Collections.Generic;

namespace Shopping.Core
{
    public class Server
    {
        public string Name { get; set; }
        public string Url  { get; set; }
    }

    public  class Servers {
        public IEnumerable<Server> Server { get; set; }
    }
}