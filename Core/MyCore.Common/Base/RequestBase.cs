namespace MyCore.Common.Base
{
    public class RequestBase<TRequest>
        where TRequest : class
    {
        public int RequestUserId { get; set; }
        public TRequest RequestData { get; set; }
    }
}
