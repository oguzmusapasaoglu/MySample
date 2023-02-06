using MyCore.Common.Base;
using MySample.RoleDomain.Libraries.Models;
namespace MySample.RoleDomain.Services.Interfaces;
public interface IPageObjectServices
{
    ResponseBase<PageObjectModel> CreateOrUpdate(RequestBase<PageObjectCreateOrUpdateModel> request);
    ResponseBase<PageObjectModel> ChangeStatus(RequestBase<PageObjectStatusChangeModel> request);
    ResponseBase<IQueryable<PageObjectModel>> GetDataByFilter(RequestBase<PageObjectFilterModel> request);
    ResponseBase<PageObjectModel> GetSingleDataByFilter(RequestBase<PageObjectFilterModel> request);
}