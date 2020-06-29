using Gen = System.Collections.Generic;

namespace System.Performance.Collection.Generic {
    public struct List<T> : IDisposable {
        internal bool _wrapperObjectIsSet;
        internal ObjectWrapper<Gen.List<T>> _wrapperObject;
        internal ObjectWrapper<Gen.List<T>> wrapperObject {
            get {
                if (!_wrapperObjectIsSet) {
                    var policyHashCode = GetPolicyHashCode(PoolKey);
                    if (!StaticPerformanceBase<Gen.List<T>>.PooledObjectPolicyFactoryCollection.ContainsKey(policyHashCode)) {
                        // Add in policy factory into collection.
                        StaticPerformanceBase<Gen.List<T>>.PooledObjectPolicyFactoryCollection.TryAdd(policyHashCode, getPooledObjectPolicyFactory);
                    }
                    DefaultObjectPool<Gen.List<T>> objectPool = null;
                    _wrapperObject = StaticPerformanceBase<Gen.List<T>>.GetWrapperObject(policyHashCode, PoolSize, out objectPool);
                    this.objectPool = objectPool;
                    _wrapperObjectIsSet = true;
                }

                return _wrapperObject;
            }
        }

        public string PoolKey { get; }
        public int PoolSize { get; }

        internal DefaultObjectPool<Gen.List<T>> objectPool;

        internal Gen.List<T> performanceObject => wrapperObject.Element;

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.List`1 class that
        //     is empty and has the default initial capacity.
        public List(int? poolSize, string poolKey = "") {
            PoolKey = poolKey;
            PoolSize = !poolSize.HasValue ? Environment.ProcessorCount * 2 : poolSize.Value;
            
            _wrapperObject = default;
            _wrapperObjectIsSet = false;
            objectPool = default;
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.List`1 class that
        //     contains elements copied from the specified collection and has sufficient capacity
        //     to accommodate the number of elements copied.
        //
        // Parameters:
        //   collection:
        //     The collection whose elements are copied to the new list.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     collection is null.
        public List(Gen.IEnumerable<T> collection, string poolKey = "", int? poolSize = null) {
            PoolKey = poolKey;
            PoolSize = !poolSize.HasValue ? Environment.ProcessorCount * 2 : poolSize.Value;

            _wrapperObject = default;
            _wrapperObjectIsSet = false;
            objectPool = default;

            // Add the range to the collection.
            performanceObject.AddRange(collection);
        }
        //
        // Summary:
        //     Initializes a new instance of the System.Collections.Generic.List`1 class that
        //     is empty and has the specified initial capacity.
        //
        // Parameters:
        //   capacity:
        //     The number of elements that the new list can initially store.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     capacity is less than 0.
        public List(int capacity, string poolKey = "", int? poolSize = null) {
            PoolKey = poolKey;
            PoolSize = !poolSize.HasValue ? Environment.ProcessorCount * 2 : poolSize.Value;

            _wrapperObject = default;
            _wrapperObjectIsSet = false;
            objectPool = default;

            // Validate capacity.
            if (performanceObject.Capacity < capacity) {
                performanceObject.Capacity = capacity;
            }
        }

        static int GetPolicyHashCode(string poolKey) {
            var hashCode = string.IsNullOrEmpty(poolKey) ? -686918596 : poolKey.GetHashCode();

            return hashCode;
        }

        IPooledObjectPolicy<Gen.List<T>> getPooledObjectPolicyFactory() {
            return new DefaultPooledObjectPolicy<Gen.List<T>>(true);
        }
        #region List<T> Implemention
        //
        // Summary:
        //     Gets or sets the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to get or set.
        //
        // Returns:
        //     The element at the specified index.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0. -or- index is equal to or greater than System.Collections.Generic.List`1.Count.
        public T this[int index] { get => performanceObject[index]; set => performanceObject[index] = value; }

        //
        // Summary:
        //     Gets the number of elements contained in the System.Collections.Generic.List`1.
        //
        // Returns:
        //     The number of elements contained in the System.Collections.Generic.List`1.
        public int Count { get => performanceObject.Count; }
        //
        // Summary:
        //     Gets or sets the total number of elements the internal data structure can hold
        //     without resizing.
        //
        // Returns:
        //     The number of elements that the System.Collections.Generic.List`1 can contain
        //     before resizing is required.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     System.Collections.Generic.List`1.Capacity is set to a value that is less than
        //     System.Collections.Generic.List`1.Count.
        //
        //   T:System.OutOfMemoryException:
        //     There is not enough memory available on the system.
        public int Capacity { get => performanceObject.Capacity; set => performanceObject.Capacity = value; }

        //
        // Summary:
        //     Adds an object to the end of the System.Collections.Generic.List`1.
        //
        // Parameters:
        //   item:
        //     The object to be added to the end of the System.Collections.Generic.List`1. The
        //     value can be null for reference types.
        public void Add(T item) => performanceObject.Add(item);
        //
        // Summary:
        //     Adds the elements of the specified collection to the end of the System.Collections.Generic.List`1.
        //
        // Parameters:
        //   collection:
        //     The collection whose elements should be added to the end of the System.Collections.Generic.List`1.
        //     The collection itself cannot be null, but it can contain elements that are null,
        //     if type T is a reference type.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     collection is null.
        public void AddRange(Gen.IEnumerable<T> collection) => performanceObject.AddRange(collection);
        //
        // Summary:
        //     Returns a read-only System.Collections.ObjectModel.ReadOnlyCollection`1 wrapper
        //     for the current collection.
        //
        // Returns:
        //     An object that acts as a read-only wrapper around the current System.Collections.Generic.List`1.
        public System.Collections.ObjectModel.ReadOnlyCollection<T> AsReadOnly() => performanceObject.AsReadOnly();
        //
        // Summary:
        //     Searches a range of elements in the sorted System.Collections.Generic.List`1
        //     for an element using the specified comparer and returns the zero-based index
        //     of the element.
        //
        // Parameters:
        //   index:
        //     The zero-based starting index of the range to search.
        //
        //   count:
        //     The length of the range to search.
        //
        //   item:
        //     The object to locate. The value can be null for reference types.
        //
        //   comparer:
        //     The System.Collections.Generic.IComparer`1 implementation to use when comparing
        //     elements, or null to use the default comparer System.Collections.Generic.Comparer`1.Default.
        //
        // Returns:
        //     The zero-based index of item in the sorted System.Collections.Generic.List`1,
        //     if item is found; otherwise, a negative number that is the bitwise complement
        //     of the index of the next element that is larger than item or, if there is no
        //     larger element, the bitwise complement of System.Collections.Generic.List`1.Count.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0. -or- count is less than 0.
        //
        //   T:System.ArgumentException:
        //     index and count do not denote a valid range in the System.Collections.Generic.List`1.
        //
        //   T:System.InvalidOperationException:
        //     comparer is null, and the default comparer System.Collections.Generic.Comparer`1.Default
        //     cannot find an implementation of the System.IComparable`1 generic interface or
        //     the System.IComparable interface for type T.
        public int BinarySearch(int index, int count, T item, Gen.IComparer<T> comparer) => performanceObject.BinarySearch(index, count, item, comparer);
        //
        // Summary:
        //     Searches the entire sorted System.Collections.Generic.List`1 for an element using
        //     the default comparer and returns the zero-based index of the element.
        //
        // Parameters:
        //   item:
        //     The object to locate. The value can be null for reference types.
        //
        // Returns:
        //     The zero-based index of item in the sorted System.Collections.Generic.List`1,
        //     if item is found; otherwise, a negative number that is the bitwise complement
        //     of the index of the next element that is larger than item or, if there is no
        //     larger element, the bitwise complement of System.Collections.Generic.List`1.Count.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     The default comparer System.Collections.Generic.Comparer`1.Default cannot find
        //     an implementation of the System.IComparable`1 generic interface or the System.IComparable
        //     interface for type T.
        public int BinarySearch(T item) => performanceObject.BinarySearch(item);
        //
        // Summary:
        //     Searches the entire sorted System.Collections.Generic.List`1 for an element using
        //     the specified comparer and returns the zero-based index of the element.
        //
        // Parameters:
        //   item:
        //     The object to locate. The value can be null for reference types.
        //
        //   comparer:
        //     The System.Collections.Generic.IComparer`1 implementation to use when comparing
        //     elements. -or- null to use the default comparer System.Collections.Generic.Comparer`1.Default.
        //
        // Returns:
        //     The zero-based index of item in the sorted System.Collections.Generic.List`1,
        //     if item is found; otherwise, a negative number that is the bitwise complement
        //     of the index of the next element that is larger than item or, if there is no
        //     larger element, the bitwise complement of System.Collections.Generic.List`1.Count.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     comparer is null, and the default comparer System.Collections.Generic.Comparer`1.Default
        //     cannot find an implementation of the System.IComparable`1 generic interface or
        //     the System.IComparable interface for type T.
        public int BinarySearch(T item, Gen.IComparer<T> comparer) => performanceObject.BinarySearch(item, comparer);
        //
        // Summary:
        //     Removes all elements from the System.Collections.Generic.List`1.
        public void Clear() => performanceObject.Clear();
        //
        // Summary:
        //     Determines whether an element is in the System.Collections.Generic.List`1.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List`1. The value can
        //     be null for reference types.
        //
        // Returns:
        //     true if item is found in the System.Collections.Generic.List`1; otherwise, false.
        public bool Contains(T item) => performanceObject.Contains(item);

        //
        // Summary:
        //     Converts the elements in the current System.Collections.Generic.List`1 to another
        //     type, and returns a list containing the converted elements.
        //
        // Parameters:
        //   converter:
        //     A System.Converter`2 delegate that converts each element from one type to another
        //     type.
        //
        // Type parameters:
        //   TOutput:
        //     The type of the elements of the target array.
        //
        // Returns:
        //     A System.Collections.Generic.List`1 of the target type containing the converted
        //     elements from the current System.Collections.Generic.List`1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     converter is null.
        public Gen.List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) => performanceObject.ConvertAll(converter);
        //
        // Summary:
        //     Copies the entire System.Collections.Generic.List`1 to a compatible one-dimensional
        //     array, starting at the specified index of the target array.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements copied
        //     from System.Collections.Generic.List`1. The System.Array must have zero-based
        //     indexing.
        //
        //   arrayIndex:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     array is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     arrayIndex is less than 0.
        //
        //   T:System.ArgumentException:
        //     The number of elements in the source System.Collections.Generic.List`1 is greater
        //     than the available space from arrayIndex to the end of the destination array.
        public void CopyTo(T[] array, int arrayIndex) => performanceObject.CopyTo(array, arrayIndex);

        //
        // Summary:
        //     Copies the entire System.Collections.Generic.List`1 to a compatible one-dimensional
        //     array, starting at the beginning of the target array.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements copied
        //     from System.Collections.Generic.List`1. The System.Array must have zero-based
        //     indexing.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     array is null.
        //
        //   T:System.ArgumentException:
        //     The number of elements in the source System.Collections.Generic.List`1 is greater
        //     than the number of elements that the destination array can contain.
        public void CopyTo(T[] array) => performanceObject.CopyTo(array);

        //
        // Summary:
        //     Copies a range of elements from the System.Collections.Generic.List`1 to a compatible
        //     one-dimensional array, starting at the specified index of the target array.
        //
        // Parameters:
        //   index:
        //     The zero-based index in the source System.Collections.Generic.List`1 at which
        //     copying begins.
        //
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements copied
        //     from System.Collections.Generic.List`1. The System.Array must have zero-based
        //     indexing.
        //
        //   arrayIndex:
        //     The zero-based index in array at which copying begins.
        //
        //   count:
        //     The number of elements to copy.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     array is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0. -or- arrayIndex is less than 0. -or- count is less than
        //     0.
        //
        //   T:System.ArgumentException:
        //     index is equal to or greater than the System.Collections.Generic.List`1.Count
        //     of the source System.Collections.Generic.List`1. -or- The number of elements
        //     from index to the end of the source System.Collections.Generic.List`1 is greater
        //     than the available space from arrayIndex to the end of the destination array.
        public void CopyTo(int index, T[] array, int arrayIndex, int count) => performanceObject.CopyTo(index, array, arrayIndex, count);

        //
        // Summary:
        //     Determines whether the System.Collections.Generic.List`1 contains elements that
        //     match the conditions defined by the specified predicate.
        //
        // Parameters:
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the elements to
        //     search for.
        //
        // Returns:
        //     true if the System.Collections.Generic.List`1 contains one or more elements that
        //     match the conditions defined by the specified predicate; otherwise, false.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        public bool Exists(Predicate<T> match) => performanceObject.Exists(match);

        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the first occurrence within the entire System.Collections.Generic.List`1.
        //
        // Parameters:
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the element to
        //     search for.
        //
        // Returns:
        //     The first element that matches the conditions defined by the specified predicate,
        //     if found; otherwise, the default value for type T.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        public T Find(Predicate<T> match) => performanceObject.Find(match);

        //
        // Summary:
        //     Retrieves all the elements that match the conditions defined by the specified
        //     predicate.
        //
        // Parameters:
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the elements to
        //     search for.
        //
        // Returns:
        //     A System.Collections.Generic.List`1 containing all the elements that match the
        //     conditions defined by the specified predicate, if found; otherwise, an empty
        //     System.Collections.Generic.List`1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        public Gen.List<T> FindAll(Predicate<T> match) => performanceObject.FindAll(match);

        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the first occurrence within the
        //     range of elements in the System.Collections.Generic.List`1 that starts at the
        //     specified index and contains the specified number of elements.
        //
        // Parameters:
        //   startIndex:
        //     The zero-based starting index of the search.
        //
        //   count:
        //     The number of elements in the section to search.
        //
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the element to
        //     search for.
        //
        // Returns:
        //     The zero-based index of the first occurrence of an element that matches the conditions
        //     defined by match, if found; otherwise, -1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is outside the range of valid indexes for the System.Collections.Generic.List`1.
        //     -or- count is less than 0. -or- startIndex and count do not specify a valid section
        //     in the System.Collections.Generic.List`1.
        public int FindIndex(int startIndex, int count, Predicate<T> match) => performanceObject.FindIndex(startIndex, count, match);

        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the first occurrence within the
        //     range of elements in the System.Collections.Generic.List`1 that extends from
        //     the specified index to the last element.
        //
        // Parameters:
        //   startIndex:
        //     The zero-based starting index of the search.
        //
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the element to
        //     search for.
        //
        // Returns:
        //     The zero-based index of the first occurrence of an element that matches the conditions
        //     defined by match, if found; otherwise, -1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is outside the range of valid indexes for the System.Collections.Generic.List`1.
        public int FindIndex(int startIndex, Predicate<T> match) => performanceObject.FindIndex(startIndex, match);

        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the first occurrence within the
        //     entire System.Collections.Generic.List`1.
        //
        // Parameters:
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the element to
        //     search for.
        //
        // Returns:
        //     The zero-based index of the first occurrence of an element that matches the conditions
        //     defined by match, if found; otherwise, -1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        public int FindIndex(Predicate<T> match) => performanceObject.FindIndex(match);

        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the last occurrence within the entire System.Collections.Generic.List`1.
        //
        // Parameters:
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the element to
        //     search for.
        //
        // Returns:
        //     The last element that matches the conditions defined by the specified predicate,
        //     if found; otherwise, the default value for type T.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        public T FindLast(Predicate<T> match) => performanceObject.FindLast(match);

        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the last occurrence within the
        //     range of elements in the System.Collections.Generic.List`1 that contains the
        //     specified number of elements and ends at the specified index.
        //
        // Parameters:
        //   startIndex:
        //     The zero-based starting index of the backward search.
        //
        //   count:
        //     The number of elements in the section to search.
        //
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the element to
        //     search for.
        //
        // Returns:
        //     The zero-based index of the last occurrence of an element that matches the conditions
        //     defined by match, if found; otherwise, -1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is outside the range of valid indexes for the System.Collections.Generic.List`1.
        //     -or- count is less than 0. -or- startIndex and count do not specify a valid section
        //     in the System.Collections.Generic.List`1.
        public int FindLastIndex(int startIndex, int count, Predicate<T> match) => performanceObject.FindLastIndex(startIndex, count, match);

        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the last occurrence within the
        //     range of elements in the System.Collections.Generic.List`1 that extends from
        //     the first element to the specified index.
        //
        // Parameters:
        //   startIndex:
        //     The zero-based starting index of the backward search.
        //
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the element to
        //     search for.
        //
        // Returns:
        //     The zero-based index of the last occurrence of an element that matches the conditions
        //     defined by match, if found; otherwise, -1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is outside the range of valid indexes for the System.Collections.Generic.List`1.
        public int FindLastIndex(int startIndex, Predicate<T> match) => performanceObject.FindLastIndex(startIndex, match);

        //
        // Summary:
        //     Searches for an element that matches the conditions defined by the specified
        //     predicate, and returns the zero-based index of the last occurrence within the
        //     entire System.Collections.Generic.List`1.
        //
        // Parameters:
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the element to
        //     search for.
        //
        // Returns:
        //     The zero-based index of the last occurrence of an element that matches the conditions
        //     defined by match, if found; otherwise, -1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        public int FindLastIndex(Predicate<T> match) => performanceObject.FindLastIndex(match);

        //
        // Summary:
        //     Performs the specified action on each element of the System.Collections.Generic.List`1.
        //
        // Parameters:
        //   action:
        //     The System.Action`1 delegate to perform on each element of the System.Collections.Generic.List`1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     action is null.
        //
        //   T:System.InvalidOperationException:
        //     An element in the collection has been modified.
        public void ForEach(Action<T> action) => performanceObject.ForEach(action);

        //
        // Summary:
        //     Returns an enumerator that iterates through the System.Collections.Generic.List`1.
        //
        // Returns:
        //     A System.Collections.Generic.List`1.Enumerator for the System.Collections.Generic.List`1.
        public Gen.List<T>.Enumerator GetEnumerator() => performanceObject.GetEnumerator();
        //
        // Summary:
        //     Creates a shallow copy of a range of elements in the source System.Collections.Generic.List`1.
        //
        // Parameters:
        //   index:
        //     The zero-based System.Collections.Generic.List`1 index at which the range starts.
        //
        //   count:
        //     The number of elements in the range.
        //
        // Returns:
        //     A shallow copy of a range of elements in the source System.Collections.Generic.List`1.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0. -or- count is less than 0.
        //
        //   T:System.ArgumentException:
        //     index and count do not denote a valid range of elements in the System.Collections.Generic.List`1.
        public Gen.List<T> GetRange(int index, int count) => performanceObject.GetRange(index, count);

        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the first
        //     occurrence within the range of elements in the System.Collections.Generic.List`1
        //     that starts at the specified index and contains the specified number of elements.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List`1. The value can
        //     be null for reference types.
        //
        //   index:
        //     The zero-based starting index of the search. 0 (zero) is valid in an empty list.
        //
        //   count:
        //     The number of elements in the section to search.
        //
        // Returns:
        //     The zero-based index of the first occurrence of item within the range of elements
        //     in the System.Collections.Generic.List`1 that starts at index and contains count
        //     number of elements, if found; otherwise, -1.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is outside the range of valid indexes for the System.Collections.Generic.List`1.
        //     -or- count is less than 0. -or- index and count do not specify a valid section
        //     in the System.Collections.Generic.List`1.
        public int IndexOf(T item, int index, int count) => performanceObject.IndexOf(item, index, count);

        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the first
        //     occurrence within the range of elements in the System.Collections.Generic.List`1
        //     that extends from the specified index to the last element.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List`1. The value can
        //     be null for reference types.
        //
        //   index:
        //     The zero-based starting index of the search. 0 (zero) is valid in an empty list.
        //
        // Returns:
        //     The zero-based index of the first occurrence of item within the range of elements
        //     in the System.Collections.Generic.List`1 that extends from index to the last
        //     element, if found; otherwise, -1.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is outside the range of valid indexes for the System.Collections.Generic.List`1.
        public int IndexOf(T item, int index) => performanceObject.IndexOf(item, index);

        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the first
        //     occurrence within the entire System.Collections.Generic.List`1.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List`1. The value can
        //     be null for reference types.
        //
        // Returns:
        //     The zero-based index of the first occurrence of item within the entire System.Collections.Generic.List`1,
        //     if found; otherwise, -1.
        public int IndexOf(T item) => performanceObject.IndexOf(item);

        //
        // Summary:
        //     Inserts an element into the System.Collections.Generic.List`1 at the specified
        //     index.
        //
        // Parameters:
        //   index:
        //     The zero-based index at which item should be inserted.
        //
        //   item:
        //     The object to insert. The value can be null for reference types.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0. -or- index is greater than System.Collections.Generic.List`1.Count.
        public void Insert(int index, T item) => performanceObject.Insert(index, item);

        //
        // Summary:
        //     Inserts the elements of a collection into the System.Collections.Generic.List`1
        //     at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index at which the new elements should be inserted.
        //
        //   collection:
        //     The collection whose elements should be inserted into the System.Collections.Generic.List`1.
        //     The collection itself cannot be null, but it can contain elements that are null,
        //     if type T is a reference type.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     collection is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0. -or- index is greater than System.Collections.Generic.List`1.Count.
        public void InsertRange(int index, Gen.IEnumerable<T> collection) => performanceObject.InsertRange(index, collection);

        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the last
        //     occurrence within the entire System.Collections.Generic.List`1.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List`1. The value can
        //     be null for reference types.
        //
        // Returns:
        //     The zero-based index of the last occurrence of item within the entire the System.Collections.Generic.List`1,
        //     if found; otherwise, -1.
        public int LastIndexOf(T item) => performanceObject.LastIndexOf(item);

        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the last
        //     occurrence within the range of elements in the System.Collections.Generic.List`1
        //     that extends from the first element to the specified index.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List`1. The value can
        //     be null for reference types.
        //
        //   index:
        //     The zero-based starting index of the backward search.
        //
        // Returns:
        //     The zero-based index of the last occurrence of item within the range of elements
        //     in the System.Collections.Generic.List`1 that extends from the first element
        //     to index, if found; otherwise, -1.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is outside the range of valid indexes for the System.Collections.Generic.List`1.
        public int LastIndexOf(T item, int index) => performanceObject.LastIndexOf(item, index);

        //
        // Summary:
        //     Searches for the specified object and returns the zero-based index of the last
        //     occurrence within the range of elements in the System.Collections.Generic.List`1
        //     that contains the specified number of elements and ends at the specified index.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.List`1. The value can
        //     be null for reference types.
        //
        //   index:
        //     The zero-based starting index of the backward search.
        //
        //   count:
        //     The number of elements in the section to search.
        //
        // Returns:
        //     The zero-based index of the last occurrence of item within the range of elements
        //     in the System.Collections.Generic.List`1 that contains count number of elements
        //     and ends at index, if found; otherwise, -1.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is outside the range of valid indexes for the System.Collections.Generic.List`1.
        //     -or- count is less than 0. -or- index and count do not specify a valid section
        //     in the System.Collections.Generic.List`1.
        public int LastIndexOf(T item, int index, int count) => performanceObject.LastIndexOf(item, index, count);

        //
        // Summary:
        //     Removes the first occurrence of a specific object from the System.Collections.Generic.List`1.
        //
        // Parameters:
        //   item:
        //     The object to remove from the System.Collections.Generic.List`1. The value can
        //     be null for reference types.
        //
        // Returns:
        //     true if item is successfully removed; otherwise, false. This method also returns
        //     false if item was not found in the System.Collections.Generic.List`1.
        public bool Remove(T item) => performanceObject.Remove(item);

        //
        // Summary:
        //     Removes all the elements that match the conditions defined by the specified predicate.
        //
        // Parameters:
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions of the elements to
        //     remove.
        //
        // Returns:
        //     The number of elements removed from the System.Collections.Generic.List`1.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        public int RemoveAll(Predicate<T> match) => performanceObject.RemoveAll(match);

        //
        // Summary:
        //     Removes the element at the specified index of the System.Collections.Generic.List`1.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to remove.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0. -or- index is equal to or greater than System.Collections.Generic.List`1.Count.
        public void RemoveAt(int index) => performanceObject.RemoveAt(index);

        //
        // Summary:
        //     Removes a range of elements from the System.Collections.Generic.List`1.
        //
        // Parameters:
        //   index:
        //     The zero-based starting index of the range of elements to remove.
        //
        //   count:
        //     The number of elements to remove.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0. -or- count is less than 0.
        //
        //   T:System.ArgumentException:
        //     index and count do not denote a valid range of elements in the System.Collections.Generic.List`1.
        public void RemoveRange(int index, int count) => performanceObject.RemoveRange(index, count);

        //
        // Summary:
        //     Reverses the order of the elements in the specified range.
        //
        // Parameters:
        //   index:
        //     The zero-based starting index of the range to reverse.
        //
        //   count:
        //     The number of elements in the range to reverse.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0. -or- count is less than 0.
        //
        //   T:System.ArgumentException:
        //     index and count do not denote a valid range of elements in the System.Collections.Generic.List`1.
        public void Reverse(int index, int count) => performanceObject.Reverse(index, count);

        //
        // Summary:
        //     Reverses the order of the elements in the entire System.Collections.Generic.List`1.
        public void Reverse() => performanceObject.Reverse();

        //
        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.List`1 using the
        //     specified System.Comparison`1.
        //
        // Parameters:
        //   comparison:
        //     The System.Comparison`1 to use when comparing elements.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     comparison is null.
        //
        //   T:System.ArgumentException:
        //     The implementation of comparison caused an error during the sort. For example,
        //     comparison might not return 0 when comparing an item with itself.
        public void Sort(Comparison<T> comparison) => performanceObject.Sort(comparison);

        //
        // Summary:
        //     Sorts the elements in a range of elements in System.Collections.Generic.List`1
        //     using the specified comparer.
        //
        // Parameters:
        //   index:
        //     The zero-based starting index of the range to sort.
        //
        //   count:
        //     The length of the range to sort.
        //
        //   comparer:
        //     The System.Collections.Generic.IComparer`1 implementation to use when comparing
        //     elements, or null to use the default comparer System.Collections.Generic.Comparer`1.Default.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     index is less than 0. -or- count is less than 0.
        //
        //   T:System.ArgumentException:
        //     index and count do not specify a valid range in the System.Collections.Generic.List`1.
        //     -or- The implementation of comparer caused an error during the sort. For example,
        //     comparer might not return 0 when comparing an item with itself.
        //
        //   T:System.InvalidOperationException:
        //     comparer is null, and the default comparer System.Collections.Generic.Comparer`1.Default
        //     cannot find implementation of the System.IComparable`1 generic interface or the
        //     System.IComparable interface for type T.
        public void Sort(int index, int count, Gen.IComparer<T> comparer) => performanceObject.Sort(index, count, comparer);

        //
        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.List`1 using the
        //     default comparer.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     The default comparer System.Collections.Generic.Comparer`1.Default cannot find
        //     an implementation of the System.IComparable`1 generic interface or the System.IComparable
        //     interface for type T.
        public void Sort() => performanceObject.Sort();

        //
        // Summary:
        //     Sorts the elements in the entire System.Collections.Generic.List`1 using the
        //     specified comparer.
        //
        // Parameters:
        //   comparer:
        //     The System.Collections.Generic.IComparer`1 implementation to use when comparing
        //     elements, or null to use the default comparer System.Collections.Generic.Comparer`1.Default.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     comparer is null, and the default comparer System.Collections.Generic.Comparer`1.Default
        //     cannot find implementation of the System.IComparable`1 generic interface or the
        //     System.IComparable interface for type T.
        //
        //   T:System.ArgumentException:
        //     The implementation of comparer caused an error during the sort. For example,
        //     comparer might not return 0 when comparing an item with itself.
        public void Sort(Gen.IComparer<T> comparer) => performanceObject.Sort(comparer);

        //
        // Summary:
        //     Copies the elements of the System.Collections.Generic.List`1 to a new array.
        //
        // Returns:
        //     An array containing copies of the elements of the System.Collections.Generic.List`1.
        public T[] ToArray() => performanceObject.ToArray();

        /// <summary>
        /// Pooled object cannot be trimmed for performance reasons.
        /// </summary>
        /// <remarks>
        /// Base object's comment:
        /// Sets the capacity to the actual number of elements in the System.Collections.Generic.List`1,
        /// if that number is less than a threshold value.
        /// </remarks>
        /// <exception cref="InvalidOperationException">Will always throw Invalid Operation.</exception>
        public void TrimExcess() {
            throw new InvalidOperationException("Cannot trim excess on pooled object.");
        }

        //
        // Summary:
        //     Determines whether every element in the System.Collections.Generic.List`1 matches
        //     the conditions defined by the specified predicate.
        //
        // Parameters:
        //   match:
        //     The System.Predicate`1 delegate that defines the conditions to check against
        //     the elements.
        //
        // Returns:
        //     true if every element in the System.Collections.Generic.List`1 matches the conditions
        //     defined by the specified predicate; otherwise, false. If the list has no elements,
        //     the return value is true.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     match is null.
        public bool TrueForAll(Predicate<T> match) => performanceObject.TrueForAll(match);
        #endregion

        public const bool DoNotLeaveObjectContents = false;
        public void Dispose() => Dispose(DoNotLeaveObjectContents);

        internal void Dispose(bool leavePooledObjectContents) {
            if (!leavePooledObjectContents) {
                Clear();
            }

            // Only need to Dispose if it's been allocated from the pool.
            if (wrapperObject.IsPoolAllocated) {
                StaticPerformanceBase<Gen.List<T>>.Dispose(objectPool, wrapperObject);
            }
        }
    }
}
