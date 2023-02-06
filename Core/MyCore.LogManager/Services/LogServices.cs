using MyCore.Elastic.Interfaces;
using MyCore.LogManager.Models;

namespace MyCore.LogManager.Services
{
    public class LogServices : ILogServices
    {
        private string connection => "";
        private IElasticManageServices elasticServices;
        public LogServices(IElasticManageServices _elasticServices)
        {
            elasticServices = _elasticServices;
        }
        public async void AddExceptionLog(ExceptionLogModel model)
        {
            //TODO: Add Exception to Elastic 
        }
        public async void AddResponseLog(ReqResLogModel model)
        {
            //TODO: Add ReqRes to Elastic 
        }
    }
}
