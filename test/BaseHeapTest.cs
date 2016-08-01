using System;
using Xunit;

namespace GrowingWithTheWeb.DataStructures {
    public abstract class BaseHeapTest {
        protected Func<IPriorityQueue<int, int>> _integerHeapConstructor;

        public BaseHeapTest(Func<IPriorityQueue<int, int>> integerHeapConstructor) {
            _integerHeapConstructor = integerHeapConstructor;
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
    }
}