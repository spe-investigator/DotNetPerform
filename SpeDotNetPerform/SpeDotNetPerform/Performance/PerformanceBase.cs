using SpeDotNetPerform.Performance;
using System.Collections.Concurrent;
using System.Linq;

namespace System.Performance {
    public abstract class PerformanceBase<T> : IDisposable where T : class {
        static ConcurrentDictionary<int, DefaultObjectPool<T>> _objectPoolCollection;
        static ConcurrentDictionary<int, DefaultObjectPool<T>> objectPoolCollection {
            get {
                if (_objectPoolCollection == null) {
                    _objectPoolCollection = new ConcurrentDictionary<int, DefaultObjectPool<T>>();
                }

                return _objectPoolCollection;
            }
        }

        internal bool hasWrapperObject;

        internal ObjectWrapper<T>? _wrapperObject;
        internal ObjectWrapper<T> wrapperObject {
            get {
                if (_wrapperObject == null) {
                    var objectPoolCollection = PerformanceBase<T>.objectPoolCollection; // Get accessor.
                    var policyHashCode = GetHashCode();
                    if (!objectPoolCollection.TryGetValue(policyHashCode, out _objectPool)) {
                        _objectPool = new DefaultObjectPool<T>(getPooledObjectPolicyFactory(), PoolSize);

                        // If failed, then another thread inserted an object pool.
                        if (!objectPoolCollection.TryAdd(policyHashCode, _objectPool)) {
                            // Retry Get to reuse other thread's inserted Object Pool.
                            objectPoolCollection.TryGetValue(policyHashCode, out _objectPool);
                        }
                    }

                    hasWrapperObject = true;
                    _wrapperObject = _objectPool.Get();
                }

                return _wrapperObject.Value;
            }
        }
        internal bool _performanceObjectIsSet = false;
        internal T _performanceObject;
        internal T performanceObject {
            get {
                if (_performanceObjectIsSet) {
                    return _performanceObject;
                }

                var performanceObject = wrapperObject.Element;
                _performanceObject = performanceObject;
                _performanceObjectIsSet = true;
                return performanceObject;
            }
        }
        internal DefaultObjectPool<T> _objectPool;

        public string PoolKey { get; }
        public int PoolSize { get; }

        public bool IsPoolAllocated => hasWrapperObject ? wrapperObject.Index > -1 : false;

        protected PerformanceBase(string poolKey, int? poolSize = null) {
            if (poolSize < 1) {
                throw new ArgumentException("Positive Pool Size value required.", "poolSize");
            }
            
            PoolKey = poolKey;
            PoolSize = !poolSize.HasValue ? Environment.ProcessorCount * 2 : poolSize.Value;
        }

        public override int GetHashCode() {
            return string.IsNullOrEmpty(PoolKey) ? -686918596 : PoolKey.GetHashCode();
        }

        /// <summary>
        /// Creates the policy object from the parameters of the constructor that uniquely identify the objects to be created by the policy.
        /// </summary>
        /// <returns></returns>
        protected abstract IPooledObjectPolicy<T> getPooledObjectPolicyFactory();

        /// <summary>
        /// Override method and perform custom IPO cleanup logic, then call base.Dispose().
        /// </summary>
        public virtual void Dispose() {
            // Do not construct the pool or wrapper if we have not used the object.
            if (hasWrapperObject) {
                _objectPool.Return(wrapperObject);
                
                _wrapperObject = null;
                _performanceObject = null;
                _objectPool = null;
            }
        }

        static internal void disposeAll() {
            objectPoolCollection.Values.ToList().ForEach(pool => {
                pool._items.Where(item => item.Check != null).ToList().ForEach(item => pool.Return(item));
            });
        }

        static internal void resetItemCounter() {
            foreach (var keyedObjectPool in objectPoolCollection.Values) {
                keyedObjectPool.resetItemCounter();
            }
        }

        static internal void reset() {
            _objectPoolCollection = null;
        }
    }
}
