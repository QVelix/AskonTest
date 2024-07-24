using System.Net;
using System.Text.Json;

namespace AskonTest;

public class GlobalExceptionHandler
{
	private readonly RequestDelegate _requestDelegate;
	private readonly ILogger<GlobalExceptionHandler> _logger;

	public GlobalExceptionHandler(RequestDelegate requestDelegate, ILogger<GlobalExceptionHandler> logger)
	{
		_requestDelegate = requestDelegate;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await _requestDelegate(httpContext);
		}
		catch (ArgumentNullException exception)
		{
			await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.BadRequest,
				"Data not found, probably bad request");
		}
		catch (ArgumentException exception)
		{
			await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.InternalServerError,
				"Same item exist in database");
		}
		catch (NullReferenceException exception)
		{
			await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.NotFound, "Data not found");
		}
		catch (Exception exception)
		{
			await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.InternalServerError,
				"Something wrong");
		}
	}

	private async Task HandleExceptionAsync(
		HttpContext httpContext,
		string exceptionMessage,
		HttpStatusCode httpStatusCode, 
		string message)
	{
		_logger.LogError(exceptionMessage);

		HttpResponse response = httpContext.Response;
		response.ContentType = "application/json";
		response.StatusCode = Convert.ToInt32(httpStatusCode);
		await response.WriteAsJsonAsync(
			JsonSerializer.Serialize(
				new { Message = message, StatusCode = response.StatusCode }
			));
	}
}