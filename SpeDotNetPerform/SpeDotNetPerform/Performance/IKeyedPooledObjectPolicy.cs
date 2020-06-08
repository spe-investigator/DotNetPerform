using OP = Microsoft.Extensions.ObjectPool;

namespace SpeDotNetPerform.Performance {
    //
    // Summary:
    //     Represents a policy for managing pooled objects.
    //
    // Type parameters:
    //   T:
    //     The type of object which is being pooled.
    public interface IPooledObjectPolicy<T> : OP.IPooledObjectPolicy<T> {
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
        bool ShouldReturn(T obj);
    }
}
