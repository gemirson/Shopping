namespace Shopping.Core
{
    public interface IInfrastructure<T>
    {
        T Instance { get; }
    }

}