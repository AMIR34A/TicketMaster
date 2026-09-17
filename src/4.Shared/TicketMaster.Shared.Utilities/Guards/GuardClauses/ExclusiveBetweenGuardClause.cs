using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class ExclusiveBetweenGuardClause
{
    public static void ExclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue, IComparer<T> comparer) =>
        guard.ExclusiveBetween(value, maximumValue, maximumValue, comparer, new BaseException(Error.Validation()));

    public static void ExclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue, IComparer<T> comparer, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.ExclusiveBetween(value, maximumValue, maximumValue, comparer, new BaseException(Error.Validation(description: message)));
    }

    public static void ExclusiveBetween<T, TException>(this Guard guard,
        T value,
        T minimumValue,
        T maximumValue,
        IComparer<T> comparer,
        TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        int minimumValueComparerResult = comparer.Compare(value, minimumValue);
        int maximumValueComparerResult = comparer.Compare(value, maximumValue);

        if (minimumValueComparerResult == 1 && maximumValueComparerResult == -1)
            throw exception;
    }

    public static void ExclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue)
        where T : IComparable<T>, IComparable => guard.ExclusiveBetween(value, minimumValue, maximumValue, Comparer<T>.Default);

    public static void ExclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue, string message)
        where T : IComparable<T>, IComparable => guard.ExclusiveBetween(value, minimumValue, maximumValue, Comparer<T>.Default, message);

    public static void ExclusiveBetween<T, TException>(this Guard guard, T value, T minimumValue, T maximumValue, TException exception)
        where T : IComparable<T>, IComparable
        where TException : BaseException => guard.ExclusiveBetween(value, minimumValue, maximumValue, Comparer<T>.Default, exception);
}