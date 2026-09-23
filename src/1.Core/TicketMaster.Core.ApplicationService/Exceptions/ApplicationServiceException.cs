using TicketMaster.Shared.Utilities.Exceptions;

namespace TicketMaster.Core.ApplicationService.Exceptions;

public class ApplicationServiceException : BaseException
{
    public ApplicationServiceException(Error error) : base(error)
    {
    }
}