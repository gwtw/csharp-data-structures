using System;
using System.Collections.Generic;

namespace GrowingWithTheWeb.DataStructures {
    public class FibonacciHeap<T> where T : IComparable {

        private Node _minNode;
        public int Size { get; private set; }

        public FibonacciHeap() {
            _minNode = null;
            Size = 0;
        }

        private FibonacciHeap(Node node) {
            _minNode = node;
            Size = 1;
        }

        private FibonacciHeap(Node minNode, int size) {
            this._minNode = minNode;
            this.Size = size;
        }

        public void Clear() {
            _minNode = null;
            Size = 0;
        }

        public Node Insert(T key) {
            Node node = new Node(key);
            _minNode = MergeLists(_minNode, node);
            Size++;
            return node;
        }

        public Node FindMinimum() {
            return _minNode;
        }

        public void DecreaseKey(Node node, T newKey) {
            if (newKey.CompareTo(node.Key) > 0) {
                throw new ArgumentOutOfRangeException("New key is larger than old key.");
            }

            node.Key = newKey;
            Node parent = node.Parent;
            if (parent != null && node.CompareTo(parent) < 0) {
                Cut(node, parent);
                CascadingCut(parent);
            }
            if (node.CompareTo(_minNode) < 0) {
                _minNode = node;
            }
        }

        /// <summary>
        /// Cut the link between a node and its parent, moving the node to the root list.
        /// </summary>

        private void Cut(Node node, Node parent) {
            parent.Degree--;
            parent.Child = (node.Next == node ? null : node.Next);
            RemoveNodeFromList(node);
            MergeLists(_minNode, node);
            node.IsMarked = false;
        }

        /// <summary>
        /// Perform a cascading cut on a node; mark the node if it is not marked, otherwise cut the
        /// node and perform a cascading cut on its parent.
        /// </summary>
        private void CascadingCut(Node node) {
            Node parent = node.Parent;
            if (parent != null) {
                if (node.IsMarked) {
                    Cut(node, parent);
                    CascadingCut(parent);
                } else {
                    node.IsMarked = true;
                }
            }
        }

        public void Delete(Node node) {
            // This is a special implementation of decreaseKey that sets the
            // argument to the minimum value. This is necessary to make generic keys
            // work, since there is no MIN_VALUE constant for generic types.
            Node parent = node.Parent;
            if (parent != null) {
                Cut(node, parent);
                CascadingCut(parent);
            }
            _minNode = node;

            ExtractMinimum();
        }

        public Node ExtractMinimum() {
            Node extractedMin = _minNode;
            if (extractedMin != null) {
                // Set parent to null for the minimum's children
                if (extractedMin.Child != null) {
                    Node child = extractedMin.Child;
                    do {
                        child.Parent = null;
                        child = child.Next;
                    } while (child != extractedMin.Child);
                }

                Node nextInRootList = extractedMin.Next == extractedMin ? null : extractedMin.Next;

                // Remove min from root list
                RemoveNodeFromList(extractedMin);
                Size--;

                // Merge the children of the minimum node with the root list
                _minNode = MergeLists(nextInRootList, extractedMin.Child);

                if (nextInRootList != null) {
                    _minNode = nextInRootList;
                    Consolidate();
                }
            }
            return extractedMin;
        }

        private void Consolidate() {
            IList<Node> aux = new List<Node>();

            // TODO: Pull out into helper function
            var start = _minNode;
            var items = new Queue<Node>();
            if (start == null) {
                return;
            }

            Node current2 = start;
            do {
                items.Enqueue(current2);
                current2 = current2.Next;
            } while (start != current2);

            foreach (var current in items) {
                //Node current = it.next();
                var top = current;

                while (aux.Count <= top.Degree + 1) {
                    aux.Add(null);
                }

                // If there exists another node with the same degree, merge them
                while (aux[top.Degree] != null) {
                    if (top.Key.CompareTo(aux[top.Degree].Key) > 0) {
                        Node temp = top;
                        top = aux[top.Degree];
                        aux[top.Degree] = temp;
                    }
                    LinkHeaps(aux[top.Degree], top);
                    aux[top.Degree] = null;
                    top.Degree++;
                }

                while (aux.Count <= top.Degree + 1) {
                    aux.Add(null);
                }
                aux[top.Degree] = top;
            }

            _minNode = null;
            for (int i = 0; i < aux.Count; i++) {
                if (aux[i] != null) {
                    // Remove siblings before merging
                    aux[i].Next = aux[i];
                    aux[i].Prev = aux[i];
                    _minNode = MergeLists(_minNode, aux[i]);
                }
            }
        }

        private void RemoveNodeFromList(Node node) {
            Node prev = node.Prev;
            Node next = node.Next;
            prev.Next = next;
            next.Prev = prev;

            node.Next = node;
            node.Prev = node;
        }

        /// <summary>
        /// Links two heaps of the same order together.
        /// </summary>
        private void LinkHeaps(Node max, Node min) {
            RemoveNodeFromList(max);
            min.Child = MergeLists(max, min.Child);
            max.Parent = min;
            max.IsMarked = false;
        }

        // Union another fibonacci heap with this one
        public void Union(FibonacciHeap<T> other) {
            _minNode = MergeLists(_minNode, other._minNode);
            Size += other.Size;
        }

        // Merges two lists and returns the minimum node
        private Node MergeLists(Node a, Node b) {
            if (a == null && b == null) {
                return null;
            }
            if (a == null) {
                return b;
            }
            if (b == null) {
                return a;
            }

            Node temp = a.Next;
            a.Next = b.Next;
            a.Next.Prev = a;
            b.Next = temp;
            b.Next.Prev = b;

            return a.CompareTo(b) < 0 ? a : b;
        }

        public bool IsEmpty {
            get {
                return _minNode == null;
            }
        }

        public class Node : IComparable {
            public T Key { get; set; }
            public int Degree { get; set; }
            public Node Parent { get; set; }
            public Node Child { get; set; }
            public Node Prev { get; set; }
            public Node Next { get; set; }
            public bool IsMarked { get; set; } 

            public Node() {
                Key = default(T);
            }

            public Node(T key) {
                Key = key;
                Next = this;
                Prev = this;
            }

            public int CompareTo(object other) {
                var casted = other as Node;
                if (casted == null) {
                    throw new NotImplementedException("Cannot compare to a non-Node object");
                }
                return this.Key.CompareTo(casted.Key);
            }
        }
    }
}