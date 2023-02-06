using MyCore.Dapper.Interfaces;

using MySample.UserDomain.Libraries.Entities;

using System.Linq.Expressions;

namespace MySample.UserDomain.Data.Interfaces;

public interface IUserGroupRepository : ICreateUpdateRepository<UserGroupEntity>
{
    Task<IQueryable<UserGroupEntity>> GetAllByFilter(Expression<Func<UserGroupEntity, bool>> filter);
    Task<IQueryable<UserGroupEntity>> GetAllActive();
    Task<UserGroupEntity> GetSingleById(int id);
}