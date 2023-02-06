using MyCore.Common.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySample.UserDomain.Services.Interfaces;
using MySample.UserDomain.Libraries.Models;

[Route("[controller]")]
[ApiController, Authorize]
public class UserRolesController : ControllerBase
{
    private IUsersRolesServices services;
    public UserRolesController(IUsersRolesServices _services)
    {
        services = _services;
    }

    [HttpPost("CreateOrUpdate")]
    public ResponseBase<UsersRolesModel> CreateOrUpdate([FromBody] RequestBase<UsersRolesCreateOrUpdateModel> request)
    {
        return services.CreateOrUpdate(request);
    }

    [HttpPost("BulkCreate")]
    public ResponseBase<UsersRolesModel> BulkCreate([FromBody] RequestBase<List<UsersRolesCreateOrUpdateModel>> request)
    {
        return services.BulkCreate(request);
    }
    [HttpGet("GetDataByUserID")]
    public ResponseBase<IQueryable<UsersRolesModel>> GetDataByUserID(int userID)
    {
        return services.GetDataByUserID(userID);
    }

    [HttpGet("GetDataByRoleID")]
    public ResponseBase<IQueryable<UsersRolesModel>> GetDataByRoleID(int roleID)
    {
        return services.GetDataByUserID(roleID);
    }

}
