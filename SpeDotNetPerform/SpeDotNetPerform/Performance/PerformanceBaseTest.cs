using System;
using System.Collections.Generic;
using System.Performance;
using System.Text;

namespace SpeDotNetPerform.Performance {
    internal class PerformanceBaseTest<T> : IDisposable where T : class {
        public void Dispose() {
            PerformanceBase<T>.reset();
        }
    }
}
