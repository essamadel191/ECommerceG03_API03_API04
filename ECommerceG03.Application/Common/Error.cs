
using System.Text.Json.Serialization;

namespace ECommerceG03.Application.Common
{
    public record Error (string Code, string Description, ErrorType ErrorType = ErrorType.Failure)
    {
        public static Error Failure(string code = "General.Failure", string description = "An error occurred.") 
            => new(code, description, ErrorType.Failure);
        public static Error NotFound(string code = "General.NotFound", string description = "The requested resource was not found.")
            => new(code, description, ErrorType.NotFound);
        public static Error Forbidden(string code = "General.Forbidden", string description = "You do not have permission to access this resource.")
            => new(code, description, ErrorType.Forbidden);
        public static Error Unauthorized(string code = "General.Unauthorized", string description = "You are not authorized to access this resource.")
            => new(code, description, ErrorType.Unauthorized);
        public static Error Conflict(string code = "General.Conflict", string description = "A conflict occurred while processing your request.")
            => new(code, description, ErrorType.Conflict);
        public static Error Validation(string code = "General.Validation", string description = "Validation failed for the request.")
            => new(code, description, ErrorType.Validation);
        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "The provided credentials are invalid.")
            => new(code, description, ErrorType.InvalidCredentials);    
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorType
    {
        Failure = 0,
        NotFound,
        Forbidden,
        Unauthorized,
        Conflict,
        Validation,
        InvalidCredentials,
    }
}