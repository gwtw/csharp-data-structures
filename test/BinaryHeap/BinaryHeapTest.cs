namespace GrowingWithTheWeb.DataStructures {
    public class BinaryHeapTest : BasePriorityQueueTest {
        public BinaryHeapTest() : base(
                () => new BinaryHeap<int, int>(),
                () => new BinaryHeap<string, int>()) {
        }
    }
}