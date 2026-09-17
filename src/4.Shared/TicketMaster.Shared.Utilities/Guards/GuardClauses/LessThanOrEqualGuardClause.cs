using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class LessThanOrEqualGuardClause
{
    public static void LessThanOrEqual<T>(this Guard guard, T value, T targetValue, IComparer<T> comparer) =>
        guard.LessThanOrEqual(value, targetValue, comparer, new BaseException(Error.Validation()));

    public static void LessThanOrEqual<T>(this Guard guard, T value, T targetValue, IComparer<T> comparer, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.LessThanOrEqual(value, targetValue, comparer, new BaseException(Error.Validation(description: message)));
    }

    public static void LessThanOrEqual<T, TException>(this Guard guard, T value, T targetValue, IComparer<T> comparer, TException exception)
    where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        int comparerResult = comparer.Compare(value, targetValue);

        if (comparerResult <= 0)
            throw exception;
    }

    public static void LessThanOrEqual<T>(this Guard guard, T value, T targetValue)
        where T : IComparable<T>, IComparable => guard.LessThanOrEqual(value, targetValue, Comparer<T>.Default);

    public static void LessThanOrEqual<T>(this Guard guard, T value, T targetValue, string message)
        where T : IComparable<T>, IComparable => guard.LessThanOrEqual(value, targetValue, Comparer<T>.Default, message);

    public static void LessThanOrEqual<T, TException>(this Guard guard, T value, T targetValue, TException exception)
        where T : IComparable<T>, IComparable
        where TException : BaseException => guard.LessThanOrEqual(value, targetValue, Comparer<T>.Default, exception);
}