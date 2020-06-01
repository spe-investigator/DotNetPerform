using Microsoft.Extensions.ObjectPool;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Performance {
    public class PooledObjectPolicyFactory : IPooledObjectPolicyFactory {
        public PooledObjectPolicyFactory() {
        }

        public PooledObjectPolicy<T> GetPooledObjectPolicy<T>() where T : class, new() {
            var type = typeof(T);
            
            // Pull these in via configration so we don't have to 
            if (type == typeof(StringBuilder)) {
                return new StringBuilderPooledObjectPolicy() as PooledObjectPolicy<T>;
            }

            if (type == typeof(Regex)) {
                return new Text.RegularExpressions.Perf.Regex.RegexPooledObjectPolicy() as PooledObjectPolicy<T>;
            }

            return new DefaultPooledObjectPolicy<T>();
        }
    }
}
