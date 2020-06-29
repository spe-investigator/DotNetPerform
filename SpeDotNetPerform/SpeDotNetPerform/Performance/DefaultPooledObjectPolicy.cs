namespace System.Performance {
    public class DefaultPooledObjectPolicy<T> : PooledObjectPolicy<T> where T : new() {
        public override bool OptimisticObjectCreation { get; }

        public DefaultPooledObjectPolicy(bool optimisticObjectCreation) {
            OptimisticObjectCreation = optimisticObjectCreation;
        }

        public override T Create() {
            return new T();
        }

        public override bool ShouldReturn(T obj) {
            return true;
        }
    }
}
