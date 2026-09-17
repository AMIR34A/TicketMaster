using NotifyHub.Shared.Utility.Exceptions;
using TicketMaster.Shared.Utilities.Exceptions;
using TicketMaster.Shared.Utilities.Guards;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class NotNullGuardClause
{
    public static void NotNull<T>(this Guard guard, T value) => NotNull(guard, value, new BaseException(Error.Validation()));

    public static void NotNull<T>(this Guard guard, T value, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        NotNull(guard, value, new BaseException(Error.Validation(description: message)));
    }

    public static void NotNull<T, TException>(this Guard guard, T value, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (value is not null)
            throw exception;
    }
}