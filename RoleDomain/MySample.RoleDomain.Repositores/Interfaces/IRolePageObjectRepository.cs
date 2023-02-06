using MyCore.Dapper.Interfaces;

using MySample.RoleDomain.Libraries.Entities;

using System.Linq.Expressions;

namespace MySample.RoleDomain.Repositores.Interfaces;
public interface IRolePageObjectRepository : ICreateUpdateRepository<RolePageObjectEntity>
{
    Task<IQueryable<RolePageObjectEntity>> GetAllByFilter(Expression<Func<RolePageObjectEntity, bool>> filter);
    Task<IQueryable<RolePageObjectEntity>> GetAllActive();
    Task<RolePageObjectEntity> GetSingleById(int id);
}