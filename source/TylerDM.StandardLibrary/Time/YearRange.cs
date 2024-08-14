namespace TylerDM.StandardLibrary.Time;

public record YearRange(
    Year Start,
    Year End
) : IEnumerable<Year>, IEnumerable, ITimeRange<Year>
{
    #region constructors
    public YearRange(int begin, int end) : this(new(begin), new(end))
    {
    }
    #endregion

    #region methods
    public override string ToString() =>
        $"{Start} - {End}";

    public IEnumerator<Year> GetEnumerator()
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
