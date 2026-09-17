using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class InclusiveBetweenGuardClause
{
    public static void InclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue, IComparer<T> comparer) =>
        guard.InclusiveBetween(value, minimumValue, maximumValue, comparer, new BaseException(Error.Validation()));

    public static void InclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue, IComparer<T> comparer, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.InclusiveBetween(value, minimumValue, maximumValue, comparer, new BaseException(Error.Validation(description: message)));
    }

    public static void InclusiveBetween<T, TException>(this Guard guard,
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

        if (minimumValueComparerResult >= 0 && maximumValueComparerResult <= 0)
            throw exception;
    }

    public static void InclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue)
        where T : IComparable<T>, IComparable => guard.InclusiveBetween(value, minimumValue, maximumValue, Comparer<T>.Default);

    public static void InclusiveBetween<T>(this Guard guard, T value, T minimumValue, T maximumValue, string message)
        where T : IComparable<T>, IComparable => guard.InclusiveBetween(value, minimumValue, maximumValue, Comparer<T>.Default, message);

    public static void InclusiveBetween<T, TException>(this Guard guard, T value, T minimumValue, T maximumValue, TException exception)
        where T : IComparable<T>, IComparable
        where TException : BaseException => guard.InclusiveBetween(value, minimumValue, maximumValue, Comparer<T>.Default, exception);
}