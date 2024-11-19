using Flunt.Notifications;
using System.Net;

namespace TechChallenge.Domain.Contracts;
public interface IResponse
{
  public bool Success { get; set; }
  public string Message { get; set; }
  public object? Data { get; set; }
  public IReadOnlyCollection<Notification>? Errors { get; set; }
  public HttpStatusCode StatusCode { get; set; }
}
