using MyCore.Common.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySample.RoleDomain.Services.Interfaces;
using MySample.RoleDomain.Libraries.Models;

[Route("[controller]")]
[ApiController, Authorize]
public class PageObjectController : ControllerBase
{
    private IPageObjectServices services;
    public PageObjectController(IPageObjectServices _services)
    {
        services = _services;
    }

    [HttpPost("CreateOrUpdate")]
    public ResponseBase<PageObjectModel> CreateOrUpdate(RequestBase<PageObjectCreateOrUpdateModel> request)
    {
        return services.CreateOrUpdate(request);
    }

    [HttpPost("SearchData")]
    public ResponseBase<IQueryable<PageObjectModel>> SearchData(RequestBase<PageObjectFilterModel> request)
    {
        return services.GetDataByFilter(request);
    }

    [HttpPost("SingleData")]
    public ResponseBase<PageObjectModel> SingleData(RequestBase<PageObjectFilterModel> request)
    {
        return services.GetSingleDataByFilter(request);
    }
}
