using MyCore.LogManager.Models;

namespace MyCore.LogManager.Services
{
    public interface ILogServices
    {
        void AddExceptionLog(ExceptionLogModel model);
        void AddResponseLog(ReqResLogModel model);
    }
}