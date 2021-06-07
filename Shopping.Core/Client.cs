namespace Shopping.Core
{
    public class Client
    {
        public string Name { get; private set; }

        public Client(string name)
        {
            Name = name;
        }
    }
}