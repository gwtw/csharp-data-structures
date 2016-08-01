namespace GrowingWithTheWeb.DataStructures {
    public class FibonacciHeapTest : BasePriorityQueueTest {
        public FibonacciHeapTest() : base(
                () => new FibonacciHeap<int, int>(),
                () => new FibonacciHeap<string, int>()) {
        }
    }
}