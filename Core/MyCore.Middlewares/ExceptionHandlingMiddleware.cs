using Microsoft.AspNetCore.Http;
using MyCore.LogManager.Services;
using MyCore.LogManager.ExceptionHandling;
using MyCore.Common.Base;
using MyCore.LogManager.Models;
using Microsoft.Extensions.DependencyInjection;
using MyCore.Middlewares.Helper;

namespace MyCore.Middlewares;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private IServiceProvider provider;
    public ExceptionHandlingMiddleware(IServiceProvider _provider, RequestDelegate _next)
    {
        next = _next;
        provider = _provider;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (NotificationException nex)
        {
            HandleNotificationException(httpContext, nex);
        }
        catch (KnownException kex)
        {
            HandleKnownException(httpContext, kex);
        }
        catch (Exception ex)
        {
            HandleException(httpContext, ex);
        }
    }

    private ResponseBase<string> HandleNotificationException(HttpContext context, NotificationException ex)
    {
        var reqModel = MiddlewareHelper.FormatRequest(context.Request).Result;
        CreateExceptionLog(ex.MethotName, reqModel.RequestData, reqModel.RequestIP, reqModel.RequestUserId, ex.Message, ex.ExceptionType, ex.ExceptionProp);
        return CreateExceptionResponse<string>(ex.ExceptionType, ex.Message);
    }
    private ResponseBase<string> HandleKnownException(HttpContext context, KnownException ex)
    {
        //First, get the incoming request
        var reqModel = MiddlewareHelper.FormatRequest(context.Request).Result;
        CreateExceptionLog(ex.MethotName, reqModel.RequestData, reqModel.RequestIP, reqModel.RequestUserId, ex.Message, ex.ExceptionType, ex.ExceptionProp);
        return CreateExceptionResponse<string>(ex.ExceptionType, ex.Message);
    }
    private ResponseBase<string> HandleException(HttpContext context, Exception ex)
    {
        //First, get the incoming request
        var reqModel = MiddlewareHelper.FormatRequest(context.Request).Result;
        CreateExceptionLog(KnownException.GetMethodName(ex), reqModel.RequestData, reqModel.RequestIP, reqModel.RequestUserId, ExceptionMessageHelper.UnexpectedSystemError, ExceptionTypeEnum.Fattal, ex);
        return CreateExceptionResponse<string>(ExceptionTypeEnum.Fattal, ExceptionMessageHelper.UnexpectedSystemError);
    }

    private ResponseBase<string> CreateExceptionResponse<T>(ExceptionTypeEnum exceptionType, string exceptionMessage)
    {
        var resultEnum = (exceptionType != ExceptionTypeEnum.Fattal || exceptionType != ExceptionTypeEnum.Error)
            ? ResultEnum.Warning
           : ResultEnum.Error;
        return ResponseHelper.ReturnErrorResponse<string>(exceptionMessage, resultEnum);
    }
    private void CreateExceptionLog(string methodName, string requestData,
        string requestIP, int requestUserId, string exceptionMessage, ExceptionTypeEnum exceptionType, Exception exceptionProp)
    {
        ILogServices logServices = provider.GetService<ILogServices>();
        logServices.AddExceptionLog(new ExceptionLogModel
        {
            MethodName = methodName,
            RequestData = requestData,
            RequestIP = requestIP,
            ExceptionMessage = exceptionMessage,
            ExceptionType = exceptionType,
            ExceptionProp = exceptionProp,
            RequestDate = DateTime.Now,
            RequestUserId = requestUserId
        });
    }
}
