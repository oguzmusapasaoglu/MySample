using System.Text;
using System.Text.Json;
using MyCore.LogManager.ExceptionHandling;
using MyCore.Common.Token;
using MyCore.LogManager.Models;
using MyCore.Common.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Extensions.DependencyInjection;
using MySample.RoleDomain.Services.Interfaces;

namespace MyCore.Middlewares.Helper;

public class MiddlewareHelper
{
    internal static async Task<ReqResLogModel> FormatRequest(HttpRequest request)
    {
        //This line allows us to set the reader for the request back at the beginning of its stream.
        request.EnableBuffering();
        //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        //...Then we copy the entire request stream into the new buffer.
        await request.Body.ReadAsync(buffer, 0, buffer.Length);
        //We convert the byte[] into a string using UTF8 encoding...
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        var reqData = JsonSerializer.Deserialize<RequestBase<dynamic>>(bodyAsText);
        if (reqData.RequestUserId.IsNullOrLessOrEqToZero())
            throw new NotificationException(ExceptionMessageHelper.ParseFieldError("RequestUserId"), ExceptionTypeEnum.Error);

        return new ReqResLogModel
        {
            RequestUserId = reqData.RequestUserId,
            ServicesName = request.Path,
            RequestData = bodyAsText,
            RequestDate = DateTime.Now
        };
    }
    internal static async Task<ReqResLogModel> FormatResponse(ReqResLogModel reqResLogModel, HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        string text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        var buffer = new byte[Convert.ToInt32(response.ContentLength)];
        await response.Body.ReadAsync(buffer, 0, buffer.Length);
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        var requestHeader = JsonSerializer.Deserialize<ResponseBase<dynamic>>(bodyAsText);

        reqResLogModel.ResponseMessage = text;
        reqResLogModel.ResponseDate = DateTime.Now;
        reqResLogModel.ResponseData = text;
        reqResLogModel.ResponseTotalTime = reqResLogModel.ResponseDate - reqResLogModel.RequestDate;
        return reqResLogModel;
    }
    internal static async Task<int?> ControlToken(HttpRequest httpRequest, int requestUserID)
    {
        string authValue = httpRequest.Headers["Authorization"];
        if (authValue.IsNotNullOrEmpty())
        {
            var token = authValue.Split(' ')[1];
            var claims = TokenHelper.GetClaimsFromToken(token);
            int tokenUserID = claims.FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.UniqueName).Value.ToInt();
            if (requestUserID != tokenUserID)
                throw new NotificationException(ExceptionMessageHelper.TokenException, ExceptionTypeEnum.Warn);
            DateTime tokenExpireDate = DateTime.Parse(claims.FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Exp).Value);
            if (tokenExpireDate >= DateTime.Now.AddMinutes(-1))
                throw new NotificationException(ExceptionMessageHelper.TokenException, ExceptionTypeEnum.Warn);
        }
        //throw new NotificationException(ExceptionMessageHelper.TokenException, ExceptionTypeEnum.Warn);
        throw new KnownException(ExceptionTypeEnum.Warn, new Exception(), ExceptionMessageHelper.TokenException);
    }

    internal static bool AuthorizationControl(IServiceProvider provider, int requestUserId, string servicesName)
    {
        IAuthorizationControlServices authorizationServices;
        authorizationServices = provider.GetService<IAuthorizationControlServices>();
        if (!authorizationServices.AuthorizationControlByUser(requestUserId, servicesName))
            throw new NotificationException(ExceptionMessageHelper.UnauthorizedAccess(servicesName), ExceptionTypeEnum.Error);
        return true;
    }
}
