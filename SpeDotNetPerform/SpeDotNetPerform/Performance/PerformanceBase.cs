using OP = Microsoft.Extensions.ObjectPool;
using SpeDotNetPerform.Performance;
using System.Collections.Concurrent;

namespace System.Performance {
    public abstract class PerformanceBase<T> : IDisposable where T : class {
        [ThreadStatic]
        protected T _threadedObject;

        static readonly ConcurrentDictionary<Type, DefaultKeyedObjectPool<T>> _keyedObjectPoolCollection = new ConcurrentDictionary<Type, DefaultKeyedObjectPool<T>>();

        internal readonly ObjectWrapper<T> _wrapperObject;
        internal readonly T _performanceObject;
        protected readonly PerformanceCharacteristic _characteristic;
        internal readonly DefaultObjectPool<T> _objectPool;
        protected readonly DefaultKeyedObjectPool<T> _keyedObjectPool;

        public string PoolKey { get; }

        protected PerformanceBase(PerformanceCharacteristic characteristic, int? maximumRetained, string poolKey) {
            _characteristic = characteristic;

            if (_characteristic == PerformanceCharacteristic.ThreadStatic) {
                if (!string.IsNullOrEmpty(poolKey)) {
                    throw new ArgumentOutOfRangeException(nameof(poolKey), $"Pool key cannot be specified on Thread Static type {typeof(T).Name}.");
                }
                maximumRetained = 1;
            }

            if (!_keyedObjectPoolCollection.TryGetValue(typeof(T), out _keyedObjectPool)) {
                if (maximumRetained.HasValue) {
                    _keyedObjectPool = new DefaultKeyedObjectPool<T>(getPooledObjectPolicyFactory());
                } else {
                    _keyedObjectPool = new DefaultKeyedObjectPool<T>(getPooledObjectPolicyFactory(), maximumRetained.Value);
                }
                _keyedObjectPoolCollection.TryAdd(typeof(T), _keyedObjectPool);
            }

            PoolKey = poolKey;

            if (_characteristic == PerformanceCharacteristic.ThreadStatic) {
                if (_threadedObject != null) {
                    _wrapperObject = _keyedObjectPool.Get(poolKey);
                    _performanceObject = _threadedObject = _wrapperObject.Element;
                } else {
                    _wrapperObject = _keyedObjectPool.Get(poolKey);
                    _performanceObject = _threadedObject = _wrapperObject.Element;
                }
            }

            if (_characteristic == PerformanceCharacteristic.Pooled) {
                if (maximumRetained < 1) {
                    throw new ArgumentException("Positive Pool Size value required.", "poolSize");
                }
                
                _wrapperObject = _keyedObjectPool.Get(poolKey);
                _performanceObject = _wrapperObject.Element;
            }
        }

        protected abstract IPooledObjectPolicy<T> getPooledObjectPolicyFactory();

        /// <summary>
        /// Override method and perform custom IPO cleanup logic, then call base.Dispose().
        /// </summary>
        public virtual void Dispose() {
            _keyedObjectPool.Return(PoolKey, _wrapperObject);
        }
    }
}
