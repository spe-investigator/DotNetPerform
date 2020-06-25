namespace System.Performance {
    /// <summary>
    /// Cleans up the pool after having run a test.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PoolBoy<T> : IDisposable where T : class {
        public void ResetItemCounter() {
            StaticPerformanceBase<T>.resetItemCounter();
        }
        
        public void Dispose() {
            StaticPerformanceBase<T>.reset();
        }
        
        public void DisposeAll() {
            StaticPerformanceBase<T>.disposeAll();
        }
    }
}
