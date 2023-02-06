using System.Linq.Expressions;

using MyCore.Common.ConfigHelper;
using MyCore.Dapper.Interfaces;
using MyCore.LogManager.ExceptionHandling;

using MySample.UserDomain.Data.Interfaces;
using MySample.UserDomain.Libraries.Entities;

namespace MySample.UserDomain.Repositories.RepositoryManager;
public class UserGroupRepository : IUserGroupRepository
{
    private IDbFactory dbFactory;
    private string connectionString => MySampleSettingsConfigModelHelper.GetConnection();
    private List<int> activeEnums = new List<int>
    {
        (int)ActivationStatusEnum.Active,
        (int)ActivationStatusEnum.Locked
    };
    public async Task<UserGroupEntity> Create(UserGroupEntity entity, int requestUserId)
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
    public async Task<UserGroupEntity> Update(UserGroupEntity entity, int requestUserId)
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
    public async Task<IQueryable<UserGroupEntity>> GetAllByFilter(Expression<Func<UserGroupEntity, bool>> filter)
    {
        return await dbFactory.GetAllByFilter(connectionString, filter);
    }
    public async Task<IQueryable<UserGroupEntity>> GetAllActive()
    {
        var result = await dbFactory.GetAll<UserGroupEntity>(connectionString);
        return result.IsNotNullOrEmpty()
            ? new List<UserGroupEntity>().AsQueryable()
            : result.Where(q => activeEnums.Contains(q.ActivationStatus));
    }
    public async Task<UserGroupEntity> GetSingleById(int id)
    {
        return await dbFactory.GetSingleById<UserGroupEntity>(connectionString, id);
    }
}
