using MyCore.Dapper.Interfaces;

using MySample.UserDomain.Libraries.Entities;

using System.Linq.Expressions;

namespace MySample.UserDomain.Data.Interfaces;
public interface IUsersRolesRepository : ICreateUpdateRepository<UsersRolesEntity>
{
    Task<bool> BulkCreate(List<UsersRolesEntity> request);
    Task<IQueryable<UsersRolesEntity>> GetAllByFilter(Expression<Func<UsersRolesEntity, bool>> filter);
    Task<IQueryable<UsersRolesEntity>> GetAllActive();
    Task<UsersRolesEntity> GetSingleById(int id);
}