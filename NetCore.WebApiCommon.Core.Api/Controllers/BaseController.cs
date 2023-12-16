using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.WebApiCommon.Api.Models;
using NetCore.WebApiCommon.Core.Common.Constants;
using NetCore.WebApiCommon.Core.Common.Enums;
using NetCore.WebApiCommon.Core.Common.Exceptions;
using NetCore.WebApiCommon.Core.Common.Helpers;

namespace NetCore.WebApiCommon.Api.Controllers;

[ApiController]
public class BaseController : Controller
{
    #region Api Action Result

    #region Success

    public override OkObjectResult Ok(object? value)
    {
        return Success(value);
    }

    protected OkObjectResult Success(object? data, string? message = null)
    {
        var successResult = new ApiResponse(true)
        {
            Data = data
        };
        
        var detail = !string.IsNullOrEmpty(message) ? message : ApiResultConstants.SUCCESS;
        successResult.AddSuccessMessage(detail);
        return base.Ok(successResult);
    }

    #endregion

    #region Failure

    protected IActionResult ClientError(string messageContent, object? data = null)
    {
        var apiResponse = new ApiResponse(false)
        {
            Data = data,
            Messages = new List<AppMessage>()
            {
                new()
                {
                    Content = messageContent,
                    Type = ApiMessageType.Error
                }
            }
        };
        
        return ClientError(apiResponse);
    }
    
    protected IActionResult ClientError(ApiResponse apiResponse)
    {
        return BadRequest(apiResponse);
    }

    private IActionResult Error(string messageContent, ApiErrorType errorType = ApiErrorType.InternalServerError, object? data = null)
    {
        var apiResponse = new ApiResponse(false)
        {
            Data = data,
            Messages = new List<AppMessage>()
            {
                new()
                {
                    Content = messageContent,
                    Type = ApiMessageType.Error
                }
            }
        };

        return errorType switch
        {
            ApiErrorType.ClientError => ClientError(apiResponse),
            ApiErrorType.BusinessError => StatusCode(StatusCodes.Status409Conflict, apiResponse),
            _ => InternalError(messageContent)
        };
    }

    protected IActionResult InternalError(string message)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, message);
    }

    #endregion
    
    #endregion
    
    #region Execute Api Action

    protected async Task<IActionResult> ExecuteApiAsync(Func<Task<ApiActionResult>> apiLogicFunc)
    {
        var startTime = DateTime.UtcNow;
        var correlationId = HttpContext.Response.Headers["X-Correlation-Id"];
        StringInterpolationHelper.AppendToStart(apiLogicFunc.Method.Name!);
        StringInterpolationHelper.AppendWithDefaultFormat($"CorrelationId = {correlationId.ToString().ToUpper()}");
        var methodInfo = StringInterpolationHelper.BuildAndClear();
        LogHelper.WriteInfo($"[START] [API-Method] - {methodInfo}");
        
        try
        {
            var apiActionResult = await apiLogicFunc();

            StringInterpolationHelper.AppendToStart("Result of [[");
            StringInterpolationHelper.Append(methodInfo);
            StringInterpolationHelper.Append($"]]. IsSuccess: {apiActionResult.IsSuccess}");
            StringInterpolationHelper.Append(". Detail: ");
            StringInterpolationHelper.Append(apiActionResult.Detail ?? "no details.");
            LogHelper.WriteInfo(StringInterpolationHelper.BuildAndClear());
            
            return apiActionResult.IsSuccess ? Success(apiActionResult.Data) : Problem(apiActionResult.Detail);
        }
        catch (Exception e)
        {
            StringInterpolationHelper.AppendToStart("Result of [[");
            StringInterpolationHelper.Append(methodInfo);
            StringInterpolationHelper.Append($"]]. IsSuccess: false");
            StringInterpolationHelper.Append(". Detail: ");
            StringInterpolationHelper.Append(e.Message);
            LogHelper.WriteInfo(StringInterpolationHelper.BuildAndClear());
            
            return e.GetType().IsAssignableTo(typeof(IAppException)) ? Error(e.Message, ApiErrorType.BusinessError) : InternalError(e.Message);
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