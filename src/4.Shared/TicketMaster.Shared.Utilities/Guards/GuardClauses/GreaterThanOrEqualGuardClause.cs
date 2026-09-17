using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class GreaterThanOrEqualGuardClause
{
    public static void GreaterThanOrEqual<T>(this Guard guard, T value, T targetValue, IComparer<T> comparer) =>
        guard.GreaterThanOrEqual(value, targetValue, comparer, new BaseException(Error.Validation()));

    public static void GreaterThanOrEqual<T>(this Guard guard, T value, T targetValue, IComparer<T> comparer, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.GreaterThanOrEqual(value, targetValue, comparer, new BaseException(Error.Validation(description: message)));
    }

    public static void GreaterThanOrEqual<T, TException>(this Guard guard, T value, T targetValue, IComparer<T> comparer, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        int comparerResult = comparer.Compare(value, targetValue);

        if (comparerResult >= 0)
            throw exception;
    }

    public static void GreaterThanOrEqual<T>(this Guard guard, T value, T targetValue)
        where T : IComparable<T>, IComparable => guard.GreaterThanOrEqual(value, targetValue, Comparer<T>.Default);

    public static void GreaterThanOrEqual<T>(this Guard guard, T value, T targetValue, string message)
        where T : IComparable<T>, IComparable => guard.GreaterThanOrEqual(value, targetValue, Comparer<T>.Default, message);

    public static void GreaterThanOrEqual<T, TException>(this Guard guard, T value, T targetValue, TException exception)
        where T : IComparable<T>, IComparable
        where TException : BaseException => guard.GreaterThanOrEqual(value, targetValue, Comparer<T>.Default, exception);
}