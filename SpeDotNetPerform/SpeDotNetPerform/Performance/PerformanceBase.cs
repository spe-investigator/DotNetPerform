using OP = Microsoft.Extensions.ObjectPool;
using SpeDotNetPerform.Performance;
using System.Collections.Concurrent;

namespace System.Performance {
    public abstract class PerformanceBase<T> : IDisposable where T : class {
        [ThreadStatic]
        protected T _threadedObject;

        static readonly ConcurrentDictionary<Type, DefaultKeyedObjectPool<T>> _keyedObjectPoolCollection;

        internal readonly T _performanceObject;
        internal readonly ObjectWrapper<T> _wrapperObject;
        protected readonly DefaultKeyedObjectPool<T> _keyedObjectPool;

        public string PoolKey { get; }

        public bool IsPoolAllocated => _wrapperObject.Index > -1;

        static PerformanceBase() {
            _keyedObjectPoolCollection = new ConcurrentDictionary<Type, DefaultKeyedObjectPool<T>>();
        }

        protected PerformanceBase(string poolKey, int? poolSize = null) {
            if (!poolSize.HasValue) {
                poolSize = Environment.ProcessorCount * 2;
            }

            if (!_keyedObjectPoolCollection.TryGetValue(typeof(T), out _keyedObjectPool)) {
                _keyedObjectPool = new DefaultKeyedObjectPool<T>(getPooledObjectPolicyFactory(), poolSize.Value);
                // If failed, then another thread inserted an object pool.
                if (_keyedObjectPoolCollection.TryAdd(typeof(T), _keyedObjectPool)) {
                    // Retry Get to reuse other thread's inserted Object Pool.
                    _keyedObjectPoolCollection.TryGetValue(typeof(T), out _keyedObjectPool);
                }
            }

            PoolKey = poolKey;

            if (poolSize < 1) {
                throw new ArgumentException("Positive Pool Size value required.", "poolSize");
            }
                
            _wrapperObject = _keyedObjectPool.Get(poolKey);
            _performanceObject = _wrapperObject.Element;
        }

        protected abstract IPooledObjectPolicy<T> getPooledObjectPolicyFactory();

        /// <summary>
        /// Override method and perform custom IPO cleanup logic, then call base.Dispose().
        /// </summary>
        public virtual void Dispose() {
            _keyedObjectPool.Return(PoolKey, _wrapperObject);
        }

        static internal void reset() {
            _keyedObjectPoolCollection.Clear();
        }
    }
}
