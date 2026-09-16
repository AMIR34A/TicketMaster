using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.Domain.Exceptions;

public class DomainException : BaseException
{
    public DomainException(Error error) : base(error)
    {
    }
}