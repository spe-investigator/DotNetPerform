using System.Collections.Concurrent;
using System.Linq;

namespace System.Performance {
    public delegate IPooledObjectPolicy<T> GetPooledObjectPolicyFactory<T>() where T : class;

    public static class StaticPerformanceBase<T> where T : class {
        public static ConcurrentDictionary<int, GetPooledObjectPolicyFactory<T>> PooledObjectPolicyFactoryCollection { get;set; }

        static ConcurrentDictionary<int, DefaultObjectPool<T>> _objectPoolCollection;
        internal static ConcurrentDictionary<int, DefaultObjectPool<T>> objectPoolCollection {
            get {
                if (_objectPoolCollection == null) {
                    _objectPoolCollection = new ConcurrentDictionary<int, DefaultObjectPool<T>>();
                }

                return _objectPoolCollection;
            }
        }

        static StaticPerformanceBase() {
            PooledObjectPolicyFactoryCollection = new ConcurrentDictionary<int, GetPooledObjectPolicyFactory<T>>(1, 10);
        }

        static internal ObjectWrapper<T> GetWrapperObject(int policyHashCode, int poolSize, out DefaultObjectPool<T> _objectPool) {
            //var policyHashCode = GetHashCode();
            //DefaultObjectPool<T> _objectPool;
            if (!objectPoolCollection.TryGetValue(policyHashCode, out _objectPool)) {
                GetPooledObjectPolicyFactory<T> pooledObjectPolicyFactory;
                if (!PooledObjectPolicyFactoryCollection.TryGetValue(policyHashCode, out pooledObjectPolicyFactory)) {
                    throw new ArgumentNullException("PooledObjectPolicyFactory does not exist.");
                }
                _objectPool = new DefaultObjectPool<T>(pooledObjectPolicyFactory(), poolSize);

                // If failed, then another thread inserted an object pool.
                if (!objectPoolCollection.TryAdd(policyHashCode, _objectPool)) {
                    // Retry Get to reuse other thread's inserted Object Pool.
                    objectPoolCollection.TryGetValue(policyHashCode, out _objectPool);
                }
            }

            var _wrapperObject = _objectPool.Get();

            return _wrapperObject;
        }
        /// <summary>
        /// Override method and perform custom IPO cleanup logic, then call base.Dispose().
        /// </summary>
        public static void Dispose(DefaultObjectPool<T> _objectPool, ObjectWrapper<T> wrapperObject) {
            _objectPool.Return(wrapperObject);
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
            if (_objectPoolCollection != null) {
                _objectPoolCollection.Clear();
                _objectPoolCollection = null;
            }
        }
    }
}
