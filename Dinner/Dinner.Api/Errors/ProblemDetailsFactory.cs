using System.Diagnostics;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace Dinner.Api.Errors;

public class DinnerProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    public DinnerProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 500;
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };
        ApplyProblemDetails(httpContext, problemDetails, statusCode.Value);
        return problemDetails;
    }

    private void ApplyProblemDetails(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;
        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null) problemDetails.Extensions["traceId"] = traceId;


        var errors = httpContext?.Items["errors"] as List<Error>;
        if (errors != null)
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
        }

        problemDetails.Extensions.Add("customProp", "customItem");
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext,
        ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null,
        string? detail = null, string? instance = null)
    {
        throw new NotImplementedException();
    }
}