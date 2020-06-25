namespace System.Performance {
    public abstract class PooledObjectPolicy<T> : IPooledObjectPolicy<T> {
        public abstract bool OptimisticObjectCreation { get; }

        /// <summary>
        /// Create a T.
        /// </summary>
        /// <returns>The T which was created.</returns>
        public abstract T Create();

        /// <summary>
        /// Runs some processing when an object was returned to the pool.Can be used to
        /// reset the state of an object and indicate if the object should be returned to
        /// the pool.
        /// </summary>
        /// <param name="obj">The object to return to the pool.</param>
        /// <returns>
        /// true
        /// if the object should be returned to the pool.
        /// false
        /// if it's not possible/desirable for the pool to keep the object
        /// </returns>
        public bool Return(T obj) => ShouldReturn(obj);

        /// <summary>
        /// Runs some processing when an object was returned to the pool.Can be used to
        /// reset the state of an object and indicate if the object should be returned to
        /// the pool.
        /// </summary>
        /// <param name="obj">The object to return to the pool.</param>
        /// <returns>
        /// true
        /// if the object should be returned to the pool.
        /// false
        /// if it's not possible/desirable for the pool to keep the object
        /// </returns>
        public abstract bool ShouldReturn(T obj);
    }
}
