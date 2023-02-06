using System.Linq.Expressions;

using MyCore.Common.ConfigHelper;
using MyCore.Dapper.Interfaces;
using MyCore.LogManager.ExceptionHandling;

using MySample.UserDomain.Data.Interfaces;
using MySample.UserDomain.Libraries.Entities;

namespace MySample.UserDomain.Repositories.RepositoryManager;
public class UsersRolesRepository : IUsersRolesRepository
{
    private IDbFactory dbFactory;
    private string connectionString => MySampleSettingsConfigModelHelper.GetConnection();
    public async Task<UsersRolesEntity> Create(UsersRolesEntity entity, int requestUserId)
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
    public async Task<UsersRolesEntity> Update(UsersRolesEntity entity, int requestUserId)
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
    public async Task<IQueryable<UsersRolesEntity>> GetAllByFilter(Expression<Func<UsersRolesEntity, bool>> filter)
    {
        return await dbFactory.GetAllByFilter(connectionString, filter);
    }
    public async Task<IQueryable<UsersRolesEntity>> GetAllActive()
    {
        var result = await dbFactory.GetAll<UsersRolesEntity>(connectionString);
        return result.IsNotNullOrEmpty()
            ? new List<UsersRolesEntity>().AsQueryable()
            : result.Where(q => q.ActivationStatus == (int)ActivationStatusEnum.Active);
    }
    public async Task<UsersRolesEntity> GetSingleById(int id)
    {
        return await dbFactory.GetSingleById<UsersRolesEntity>(connectionString, id);
    }
    public async Task<bool> BulkCreate(List<UsersRolesEntity> request)
    {
        return await dbFactory.InsertBulkEntity(connectionString, request);
    }
}
