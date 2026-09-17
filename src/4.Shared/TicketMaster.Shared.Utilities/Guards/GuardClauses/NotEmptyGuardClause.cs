using System.Collections;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class NotEmptyGuardClause
{
    public static void NotEmpty<T>(this Guard guard, T value) => guard.NotEmpty(value, new BaseException(Error.Validation()));

    public static void NotEmpty<T>(this Guard guard, T value, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.NotEmpty(value, new BaseException(Error.Validation(description: message)));
    }

    public static void NotEmpty<T, TException>(this Guard guard, T value, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (value is ICollection collectionValue && collectionValue.Count > 0)
            throw exception;

        if (value is IEnumerable enumerableValue && enumerableValue.GetEnumerator().MoveNext())
            throw exception;

        if (value is string strValue && !string.IsNullOrWhiteSpace(strValue))
            throw exception;

        if (!EqualityComparer<T>.Default.Equals(value, default))
            throw exception;
    }

    public static void NotEmpty<T>(this Guard guard, T value, IEqualityComparer<T> equalityComparer) =>
        guard.NotEmpty(value, equalityComparer, new BaseException(Error.Validation()));

    public static void NotEmpty<T>(this Guard guard, T value, IEqualityComparer<T> equalityComparer, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.NotEmpty(value, equalityComparer, new BaseException(Error.Validation(description: message)));
    }

    public static void NotEmpty<T, TException>(this Guard guard, T value, IEqualityComparer<T> equalityComparer, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (!equalityComparer.Equals(value, default))
            throw exception;
    }
}