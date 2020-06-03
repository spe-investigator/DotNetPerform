# DotNetPerform
.NET does not make several key APIs infallible for developers to utilize. Instead, you have to understand the internal workings of .NET. This Nuget package aims to remove all fallibility from utilizing key .NET constructs in order to simplify and dumby-proof the APIs.

There are recenty additions to .NET Framework and Core that enable developers to **write additional code** in order to make existing APIs perform, as expected, however these APIs create added need to understand the internal performance characteristics.

They bring developers **closer** to internal performance concerns, rather than abstract them away.

The primary goal of this toolkit is to **almost entirely abstract** performance problems away from developers.

This is done by utilizing, internally, ObjectPool<T> in order to abstract pooling away from IPOs or Implicitly Pooled Objects.

**Implicitly Pooled Object**: A class of Objects that, when designed properly for performance, require an underlying pooling mechanism. Pooling enables efficient management internal resources such as memory.

StringBuilder is one such object. Although, it may appear enticing to use StringBuilder out of the box. The basic pattern of newing up a StringBuilder object, using it, and letting it be garbage collected is the obvious implementation, but this results in nominal performance improvements, despite the intent of the object.

StringBuilder's purpose is to prevent the over-creation and over-collection of memory, but its out of the box implementation is not one of an IPO. For non-performance engineers, it's non-obvious this object requires pooling to perform well.

This library works as a bridge for IPOs in order to get developers all the way to performance goals while simultaneously not having to concern themselves with the implementation details in order to make common performance patterns a reality.

We hope you find this toolkit of performance useful.
