namespace Shopping.Core
{
    public interface IInternalServiceCollectionMap
    {
        /// <summary>
        ///     Adds a <see cref="ServiceLifetime.Singleton" />  dependency object.
        /// </summary>
        /// <typeparam name="TDependencies"> The dependency type. </typeparam>
        /// <returns> The same collection map so that further methods can be chained. </returns>
        IInternalServiceCollectionMap AddDependencySingleton<TDependencies>();

        /// <summary>
        ///     Adds a <see cref="ServiceLifetime.Scoped" />  dependency object.
        /// </summary>
        /// <typeparam name="TDependencies"> The dependency type. </typeparam>
        /// <returns> The same collection map so that further methods can be chained. </returns>
        IInternalServiceCollectionMap AddDependencyScoped<TDependencies>();


      
    }
}