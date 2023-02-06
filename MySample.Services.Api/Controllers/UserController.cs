using MyCore.Common.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySample.UserDomain.Services.Interfaces;
using MySample.UserDomain.Libraries.Models;

[Route("[controller]")]
[ApiController, Authorize]
public class UserController : ControllerBase
{
    private IUserInfoServices services;
    public UserController(IUserInfoServices _services)
    {
        services = _services;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public ResponseBase<UserLoginResponseModel> UserLogin([FromBody] UserLoginRequestModel model)
    {
        return services.UserLogin(model);
    }

    [HttpPost("Create")]
    public ResponseBase<UserInfoModel> CreateUserInfo([FromBody] RequestBase<UserInfoCreateModel> request)
    {
        return services.Create(request);
    }

    [HttpPost("Update")]
    public ResponseBase<UserInfoModel> UpdateUserInfo([FromBody] RequestBase<UserInfoUpdateModel> request)
    {
        return services.Update(request);
    }

    [HttpPost("ChangePassword")]
    public ResponseBase<UserInfoModel> ChangePassword([FromBody] UserInfoChangePasswordModel request)
    {
        return services.ChangePassword(request);
    }

    [HttpPost("ChangeStatus")]
    public ResponseBase<UserInfoModel> ChangeStatus(RequestBase<UserInfoStatusChangeModel> request)
    {
        return services.ChangeStatus(request);
    }

    [HttpPost("SearchData")]
    public ResponseBase<IQueryable<UserInfoModel>> SearchData(UserInfoFilterModel request)
    {
        return services.GetDataByFilter(request);
    }

    [HttpPost("SingleData")]
    public ResponseBase<UserInfoModel> SingleData(UserInfoFilterModel request)
    {
        return services.GetSingleDataByFilter(request);
    }

}
