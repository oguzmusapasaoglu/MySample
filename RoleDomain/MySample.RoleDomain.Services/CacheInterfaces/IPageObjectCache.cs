using MyCore.Cache.Interfaces;
using MySample.RoleDomain.Libraries.Models;
namespace MySample.RoleDomain.Services.CacheInterfaces;
public interface IPageObjectCache : ICacheManager<PageObjectModel>
{
    IQueryable<PageObjectModel> GetDataByPageId(int? pageID);
}
