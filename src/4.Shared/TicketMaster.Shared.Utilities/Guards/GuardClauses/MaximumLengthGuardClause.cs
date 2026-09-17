using System.Collections;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class MaximumLengthGuardClause
{
    public static void MaximumLength(this Guard guard, string value, int maximumLength) =>
        guard.MaximumLength(value, maximumLength, new BaseException(Error.Validation()));

    public static void MaximumLength(this Guard guard, string value, int maximumLength, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.MaximumLength(value, maximumLength, new BaseException(Error.Validation(description: message)));
    }

    public static void MaximumLength<TException>(this Guard guard, string value, int maximumLength, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (value is null || value.Length > maximumLength)
            throw exception;
    }

    public static void MaximumLength(this Guard guard, ICollection value, int maximumLength) =>
        guard.MaximumLength(value, maximumLength, new BaseException(Error.Validation()));

    public static void MaximumLength(this Guard guard, ICollection value, int maximumLength, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.MaximumLength(value, maximumLength, new BaseException(Error.Validation(description: message)));
    }

    public static void MaximumLength<TException>(this Guard guard, ICollection value, int maximumLength, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (value is null || value.Count > maximumLength)
            throw exception;
    }
}