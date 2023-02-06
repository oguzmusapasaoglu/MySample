using MyCore.Common.Base;
using MySample.RoleDomain.Libraries.Models;
namespace MySample.RoleDomain.Services.Interfaces;
public interface IRolePageObjectServices
{
    ResponseBase<RolePageObjectModel> CreateOrUpdate(RequestBase<RolePageObjectCreateOrUpdateModel> request);
    ResponseBase<IQueryable<RolePageObjectModel>> GetDataByFilter(RequestBase<RolePageObjectFilterModel >request);
    ResponseBase<RolePageObjectModel> GetSingleDataByFilter(RequestBase<RolePageObjectFilterModel> request);
}