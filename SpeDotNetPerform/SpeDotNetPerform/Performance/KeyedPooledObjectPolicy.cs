//using Microsoft.Extensions.ObjectPool;
//using System;
//using System.Collections.Concurrent;

//namespace SpeDotNetPerform.Performance {
//    public abstract class KeyedPooledObjectPolicy<T> : IKeyedPooledObjectPolicy<T> where T : class {
//        private readonly Func<string, IKeyedObjectPool<T>> _objectPoolFactory;

//        protected KeyedPooledObjectPolicy(Func<string, IKeyedObjectPool<T>> objectPoolFactory) {
//            this._objectPoolFactory = objectPoolFactory;
//        }

//        public abstract T Create(string poolKey);

//        public abstract bool ShouldReturn(string poolKey, T obj);
//    }
//}
