using MyCore.Common.Base;
using MySample.RoleDomain.Libraries.Models;

namespace MySample.RoleDomain.Services.Interfaces
{
    public interface IRolePageServices
    {
        ResponseBase<RolePageListModel> CreateOrUpdate(RequestBase<RolePageModel> request);
        Task<ResponseBase<IQueryable<RolePageListModel>>> GetDataByFilter(RequestBase<RolePageModel> request);
        Task<IQueryable<RolePageListModel>> GetRolePagesByRoleID(int roleID);
        Task<IQueryable<RolePageListModel>> GetRolePagesByPageID(int pageID);
        Task<ResponseBase<RolePageListModel>> GetSingleDataByID(int id);
        Task<IQueryable<RolePageListModel>> GetRolePagesByUserID(int userID);
    }
}
