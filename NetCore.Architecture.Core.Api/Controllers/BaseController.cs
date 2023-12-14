using Microsoft.AspNetCore.Mvc;
using NetCore.Architecture.Api.Models;
using NetCore.Architecture.Core.Common.Constants;
using NetCore.Architecture.Core.Common.Helpers;

namespace NetCore.Architecture.Api.Controllers;

[ApiController]
public class BaseController : Controller
{
    #region Api Action Result

    #region Success

    public override OkObjectResult Ok(object? value)
    {
        return Success(value);
    }

    protected OkObjectResult Success(object? data, string message = null)
    {
        var successResult = new AppApiResponse(true)
        {
            Data = data
        };
        
        var detail = !string.IsNullOrEmpty(message) ? message : ApiResultConstants.SUCCESS;
        successResult.AddSuccessMessage(detail);
        return base.Ok(successResult);
    }

    #endregion

    #region Failed

    

    #endregion
    
    #endregion
    
    #region Execute Api Action

    protected async Task<IActionResult> ExecuteApiAsync(Func<Task<AppApiActionResult>> apiLogicFuc)
    {
        string methodInfo = null;
        var startTime = DateTime.UtcNow;
        try
        {
            var correlationId = HttpContext.Response.Headers["X-Correlation-Id"];
            StringInterpolationHelper.AppendToStart(apiLogicFuc.Method.Name!);
            StringInterpolationHelper.AppendWithDefaultFormat($"CorrelationId = {correlationId.ToString().ToUpper()}");
            methodInfo = StringInterpolationHelper.BuildAndClear();
            LogHelper.WriteInfo($"[START] [API-Method] - {methodInfo}");

            var apiActionResult = await apiLogicFuc();

            StringInterpolationHelper.AppendToStart("Result of [[");
            StringInterpolationHelper.Append(methodInfo);
            StringInterpolationHelper.Append($"]]. IsSuccess: {apiActionResult.IsSuccess}");
            StringInterpolationHelper.Append(". Detail: ");
            StringInterpolationHelper.Append(apiActionResult.Detail);
            LogHelper.WriteInfo(StringInterpolationHelper.BuildAndClear());
            if (apiActionResult.IsSuccess)
            {
                return Success(apiActionResult.Data);
            }

            return Problem("An error occurred");
        }
        finally
        {
            StringInterpolationHelper.AppendToStart($"[END] - {methodInfo}. ");
            StringInterpolationHelper.Append("Total: ");
            StringInterpolationHelper.Append((DateTime.UtcNow - startTime).Milliseconds.ToString());
            StringInterpolationHelper.Append(" ms.");
            LogHelper.WriteInfo(StringInterpolationHelper.BuildAndClear());
        }
    }
    
    #endregion
}