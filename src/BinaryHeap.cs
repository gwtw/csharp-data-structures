using System;
using System.Collections.Generic;

namespace GrowingWithTheWeb.DataStructures {
    public class BinaryHeap<TKey, TValue> : IPriorityQueue<TKey, TValue>
        where TKey : System.IComparable
    {
        /// <summary>Represents an invalid index that comes from GetParentIndex.</summary>
        private const int _invalidIndex = -1;

        /**
        * The heap's data.
        */
        private IList<Node> list;

        /**
        * Creates a new {@link BinaryHeap}.
        */
        public BinaryHeap() : this(0) 
        {
        }

        /**
        * Creates a new {@link BinaryHeap}.
        *
        * @param size The expected maximum size of the heap. this value reduces the number of
        * reallocations to the backing {@link ArrayList}.
        */
        public BinaryHeap(int size) 
        {
            list = new List<Node>(size);
        }

        /**
        * Creates a new {@link BinaryHeap}.
        *
        * @param items An {@link ArrayList} to use as the backing array.
        */
        public BinaryHeap(List<Node> items)
        {
            list = items;
            BuildHeap();
        }

        public INode<TKey, TValue> Insert(TKey key, TValue val) 
        {
            int i = list.Count;
            var node = new Node(key, val); 
            list.Add(node);
            int parent = GetParentIndex(i);
            while (parent != -1 && list[i].CompareTo(list[parent]) < 0) 
            {
                Swap(i, parent);
                i = parent;
                parent = GetParentIndex(i);
            }
            return node;
        }

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

        public INode<TKey, TValue> FindMinimum() 
        {
            if (list.Count == 0)
                return null;
            return list[0];
        }

        public void Clear() 
        {
            list.Clear();
        }

        private void BuildHeap() 
        {
            for (int i = (int)(list.Count / 2); i >= 0; i--) 
                Heapify(i);
        }

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

        private void Swap(int i1, int i2)
        {
            Node temp = list[i1];
            list[i1] = list[i2];
            list[i2] = temp;
        }

        private int GetParentIndex(int i)
        {
            if (i == 0) 
                return _invalidIndex;
            return (i - 1) / 2;
        }

        private int GetLeftIndex(int i)
        {
            return 2 * i + 1;
        }

        private int GetRightIndex(int i)
        {
            return 2 * i + 2;
        }

        public void DecreaseKey(INode<TKey, TValue> node, TKey newKey)
        {
            throw new NotImplementedException();
        }

        public void Delete(INode<TKey, TValue> node)
        {
            // Searching for the index of node is an O(n) operation
            var castedNode = (Node)node;
            var index = list.IndexOf(castedNode);
            if (index == _invalidIndex)
            {
                throw new ArgumentException("The node is not within the list");
            }
            DeleteAt(index);
        }

        public void DeleteAt(int index)
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
                FilterUp(index);
            }
            else
            {
                FilterDown(index);
            } 
        }
        
        private void FilterUp(int index)
        {
            var parentIndex = GetParentIndex(index);
            while (parentIndex != _invalidIndex && list[index].CompareTo(list[parentIndex]) < 0) 
            {
                Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = GetParentIndex(index);
            }
        }
        
        private void FilterDown(int index)
        {
            var leftIndex = GetLeftIndex(index);
            var rightIndex = GetRightIndex(index);
            while (leftIndex < list.Count && list[index].CompareTo(list[leftIndex]) > 0 ||
                rightIndex < list.Count && list[index].CompareTo(list[rightIndex]) > 0) 
            {
                var minValueIndex = leftIndex;
                if (list.Count != rightIndex && list[rightIndex].CompareTo(list[leftIndex]) < 0)
                    minValueIndex = rightIndex;
                Swap(index, minValueIndex);
                index = minValueIndex;
                leftIndex = GetLeftIndex(index);
                rightIndex = GetRightIndex(index);
            }
        }

        public void Union(IPriorityQueue<TKey, TValue> other)
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty
        {
            get { return list.Count == 0; }
        }

        public int Size 
        {
            get { return list.Count; }
        }

        public class Node : INode<TKey, TValue>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }

            /// <summary>
            /// Creates a Fibonacci heap node initialised with a key.
            /// </summary>
            /// <param name="key">The key to use.</param>
            public Node(TKey key, TValue value) 
            {
                Key = key;
                Value = value;
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