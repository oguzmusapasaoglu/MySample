using System.Diagnostics;

namespace MyCore.LogManager.ExceptionHandling
{
    public class NotificationException : Exception
    {
        public string MethotName { get; set; }
        public ExceptionTypeEnum ExceptionType { get; set; }
        public List<string> MessageList { get; set; }
        public Exception ExceptionProp { get; set; }
        public NotificationException(string message, ExceptionTypeEnum exceptionType = ExceptionTypeEnum.Info)
            : base(message)
        {
            if (MessageList == null)
                MessageList = new List<string>();
            ExceptionType = exceptionType;
            MessageList.Add(message);
        }
        public NotificationException(List<string> messages, ExceptionTypeEnum exceptionType = ExceptionTypeEnum.Warn)
        {
            if (MessageList == null)
                MessageList = new List<string>();
            ExceptionType = exceptionType;
            MessageList = messages;
        }
    }
    public class KnownException : Exception
    {
        public string MethotName { get; set; }
        public ExceptionTypeEnum ExceptionType { get; set; }
        public string Message { get; set; }
        public Exception ExceptionProp { get; set; }
        public KnownException(ExceptionTypeEnum exceptionType, Exception exception, string message)
            : base(message, exception)
        {
            ExceptionType = exceptionType;
            Message = message;
            ExceptionProp = exception;
            MethotName = GetMethodName(exception);
        }
        public KnownException(ExceptionTypeEnum exceptionType, Exception exception)
        {
            ExceptionType = exceptionType;
            Message = exception.Message;
            ExceptionProp = exception;
            MethotName = GetMethodName(exception);
        }
        public static string GetMethodName(Exception exception)
        {
            var trace = new StackTrace(exception).GetFrames().Select(q => q.GetMethod()).FirstOrDefault();
            return (trace.IsNullOrEmpty())
                ? string.Empty
                : trace.DeclaringType.FullName + "." + trace.Name;
        }
    }
    public class FattalException : KnownException
    {
        public FattalException(ExceptionTypeEnum exceptionType, Exception exception, string exceptionMessage)
            : base(exceptionType, exception, exceptionMessage)
        {
            ExceptionProp = exception;
            MethotName = GetMethodName(exception);
            Message = exceptionMessage;
            ExceptionType = exceptionType;
        }
    }
}
