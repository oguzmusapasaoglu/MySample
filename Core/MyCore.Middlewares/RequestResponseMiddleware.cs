using MyCore.LogManager.Models;

using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using MyCore.LogManager.Services;
using MyCore.Middlewares.Helper;

namespace MyCore.Middlewares;

public class RequestResponseMiddleware
{
    private readonly RequestDelegate next;
    private IServiceProvider provider;
    public RequestResponseMiddleware(IServiceProvider _provider, RequestDelegate _next)
    {
        next = _next;
        provider = _provider;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = new Stopwatch();
        watch.Start();

        //First, get the incoming request
        var reqResLogModel = await MiddlewareHelper.FormatRequest(context.Request);
        //Control token & requserid
        if (!MiddlewareHelper.ControlToken(context.Request, reqResLogModel.RequestUserId).Result.IsNotNullOrEmpty())
            return;

        if (!MiddlewareHelper.AuthorizationControl(provider, reqResLogModel.RequestUserId, reqResLogModel.ServicesName))
            return;

        //Copy a pointer to the original response body stream
        var originalBodyStream = context.Response.Body;
        //Create a new memory stream...
        using (var responseBody = new MemoryStream())
        {
            //...and use that for the temporary response body
            context.Response.Body = responseBody;
            //Continue down the Middleware pipeline, eventually returning to this class
            await next(context);
            watch.Stop();
            //Format the response from the server
            var response = await MiddlewareHelper.FormatResponse(reqResLogModel, context.Response);
            //TODO: Save log to chosen datastore
            AddReqResLogData(reqResLogModel);
            //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    async void AddReqResLogData(ReqResLogModel reqResLogModel)
    {
        ILogServices logServices = provider.GetService<ILogServices>();
        logServices.AddResponseLog(reqResLogModel);
    }
}
