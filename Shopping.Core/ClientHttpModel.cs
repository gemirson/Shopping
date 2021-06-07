using System;

namespace Shopping.Core
{
    public class ClientHttpModel
    {
        public string Name { get; internal set; }
        public Uri Url { get; internal set; }

        public ClientHttpModel(string name, Uri url)
        {
            Name = name;
            Url = url;
        }
    }
}