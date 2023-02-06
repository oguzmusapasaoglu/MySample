using MyCore.Common.Base;

public class ResponseHelper
{
    public static ResponseBase<TResult> ReturnErrorResponse<TResult>
        (string erorMessage,
        ResultEnum resultType = ResultEnum.Error)
    {
        var response = new ResponseBase<TResult>();
        response.Message = erorMessage;
        response.Result = resultType;
        return response;
    }
    public static ResponseBase<TResult> ReturnSuccessResponse<TResult>
        (TResult result, ResultEnum resultType = ResultEnum.Success)
    where TResult : class
    {
        var response = new ResponseBase<TResult>();
        response.Data = result;
        response.Result = resultType;
        return response;
    }
}
