using MyCore.Dapper.Interfaces;

using MySample.RoleDomain.Libraries.Entities;

using System.Linq.Expressions;

namespace MySample.RoleDomain.Repositores.Interfaces;

public interface IRolesRepository : ICreateUpdateRepository<RolesEntity>
{
    Task<RolesEntity> GetSingleById(int ID);
    Task<IQueryable<RolesEntity>> GetAllByFilter(Expression<Func<RolesEntity, bool>> filter);
    Task<IQueryable<RolesEntity>> GetAllActive();
}
