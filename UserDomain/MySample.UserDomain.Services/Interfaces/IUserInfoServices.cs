using MyCore.Common.Base;
using MySample.UserDomain.Libraries.Models;

namespace MySample.UserDomain.Services.Interfaces;
public interface IUserInfoServices
{
    ResponseBase<UserInfoModel> Create(RequestBase<UserInfoCreateModel> request);
    ResponseBase<UserInfoModel> Update(RequestBase<UserInfoUpdateModel> request);
    ResponseBase<UserInfoModel> ChangeStatus(RequestBase<UserInfoStatusChangeModel> request);
    ResponseBase<UserInfoModel> ChangePassword(UserInfoChangePasswordModel request);
    ResponseBase<IQueryable<UserInfoModel>> GetDataByFilter(UserInfoFilterModel request);
    ResponseBase<UserInfoModel> GetSingleDataByFilter(UserInfoFilterModel request);
    ResponseBase<UserLoginResponseModel> UserLogin(UserLoginRequestModel request);
}