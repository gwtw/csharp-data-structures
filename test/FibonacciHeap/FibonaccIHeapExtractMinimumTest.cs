using Xunit;

namespace GrowingWithTheWeb.DataStructures {
    public class FibonacciHeapExtractMinimumTest {
        [Fact]
        public void Consolidate8NodesIntoWellFormedOrder1TreeTest() {
            var heap = new FibonacciHeap<int, int>();
            var node0 = (FibonacciHeap<int, int>.Node)heap.Insert(0, 0);
            var node1 = (FibonacciHeap<int, int>.Node)heap.Insert(1, 0);
            var node2 = (FibonacciHeap<int, int>.Node)heap.Insert(2, 0);

            // Extracting minimum should trigger consolidate.
            //
            //               1
            //  0--1--2  ->  |
            //               2
            //
            Assert.Equal(heap.ExtractMinimum(), node0);
            Assert.Equal(heap.Size, 2);
            Assert.True(node1.Parent == null);
            Assert.True(node2.Parent == node1);
            Assert.True(node1.Next == node1);
            Assert.True(node2.Next == node2);
            Assert.True(node1.Child == node2);
            Assert.True(node2.Child == null);
        }

        [Fact]
        public void Consolidate8NodesIntoWellFormedOrder2TreeTest() {
            var heap = new FibonacciHeap<int, int>();
            var node0 = (FibonacciHeap<int, int>.Node)heap.Insert(0, 0);
            var node1 = (FibonacciHeap<int, int>.Node)heap.Insert(1, 0);
            var node2 = (FibonacciHeap<int, int>.Node)heap.Insert(2, 0);
            var node3 = (FibonacciHeap<int, int>.Node)heap.Insert(3, 0);
            var node4 = (FibonacciHeap<int, int>.Node)heap.Insert(4, 0);

            // Extracting minimum should trigger consolidate.
            //
            //                       1
            //                      /|
            //  0--1--2--3--4  ->  3 2
            //                     |
            //                     4
            //
            Assert.Equal(heap.ExtractMinimum(), node0);
            Assert.Equal(heap.Size, 4);
            Assert.True(node1.Parent == null);
            Assert.True(node2.Parent == node1);
            Assert.True(node3.Parent == node1);
            Assert.True(node4.Parent == node3);
            Assert.True(node1.Next == node1);
            Assert.True(node2.Next == node3);
            Assert.True(node3.Next == node2);
            Assert.True(node4.Next == node4);
            Assert.True(node1.Child == node2);
            Assert.True(node2.Child == null);
            Assert.True(node3.Child == node4);
            Assert.True(node4.Child == null);
        }

        [Fact]
        public void Consolidate8NodesIntoWellFormedOrder3TreeTest() {
            var heap = new FibonacciHeap<int, int>();
            var node0 = (FibonacciHeap<int, int>.Node)heap.Insert(0, 0);
            var node1 = (FibonacciHeap<int, int>.Node)heap.Insert(1, 0);
            var node2 = (FibonacciHeap<int, int>.Node)heap.Insert(2, 0);
            var node3 = (FibonacciHeap<int, int>.Node)heap.Insert(3, 0);
            var node4 = (FibonacciHeap<int, int>.Node)heap.Insert(4, 0);
            var node5 = (FibonacciHeap<int, int>.Node)heap.Insert(5, 0);
            var node6 = (FibonacciHeap<int, int>.Node)heap.Insert(6, 0);
            var node7 = (FibonacciHeap<int, int>.Node)heap.Insert(7, 0);
            var node8 = (FibonacciHeap<int, int>.Node)heap.Insert(8, 0);

            // Extracting minimum should trigger consolidate.
            //
            //                                 __1
            //                                / /|
            //                               5 3 2
            //  1--2--3--4--5--6--7--8  ->  /| |
            //                             7 6 4
            //                             |
            //                             8
            //
            Assert.Equal(heap.ExtractMinimum(), node0);
            Assert.True(node1.Parent == null);
            Assert.True(node2.Parent == node1);
            Assert.True(node3.Parent == node1);
            Assert.True(node4.Parent == node3);
            Assert.True(node5.Parent == node1);
            Assert.True(node6.Parent == node5);
            Assert.True(node7.Parent == node5);
            Assert.True(node8.Parent == node7);
            Assert.True(node1.Next == node1);
            Assert.True(node2.Next == node5);
            Assert.True(node3.Next == node2);
            Assert.True(node4.Next == node4);
            Assert.True(node5.Next == node3);
            Assert.True(node6.Next == node7);
            Assert.True(node7.Next == node6);
            Assert.True(node8.Next == node8);
            Assert.True(node1.Child == node2);
            Assert.True(node2.Child == null);
            Assert.True(node3.Child == node4);
            Assert.True(node4.Child == null);
            Assert.True(node5.Child == node6);
            Assert.True(node6.Child == null);
            Assert.True(node7.Child == node8);
            Assert.True(node8.Child == null);
        }
    }
}