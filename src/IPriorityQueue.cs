using System;

namespace GrowingWithTheWeb.DataStructures {
    public interface IPriorityQueue<TKey, TValue> where TKey : IComparable {
        int Size { get; }
        bool IsEmpty { get; }
        
        void Clear();
        void DecreaseKey(INode<TKey, TValue> node, TKey newKey);
        void Delete(INode<TKey, TValue> node);
        INode<TKey, TValue> ExtractMinimum();
        INode<TKey, TValue> FindMinimum();
        INode<TKey, TValue> Insert(TKey key, TValue value);
        // TODO: Use generic IPriorityQueue here
        void Union(FibonacciHeap<TKey, TValue> other);
    }
}