namespace TylerDM.StandardLibrary.Time;

public interface ITimeUnit
{
    public ITimeUnit Parse(string text) =>
        TryParse(text) ?? throw new ArgumentException("Input string is not a unit of time.", nameof(text));

    public ITimeUnit? TryParse(string text)
    {
        throw new NotImplementedException();
    }

    private ITimeUnit? tryParseYear(string text)
    {
        if (
            int.TryParse(text, out var year) &&
            year >= 1_900 &&
            year <= 3_000
        )
            return new Year(year);
        return null;
    }
}
