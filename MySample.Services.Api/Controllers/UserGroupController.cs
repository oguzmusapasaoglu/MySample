using MyCore.Common.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySample.UserDomain.Services.Interfaces;
using MySample.UserDomain.Libraries.Models;

[Route("[controller]")]
[ApiController, Authorize]
public class UserGroupController : ControllerBase
{
    private IUserGroupServices services;

    public UserGroupController(IUserGroupServices _services)
    {
        services = _services;
    }

    [HttpPost("CreateOrUpdate")]
    public ResponseBase<UserGroupModel> CreateOrUpdateUserGroup([FromBody] RequestBase<UserGroupCreateOrUpdateModel> request)
    {
        return services.CreateOrUpdate(request);
    }

    [HttpPost("Search")]
    public ResponseBase<IQueryable<UserGroupModel>> Search(UserGroupFilterModel request)
    {
        return services.GetDataByFilter(request);
    }

    [HttpPost("SingleData")]
    public ResponseBase<UserGroupModel> GetSingleDataByFilter(UserGroupFilterModel request)
    {
        return services.GetSingleDataByFilter(request);
    }
}
