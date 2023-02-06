using MyCore.Common.Base;
using MySample.RoleDomain.Libraries.Models;

namespace MySample.RoleDomain.Services.Interfaces;
public interface IRolesServices
{
    ResponseBase<RolesModel> CreateOrUpdate(RequestBase<RolesCreateOrUpdateModel> request);
    ResponseBase<RolesModel> ChangeStatus(RequestBase<RolesStatusChangeModel> request);
    ResponseBase<IQueryable<RolesModel>> GetDataByFilter(RequestBase<RolesFilterModel> request);
    ResponseBase<RolesModel> GetSingleDataByFilter(RequestBase<RolesFilterModel> request);
}