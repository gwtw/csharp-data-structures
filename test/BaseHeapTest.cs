using System;
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
    }
}