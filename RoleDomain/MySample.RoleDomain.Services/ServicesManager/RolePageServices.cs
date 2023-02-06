using Helper.Maps;
using Helper.Validations.Interfaces;

using MyCore.Common.Base;
using MyCore.CommonHelper;
using MyCore.LogManager.ExceptionHandling;

using MySample.RoleDomain.Libraries.Entities;
using MySample.RoleDomain.Libraries.Models;
using MySample.RoleDomain.Repositores.Interfaces;
using MySample.RoleDomain.Services.CacheInterfaces;
using MySample.RoleDomain.Services.Interfaces;

using System.Linq.Expressions;

namespace MySample.RoleDomain.Services.ServicesManager
{
    public class RolePageServices : IRolePageServices
    {
        #region private
        private IRolePageRepository repository;
        private IRolePageCache cache;
        private IValidateManager validate;
        #endregion

        #region Ctor
        public RolePageServices(
            IRolePageRepository _repository,
            IRolePageCache _cache,
            IValidateManager _validate)
        {
            repository = _repository;
            cache = _cache;
            validate = _validate;
        }
        #endregion

        #region Methods
        public ResponseBase<RolePageListModel> CreateOrUpdate(RequestBase<RolePageModel> request)
        {
            var rData = request.RequestData;
            var entity = MapperInstance.Instance.Map<RolePageModel, RolePageEntity>(rData);

            var validateResult = validate.RolesValidate(entity).Result;
            if (validateResult.Any())
                throw new NotificationException(validateResult, ExceptionTypeEnum.Validation);
            var result = (rData.ID.HasValue)
                ? repository.Create(entity, request.RequestUserId)
                : repository.Update(entity, request.RequestUserId);

            if (result.IsCompletedSuccessfully && !result.Id.IsNullOrLessOrEqToZero())
            {
                var rolePageList = repository.GetSingleRolePageByID(result.Id);
                var returnModel = MapperInstance.Instance.Map<RolePageEntity, RolePageListModel>(rolePageList.Result);
                cache.AddSingleData(returnModel);
                return new ResponseBase<RolePageListModel>
                {
                    Data = returnModel,
                    Result = ResultEnum.Success
                };
            }
            return new ResponseBase<RolePageListModel>
            {
                Message = ExceptionMessageHelper.UnexpectedSystemError,
                Result = ResultEnum.Error
            };
        }
        public async Task<ResponseBase<IQueryable<RolePageListModel>>> GetDataByFilter(RequestBase<RolePageModel> request)
        {
            var data = cache.GetAllData();
            var response = data.Where(GetPredicate(request.RequestData));
            return new ResponseBase<IQueryable<RolePageListModel>>
            {
                Data = response,
                Result = ResultEnum.Success
            };
        }
        public async Task<IQueryable<RolePageListModel>> GetRolePagesByPageID(int pageID)
        {
            var data = cache.GetAllData();
            var response = data.Where(q => q.PageID == pageID).AsQueryable();
            return response;
        }
        public async Task<IQueryable<RolePageListModel>> GetRolePagesByRoleID(int roleID)
        {
            var data = cache.GetAllData();
            var response = data.Where(q => q.RoleID == roleID).AsQueryable();
            return response;
        }
        public async Task<IQueryable<RolePageListModel>> GetRolePagesByUserID(int userID)
        {
            var data = repository.GetRolePagesByUserID(userID);
            var returnModel = MapperInstance.Instance.Map<IQueryable<RolePageEntity>, IQueryable<RolePageListModel>>(data.Result);
            return returnModel;
        }
        public async Task<ResponseBase<RolePageListModel>> GetSingleDataByID(int id)
        {
            var data = cache.GetAllData();
            var response = data.FirstOrDefault(q => q.ID == id);
            return new ResponseBase<RolePageListModel>
            {
                Data = response,
                Result = ResultEnum.Success
            };
        }
        private Expression<Func<RolePageListModel, bool>> GetPredicate(RolePageModel request)
        {
            var predicate = PredicateBuilderHelper.False<RolePageListModel>();
            predicate = predicate.And(q => q.ActivationStatus == (int)ActivationStatusEnum.Active);
            predicate = predicate.Or(q => q.RoleID == request.RoleID);
            predicate = predicate.Or(q => q.PageID == request.PageID);
            return predicate;
        }
        #endregion
    }
}
