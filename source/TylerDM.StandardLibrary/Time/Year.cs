namespace TylerDM.StandardLibrary.Time;

public readonly record struct Year : ITimeUnit
{
    #region properties
    public readonly int Value { get; }
    #endregion

    #region constructors
    public Year(int value)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(value, default, nameof(value));
        if (value < 1000 || value > 3000) throw new ArgumentOutOfRangeException(nameof(value));

        Value = value;
    }
    #endregion

    #region operators
    public static Year? TryParse(string text)
    {
        if (int.TryParse(text, out var i))
            return new Year(i);

        return null;
    }

    public static Year Parse(string text) =>
        TryParse(text) ??
        throw new ArgumentOutOfRangeException(nameof(text));

    public static implicit operator int(Year y) =>
        y.Value;

    public static explicit operator Year(int y) =>
        new(y);

    public static Year operator ++(Year x) =>
        new(x.Value + 1);

    public static Year operator --(Year x) =>
        new(x.Value - 1);

    public static bool operator <(Year x, Year y) =>
        x.Value < y.Value;

    public static bool operator >(Year x, Year y) =>
        !(x < y);
    #endregion

    #region methods
    public override string ToString() =>
        Value.ToString();
    #endregion
}
