namespace TylerDM.StandardLibrary.System;

public class ActOncePerTests
{
	[Fact]
	public void Act()
	{
		var acts = 0;
		var actOncePer = new ActOncePer<int>(x => acts++);
		actOncePer.Act(1);
		actOncePer.Act(2);
		actOncePer.Act(3);
		Assert.Equal(3, acts);
		actOncePer.Act(1);
		Assert.Equal(3, acts);
	}

	[Fact]
	public async Task ActAsync()
	{
		var acts = 0;
		var actOncePer = new ActOncePerAsync<int>(x => Task.FromResult(acts++));
		await actOncePer.ActAsync(1);
		await actOncePer.ActAsync(2);
		await actOncePer.ActAsync(3);
		Assert.Equal(3, acts);
		await actOncePer.ActAsync(1);
		Assert.Equal(3, acts);
	}

	[Fact]
	public void ActWithReturn()
	{
		var acts = 0;
		var actOncePer = new ActOncePer<int, int>(x => { acts++; return x; });
		Assert.Equal(1, actOncePer.Act(1));
		Assert.Equal(2, actOncePer.Act(2));
		Assert.Equal(3, actOncePer.Act(3));
		Assert.Equal(3, acts);
		Assert.Equal(default, actOncePer.Act(1));
		Assert.Equal(3, acts);
	}

	[Fact]
	public void TestThreadSafety()
	{
		const int iterations = 100;

		var acts = 0;
		void increment(int _) =>
			acts++;
		var actOncePer = new ActOncePer<int>(increment);

		Parallel.For(0, iterations, actOncePer.Act);
		Assert.Equal(iterations, acts);
	}
}
