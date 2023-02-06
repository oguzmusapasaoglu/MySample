using MyCore.Common.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySample.RoleDomain.Services.Interfaces;
using MySample.RoleDomain.Libraries.Models;


[Route("[controller]")]
[ApiController, Authorize]
public class PagesController : ControllerBase
{
    private IPagesServices services;
    public PagesController(IPagesServices _services)
    {
        services = _services;
    }

    [HttpPost("CreateOrUpdate")]
    public ResponseBase<PagesModel> CreateOrUpdate(RequestBase<PagesCreateOrUpdateModel> request)
    {
        return services.CreateOrUpdate(request);
    }

    [HttpPost("SearchData")]
    public ResponseBase<IQueryable<PagesModel>> SearchData(RequestBase<PagesFilterModel> request)
    {
        return services.GetDataByFilter(request);
    }

    [HttpPost("SingleData")]
    public ResponseBase<PagesModel> SingleData(RequestBase<PagesFilterModel> request)
    {
        return services.GetSingleDataByFilter(request);
    }
}
