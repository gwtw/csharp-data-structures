
using System;

namespace GrowingWithTheWeb.DataStructures {
    public interface IHeap<T> where T : IComparable {
        int Size { get; }
        bool IsEmpty { get; }
        
        // TODO: Factor out Node into generic interface
        void Clear();
        void DecreaseKey(FibonacciHeap<T>.Node node, T newKey);
        void Delete(FibonacciHeap<T>.Node node);
        FibonacciHeap<T>.Node ExtractMinimum();
        FibonacciHeap<T>.Node FindMinimum();
        FibonacciHeap<T>.Node Insert(T key);
        void Union(FibonacciHeap<T> other);
    }
}