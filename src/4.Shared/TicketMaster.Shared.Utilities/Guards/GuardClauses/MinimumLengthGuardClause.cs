using System.Collections;
using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class MinimumLengthGuardClause
{
    public static void MinimumLength(this Guard guard, string value, int minimumLength) =>
        guard.MinimumLength(value, minimumLength, new BaseException(Error.Validation()));

    public static void MinimumLength(this Guard guard, string value, int minimumLength, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.MinimumLength(value, minimumLength, new BaseException(Error.Validation(description: message)));
    }

    public static void MinimumLength<TException>(this Guard guard, string value, int maximumLength, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (value is null || value.Length > maximumLength)
            throw exception;
    }

    public static void MinimumLength(this Guard guard, ICollection value, int minimumLength) =>
        guard.MinimumLength(value, minimumLength, new BaseException(Error.Validation()));

    public static void MinimumLength(this Guard guard, ICollection value, int minimumLength, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        guard.MinimumLength(value, minimumLength, new BaseException(Error.Validation(description: message)));
    }

    public static void MinimumLength<TException>(this Guard guard, ICollection value, int minimumLength, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (value is null || value.Count > minimumLength)
            throw exception;
    }
}