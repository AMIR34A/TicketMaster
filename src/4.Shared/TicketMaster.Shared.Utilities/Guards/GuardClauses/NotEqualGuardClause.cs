using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class NotEqualGuardClause
{
    public static void NotEqual<T>(this Guard guard, T value, T targetValue) =>
        NotEqual(guard, value, targetValue, new BaseException(Error.Validation()));

    public static void NotEqual<T>(this Guard guard, T value, T targetValue, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        NotEqual(guard, value, targetValue, new BaseException(Error.Validation(description: message)));
    }

    public static void NotEqual<T, TException>(this Guard guard, T value, T targetValue, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (!Equals(value, targetValue))
            throw exception;
    }

    public static void NotEqual<T>(this Guard guard, T value, T targetValue, IEqualityComparer<T> equalityComparer) =>
        guard.NotEqual(value, targetValue, equalityComparer, new BaseException(Error.Validation()));

    public static void NotEqual<T>(this Guard guard, T value, T targetValue, IEqualityComparer<T> equalityComparer, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.NotEqual(value, targetValue, equalityComparer, new BaseException(Error.Validation(description: message)));
    }

    public static void NotEqual<T, TException>(this Guard guard, T value, T targetValue, IEqualityComparer<T> equalityComparer, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);
        if (!equalityComparer.Equals(value, targetValue))
            throw exception;
    }
}