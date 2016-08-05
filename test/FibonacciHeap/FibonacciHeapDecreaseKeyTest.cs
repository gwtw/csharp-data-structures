using Xunit;

namespace GrowingWithTheWeb.DataStructures {
    public class FibonacciHeapDecreaseKeyTest {
        [Fact]
        public void LeavesValidTreeOnFlatHeapTest() {
            var heap = new FibonacciHeap<int, int>();
            heap.Insert(13, 0);
            heap.Insert(26, 0);
            heap.Insert(3, 0);
            heap.Insert(-6, 0);
            heap.Insert(27, 0);
            var node6 = heap.Insert(88, 0);
            heap.Insert(59, 0);
            heap.Insert(-10, 0);
            heap.Insert(16, 0);
            heap.DecreaseKey(node6, -8);
            Assert.Equal(heap.ExtractMinimum().Key, -10);
            Assert.Equal(heap.ExtractMinimum().Key, -8);
            Assert.Equal(heap.ExtractMinimum().Key, -6);
            Assert.Equal(heap.ExtractMinimum().Key, 3);
            Assert.Equal(heap.ExtractMinimum().Key, 13);
            Assert.Equal(heap.ExtractMinimum().Key, 16);
            Assert.Equal(heap.ExtractMinimum().Key, 26);
            Assert.Equal(heap.ExtractMinimum().Key, 27);
            Assert.Equal(heap.ExtractMinimum().Key, 59);
        }

        [Fact]
        public void LeavesValidTreeOnConsolidatedHeapTest() {
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
            //                                    __1
            //                                   / /|
            //                                  5 3 2
            //  0--1--2--3--4--5--6--7--8  ->  /| |
            //                                7 6 4
            //                                |
            //                                8
            //
            Assert.Equal(heap.ExtractMinimum(), node0);

            // Decrease node 8 to 0
            //
            //      __1
            //     / /|        __1--0
            //    5 3 2       / /|
            //   /| |    ->  5 3 2
            //  7 6 4       /| |
            //  |          7 6 4
            //  8
            //
            heap.DecreaseKey(node8, 0);
            Assert.True(node1.Next == node8);

            Assert.Equal(heap.Size, 8);
            Assert.True(heap.ExtractMinimum() == node8);
            Assert.True(heap.ExtractMinimum() == node1);
            Assert.True(heap.ExtractMinimum() == node2);
            Assert.True(heap.ExtractMinimum() == node3);
            Assert.True(heap.ExtractMinimum() == node4);
            Assert.True(heap.ExtractMinimum() == node5);
            Assert.True(heap.ExtractMinimum() == node6);
            Assert.True(heap.ExtractMinimum() == node7);
            Assert.True(heap.IsEmpty);
        }
    }
}