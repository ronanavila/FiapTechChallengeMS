using TechChallenge.Domain.Contracts;
using Flunt.Notifications;
using System.Net;

namespace TechChallenge.Domain.Shared;
public class BaseResponse : IResponse

{
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty;
  public object? Data { get; set; }
  public IReadOnlyCollection<Notification>? Errors { get; set; }
  public HttpStatusCode StatusCode {get; set; }
  public BaseResponse() { }
  public BaseResponse(HttpStatusCode statusCode, bool success, string message, object? data)
  {
    Success = success;
    Message = message;
    Data = data;
    StatusCode = statusCode;
  }

  public BaseResponse(HttpStatusCode statusCode, bool success, IReadOnlyCollection<Notification> errors)
  {
    Success = success;
    Errors = errors;
    StatusCode = statusCode;
  }
}
