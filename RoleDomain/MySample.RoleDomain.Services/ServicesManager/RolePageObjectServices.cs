using System.Linq.Expressions;
using MySample.RoleDomain.Services.CacheInterfaces;
using MyCore.LogManager.ExceptionHandling;
using MySample.RoleDomain.Services.Interfaces;
using MyCore.Common.Base;
using MySample.RoleDomain.Libraries.Models;
using MySample.RoleDomain.Libraries.Entities;
using Helper.Validations.Interfaces;
using MySample.RoleDomain.Repositores.Interfaces;
using Helper.Maps;
using MyCore.CommonHelper;

namespace MySample.RoleDomain.Services.ServicesManager;
public class RolePageObjectServices : IRolePageObjectServices
{
    #region private
    private IRolePageObjectRepository RolePageObjectRepository;
    private IRolePageObjectCache cache;
    private IRolesCache rolesCache;
    private IPageObjectCache pageObjectCache;
    private IValidateManager validate;
    #endregion

    #region Ctor
    public RolePageObjectServices(
        IRolePageObjectRepository _RolePageObjectRepository,
        IRolePageObjectCache _cache,
        IRolesCache _rolesCache,
        IPageObjectCache _pageObjectCache,
        IValidateManager _validate)
    {
        RolePageObjectRepository = _RolePageObjectRepository;
        cache = _cache;
        rolesCache = _rolesCache;
        pageObjectCache = _pageObjectCache;
        validate = _validate;
    }
    #endregion

    #region Methods
    public ResponseBase<RolePageObjectModel> CreateOrUpdate(RequestBase<RolePageObjectCreateOrUpdateModel> request)
    {
        var rData = request.RequestData;
        var entity = MapperInstance.Instance.Map<RolePageObjectCreateOrUpdateModel, RolePageObjectEntity>(rData);

        var validateResult = validate.RolesValidate(entity).Result;
        if (validateResult.Any())
            throw new NotificationException(validateResult, ExceptionTypeEnum.Validation);
        var result = (rData.ID.HasValue)
            ? RolePageObjectRepository.Create(entity, request.RequestUserId)
            : RolePageObjectRepository.Update(entity, request.RequestUserId);

        if (result.IsCompletedSuccessfully && !result.Id.IsNullOrLessOrEqToZero())
        {
            var returnModel = MapperInstance.Instance.Map<RolePageObjectEntity, RolePageObjectModel>(result.Result);
            cache.AddSingleData(returnModel);
            return new ResponseBase<RolePageObjectModel>
            {
                Data = returnModel,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<RolePageObjectModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<IQueryable<RolePageObjectModel>> GetDataByFilter(RequestBase<RolePageObjectFilterModel> request)
    {
        var data = cache.GetAllData();
        var response = data.Where(GetPredicate(request.RequestData));
        return new ResponseBase<IQueryable<RolePageObjectModel>>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }
    public ResponseBase<RolePageObjectModel> GetSingleDataByFilter(RequestBase<RolePageObjectFilterModel> request)
    {
        var data = cache.GetAllData();
        var response = data.FirstOrDefault(GetPredicate(request.RequestData));
        var role = rolesCache.GetSingleDataById(request.RequestData.RoleID);
        var pageObject = pageObjectCache.GetSingleDataById(request.RequestData.PageObjectID);
        response.Role = role;
        response.PageObject = pageObject;
        return new ResponseBase<RolePageObjectModel>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }
    private Expression<Func<RolePageObjectModel, bool>> GetPredicate(RolePageObjectFilterModel request)
    {
        var predicate = PredicateBuilderHelper.False<RolePageObjectModel>();
        if (!request.ActivationStatus.HasValue)
            predicate = predicate.And(q => q.ActivationStatus == (int)ActivationStatusEnum.Active);
        else
            predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);

        predicate = predicate.Or(q => q.RoleID == request.RoleID);
        predicate = predicate.Or(q => q.PageObjectID == request.PageObjectID);
        return predicate;
    }
    #endregion
}
