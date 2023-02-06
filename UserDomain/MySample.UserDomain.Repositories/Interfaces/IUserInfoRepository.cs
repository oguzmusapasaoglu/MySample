using MyCore.Dapper.Interfaces;

using MySample.UserDomain.Libraries.Entities;

using System.Linq.Expressions;

namespace MySample.UserDomain.Data.Interfaces;
public interface IUserInfoRepository : ICreateUpdateRepository<UserInfoEntity>
{
    Task<UserInfoEntity> GetSingleById(int userID);
    UserInfoEntity? GetUserInfoLogin(string userName, string email);
    Task<IQueryable<UserInfoEntity>> GetAllByFilter(Expression<Func<UserInfoEntity, bool>> filter);
    Task<IQueryable<UserInfoEntity>> GetAllActive();
}