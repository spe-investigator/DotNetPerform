using SpeDotNetPerform.Performance;
using System.Collections.Concurrent;
using System.Linq;

namespace System.Performance {
    public abstract class PerformanceBase<T> : IDisposable where T : class {
        [ThreadStatic]
        protected T _threadedObject;

        static ConcurrentDictionary<int, DefaultKeyedObjectPool<T>> _keyedObjectPoolCollection;
        static ConcurrentDictionary<int, DefaultKeyedObjectPool<T>> keyedObjectPoolCollection {
            get {
                if (_keyedObjectPoolCollection == null) {
                    _keyedObjectPoolCollection = new ConcurrentDictionary<int, DefaultKeyedObjectPool<T>>();
                }

                return _keyedObjectPoolCollection;
            }
        }

        internal ObjectWrapper<T>? _wrapperObject;
        internal ObjectWrapper<T> wrapperObject {
            get {
                if (_wrapperObject == null) {
                    var objectPoolCollection = keyedObjectPoolCollection; // Get accessor.
                    var policyHashCode = getPolicyHashCode();
                    if (!objectPoolCollection.TryGetValue(policyHashCode, out _keyedObjectPool)) {
                        _keyedObjectPool = new DefaultKeyedObjectPool<T>(getPooledObjectPolicyFactory(), PoolSize);

                        // If failed, then another thread inserted an object pool.
                        if (!objectPoolCollection.TryAdd(policyHashCode, _keyedObjectPool)) {
                            // Retry Get to reuse other thread's inserted Object Pool.
                            objectPoolCollection.TryGetValue(policyHashCode, out _keyedObjectPool);
                        }
                    }

                    _wrapperObject = _keyedObjectPool.Get(PoolKey);
                }

                return _wrapperObject.Value;
            }
        }
        internal T _performanceObject => wrapperObject.Element;
        internal DefaultKeyedObjectPool<T> _keyedObjectPool;

        public string PoolKey { get; }
        public int PoolSize { get; }

        public bool IsPoolAllocated => wrapperObject.Index > -1;

        protected PerformanceBase(string poolKey, int? poolSize = null) {
            if (poolSize < 1) {
                throw new ArgumentException("Positive Pool Size value required.", "poolSize");
            }
            
            PoolKey = poolKey;
            PoolSize = !poolSize.HasValue ? Environment.ProcessorCount * 2 : poolSize.Value;   
        }

        protected abstract int getPolicyHashCode();

        /// <summary>
        /// Creates the policy object from the parameters of the constructor that uniquely identify the objects to be created by the policy.
        /// </summary>
        /// <returns></returns>
        protected abstract IPooledObjectPolicy<T> getPooledObjectPolicyFactory();

        /// <summary>
        /// Override method and perform custom IPO cleanup logic, then call base.Dispose().
        /// </summary>
        public virtual void Dispose() {
            _keyedObjectPool.Return(PoolKey, wrapperObject);
        }

        static internal void disposeAll() {
            _keyedObjectPoolCollection.Values.ToList().ForEach(keyedPool => {
                keyedPool.Values.ToList().ForEach(pool => {
                    pool._items.Where(item => item.CheckOut.HasValue).ToList().ForEach(item => pool.Return(item));
                });
            });
        }
            
        static internal void reset() {
            _keyedObjectPoolCollection = null;
        }
    }
}
