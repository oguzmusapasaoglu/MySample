using MyCore.Dapper.Interfaces;

using MySample.RoleDomain.Libraries.Entities;

using System.Linq.Expressions;

namespace MySample.RoleDomain.Repositores.Interfaces;
public interface IPageObjectRepository : ICreateUpdateRepository<PageObjectEntity>
{
    Task<IQueryable<PageObjectEntity>> GetAllByFilter(Expression<Func<PageObjectEntity, bool>> filter);
    Task<IQueryable<PageObjectEntity>> GetAllActive();
    Task<PageObjectEntity> GetSingleById(int id);
}