namespace TicketMaster.Core.BuildingBlocks.Entities;

public abstract class Entity<TId> : IEquatable<TId>
    where TId : struct
{
    public TId Id { get; protected set; }

    protected Entity() { }

    #region Equality
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => left is not null && right is not null && left.Equals(right);

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);

    public override bool Equals(object? obj) => obj is Entity<TId> entity && Id.Equals(entity.Id);

    public override int GetHashCode() => HashCode.Combine(GetType(), Id);

    public bool Equals(TId other) => Equals(other);
    #endregion
}