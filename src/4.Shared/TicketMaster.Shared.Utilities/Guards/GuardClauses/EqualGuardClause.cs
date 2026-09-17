using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class EqualGuardClause
{
    public static void Equal<T>(this Guard guard, T value, T targetValue) =>
        guard.Equal(value, targetValue, new BaseException(Error.Validation()));

    public static void Equal<T>(this Guard guard, T value, T targetValue, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.Equal(value, targetValue, new BaseException(Error.Validation(description: message)));
    }

    public static void Equal<T, TException>(this Guard guard, T value, T targetValue, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (Equals(value, targetValue))
            throw exception;
    }

    public static void Equal<T>(this Guard guard, T value, T targetValue, IEqualityComparer<T> equalityComparer) =>
        guard.Equal(value, targetValue, equalityComparer, new BaseException(Error.Validation()));

    public static void Equal<T>(this Guard guard, T value, T targetValue, IEqualityComparer<T> equalityComparer, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.Equal(value, targetValue, equalityComparer, new BaseException(Error.Validation(description: message)));
    }

    public static void Equal<T, TException>(this Guard guard, T value, T targetValue, IEqualityComparer<T> equalityComparer, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);
        if (equalityComparer.Equals(value, targetValue))
            throw exception;
    }
}