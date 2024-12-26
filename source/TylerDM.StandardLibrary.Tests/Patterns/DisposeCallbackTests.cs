namespace TylerDM.StandardLibrary.Patterns;

public class DisposeCallbackTests
{
    [Fact]
    public void Test()
    {
        var calledBack = false;
        using (var _ = new DisposeCallback(() => calledBack = true))
            Assert.False(calledBack);
        Assert.True(calledBack);
    }

    [Fact]
    public async Task TestAsync()
    {
        var calledBack = false;
        await using (var _ = new DisposeCallbackAsync(async () => calledBack = true))
            Assert.False(calledBack);
        Assert.True(calledBack);
    }
}