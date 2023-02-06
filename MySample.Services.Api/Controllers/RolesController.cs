using MyCore.Common.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySample.RoleDomain.Services.Interfaces;
using MySample.RoleDomain.Libraries.Models;


[Route("[controller]")]
[ApiController, Authorize]
public class RolesController : ControllerBase
{
    private IRolesServices services;
    public RolesController(IRolesServices _services)
    {
        services = _services;
    }       

    [HttpPost("CreateOrUpdate")]
    public ResponseBase<RolesModel> CreateOrUpdate(RequestBase<RolesCreateOrUpdateModel> request)
    {
        return services.CreateOrUpdate(request);
    }

    [HttpPost("SearchData")]
    public ResponseBase<IQueryable<RolesModel>> SearchData(RequestBase<RolesFilterModel> request)
    {
        return services.GetDataByFilter(request);
    }

    [HttpPost("SingleData")]
    public ResponseBase<RolesModel> SingleData(RequestBase<RolesFilterModel> request)
    {
        return services.GetSingleDataByFilter(request);
    }
}
