using System;
using System.Collections.Concurrent;
using OP = Microsoft.Extensions.ObjectPool;

namespace SpeDotNetPerform.Performance {
    public class DefaultKeyedObjectPool<T> : ConcurrentDictionary<string, DefaultObjectPool<T>> where T : class {
        //private readonly IEqualityComparer<T> _equalityComparer;
        public readonly IPooledObjectPolicy<T> Policy;

        // This class was introduced in 2.1 to avoid the interface call where possible
        private protected readonly PooledObjectPolicy<T> _fastPolicy;
        private readonly int _poolSize;

        //public DefaultKeyedObjectPool(IPooledObjectPolicy<T> policy) : this(policy, Environment.ProcessorCount * 2) {
        //}

        public DefaultKeyedObjectPool(IPooledObjectPolicy<T> policy, int poolSize) {
            if (poolSize < 1) throw new ArgumentOutOfRangeException(nameof(poolSize));
            
            Policy = policy ?? throw new ArgumentNullException(nameof(policy));
            _fastPolicy = policy as PooledObjectPolicy<T>;
            _poolSize = poolSize;
        }

        internal DefaultObjectPool<T> getObjectPool(string poolKey) {
            if (poolKey == null) {
                poolKey = string.Empty;
            }

            DefaultObjectPool<T> objectPool = null;

            if (!TryGetValue(poolKey, out objectPool)) {
                objectPool = new DefaultObjectPool<T>(_fastPolicy ?? Policy, _poolSize);
                TryAdd(poolKey, objectPool);
            }

            return objectPool;
        }

        public ObjectWrapper<T> Get(string poolKey) {
            if (poolKey == null) {
                poolKey = string.Empty;
            }

            var objectPool = getObjectPool(poolKey);
            return objectPool.Get();
        }

        public void Return(string poolKey, ObjectWrapper<T> obj) {
            if (poolKey == null) {
                poolKey = string.Empty;
            }

            var objectPool = getObjectPool(poolKey);
            objectPool.Return(obj);
        }
    }
}
