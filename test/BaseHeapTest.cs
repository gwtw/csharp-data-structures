using System;
using System.Collections.Generic;
using Xunit;

namespace GrowingWithTheWeb.DataStructures {
    public abstract class BaseHeapTest {
        protected Func<IPriorityQueue<int, int>> _integerHeapConstructor;
        protected Func<IPriorityQueue<string, int>> _stringHeapConstructor;

        public BaseHeapTest(
                Func<IPriorityQueue<int, int>> integerHeapConstructor,
                Func<IPriorityQueue<string, int>> stringHeapConstructor) {
            _integerHeapConstructor = integerHeapConstructor;
            _stringHeapConstructor = stringHeapConstructor;
        }

        [Fact]
        public void ClearSetHeapSizeTo0Test() {
            var heap = _integerHeapConstructor();
            heap.Insert(1, 0);
            heap.Insert(2, 0);
            heap.Insert(3, 0);
            heap.Clear();
            Assert.Equal(heap.Size, 0);
        }

        [Fact]
        public void ClearSetHeapsMinimumNodeToNull() {
            var heap = _integerHeapConstructor();
            heap.Insert(1, 0);
            heap.Insert(2, 0);
            heap.Insert(3, 0);
            heap.Clear();
            Assert.Equal(heap.FindMinimum(), null);
        }

        [Fact]
        public void KeysRetainValueTest() {
            var heap = _integerHeapConstructor();
            heap.Insert(1, 3);
            heap.Insert(2, 2);
            heap.Insert(3, 1);
            Assert.Equal(heap.ExtractMinimum().Value, 3);
            Assert.Equal(heap.ExtractMinimum().Value, 2);
            Assert.Equal(heap.ExtractMinimum().Value, 1);
        }

        [Fact]
        public void StringTypedHeapTest() {
            var heap = _stringHeapConstructor();
            var node3 = heap.Insert("c", 0);
            var node4 = heap.Insert("d", 0);
            var node2 = heap.Insert("b", 0);
            var node1 = heap.Insert("a", 0);
            var node5 = heap.Insert("e", 0);
            Assert.Equal(heap.Size, 5);
            Assert.Equal(heap.ExtractMinimum().Key, node1.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node2.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node3.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node4.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node5.Key);
            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void DecreaseKeyThrowsExceptionOnNonExistentNodeTest() {
            var heap = _integerHeapConstructor();
            Assert.ThrowsAny<ArgumentException>(() => heap.DecreaseKey(null, 2));
        }

        [Fact]
        public void DecreaseKeyThrowsExceptionWithInvalidNewKeyTest() {
            var heap = _integerHeapConstructor();
            var node = heap.Insert(1, 0);
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() => heap.DecreaseKey(node, 2));
        }

        [Fact]
        public void DecreaseKeyDecreasesMinimumNodeTest() {
            var heap = _integerHeapConstructor();
            var node1 = heap.Insert(1, 0);
            heap.Insert(2, 0);
            heap.DecreaseKey(node1, -3);
            var key = heap.FindMinimum().Key;
            Assert.Equal(key, node1.Key);
            Assert.Equal(key, -3);
        }

        [Fact]
        public void DecreaseKeyDecreasesAndBubbledUpMinimumNodeTest() {
            var heap = _integerHeapConstructor();
            heap.Insert(1, 0);
            var node2 = heap.Insert(2, 0);
            heap.DecreaseKey(node2, -3);
            var key = heap.FindMinimum().Key;
            Assert.Equal(key, node2.Key);
            Assert.Equal(key, -3);
        }

        [Fact]
        public void DecreaseKeyDecreasesAndBubbledUpNonMinimumNodeInLargeHeapTest() {
            var heap = _integerHeapConstructor();
            heap.Insert(13, 0);
            heap.Insert(26, 0);
            heap.Insert(3, 0);
            heap.Insert(-6, 0);
            var node5 = heap.Insert(27, 0);
            heap.Insert(88, 0);
            heap.Insert(59, 0);
            heap.Insert(-10, 0);
            heap.Insert(16, 0);
            heap.DecreaseKey(node5, -11);
            Assert.Equal(heap.FindMinimum().Key, node5.Key);
        }

        [Fact]
        public void DeleteHeadOfHeapTest() {
            var heap = _integerHeapConstructor();
            var node1 = heap.Insert(1, 0);
            var node2 = heap.Insert(2, 0);
            heap.Delete(node1);
            Assert.Equal(heap.ExtractMinimum(), node2);
            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void DeleteNodesWithMultipleElementsTest() {
            var heap = _integerHeapConstructor();
            var node3 = heap.Insert(13, 0);
            var node4 = heap.Insert(26, 0);
            var node2 = heap.Insert(3, 0);
            var node1 = heap.Insert(-6, 0);
            var node5 = heap.Insert(27, 0);
            Assert.Equal(heap.Size, 5);
            Assert.Equal(heap.ExtractMinimum(), node1);
            Assert.Equal(heap.ExtractMinimum(), node2);
            Assert.Equal(heap.ExtractMinimum(), node3);
            Assert.Equal(heap.ExtractMinimum(), node4);
            Assert.Equal(heap.ExtractMinimum(), node5);
            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void ExtractMinimumReturnNullOnEmptyHeapTest() {
            var heap = _integerHeapConstructor();
            Assert.Equal(heap.ExtractMinimum(), null);
        }

        [Fact]
        public void ExtractMinimumExtractsMinimumTest() {
            var heap = _integerHeapConstructor();
            var node5 = heap.Insert(5, 0);
            var node3 = heap.Insert(3, 0);
            var node4 = heap.Insert(4, 0);
            var node1 = heap.Insert(1, 0);
            var node2 = heap.Insert(2, 0);
            Assert.Equal(heap.ExtractMinimum().Key, node1.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node2.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node3.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node4.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node5.Key);
        }

        [Fact]
        public void ExtractMinimumExtractsMinimumFromJumbledHeapTest() {
            var heap = _integerHeapConstructor();
            var node1 = heap.Insert(1, 0);
            var node4 = heap.Insert(4, 0);
            var node3 = heap.Insert(3, 0);
            var node5 = heap.Insert(5, 0);
            var node2 = heap.Insert(2, 0);
            Assert.Equal(heap.ExtractMinimum().Key, node1.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node2.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node3.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node4.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node5.Key);
        }

        [Fact]
        public void ExtractMinimumExtractsMinimumFromHeapWithNegativeKeysTest() {
            var heap = _integerHeapConstructor();
            var node1 = heap.Insert(-9, 0);
            var node4 = heap.Insert(6, 0);
            var node3 = heap.Insert(3, 0);
            var node5 = heap.Insert(10, 0);
            var node2 = heap.Insert(-4, 0);
            Assert.Equal(heap.ExtractMinimum().Key, node1.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node2.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node3.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node4.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node5.Key);
        }

        [Fact]
        public void ExtractMinimumReturnsMinimumTest() {
            var heap = _integerHeapConstructor();
            heap.Insert(5, 0);
            heap.Insert(3, 0);
            heap.Insert(1, 0);
            heap.Insert(4, 0);
            heap.Insert(2, 0);
            Assert.Equal(heap.FindMinimum().Key, 1);
        }

        [Fact]
        public void InsertItemsIntoHeapTest() {
            var heap = _integerHeapConstructor();
            heap.Insert(1, 0);
            heap.Insert(2, 0);
            heap.Insert(3, 0);
            heap.Insert(4, 0);
            heap.Insert(5, 0);
            Assert.Equal(heap.Size, 5);
        }

        [Fact]
        public void InsertReturnsInsertedNodeTest() {
            var heap = _integerHeapConstructor();
            var ret = heap.Insert(1, 10);
            Assert.Equal(ret.Key, 1);
            Assert.Equal(ret.Value, 10);
        }

        [Fact]
        public void InsertDuplicateKeysTest() {
            var heap = _integerHeapConstructor();
            heap.Insert(1, 0);
            heap.Insert(1, 0);
            Assert.Equal(1, heap.ExtractMinimum().Key);
            Assert.Equal(1, heap.ExtractMinimum().Key);
        }

        [Fact]
        public void IntegrationStringKeysTest() {
            var heap = _stringHeapConstructor();
            var node3 = heap.Insert("f", 0);
            var node4 = heap.Insert("o", 0);
            var node2 = heap.Insert("c", 0);
            var node1 = heap.Insert("a", 0);
            var node5 = heap.Insert("q", 0);
            Assert.Equal(heap.Size, 5);
            Assert.Equal(heap.ExtractMinimum().Key, node1.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node2.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node3.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node4.Key);
            Assert.Equal(heap.ExtractMinimum().Key, node5.Key);
            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void IntegrationGiveEmptyHeapAfterInsertingAndExtracting1000InOrderItemsTest() {
            var heap = _integerHeapConstructor();
            for (var i = 0; i < 1000; i++) {
                heap.Insert(i, i);
            }
            for (var i = 0; i < 1000; i++) {
                heap.ExtractMinimum();
            }
            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void IntegrationGiveEmptyHeapAfterInsertingAndExtracting1000ReverseItemsTest() {
            var heap = _integerHeapConstructor();
            for (var i = 999; i >= 0; i--) {
                heap.Insert(i, i);
            }
            for (var i = 0; i < 1000; i++) {
                heap.ExtractMinimum();
            }
            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void IntegrationGiveEmptyHeapAfterInsertingAndExtracting1000PseudoRandomItemsTest() {
            var heap = _integerHeapConstructor();
            for (var i = 0; i < 1000; i++) {
                if (i % 2 == 0) {
                    heap.Insert(i, i);
                } else {
                    heap.Insert(999 - i, 999 - i);
                }
            }
            for (var i = 0; i < 1000; i++) {
                heap.ExtractMinimum();
            }
            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void IntegrationHandle1000ShuffledElementsTest() {
            var heap = _integerHeapConstructor();
            var input = new List<int>();
            for (var i = 0; i < 1000; i++) {
                input.Add(i);
            }
            // shuffle
            var random = new Random();
            for (var i = 0; i < 1000; i++) {
                var swapWith = random.Next(1000);
                var temp = input[i];
                input[i] = input[swapWith];
                input[swapWith] = temp;
            }
            // insert
            for (var i = 0; i < 1000; i++) {
                heap.Insert(input[i], 0);
            }
            // extract
            var output = new List<int>();
            var errorReported = false;
            var counter = 0;
            while (!heap.IsEmpty) {
                output.Add(heap.ExtractMinimum().Key);
                if (!errorReported && counter != output[output.Count - 1]) {
                    Assert.True(false, "the heap property was not maintained (elements in order 0, 1, 2, ..., 997, 998, 999)");
                }
                counter++;
            }
            Assert.Equal(output.Count, 1000);
        }

        [Fact]
        public void IsEmptyTest() {
            var heap = _integerHeapConstructor();
            Assert.True(heap.IsEmpty);
            heap.Insert(1, 0);
            Assert.False(heap.IsEmpty);
            heap.ExtractMinimum();
            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void SizeTest() {
            var heap = _integerHeapConstructor();
            Assert.Equal(heap.Size, 0);
            heap.Insert(1, 0);
            Assert.Equal(heap.Size, 1);
            heap.Insert(2, 0);
            Assert.Equal(heap.Size, 2);
            heap.Insert(3, 0);
            Assert.Equal(heap.Size, 3);
            heap.Insert(4, 0);
            Assert.Equal(heap.Size, 4);
            heap.Insert(5, 0);
            Assert.Equal(heap.Size, 5);
            heap.Insert(6, 0);
            Assert.Equal(heap.Size, 6);
            heap.Insert(7, 0);
            Assert.Equal(heap.Size, 7);
            heap.Insert(8, 0);
            Assert.Equal(heap.Size, 8);
            heap.Insert(9, 0);
            Assert.Equal(heap.Size, 9);
            heap.Insert(10, 0);
            Assert.Equal(heap.Size, 10);
        }

        [Fact]
        public void Union2HeapsTogetherWithOverlappingElementsTest() {
            var heap = _integerHeapConstructor();
            heap.Insert(0, 0);
            heap.Insert(2, 0);
            heap.Insert(4, 0);
            heap.Insert(6, 0);
            heap.Insert(8, 0);
            var other = _integerHeapConstructor();
            other.Insert(1, 0);
            other.Insert(3, 0);
            other.Insert(5, 0);
            other.Insert(7, 0);
            other.Insert(9, 0);
            Assert.Equal(heap.Size, 5);
            Assert.Equal(other.Size, 5);

            heap.Union((FibonacciHeap<int, int>)other);
            Assert.Equal(heap.Size, 10);
            for (var i = 0; i < 10; i++) {
                Assert.Equal(heap.ExtractMinimum().Key, i);
            }
            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void Union2HeapsTogetherWithOverlappingElementsReverseTest() {
            var heap = _integerHeapConstructor();
            heap.Insert(9, 0);
            heap.Insert(7, 0);
            heap.Insert(5, 0);
            heap.Insert(3, 0);
            heap.Insert(1, 0);
            var other = _integerHeapConstructor();
            other.Insert(8, 0);
            other.Insert(6, 0);
            other.Insert(4, 0);
            other.Insert(2, 0);
            other.Insert(0, 0);
            Assert.Equal(heap.Size, 5);
            Assert.Equal(other.Size, 5);

            heap.Union((FibonacciHeap<int, int>)other);
            Assert.Equal(heap.Size, 10);
            for (var i = 0; i < 10; i++) {
                Assert.Equal(heap.ExtractMinimum().Key, i);
            }
            Assert.True(heap.IsEmpty);
        }

        [Fact]
        public void Union2HeapsTest() {
            var first = _integerHeapConstructor();
            first.Insert(9, 0);
            first.Insert(2, 0);
            first.Insert(6, 0);
            first.Insert(1, 0);
            first.Insert(3, 0);
            Assert.Equal(first.Size, 5);
            var second = _integerHeapConstructor();
            second.Insert(4, 0);
            second.Insert(8, 0);
            second.Insert(5, 0);
            second.Insert(7, 0);
            second.Insert(0, 0);
            Assert.Equal(second.Size, 5);
            first.Union((FibonacciHeap<int, int>)second);
            Assert.Equal(first.Size, 10);
            for (var i = 0; i < 10; i++) {
                Assert.Equal(first.ExtractMinimum().Key, i);
            }
            Assert.True(first.IsEmpty);
        }

        [Fact]
        public void Union2HeapsAfterExtractingMinFromEachTest() {
            var first = _integerHeapConstructor();
            first.Insert(9, 0);
            first.Insert(2, 0);
            first.Insert(6, 0);
            first.Insert(1, 0);
            first.Insert(3, 0);
            Assert.Equal(first.Size, 5);
            var second = _integerHeapConstructor();
            second.Insert(4, 0);
            second.Insert(8, 0);
            second.Insert(5, 0);
            second.Insert(7, 0);
            second.Insert(0, 0);
            Assert.Equal(second.Size, 5);
            Assert.Equal(first.ExtractMinimum().Key, 1);
            Assert.Equal(second.ExtractMinimum().Key, 0);
            first.Union((FibonacciHeap<int, int>)second);
            Assert.Equal(first.Size, 8);
            for (var i = 2; i < 10; i++) {
                Assert.Equal(first.ExtractMinimum().Key, i);
            }
            Assert.True(first.IsEmpty);
        }
    }
}