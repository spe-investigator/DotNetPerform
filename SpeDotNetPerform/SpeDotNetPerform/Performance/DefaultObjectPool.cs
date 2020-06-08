using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace SpeDotNetPerform.Performance {
    public class DefaultObjectPool<T> where T : class {
        private protected readonly ObjectWrapper<T>[] _items;
        private protected readonly IPooledObjectPolicy<T> _policy;
        private readonly int _poolSize;
        private protected readonly bool _isDefaultPolicy;
        //private protected T _item;
        //private protected int _itemIndex;

        // This class was introduced in 2.1 to avoid the interface call where possible
        private protected readonly PooledObjectPolicy<T> _fastPolicy;

        /// <summary>
        /// Creates an instance of <see cref="Microsoft.Extensions.ObjectPool.DefaultObjectPool{T}"/>.
        /// </summary>
        /// <param name="policy">The pooling policy to use.</param>
        public DefaultObjectPool(IPooledObjectPolicy<T> policy)
            : this(policy, Environment.ProcessorCount * 2) {
        }

        /// <summary>
        /// Creates an instance of <see cref="Microsoft.Extensions.ObjectPool.DefaultObjectPool{T}"/>.
        /// </summary>
        /// <param name="policy">The pooling policy to use.</param>
        /// <param name="poolSize">The maximum number of objects to retain in the pool.</param>
        public DefaultObjectPool(IPooledObjectPolicy<T> policy, int poolSize) {
            _policy = policy ?? throw new ArgumentNullException(nameof(policy));
            _poolSize = poolSize;
            
            _fastPolicy = policy as PooledObjectPolicy<T>;
            _isDefaultPolicy = IsDefaultPolicy();

            // -1 due to _firstItem
            _items = Enumerable.Range(0, poolSize).Select(i => new ObjectWrapper<T>() { Element = Create(), Index = i, CheckOut = DateTime.MinValue, Check = null }).ToArray();

            bool IsDefaultPolicy() {
                var type = policy.GetType();

                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DefaultPooledObjectPolicy<>);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// location1: The destination, whose value is compared with comparand and possibly replaced.
        /// value: The value that replaces the destination value if the comparison results in equality.
        /// comparand: The value that is compared to the value at location1.
        /// </remarks>
        public ObjectWrapper<T> Get() {
            //if (item == null || Interlocked.CompareExchange(ref _firstItem, null, item) != item) {
            var items = _items;
            for (var i = 0; i < items.Length; i++) {
                var exchangeCheck = Interlocked.CompareExchange(ref items[i].Check, new object(), null);
                if (exchangeCheck == null) {
                    items[i].CheckOut = DateTime.Now;
                    return items[i];
                }
            }

            // If object pool is monopolized, then allocate a throw-away object not contained within the pool.
            return new ObjectWrapper<T>() {
                Element = Create(),
                Index = -1
            };
            //}

            //return item;
        }

        // Non-inline to improve its code quality as uncommon path
        [MethodImpl(MethodImplOptions.NoInlining)]
        private T Create() => _fastPolicy?.Create() ?? _policy.Create();

        public void Return(ObjectWrapper<T> returnItem) {
            if (returnItem.Index == -1) {
                returnItem.Element = null;
            } else {
                var items = _items;

                if (_isDefaultPolicy || (_fastPolicy?.ShouldReturn(returnItem.Element) ?? _policy.ShouldReturn(returnItem.Element))) {
                    items[returnItem.Index].CheckOut = null;
                    items[returnItem.Index].Check = null;
                }
            }
        }
    }

    // PERF: the struct wrapper avoids array-covariance-checks from the runtime when assigning to elements of the array.
    [DebuggerDisplay("{Element}")]
    public struct ObjectWrapper<T> {
        public T Element;
        public int Index;
        public object Check;
        public DateTime? CheckOut;
    }
}
