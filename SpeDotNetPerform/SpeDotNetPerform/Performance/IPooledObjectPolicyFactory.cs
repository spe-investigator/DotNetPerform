using Microsoft.Extensions.ObjectPool;

namespace System.Performance {
    public interface IPooledObjectPolicyFactory {
        PooledObjectPolicy<T> GetPooledObjectPolicy<T>() where T : class, new();
    }
}
