using System;
using System.Performance;

namespace SpeDotNetPerform.Performance {
    /// <summary>
    /// Cleans up the pool after having run a test.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class PoolBoy<T> : IDisposable where T : class {
        public void ResetItemCounter() {
            PerformanceBase<T>.resetItemCounter();
        }
        
        public void Dispose() {
            PerformanceBase<T>.reset();
        }
        
        public void DisposeAll() {
            PerformanceBase<T>.disposeAll();
        }
    }
}
