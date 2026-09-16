using System.Text;

namespace TicketMaster.Shared.Utilities.Exceptions;

public readonly record struct Error
{
    public string Title { get; init; } = default!;

    public string Description { get; init; } = default!;

    public int Code { get; init; } = default!;

    public ErrorType Type { get; init; }

    public string[]? Parameters { get; init; }

    public Error(string title, string description, int code, ErrorType type, string[]? parameters)
    {
        Title = title;
        Description = ToString();
        Code = code;
        Type = type;
        Parameters = parameters;
    }

    public static Error Failure(
        string title = "Operation Failed",
        string description = "An error occurred while processing the operation.",
        int code = 500,
        ErrorType type = ErrorType.Failure,
        string[]? parameters = null) => new Error(title, description, code, type, parameters);

    public static Error Validation(
        string title = "Validation Error",
        string description = "A validation error has occurred.",
        int code = 400,
        ErrorType type = ErrorType.Validation,
        string[]? parameters = null) => new Error(title, description, code, type, parameters);

    public static Error Unexpected(
        string title = "Unexpected Error",
        string description = "An unexpected error has occurred.",
        int code = 500,
        ErrorType type = ErrorType.Unexpected,
        string[]? parameters = null) => new Error(title, description, code, type, parameters);

    public static Error NotFound(
        string title = "Not Found",
        string description = "The requested resource was not found.",
        int code = 404,
        ErrorType type = ErrorType.NotFound,
        string[]? parameters = null) => new Error(title, description, code, type, parameters);

    public override string ToString()
    {
        if (Parameters is null || Parameters.Length == 0)
            return Description;

        StringBuilder errorMessage = new();
        errorMessage.AppendFormat(Description, Parameters);

        return errorMessage.ToString();
    }
}