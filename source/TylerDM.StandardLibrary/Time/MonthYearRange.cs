namespace TylerDM.StandardLibrary.Time;

public record MonthYearRange : IEnumerable<MonthYear>, IEnumerable, ITimeRange<MonthYear>
{
    #region properties
    public MonthYear Start { get; }
    public MonthYear End { get; }
    #endregion

    #region constructors
    public MonthYearRange(MonthYear start, MonthYear end)
    {
        Start = start;
        End = end;
    }
    #endregion

    #region methods
    public override string ToString() =>
        $"{Start} - {End}";

    public IEnumerator<MonthYear> GetEnumerator()
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
