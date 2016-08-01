using Xunit;

namespace GrowingWithTheWeb.DataStructures {
    public class FibonacciHeapTest : BaseHeapTest {
        public FibonacciHeapTest() : base(() => new FibonacciHeap<int>()) {

        }
    }
}