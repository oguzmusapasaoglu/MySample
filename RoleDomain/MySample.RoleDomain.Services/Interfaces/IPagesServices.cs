using MyCore.Common.Base;

using MySample.RoleDomain.Libraries.Entities;
using MySample.RoleDomain.Libraries.Models;

namespace MySample.RoleDomain.Services.Interfaces;
public interface IPagesServices
{
    ResponseBase<PagesModel> CreateOrUpdate(RequestBase<PagesCreateOrUpdateModel> request);
    ResponseBase<PagesModel> ChangeStatus(RequestBase<PagesStatusChangeModel> request);
    ResponseBase<IQueryable<PagesModel>> GetDataByFilter(RequestBase<PagesFilterModel> request);
    ResponseBase<PagesModel> GetSingleDataByFilter(RequestBase<PagesFilterModel> request);
    Task<IQueryable<UserRolesPagesRepository>> GetPagesByUserID(int userID);
}