namespace TylerDM.StandardLibrary.Collections;

public class FixedQueueTests
{
	[Fact]
	public void Test()
	{
		const int capacity = 5;

		var q = new FixedQueue<int>(capacity);
		Assert.Equal(capacity, q.Capacity);
		Assert.True(q.Empty);

		q.Enqueue(1);
		Assert.Single(q);

		q.Enqueue(2);
		q.Enqueue(3);
		q.Enqueue(4);
		q.Enqueue(5);
		Assert.True(q.Full);

		q.Enqueue(6);
		Assert.DoesNotContain(1, q);

		var dequeued = q.Dequeue();
		Assert.Equal(2, dequeued);
		Assert.False(q.Full);
	}
}
