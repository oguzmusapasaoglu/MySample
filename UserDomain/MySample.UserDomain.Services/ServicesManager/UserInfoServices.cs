using System.Linq.Expressions;

using MySample.UserDomain.Services.Interfaces;

using MySample.UserDomain.Repositories.CacheInterfaces;
using MySample.UserDomain.Libraries.Models;
using MyCore.Common.Base;
using MyCore.LogManager.ExceptionHandling;
using MySample.UserDomain.Libraries.Entities;
using MySample.UserDomain.Data.Interfaces;
using MySample.RoleDomain.Repositores.Interfaces;
using Helper.Validations.Interfaces;
using MyCore.CommonHelper;
using Helper.Maps;
using MySample.RoleDomain.Libraries.Models;
using MySample.RoleDomain.Libraries.Entities;
using MyCore.Common.Token;

namespace MySample.UserDomain.Services.ServicesManager;
public class UserInfoServices : IUserInfoServices
{
    #region private
    private List<ActivationStatusEnum> passiveEnums = new List<ActivationStatusEnum> { ActivationStatusEnum.Passive, ActivationStatusEnum.Deleted };
    private IUserInfoRepository repository;
    private IPagesRepository pagesRepository;
    private IUserInfoCache cache;
    private IValidateManager validate;
    #endregion

    #region Ctor
    public UserInfoServices(IUserInfoRepository _repository, IPagesRepository _pagesRepository, IUserInfoCache _cache
    , IValidateManager _validate)
    {
        repository = _repository;
        pagesRepository = _pagesRepository;
        cache = _cache;
        validate = _validate;
    }
    #endregion

    #region Methods
    public ResponseBase<UserInfoModel> Create(RequestBase<UserInfoCreateModel> request)
    {
        var rData = request.RequestData;
        if (DataValidation(rData.UserName, rData.EMail, rData.GSM).IsNotNullOrEmpty())
            return new ResponseBase<UserInfoModel>
            {
                Message = ExceptionMessageHelper.IsInUse("UserInfo"),
                Result = ResultEnum.Warning
            };
        var entity = MapperInstance.Instance.Map<UserInfoCreateModel, UserInfoEntity>(rData);

        var userPassword = (rData.Password.IsNullOrEmpty())
            ? PasswordHelper.GeneratePassword()
            : rData.Password;

        entity.Password = PasswordHelper.HashPassword(userPassword);
        var validateResult = validate.UsersValidate(entity).Result;
        if (validateResult.Any())
            throw new NotificationException(validateResult, ExceptionTypeEnum.Validation);
        var result = repository.Create(entity, request.RequestUserId);

        if (result.IsCompletedSuccessfully && !result.Id.IsNullOrLessOrEqToZero())
        {
            var returnModel = MapperInstance.Instance.Map<UserInfoEntity, UserInfoModel>(result.Result);
            cache.AddSingleData(returnModel);
            returnModel.Password = userPassword;
            return new ResponseBase<UserInfoModel>
            {
                Data = returnModel,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<UserInfoModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<UserInfoModel> Update(RequestBase<UserInfoUpdateModel> request)
    {
        var rData = request.RequestData;
        if (DataValidation(rData.UserName, rData.EMail, rData.GSM, rData.ID).IsNotNullOrEmpty())
            return new ResponseBase<UserInfoModel>
            {
                Message = ExceptionMessageHelper.IsInUse("UserInfo"),
                Result = ResultEnum.Warning
            };

        var cachedData = cache.GetAllData();

        var fData = cachedData.FirstOrDefault(q => q.ID.Value == rData.ID);
        var entity = MapperInstance.Instance.Map<UserInfoUpdateModel, UserInfoEntity>(rData);
        var validateResult = validate.UsersValidate(entity).Result;
        if (validateResult.Any())
            throw new NotificationException(validateResult, ExceptionTypeEnum.Validation);
        var result = repository.Update(entity, request.RequestUserId);

        if (result.IsCompleted)
        {
            var model = MapperInstance.Instance.Map<UserInfoEntity, UserInfoModel>(result.Result);
            cache.AddSingleData(model);
            return new ResponseBase<UserInfoModel>
            {
                Data = model,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<UserInfoModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<UserInfoModel> ChangeStatus(RequestBase<UserInfoStatusChangeModel> request)
    {
        var rData = request.RequestData;
        if (rData.ID.IsNullOrLessOrEqToZero())
            return ResponseHelper.ReturnErrorResponse<UserInfoModel>
                (ExceptionMessageHelper.RequiredField("ID"), ResultEnum.Warning);
        var predicate = PredicateBuilderHelper.False<UserInfoModel>();
        predicate = predicate.And(q => q.ID == rData.ID);
        predicate = predicate.And(q => q.ActivationStatus == rData.ActivationStatus);

        var model = cache.GetSingleDataByFilter(predicate);
        model.ActivationStatus = rData.ActivationStatus;
        var entity = MapperInstance.Instance.Map<UserInfoModel, UserInfoEntity>(model);
        var result = repository.Update(entity, request.RequestUserId);
        if (result.IsNotNullOrEmpty())
        {
            cache.ReFillCache();
            return new ResponseBase<UserInfoModel>
            {
                Data = model,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<UserInfoModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<IQueryable<UserInfoModel>> GetDataByFilter(UserInfoFilterModel request)
    {
        var data = cache.GetAllData();
        var response = data.Where(GetPredicate(request));
        return new ResponseBase<IQueryable<UserInfoModel>>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }

    private Expression<Func<UserInfoModel, bool>> GetPredicate(UserInfoFilterModel request)
    {
        var predicate = PredicateBuilderHelper.False<UserInfoModel>();
        if (!request.ActivationStatus.HasValue)
            predicate = predicate.And(q => passiveEnums.Contains((ActivationStatusEnum)q.ActivationStatus));
        else
            predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);

        predicate = predicate.And(q => q.UserName.ToLower().Contains(request.UserName.ToLower()));
        predicate = predicate.And(q => q.EMail.ToLower().Contains(request.EMail.ToLower()));
        predicate = predicate.And(q => q.UserGroupID == request.UserGroupID);
        predicate = predicate.And(q => q.GSM.Contains(request.GSM));
        return predicate;
    }
    public ResponseBase<UserInfoModel> GetSingleDataByFilter(UserInfoFilterModel request)
    {
        var data = cache.GetAllData();
        var response = data.FirstOrDefault(GetPredicate(request));
        return new ResponseBase<UserInfoModel>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }
    public ResponseBase<UserLoginResponseModel> UserLogin(UserLoginRequestModel request)
    {
        var user = repository.GetUserInfoLogin(request.LoginName, request.LoginName);
        if (user != null)
        {
            var hashPass = PasswordHelper.HashPassword(request.Password);
            if (user.Password == hashPass)
            {
                var userPages = pagesRepository.GetPagesByUserID(user.ID).Result;
                var UserToken = TokenHelper.GenerateToken(user.ID.ToString(), user.UserName, userPages.ToJson());
                var result = new UserLoginResponseModel
                {
                    ID = user.ID,
                    EMail = user.EMail,
                    GSM = user.GSM,
                    UserGroupID = user.UserGroupID,
                    UserName = user.UserName,
                    UserType = user.UserType,
                    UserToken = UserToken,
                    LeftMenuList = GetLeftMenu(userPages)
                };
                return ResponseHelper.ReturnSuccessResponse(result);
            }
        }
        return ResponseHelper.ReturnErrorResponse<UserLoginResponseModel>(ExceptionMessageHelper.LoginEror, ResultEnum.Warning);
    }

    private ICollection<LeftMainMenuModel> GetLeftMenu(IQueryable<UserRolesPagesRepository> userPages)
    {
        var result = new List<LeftMainMenuModel>();
        var topMenuList = userPages.Where(q => q.PageLevel == 0).ToList();
        foreach (var item in topMenuList)
        {
            var subMenuList = userPages.Where(q => q.BindPageId == item.ID).Select(t => new LeftSubMenuModel
            {
                ID = t.ID,
                BindPageID = t.BindPageId,
                IconName = t.IconName,
                PageName = t.PageName,
                PageURL = t.PageURL
            });
            var model = (subMenuList.Any())
                ? new LeftMainMenuModel
                {
                    ID = item.ID,
                    IconName = item.IconName,
                    PageName = item.PageName,
                    PageURL = item.PageURL,
                    LeftSubMenuList = subMenuList.ToList()
                }
                : new LeftMainMenuModel();
            result.Add(model);
        }
        return result;
    }

    public ResponseBase<UserInfoModel> ChangePassword(UserInfoChangePasswordModel request)
    {
        var result = repository.GetSingleById(request.Id);
        if (result.Result != null)
        {
            var hashPass = PasswordHelper.HashPassword(request.NewPassword);
            var entity = result.Result;
            entity.Password = hashPass;
            var updateResult = repository.Update(entity, entity.CreatedBy);
            return (updateResult.IsCompletedSuccessfully)
                ? ResponseHelper.ReturnSuccessResponse(MapperInstance.Instance.Map<UserInfoEntity, UserInfoModel>(updateResult.Result))
                : ResponseHelper.ReturnErrorResponse<UserInfoModel>(ExceptionMessageHelper.ProcessFailedResult, ResultEnum.Warning);
        }
        return ResponseHelper.ReturnErrorResponse<UserInfoModel>(ExceptionMessageHelper.ProcessFailedResult, ResultEnum.Warning);
    }

    private string DataValidation(string userName, string eMail, string gsm, int? id = null)
    {
        var data = cache.GetAllData();
        var predicate = PredicateBuilderHelper.False<UserInfoModel>();
        predicate = predicate.Or(q => q.UserName.ToLower() == userName.ToLower());
        predicate = predicate.Or(q => q.EMail.ToLower() == eMail.ToLower());
        predicate = predicate.Or(q => q.GSM.ToLower() == gsm.ToLower());
        var result = data.FirstOrDefault(predicate);
        if (result != null)
        {
            if (id.HasValue && id == result.ID)
                return string.Empty;
            return ExceptionMessageHelper.IsInUse("User Info");
        }
        return string.Empty;
    }
    #endregion
}
