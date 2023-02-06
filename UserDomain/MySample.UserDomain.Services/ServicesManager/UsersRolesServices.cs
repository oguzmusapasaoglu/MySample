using MySample.UserDomain.Services.Interfaces;

using MySample.UserDomain.Repositories.CacheInterfaces;
using MySample.UserDomain.Libraries.Models;
using MyCore.Common.Base;
using MyCore.LogManager.ExceptionHandling;
using MySample.UserDomain.Libraries.Entities;
using MySample.UserDomain.Data.Interfaces;
using Helper.Validations.Interfaces;
using Helper.Maps;

namespace MySample.UserDomain.Services.ServicesManager;

public class UsersRolesServices : IUsersRolesServices
{
    #region private
    private IUsersRolesRepository repository;
    private IUsersRolesCache cache;
    private IValidateManager validate;
    #endregion

    #region Ctor
    public UsersRolesServices(IUsersRolesRepository _repository, IUsersRolesCache _cache
    , IValidateManager _validate)
    {
        repository = _repository;
        cache = _cache;
        validate = _validate;
    }
    #endregion

    #region Methods
    public ResponseBase<UsersRolesModel> CreateOrUpdate(RequestBase<UsersRolesCreateOrUpdateModel> request)
    {
        var rData = request.RequestData;
        var entity = MapperInstance.Instance.Map<UsersRolesCreateOrUpdateModel, UsersRolesEntity>(rData);
        var validateResult = validate.UsersValidate(entity).Result;
        if (validateResult.Any())
            throw new NotificationException(validateResult, ExceptionTypeEnum.Validation);
        var result = (rData.ID.HasValue)
            ? repository.Create(entity, request.RequestUserId)
            : repository.Update(entity, request.RequestUserId);
        if (result.IsCompletedSuccessfully && !result.Id.IsNullOrLessOrEqToZero())
        {
            var returnModel = MapperInstance.Instance.Map<UsersRolesEntity, UsersRolesModel>(result.Result);
            cache.AddSingleData(returnModel);
            return new ResponseBase<UsersRolesModel>
            {
                Data = returnModel,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<UsersRolesModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<UsersRolesModel> BulkCreate(RequestBase<List<UsersRolesCreateOrUpdateModel>> request)
    {
        throw new NotImplementedException();
    }

    public ResponseBase<IQueryable<UsersRolesModel>> GetDataByUserID(int userID)
    {
        throw new NotImplementedException();
    }

    public ResponseBase<IQueryable<UsersRolesModel>> GetDataByRoleID(int roleID)
    {
        throw new NotImplementedException();
    }
    #endregion
}
