using MyCore.Dapper.Interfaces;

using MySample.RoleDomain.Libraries.Entities;

namespace MySample.RoleDomain.Repositores.Interfaces;

public interface IRolePageRepository : ICreateUpdateRepository<RolePageEntity>
{
    Task<IQueryable<RolePageEntity>> GetAllRolePage();
    Task<IQueryable<RolePageEntity>> GetRolePagesByUserID(int userID);
    Task<RolePageEntity> GetSingleRolePageByID(int id);
}
