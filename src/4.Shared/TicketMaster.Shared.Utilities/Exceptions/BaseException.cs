using System.Net;

namespace TicketMaster.Shared.Utilities.Exceptions;

public class BaseException : Exception
{
    public Error Error { get; set; }

    public HttpStatusCode HttpStatusCode { get; }

    public BaseException(Error error) : base(error.ToString())
    {
        Error = error;
        HttpStatusCode = Error.Type switch
        {
            ErrorType.Failure => HttpStatusCode.UnprocessableEntity,
            ErrorType.Unexpected => HttpStatusCode.UnprocessableEntity,
            ErrorType.NotFound => HttpStatusCode.NotFound,
            ErrorType.Validation => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };
    }
}