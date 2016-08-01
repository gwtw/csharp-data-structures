
using System;

namespace GrowingWithTheWeb.DataStructures {
    public interface IPriorityQueue<TKey, TValue> where TKey : IComparable {
        int Size { get; }
        bool IsEmpty { get; }
        
        // TODO: Factor out Node into generic interface
        void Clear();
        void DecreaseKey(FibonacciHeap<TKey, TValue>.Node node, TKey newKey);
        void Delete(FibonacciHeap<TKey, TValue>.Node node);
        FibonacciHeap<TKey, TValue>.Node ExtractMinimum();
        FibonacciHeap<TKey, TValue>.Node FindMinimum();
        FibonacciHeap<TKey, TValue>.Node Insert(TKey key, TValue value);
        void Union(FibonacciHeap<TKey, TValue> other);
    }
}