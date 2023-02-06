using System.Linq.Expressions;

using MySample.UserDomain.Repositories.CacheInterfaces;
using MySample.UserDomain.Libraries.Models;
using MyCore.Common.Base;
using MyCore.LogManager.ExceptionHandling;
using MySample.UserDomain.Libraries.Entities;
using MySample.UserDomain.Services.Interfaces;
using Helper.Validations.Interfaces;
using MySample.UserDomain.Data.Interfaces;
using Helper.Maps;
using MyCore.CommonHelper;

namespace MySample.UserDomain.Services.ServicesManager;

public class UserGroupServices : IUserGroupServices
{
    #region private
    private List<ActivationStatusEnum> passiveEnums = new List<ActivationStatusEnum> { ActivationStatusEnum.Passive, ActivationStatusEnum.Deleted };
    private IUserGroupRepository userGroupRepository;
    private IUserGroupCache cache;
    private IValidateManager validate;
    #endregion

    #region Ctor
    public UserGroupServices(IUserGroupRepository _userGroupRepository, IUserGroupCache _cache
    , IValidateManager _validate)
    {
        userGroupRepository = _userGroupRepository;
        cache = _cache;
        validate = _validate;
    }
    #endregion

    #region Methods
    public ResponseBase<UserGroupModel> CreateOrUpdate(RequestBase<UserGroupCreateOrUpdateModel> request)
    {
        var rData = request.RequestData;
        if (DataValidation(rData.GroupName, rData.ID).IsNotNullOrEmpty())
            return new ResponseBase<UserGroupModel>
            {
                Message = ExceptionMessageHelper.IsInUse("User Group"),
                Result = ResultEnum.Warning
            };
        var entity = MapperInstance.Instance.Map<UserGroupCreateOrUpdateModel, UserGroupEntity>(rData);

        var validateResult = validate.UsersValidate(entity).Result;
        if (validateResult.Any())
            throw new NotificationException(validateResult, ExceptionTypeEnum.Validation);
        var result = rData.ID.HasValue
            ? userGroupRepository.Create(entity, request.RequestUserId)
            : userGroupRepository.Update(entity, request.RequestUserId);

        if (result.IsCompletedSuccessfully && !result.Id.IsNullOrLessOrEqToZero())
        {
            var returnModel = MapperInstance.Instance.Map<UserGroupEntity, UserGroupModel>(result.Result);
            cache.AddSingleData(returnModel);
            return new ResponseBase<UserGroupModel>
            {
                Data = returnModel,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<UserGroupModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<IQueryable<UserGroupModel>> GetDataByFilter(UserGroupFilterModel request)
    {
        var data = cache.GetAllData();
        var response = data.Where(GetPredicate(request));
        return new ResponseBase<IQueryable<UserGroupModel>>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }
    public ResponseBase<UserGroupModel> GetSingleDataByFilter(UserGroupFilterModel request)
    {
        var data = cache.GetAllData();
        var response = data.FirstOrDefault(GetPredicate(request));
        return new ResponseBase<UserGroupModel>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }
    private Expression<Func<UserGroupModel, bool>> GetPredicate(UserGroupFilterModel request)
    {
        var predicate = PredicateBuilderHelper.False<UserGroupModel>();
        if (!request.ActivationStatus.HasValue)
            predicate = predicate.And(q => passiveEnums.Contains((ActivationStatusEnum)q.ActivationStatus));
        else
            predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);

        predicate = predicate.And(q => q.GroupName.ToLower().Contains(request.GroupName.ToLower()));
        return predicate;
    }
    private string DataValidation(string groupName, int? id = null)
    {
        var data = cache.GetAllData();
        var predicate = PredicateBuilderHelper.False<UserGroupModel>();
        predicate = predicate.Or(q => q.GroupName.ToLower() == groupName.ToLower());
        var result = data.FirstOrDefault(predicate);
        if (result != null)
        {
            if (id.HasValue && id == result.ID)
                return string.Empty;
            return ExceptionMessageHelper.IsInUse("UserInfo");
        }
        return string.Empty;
    }
    #endregion
}
