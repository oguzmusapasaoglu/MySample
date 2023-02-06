using System.Linq.Expressions;
using MyCore.Dapper.Interfaces;
using MyCore.Common.ConfigHelper;
using MyCore.LogManager.ExceptionHandling;
using MySample.RoleDomain.Repositores.Interfaces;
using MySample.RoleDomain.Libraries.Entities;

namespace MySample.RoleDomain.Repositores.RepositoryManager;
public class RolePageObjectRepository : IRolePageObjectRepository
{
    private IDbFactory dbFactory;
    private string connectionString => MySampleSettingsConfigModelHelper.GetConnection();
    private List<int> activeEnums = new List<int>
    {
        (int)ActivationStatusEnum.Active,
        (int)ActivationStatusEnum.Locked
    };
    public async Task<RolePageObjectEntity> Create(RolePageObjectEntity entity, int requestUserId)
    {
        try
        {
            if (!entity.ID.IsNullOrLessOrEqToZero())
                throw new NotificationException(ExceptionMessageHelper.UnauthorizedAccess(requestUserId));

            entity.ActivationStatus = (int)ActivationStatusEnum.Active;
            var returnData = dbFactory.InsertEntity(connectionString, entity);
            entity.ID = returnData.Result.Value;
            return entity;
        }
        catch (Exception ex)
        {
            throw new KnownException(ExceptionTypeEnum.Fattal, ex, ExceptionMessageHelper.UnexpectedSystemError);
        }
    }
    public async Task<RolePageObjectEntity> Update(RolePageObjectEntity entity, int requestUserId)
    {
        try
        {
            if (entity.ID.IsNullOrLessOrEqToZero())
                throw new NotificationException(ExceptionMessageHelper.UnauthorizedAccess(requestUserId));
            dbFactory.UpdateEntity(connectionString, entity);
            return entity;
        }
        catch (Exception ex)
        {
            throw new KnownException(ExceptionTypeEnum.Fattal, ex, ExceptionMessageHelper.UnexpectedSystemError);
        }
    }
    public async Task<IQueryable<RolePageObjectEntity>> GetAllByFilter(Expression<Func<RolePageObjectEntity, bool>> filter)
    {
        return await dbFactory.GetAllByFilter(connectionString, filter);
    }
    public async Task<IQueryable<RolePageObjectEntity>> GetAllActive()
    {
        var result = await dbFactory.GetAll<RolePageObjectEntity>(connectionString);
        return result.DataIsNullOrEmpty()
            ? new List<RolePageObjectEntity>().AsQueryable()
            : result.Where(q => activeEnums.Contains(q.ActivationStatus));
    }
    public async Task<RolePageObjectEntity> GetSingleById(int id)
    {
        return await dbFactory.GetSingleById<RolePageObjectEntity>(connectionString, id);
    }
}
