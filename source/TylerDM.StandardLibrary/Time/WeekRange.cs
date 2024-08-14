namespace TylerDM.StandardLibrary.Time;

public class WeekRange : IEnumerable<Week>, IEnumerable, ITimeRange<Week>
{
    #region properties
    public Week Start { get; }
    public Week End { get; }
    #endregion

    #region constructors
    public WeekRange(Week start, Week end)
    {
        Start = start;
        End = end;
    }
    #endregion

    #region methods
    public override string ToString() =>
    $"{Start} - {End}";

    public IEnumerator<Week> GetEnumerator()
    {
        var current = Start;
        do
            yield return current++;
        while (current < End);
        yield return End;
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
    #endregion
}
