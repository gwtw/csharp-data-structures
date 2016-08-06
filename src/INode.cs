using System;

namespace GrowingWithTheWeb.DataStructures 
{
    public interface INode<TKey, TValue> : IComparable 
        where TKey : IComparable 
    {
        TKey Key { get; }
        TValue Value { get; }
    }
}