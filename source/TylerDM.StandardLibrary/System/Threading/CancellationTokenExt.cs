namespace TylerDM.StandardLibrary.System.Threading;

public static class CancellationTokenExt
{
    public static bool ShouldContinue(this CancellationToken ct) =>
        ct.IsCancellationRequested == false;
}