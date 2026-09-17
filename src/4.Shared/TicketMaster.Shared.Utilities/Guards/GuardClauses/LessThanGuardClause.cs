using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class LessThanGuardClause
{
    public static void LessThan<T>(this Guard guard, T value, T targetValue, IComparer<T> comparer) =>
        guard.LessThan(value, targetValue, comparer, new BaseException(Error.Validation()));

    public static void LessThan<T>(this Guard guard, T value, T targetValue, IComparer<T> comparer, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.LessThan(value, targetValue, comparer, new BaseException(Error.Validation(description: message)));
    }

    public static void LessThan<T, TException>(this Guard guard, T value, T targetValue, IComparer<T> comparer, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        int comparerResult = comparer.Compare(value, targetValue);

        if (comparerResult == -1)
            throw exception;
    }

    public static void LessThan<T>(this Guard guard, T value, T targetValue)
        where T : IComparable<T>, IComparable => guard.LessThan(value, targetValue, Comparer<T>.Default);

    public static void LessThan<T>(this Guard guard, T value, T targetValue, string message)
        where T : IComparable<T>, IComparable => guard.LessThan(value, targetValue, Comparer<T>.Default, message);

    public static void LessThan<T, TException>(this Guard guard, T value, T targetValue, TException exception)
        where T : IComparable<T>, IComparable
        where TException : BaseException => guard.LessThan(value, targetValue, Comparer<T>.Default, exception);
}