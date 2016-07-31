using GrowingWithTheWeb.DataStructures;
using Xunit;

public class FibonacciHeapDecreaseKeyTest {
    [Fact]
    public void LeavesValidTreeOnFlatHeapTest() {
        var heap = new FibonacciHeap<int>();
        heap.Insert(13);
        heap.Insert(26);
        heap.Insert(3);
        heap.Insert(-6);
        heap.Insert(27);
        var node6 = heap.Insert(88);
        heap.Insert(59);
        heap.Insert(-10);
        heap.Insert(16);
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
        Assert.True(heap.IsEmpty());
    }
}
/*

test('should leave a valid tree on a consolidated Fibonacci heap', function (t) {
  var heap = new Heap();
  var node0 = heap.insert(0, null);
  var node1 = heap.insert(1, null);
  var node2 = heap.insert(2, null);
  var node3 = heap.insert(3, null);
  var node4 = heap.insert(4, null);
  var node5 = heap.insert(5, null);
  var node6 = heap.insert(6, null);
  var node7 = heap.insert(7, null);
  var node8 = heap.insert(8, null);

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
  t.is(heap.extractMinimum(), node0);

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
  heap.decreaseKey(node8, 0);
  t.true(node1.next === node8);

  t.is(heap.size(), 8);
  t.true(heap.extractMinimum() === node8);
  t.true(heap.extractMinimum() === node1);
  t.true(heap.extractMinimum() === node2);
  t.true(heap.extractMinimum() === node3);
  t.true(heap.extractMinimum() === node4);
  t.true(heap.extractMinimum() === node5);
  t.true(heap.extractMinimum() === node6);
  t.true(heap.extractMinimum() === node7);
  t.true(heap.isEmpty());
});
*/