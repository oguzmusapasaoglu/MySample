using System.Linq.Expressions;
using MyCore.Dapper.Interfaces;
using MyCore.Common.ConfigHelper;
using MyCore.LogManager.ExceptionHandling;
using MySample.RoleDomain.Repositores.Interfaces;
using MySample.RoleDomain.Libraries.Entities;

namespace MySample.RoleDomain.Repositores.RepositoryManager;
public class PageObjectRepository : IPageObjectRepository
{
    private IDbFactory dbFactory;
    private string connectionString => MySampleSettingsConfigModelHelper.GetConnection();
    private List<int> activeEnums = new List<int>
    {
        (int)ActivationStatusEnum.Active,
        (int)ActivationStatusEnum.Locked
    };

    public PageObjectRepository(IDbFactory _dbFactory)
    {
        dbFactory = _dbFactory;
    }
    public async Task<PageObjectEntity> Create(PageObjectEntity entity, int requestUserId)
    {
        try
        {
            if (!entity.ID.IsNullOrLessOrEqToZero())
                throw new NotificationException(ExceptionMessageHelper.UnauthorizedAccess(requestUserId));

            entity.ActivationStatus = (int)ActivationStatusEnum.Active;
            entity.CreatedBy = requestUserId;
            entity.CreatedDate = DateTime.Now;
            var returnData = dbFactory.InsertEntity(connectionString, entity);
            entity.ID = returnData.Result.Value;
            return entity;
        }
        catch (Exception ex)
        {
            throw new KnownException(ExceptionTypeEnum.Fattal, ex, ExceptionMessageHelper.UnexpectedSystemError);
        }
    }
    public async Task<PageObjectEntity> Update(PageObjectEntity entity, int requestUserId)
    {
        try
        {
            if (entity.ID.IsNullOrLessOrEqToZero())
                throw new NotificationException(ExceptionMessageHelper.UnauthorizedAccess(requestUserId));

            entity.UpdateBy = requestUserId;
            entity.UpdateDate = DateTime.Now;
            await dbFactory.UpdateEntity(connectionString, entity);
            return entity;
        }
        catch (Exception ex)
        {
            throw new KnownException(ExceptionTypeEnum.Fattal, ex, ExceptionMessageHelper.UnexpectedSystemError);
        }
    }
    public async Task<IQueryable<PageObjectEntity>> GetAllByFilter(Expression<Func<PageObjectEntity, bool>> filter)
    {
        return await dbFactory.GetAllByFilter(connectionString, filter);
    }
    public async Task<IQueryable<PageObjectEntity>> GetAllActive()
    {
        var result = await dbFactory.GetAll<PageObjectEntity>(connectionString);
        return result.IsNotNullOrEmpty()
            ? new List<PageObjectEntity>().AsQueryable()
            : result.Where(q => activeEnums.Contains(q.ActivationStatus));
    }
    public async Task<PageObjectEntity> GetSingleById(int userID)
    {
        return await dbFactory.GetSingleById<PageObjectEntity>(connectionString, userID);
    }
}
