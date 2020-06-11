using System;
using System.Performance;

namespace SpeDotNetPerform.Performance {
    internal class PerformanceBaseTest<T> : IDisposable where T : class {
        public void Dispose() {
            PerformanceBase<T>.reset();
        }
        
        public void DisposeAll() {
            PerformanceBase<T>.disposeAll();
        }
    }
}
