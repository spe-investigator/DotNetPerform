using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Performance {
    public class PerformanceHandler<T> where T : class, new() {
        [ThreadStatic]
        protected T _threadedObject;

        protected readonly T _performanceObject;
        protected readonly PerformanceCharacteristic _characteristic;
        protected readonly DefaultObjectPool<T> _objectPool;

        protected PerformanceHandler(PerformanceCharacteristic characteristic, int poolSize, IPooledObjectPolicyFactory<T> pooledObjectPolicyFactory = null, ObjectPool<T> objectPool = null) {
            _characteristic = characteristic;

            if (pooledObjectPolicyFactory == null) {
                pooledObjectPolicyFactory = PooledObjectPolicyFactory.GetPooledObjectPolicy<T>();
            }

            var _objectPool = pooledObjectPolicyFactory.Create();

            //if (_objectPool == null) {
            //    _objectPool = new DefaultObjectPool<T>(pooledObjectPolicy, poolSize);
            //}
            if (_characteristic == PerformanceCharacteristic.ThreadStatic) {
                if (_threadedObject == null) {
                    _performanceObject  = _threadedObject = pooledObjectPolicyFactory.Create();
                }
                
                _performanceObject = _threadedObject;
            }
                
            if (_characteristic == PerformanceCharacteristic.Pooled) {
                if (poolSize < 0) {
                    throw new ArgumentException("Positive Pool Size value required.", "poolSize");
                }

                _performanceObject = _objectPool.Get();
            }
        }

        protected PerformanceHandler(PerformanceCharacteristic characteristic, int poolSize, string poolKey, PooledObjectPolicy<T> pooledObjectPolicy = null, ObjectPool<T> objectPool = null) {
            _characteristic = characteristic;

            if (_characteristic == PerformanceCharacteristic.ThreadStatic) {
                if (_threadedObject == null) {
                    _performanceObject = _threadedObject = pooledObjectPolicy.Create();
                }

                _performanceObject = _threadedObject;
            }

            if (_characteristic == PerformanceCharacteristic.Pooled) {
                if (poolSize < 0) {
                    throw new ArgumentException("Positive Pool Size value required.", "poolSize");
                }

                if (_objectPool == null) {
                    _objectPool = new DefaultObjectPool<T>(pooledObjectPolicy, poolSize);
                }
                _performanceObject = _objectPool.Get();
            }
        }
    }
}
