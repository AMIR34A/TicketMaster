using System.Collections;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class EmptyGuardClause
{
    public static void Empty<T>(this Guard guard, T value) => Empty(guard, value, new BaseException(Error.Validation()));

    public static void Empty<T>(this Guard guard, T value, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        Empty(guard, value, new BaseException(Error.Validation(description: message)));
    }

    public static void Empty<T, TException>(this Guard guard, T value, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (value is null)
            throw exception;

        if (value is ICollection collectionValue && collectionValue.Count == 0)
            throw exception;

        if (value is IEnumerable enumerableValue && !enumerableValue.GetEnumerator().MoveNext())
            throw exception;

        if (value is string strValue && string.IsNullOrWhiteSpace(strValue))
            throw exception;

        if (Equals(value, default(T)))
            throw exception;
    }

    public static void Empty<T>(this Guard guard, T value, IEqualityComparer<T> equalityComparer) =>
        Empty(guard, value, equalityComparer, new BaseException(Error.Validation()));

    public static void Empty<T>(this Guard guard, T value, IEqualityComparer<T> equalityComparer, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        Empty(guard, value, equalityComparer, new BaseException(Error.Validation(description: message)));
    }

    public static void Empty<T, TException>(this Guard guard, T value, IEqualityComparer<T> equalityComparer, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);
        if (equalityComparer.Equals(value, default))
            throw exception;
    }
}