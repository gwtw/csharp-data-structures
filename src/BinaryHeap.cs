using System;
using System.Collections.Generic;

namespace GrowingWithTheWeb.DataStructures {
    /// <summary>
    /// Represents a binary heap data structure capable of storing generic key-value pairs.
    /// </summary>
    public class BinaryHeap<TKey, TValue> : IPriorityQueue<TKey, TValue>
        where TKey : System.IComparable
    {
        /// <summary>
        /// Represents an invalid index that comes from GetParentIndex.
        /// </summary>
        private const int _invalidIndex = -1;

        /// <summary>
        /// The heap's data.
        /// </summary>
        private List<Node> list;

        /// <summary>
        /// Creates a binary heap.
        /// </summary>
        public BinaryHeap() : this(0) 
        {
        }

        /// <summary>
        /// Creates a binary heap.
        /// </summary>
        /// <param name="size">The expected maximum size of the heap. this value reduces the number
        /// of reallocations to the backing List.</param>
        public BinaryHeap(int size) 
        {
            list = new List<Node>(size);
        }

        /// <summary>
        /// Creates a binary heap.
        /// </summary>
        /// <param name="items">A List to initialise the binary heap with.</param>
        public BinaryHeap(List<Node> items)
        {
            list = items;
            BuildHeap();
        }

        /// <summary>
        /// Inserts a new key-value pair into the heap.
        /// </summary>
        /// <param name="key">The key to insert.</param>
        /// <param name="val">The value to insert.</param>
        /// <returns>The inserted node.</returns>
        public INode<TKey, TValue> Insert(TKey key, TValue val) 
        {
            return Insert(new Node(key, val));
        }

        /// <summary>
        /// Inserts a Node into the heap.
        /// </summary>
        /// <param name="node">The node to insert.</param>
        /// <returns>The inserted node.</returns>
        public INode<TKey, TValue> Insert(Node node)
        {
            int i = list.Count;
            list.Add(node);
            SiftUp(i);
            return node;
        } 

        /// <summary>
        /// Extracts and returns the minimum node from the heap.
        /// </summary>
        /// <returns>The heap's minimum node or undefined if the heap is empty.</returns>
        public INode<TKey, TValue> ExtractMinimum() 
        {
            if (list.Count == 0) 
                return null;
            if (list.Count == 1) 
            {
                var ret = list[0];
                list.RemoveAt(0);
                return ret;
            }
            Node min = list[0];
            Node last = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            list[0] = last;
            Heapify(0);
            return min;
        }

        /// <summary>
        /// Returns the minimum node from the heap.
        /// </summary>
        /// <returns>The heap's minimum node or undefined if the heap is empty.</returns>
        public INode<TKey, TValue> FindMinimum() 
        {
            if (list.Count == 0)
                return null;
            return list[0];
        }

        /// <summary>
        /// Clears the heap's data, making it an empty heap.
        /// </summary>
        public void Clear() 
        {
            list.Clear();
        }

        /// <summary>
        /// Constructs a heap out of the data list.
        /// </summary>
        private void BuildHeap() 
        {
            for (int i = (int)(list.Count / 2); i >= 0; i--) 
                Heapify(i);
        }

        /// <summary>
        /// Heapifies the binary heap on an index, swapping the element with its smallest child if
        /// it's less than that node and recursing if so.
        /// </summary>
        /// <param name="i">The index to heapify.</param>
        private void Heapify(int i)
        {
            int l = GetLeftIndex(i);
            int r = GetRightIndex(i);
            int smallest = i;
            if (l < list.Count && list[l].CompareTo(list[i]) < 0) 
                smallest = l;
            if (r < list.Count && list[r].CompareTo(list[smallest]) < 0) 
                smallest = r;
            if (smallest != i) 
            {
                Swap(i, smallest);
                Heapify(smallest);
            }
        }

        /// <summary>
        /// Swap two indexes within the list.
        /// </summary>
        /// <param name="i1">The first index to swap.</param>
        /// <param name="i2">The second index to swap.</param>
        private void Swap(int i1, int i2)
        {
            Node temp = list[i1];
            list[i1] = list[i2];
            list[i2] = temp;
        }

        /// <summary>
        /// Gets the index of an index's parent.
        /// </summary>
        /// <param name="i">The index.</param>
        /// <returns>The index's parent.</returns>
        private int GetParentIndex(int i)
        {
            if (i == 0) 
                return _invalidIndex;
            return (i - 1) / 2;
        }

        /// <summary>
        /// Gets the index of an index's left child.
        /// </summary>
        /// <param name="i">The index.</param>
        /// <returns>The index's left child.</returns>
        private int GetLeftIndex(int i)
        {
            return 2 * i + 1;
        }

        /// <summary>
        /// Gets the index of an index's right child.
        /// </summary>
        /// <param name="i">The index.</param>
        /// <returns>The index's right child.</returns>
        private int GetRightIndex(int i)
        {
            return 2 * i + 2;
        }

        /// <summary>
        /// Finds the index of a node. This is an O(n) operation that is used in order to support
        /// operations that are less than typical on binary heaps like Delete and DecreaseKey.
        /// </summary>
        /// <param name="node">The node to find the index of.</param>
        /// <returns>The index of the node.</returns>
        private int FindIndexOfNode(INode<TKey, TValue> node)
        {
            // Searching for the index of node is an O(n) operation
            var castedNode = (Node)node;
            var index = list.IndexOf(castedNode);
            if (index == _invalidIndex)
            {
                throw new ArgumentException("The node is not within the list");
            }
            return index;
        }

        /// <summary>
        /// Decreases a key of a node.
        /// </summary>
        /// </param name="node">The node to decrease the key of.</param>
        /// </param name="newKey">The new key to assign to the node.</param>
        public void DecreaseKey(INode<TKey, TValue> node, TKey newKey)
        {
            DecreaseKeyAt(FindIndexOfNode(node), newKey);
        }

        /// <summary>
        /// Decreases a key of a node.
        /// </summary>
        /// </param name="index">The index of the node to decrease the key of.</param>
        /// </param name="newKey">The new key to assign to the node.</param>
        private void DecreaseKeyAt(int index, TKey newKey)
        {
            if (list[index].Key.CompareTo(newKey) < 0)
                throw new ArgumentOutOfRangeException("New key is larger than old key.");
            list[index].Key = newKey;
            SiftUp(index);
        }

        /// <summary>
        /// Deletes a node.
        /// </summary>
        /// <param name="node">The node to delete.</param>
        public void Delete(INode<TKey, TValue> node)
        {
            DeleteAt(FindIndexOfNode(node));
        }

        /// <summary>
        /// Deletes a node.
        /// </summary>
        /// <param name="index">The index of the node to delete.</param>
        private void DeleteAt(int index)
        {
            if (list.Count == 1)
            {
                list.RemoveAt(0);
                return;
            }
            if (index == list.Count - 1)
            {
                list.RemoveAt(list.Count - 1);
                return;
            }
            Swap(index, list.Count - 1);
            list.RemoveAt(list.Count - 1);
            var parentIndex = GetParentIndex(index);
            if (index != 0 && list[index].CompareTo(list[parentIndex]) < 0)
            {
                SiftUp(index);
            }
            else
            {
                Heapify(index);
            } 
        }
        
        /// <summary>
        /// Recursively moves a node up the heap if it is less than its parent.
        /// </summary>
        /// <param name="index">The index of the node.</param>
        private void SiftUp(int index)
        {
            var parentIndex = GetParentIndex(index);
            while (parentIndex != _invalidIndex && list[index].CompareTo(list[parentIndex]) < 0) 
            {
                Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = GetParentIndex(index);
            }
        }

        /// <summary>
        /// Joins another heap to this heap.
        /// </summary>
        /// <param name="other">The other heap.</param>
        public void Union(IPriorityQueue<TKey, TValue> other)
        {
            var casted = (BinaryHeap<TKey, TValue>)other;
            list.AddRange(casted.list);
            BuildHeap();
        }

        /// <summary>
        /// Gets whether the heap is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return list.Count == 0; }
        }

        /// <summary>
        /// The size of the heap.
        /// </summary>
        public int Size 
        {
            get { return list.Count; }
        }

        /// <summary>
        /// A node object used to store data in the binary heap.
        /// </summary>
        public class Node : INode<TKey, TValue>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }

            /// <summary>
            /// Creates a binary heap node initialised with a key.
            /// </summary>
            /// <param name="key">The key to use.</param>
            /// <param name="val">The value to use.</param>
            public Node(TKey key, TValue val) 
            {
                Key = key;
                Value = val;
            }

            public int CompareTo(object other) 
            {
                var casted = other as Node;
                if (casted == null)
                    throw new NotImplementedException("Cannot compare to a non-Node object");
                return this.Key.CompareTo(casted.Key);
            }
        }
    }
}