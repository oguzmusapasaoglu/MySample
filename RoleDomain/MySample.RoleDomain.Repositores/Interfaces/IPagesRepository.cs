using MyCore.Dapper.Interfaces;

using MySample.RoleDomain.Libraries.Entities;

using System.Linq.Expressions;
namespace MySample.RoleDomain.Repositores.Interfaces;
public interface IPagesRepository : ICreateUpdateRepository<PagesEntity>
{
    Task<PagesEntity> GetSingleById(int userID);
    Task<IQueryable<PagesEntity>> GetAllByFilter(Expression<Func<PagesEntity, bool>> filter);
    Task<IQueryable<PagesEntity>> GetAllActive();
    Task<IQueryable<UserRolesPagesRepository>> GetPagesByUserID(int userID);
}