using System.Linq.Expressions;

using MyCore.Common.ConfigHelper;
using MyCore.Dapper.Interfaces;
using MyCore.LogManager.ExceptionHandling;

using MySample.UserDomain.Data.Interfaces;
using MySample.UserDomain.Libraries.Entities;

namespace MySample.UserDomain.Repositories.RepositoryManager;
public class UserInfoRepository : IUserInfoRepository
{
    private IDbFactory dbFactory;
    private string connectionString => MySampleSettingsConfigModelHelper.GetConnection();
    private List<int> activeEnums = new List<int>
        {
            (int)ActivationStatusEnum.Active,
            (int)ActivationStatusEnum.Locked
        };

    public UserInfoRepository(IDbFactory _dbFactory)
    {
        dbFactory = _dbFactory;
    }

    public async Task<UserInfoEntity> Create(UserInfoEntity entity, int requestUserId)
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

    public async Task<UserInfoEntity> Update(UserInfoEntity entity, int requestUserId)
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

    public async Task<IQueryable<UserInfoEntity>> GetAllByFilter(Expression<Func<UserInfoEntity, bool>> filter)
    {
        return await dbFactory.GetAllByFilter(connectionString, filter);
    }
    public async Task<IQueryable<UserInfoEntity>> GetAllActive()
    {
        var result = await dbFactory.GetAll<UserInfoEntity>(connectionString);
        return result.IsNotNullOrEmpty()
            ? new List<UserInfoEntity>().AsQueryable()
            : result.Where(q => activeEnums.Contains(q.ActivationStatus));
    }
    public async Task<UserInfoEntity> GetSingleById(int userID)
    {
        return await dbFactory.GetSingleById<UserInfoEntity>(connectionString, userID);
    }

    public UserInfoEntity? GetUserInfoLogin(string userName, string email)
    {
        var data = dbFactory.GetAll<UserInfoEntity>(connectionString).Result;
        if (data.Any())
        {
            var result = data.FirstOrDefault(q => activeEnums.Contains(q.ActivationStatus)
            || q.UserName.ToLower() == userName.ToLower()
            || q.EMail.ToLower() == email.ToLower());
            return result;
        }
        return null;
    }
}

