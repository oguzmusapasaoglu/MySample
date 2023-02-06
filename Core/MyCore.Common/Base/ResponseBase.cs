namespace MyCore.Common.Base
{
    public class ResponseBase<TResponse>
    {
        public ResultEnum Result { get; set; }
        public string Message { get; set; }
        public TResponse Data { get; set; }
    }
}
