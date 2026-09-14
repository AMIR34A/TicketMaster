namespace TicketMaster.Core.BuildingBlocks.ValueObjects;

public abstract class ValueObject<T> : IEquatable<T>
    where T : ValueObject<T>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    #region Equality
    public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        => left.Equals(right);

    public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        => !left.Equals(right);

    public override bool Equals(object? obj) => obj is ValueObject<T> valueObject &&
                                                GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());

    public override int GetHashCode() => GetEqualityComponents()
                                        .Select(x => x?.GetHashCode() ?? 0)
                                        .Aggregate((x, y) => x ^ y);

    public bool Equals(T? other) => Equals(other);
    #endregion
}