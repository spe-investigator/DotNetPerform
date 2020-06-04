using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Performance {
    public class PerformanceBase<T> where T : class {
        [ThreadStatic]
        protected T _threadedObject;

        protected readonly T _performanceObject;
        protected readonly PerformanceCharacteristic _characteristic;
        protected readonly IPooledObjectPolicy<T> _pooledObjectPolicy;
        protected readonly ObjectPool<T> _objectPool;

        protected PerformanceBase(PerformanceCharacteristic characteristic, int maximumRetained, string poolKey, PooledObjectPolicy<T> pooledObjectPolicy, Func<IPooledObjectPolicy<T>, int, ObjectPool<T>> objectPoolFactory = null) {
            _characteristic = characteristic;

            _pooledObjectPolicy = pooledObjectPolicy;

            if (_characteristic == PerformanceCharacteristic.ThreadStatic) {
                if (_threadedObject == null) {
                    _performanceObject = _threadedObject = _pooledObjectPolicy.Create();
                } else {
                    _performanceObject = _threadedObject;
                }
            }

            if (_characteristic == PerformanceCharacteristic.Pooled) {
                if (maximumRetained < 0) {
                    throw new ArgumentException("Positive Pool Size value required.", "poolSize");
                }

                if (objectPoolFactory != null) {
                    _objectPool = objectPoolFactory(_pooledObjectPolicy, maximumRetained);
                } else {
                    _objectPool = new DefaultObjectPool<T>(_pooledObjectPolicy, maximumRetained);
                }
                _performanceObject = _objectPool.Get();

                if (_performanceObject == null) {
                    _performanceObject = _pooledObjectPolicy.Create();
                }
            }
        }
    }
}
