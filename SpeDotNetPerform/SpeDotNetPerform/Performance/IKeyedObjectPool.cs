namespace SpeDotNetPerform.Performance {
    public interface IKeyedObjectPool<T> where T : class {
        string PoolKey { get; }

        //
        // Summary:
        //     Gets an object from the pool if one is available, otherwise creates one.
        //
        // Returns:
        //     A T.
        T Get(string poolKey);
        //
        // Summary:
        //     Return an object to the pool.
        //
        // Parameters:
        //   obj:
        //     The object to add to the pool.
        void Return(T obj);
    }
}
