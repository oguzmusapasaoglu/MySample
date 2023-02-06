using MyCore.Common.Base;
using MySample.UserDomain.Libraries.Models;

namespace MySample.UserDomain.Services.Interfaces;

public interface IUserGroupServices
{
    ResponseBase<UserGroupModel> CreateOrUpdate(RequestBase<UserGroupCreateOrUpdateModel> request);
    ResponseBase<IQueryable<UserGroupModel>> GetDataByFilter(UserGroupFilterModel request);
    ResponseBase<UserGroupModel> GetSingleDataByFilter(UserGroupFilterModel request);
}