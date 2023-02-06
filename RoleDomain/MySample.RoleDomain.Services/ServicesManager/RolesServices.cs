using MyCore.Common.Base;
using System.Linq.Expressions;
using MySample.RoleDomain.Libraries.Models;
using MySample.RoleDomain.Services.Interfaces;
using MySample.RoleDomain.Libraries.Entities;
using MySample.RoleDomain.Services.CacheInterfaces;
using MyCore.LogManager.ExceptionHandling;
using MySample.RoleDomain.Repositores.Interfaces;
using Helper.Validations.Interfaces;
using Helper.Maps;
using MyCore.CommonHelper;

namespace MySample.RoleDomain.Services.ServicesManager;
public class RolesServices : IRolesServices
{
    #region private
    private IRolesRepository rolesRepository;
    private IRolesCache cache;
    private IRolePageObjectCache rolePageObjectCache;
    private IValidateManager validate;
    #endregion

    #region Ctor
    public RolesServices(
        IRolesRepository _rolesRepository,
        IRolesCache _cache,
        IRolePageObjectCache _rolePageObjectCache,
        IValidateManager _validate)
    {
        rolesRepository = _rolesRepository;
        cache = _cache;
        rolePageObjectCache = _rolePageObjectCache;
        validate = _validate;
    }
    #endregion

    #region Methods
    public ResponseBase<RolesModel> CreateOrUpdate(RequestBase<RolesCreateOrUpdateModel> request)
    {
        var rData = request.RequestData;
        if (DataValidation(rData.RoleName).IsNotNullOrEmpty())
            return new ResponseBase<RolesModel>
            {
                Message = ExceptionMessageHelper.IsInUse("Roles"),
                Result = ResultEnum.Warning
            };

        var entity = MapperInstance.Instance.Map<RolesCreateOrUpdateModel, RolesEntity>(rData);

        var validateResult = validate.RolesValidate(entity).Result;
        if (validateResult.Any())
            throw new NotificationException(validateResult, ExceptionTypeEnum.Validation);

        var result = rData.ID.HasValue
            ? rolesRepository.Update(entity, request.RequestUserId)
            : rolesRepository.Create(entity, request.RequestUserId);

        if (result.IsCompletedSuccessfully && !result.Id.IsNullOrLessOrEqToZero())
        {
            var returnModel = MapperInstance.Instance.Map<RolesEntity, RolesModel>(result.Result);
            cache.AddSingleData(returnModel);
            return new ResponseBase<RolesModel>
            {
                Data = returnModel,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<RolesModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<RolesModel> ChangeStatus(RequestBase<RolesStatusChangeModel> request)
    {
        var rData = request.RequestData;
        if (rData.ID.IsNullOrLessOrEqToZero())
            return ResponseHelper.ReturnErrorResponse<RolesModel>
                (ExceptionMessageHelper.RequiredField("ID"), ResultEnum.Warning);
        var predicate = PredicateBuilderHelper.False<RolesModel>();
        predicate = predicate.And(q => q.ID == rData.ID);
        predicate = predicate.And(q => q.ActivationStatus == rData.ActivationStatus);

        var model = cache.GetSingleDataByFilter(predicate);
        model.ActivationStatus = rData.ActivationStatus;
        var entity = MapperInstance.Instance.Map<RolesModel, RolesEntity>(model);
        var result = rolesRepository.Update(entity, request.RequestUserId);
        if (result.IsNotNullOrEmpty())
        {
            cache.ReFillCache();
            return new ResponseBase<RolesModel>
            {
                Data = model,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<RolesModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<IQueryable<RolesModel>> GetDataByFilter(RequestBase<RolesFilterModel> request)
    {
        var data = cache.GetAllData();
        var response = data.Where(GetPredicate(request.RequestData));
        return new ResponseBase<IQueryable<RolesModel>>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }
    public ResponseBase<RolesModel> GetSingleDataByFilter(RequestBase<RolesFilterModel> request)
    {
        var data = cache.GetAllData();
        var response = data.FirstOrDefault(GetPredicate(request.RequestData));
        var rolePageObject = rolePageObjectCache.GetDataByRoleId(response.ID.Value);
        response.RolePageObjects = rolePageObject;
        return new ResponseBase<RolesModel>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }
    private Expression<Func<RolesModel, bool>> GetPredicate(RolesFilterModel request)
    {
        var predicate = PredicateBuilderHelper.False<RolesModel>();
        if (!request.ActivationStatus.HasValue)
            predicate = predicate.And(q => q.ActivationStatus == (int)ActivationStatusEnum.Active);
        else
            predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);

        predicate = predicate.And(q => q.RoleName.ToLower().Contains(request.RoleName.ToLower()));
        return predicate;
    }
    private string DataValidation(string roleName, int? id = null)
    {
        var data = cache.GetAllData();
        var predicate = PredicateBuilderHelper.False<RolesModel>();
        predicate = predicate.And(q => q.RoleName.ToLower() == roleName.ToLower());
        predicate = predicate.And(q => q.ActivationStatus == (int)ActivationStatusEnum.Active);
        var result = data.FirstOrDefault(predicate);
        if (result != null)
        {
            if (id.HasValue && id == result.ID)
                return string.Empty;
            return ExceptionMessageHelper.IsInUse("Roles");
        }
        return string.Empty;
    }
    #endregion
}
