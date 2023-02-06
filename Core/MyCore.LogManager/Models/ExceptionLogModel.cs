namespace MyCore.LogManager.Models;

public class ExceptionLogModel
{
    public string MethodName { get; set; }
    public string RequestData { get; set; }
    public DateTime RequestDate { get; set; }
    public string RequestIP { get; set; }
    public int RequestUserId { get; set; }
    public string ExceptionMessage { get; set; }
    public ExceptionTypeEnum ExceptionType { get; set; }
    public Exception ExceptionProp { get; set; }
}
