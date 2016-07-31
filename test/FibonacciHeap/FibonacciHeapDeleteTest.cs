using Xunit;

namespace GrowingWithTheWeb.DataStructures {
    public class FibonacciHeapDeleteTest {
        [Fact]
        public void DeleteNoesInFlatHeapTest() {
            var heap = new FibonacciHeap<int>();
            var node3 = heap.Insert(13);
            var node4 = heap.Insert(26);
            var node2 = heap.Insert(3);
            var node1 = heap.Insert(-6);
            var node5 = heap.Insert(27);
            Assert.Equal(heap.Size, 5);
            heap.Delete(node3);
            Assert.Equal(heap.Size, 4);
            Assert.Equal(heap.ExtractMinimum(), node1);
            Assert.Equal(heap.ExtractMinimum(), node2);
            Assert.Equal(heap.ExtractMinimum(), node4);
            Assert.Equal(heap.ExtractMinimum(), node5);
            Assert.True(heap.IsEmpty());
        }

        [Fact]
        public void CutNodeFromTreeIfNotMinimumAndNoGrandparentTest() {
            var heap = new FibonacciHeap<int>();
            var node1 = heap.Insert(1);
            var node2 = heap.Insert(2);
            var node3 = heap.Insert(3);
            var node4 = heap.Insert(4);
            // Extract the minimum, forcing the construction of an order 2 tree which
            // is changed to an order 0 and order 1 tree after the minimum is extracted.
            //
            //                    1
            //                   /|      3--2
            //  1--2--3--4  ->  3 2  ->  |
            //                  |        4
            //                  4
            //
            Assert.Equal(heap.ExtractMinimum(), node1);
            // Deleting the node should trigger a cut and cascadingCut on the heap.
            heap.Delete(node4);

            Assert.Equal(heap.Size, 2);
            Assert.Equal(heap.ExtractMinimum(), node2);
            Assert.Equal(heap.ExtractMinimum(), node3);
            Assert.True(heap.IsEmpty());
        }

        [Fact]
        public void CutNodeFromTreeIfNotMinimumAndHasGrandparentTest() {
            var heap = new FibonacciHeap<int>();
            var node0 = heap.Insert(0);
            var node1 = heap.Insert(1);
            var node2 = heap.Insert(2);
            var node3 = heap.Insert(3);
            var node4 = heap.Insert(4);
            var node5 = heap.Insert(5);
            var node6 = heap.Insert(6);
            var node7 = heap.Insert(7);
            var node8 = heap.Insert(8);

            // extractMinimum on 0 should trigger a cut and cascadingCut on the heap.
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

            // Delete node 8
            //
            //      __1
            //     / /|        __1
            //    5 3 2       / /|
            //   /| |    ->  5 3 2
            //  7 6 4       /| |
            //  |          7 6 4
            //  8
            //
            heap.Delete(node8);

            Assert.Equal(heap.Size, 7);
            Assert.Equal(heap.ExtractMinimum(), node1);
            Assert.Equal(heap.ExtractMinimum(), node2);
            Assert.Equal(heap.ExtractMinimum(), node3);
            Assert.Equal(heap.ExtractMinimum(), node4);
            Assert.Equal(heap.ExtractMinimum(), node5);
            Assert.Equal(heap.ExtractMinimum(), node6);
            Assert.Equal(heap.ExtractMinimum(), node7);
            Assert.True(heap.IsEmpty());
        }

        [Fact]
        public void CutNodeFromTreeIfNotMinimumAndHasGrandparentWithMarkedParentTest() {
            var heap = new FibonacciHeap<int>();
            var node0 = heap.Insert(0);
            var node1 = heap.Insert(1);
            var node2 = heap.Insert(2);
            var node3 = heap.Insert(3);
            var node4 = heap.Insert(4);
            var node5 = heap.Insert(5);
            var node6 = heap.Insert(6);
            var node7 = heap.Insert(7);
            var node8 = heap.Insert(8);

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

            // Delete node 6, marking 5
            //
            //      __1         __1
            //     / /|        / /|
            //    5 3 2      >5 3 2
            //   /| |    ->  /  |
            //  7 6 4       7   4
            //  |           |
            //  8           8
            //
            heap.Delete(node6);
            Assert.True(node5.IsMarked);

            // Delete node 7, cutting the sub-tree
            //
            //      __1
            //     / /|        1--5
            //   >5 3 2       /|  |
            //   /  |    ->  3 2  8
            //  7   4        |
            //  |            4
            //  8
            //
            heap.Delete(node7);
            Assert.True(node5.Next == node1);
            Assert.True(node2.Next == node3);
            Assert.True(node3.Next == node2);

            Assert.Equal(heap.Size, 6);
            Assert.True(heap.ExtractMinimum() == node1);
            Assert.True(heap.ExtractMinimum() == node2);
            Assert.True(heap.ExtractMinimum() == node3);
            Assert.True(heap.ExtractMinimum() == node4);
            Assert.True(heap.ExtractMinimum() == node5);
            Assert.True(heap.ExtractMinimum() == node8);
            Assert.True(heap.IsEmpty());
        }

        [Fact]
        public void CorrectlyAssignIndirectChildWhenDirectChildIsCutFromParentTest() {
            var heap = new FibonacciHeap<int>();
            var node0 = heap.Insert(0);
            heap.Insert(1);
            heap.Insert(2);
            heap.Insert(3);
            heap.Insert(4);
            var node5 = heap.Insert(5);
            var node6 = heap.Insert(6);
            var node7 = heap.Insert(7);
            heap.Insert(8);

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

            // Delete node 6, marking 5
            //
            //      __1         __1
            //     / /|        / /|
            //    5 3 2      >5 3 2
            //   /| |    ->  /  |
            //  7 6 4       7   4
            //  |           |
            //  8           8
            //
            heap.Delete(node6);
            Assert.True(node5.Child == node7);
        }
    }
}