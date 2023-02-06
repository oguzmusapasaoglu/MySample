using System.Linq.Expressions;
using MyCore.Dapper.Interfaces;
using MyCore.Common.ConfigHelper;
using MyCore.LogManager.ExceptionHandling;
using MySample.RoleDomain.Repositores.Interfaces;
using MySample.RoleDomain.Libraries.Entities;

namespace MySample.RoleDomain.Repositores.RepositoryManager;

public class RolesRepository : IRolesRepository
{
    private IDbFactory dbFactory;
    private string connectionString => MySampleSettingsConfigModelHelper.GetConnection();
    public RolesRepository(IDbFactory _dbFactory)
    {
        dbFactory = _dbFactory;
    }

    public async Task<RolesEntity> Create(RolesEntity entity, int requestUserId)
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
    public async Task<RolesEntity> Update(RolesEntity entity, int requestUserId)
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
    public async Task<IQueryable<RolesEntity>> GetAllByFilter(Expression<Func<RolesEntity, bool>> filter)
    {
        return await dbFactory.GetAllByFilter(connectionString, filter);
    }
    public async Task<IQueryable<RolesEntity>> GetAllActive()
    {
        var result = await dbFactory.GetAll<RolesEntity>(connectionString);
        return result.IsNotNullOrEmpty()
            ? new List<RolesEntity>().AsQueryable()
            : result.Where(q => q.ActivationStatus == (int)ActivationStatusEnum.Active);
    }
    public async Task<RolesEntity> GetSingleById(int id)
    {
        return await dbFactory.GetSingleById<RolesEntity>(connectionString, id);
    }
}
