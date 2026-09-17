using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Shared.Utilities.Guards.GuardClauses;

public static class NullGuardClause
{
    public static void Null<T>(this Guard guard, T value) => Null(guard, value, new BaseException(Error.Validation()));

    public static void Null<T>(this Guard guard, T value, string message)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(message);
        Null(guard, value, new BaseException(Error.Validation(description: message)));
    }

    public static void Null<T, TException>(this Guard guard, T value, TException exception)
        where TException : BaseException
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (value is null)
            throw exception;
    }
}