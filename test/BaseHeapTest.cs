using System;
using Xunit;

namespace GrowingWithTheWeb.DataStructures {
    public abstract class BaseHeapTest {
        protected Func<IHeap<int>> _integerHeapConstructor;

        public BaseHeapTest(Func<IHeap<int>> integerHeapConstructor) {
            _integerHeapConstructor = integerHeapConstructor;
        }

        [Fact]
        public void ClearSetHeapSizeTo0Test() {
            var heap = _integerHeapConstructor();
            heap.Insert(1);
            heap.Insert(2);
            heap.Insert(3);
            heap.Clear();
            Assert.Equal(heap.Size, 0);
        }

        [Fact]
        public void ClearSetHeapsMinimumNodeToNull() {
            var heap = _integerHeapConstructor();
            heap.Insert(1);
            heap.Insert(2);
            heap.Insert(3);
            heap.Clear();
            Assert.Equal(heap.FindMinimum(), null);
        }
    }
}