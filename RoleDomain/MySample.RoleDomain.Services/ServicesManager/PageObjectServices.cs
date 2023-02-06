using System.Linq.Expressions;
using MySample.RoleDomain.Services.CacheInterfaces;
using MyCore.LogManager.ExceptionHandling;
using MySample.RoleDomain.Services.Interfaces;
using MyCore.Common.Base;
using MySample.RoleDomain.Libraries.Models;
using MySample.RoleDomain.Libraries.Entities;
using MySample.RoleDomain.Repositores.Interfaces;
using Helper.Validations.Interfaces;
using Helper.Maps;
using MyCore.CommonHelper;

namespace MySample.RoleDomain.Services.ServicesManager;
public class PageObjectServices : IPageObjectServices
{
    #region private
    private IPageObjectRepository PageObjectRepository;
    private IPageObjectCache cache;
    private IPagesCache pageCache;
    private IValidateManager validate;
    #endregion

    #region Ctor
    public PageObjectServices(
        IPageObjectRepository _PageObjectRepository,
        IPageObjectCache _cache,
        IPagesCache _pageCache,
        IValidateManager _validate)
    {
        PageObjectRepository = _PageObjectRepository;
        cache = _cache;
        pageCache = _pageCache;
        validate = _validate;
    }
    #endregion

    #region Methods
    public ResponseBase<PageObjectModel> CreateOrUpdate(RequestBase<PageObjectCreateOrUpdateModel> request)
    {
        var rData = request.RequestData;
        if (DataValidation(rData.PageObjectName, rData.ID).IsNotNullOrEmpty())
            return new ResponseBase<PageObjectModel>
            {
                Message = ExceptionMessageHelper.IsInUse("Page Object"),
                Result = ResultEnum.Warning
            };

        var entity = MapperInstance.Instance.Map<PageObjectCreateOrUpdateModel, PageObjectEntity>(rData);

        var validateResult = validate.RolesValidate(entity).Result;
        if (validateResult.Any())
            throw new NotificationException(validateResult, ExceptionTypeEnum.Validation);

        var result = (rData.ID.HasValue)
            ? PageObjectRepository.Update(entity, request.RequestUserId)
            : PageObjectRepository.Create(entity, request.RequestUserId);

        if (result.IsCompletedSuccessfully && !result.Id.IsNullOrLessOrEqToZero())
        {
            var returnModel = MapperInstance.Instance.Map<PageObjectEntity, PageObjectModel>(result.Result);
            cache.AddSingleData(returnModel);
            return new ResponseBase<PageObjectModel>
            {
                Data = returnModel,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<PageObjectModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<PageObjectModel> ChangeStatus(RequestBase<PageObjectStatusChangeModel> request)
    {
        var rData = request.RequestData;
        if (rData.ID.IsNullOrLessOrEqToZero())
            return ResponseHelper.ReturnErrorResponse<PageObjectModel>
                (ExceptionMessageHelper.RequiredField("ID"), ResultEnum.Warning);
        var predicate = PredicateBuilderHelper.False<PageObjectModel>();
        predicate = predicate.And(q => q.ID == rData.ID);
        predicate = predicate.And(q => q.ActivationStatus == rData.ActivationStatus);

        var model = cache.GetSingleDataByFilter(predicate);
        model.ActivationStatus = rData.ActivationStatus;
        var entity = MapperInstance.Instance.Map<PageObjectModel, PageObjectEntity>(model);
        var result = PageObjectRepository.Update(entity, request.RequestUserId);
        if (result.IsNotNullOrEmpty())
        {
            cache.ReFillCache();
            return new ResponseBase<PageObjectModel>
            {
                Data = model,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<PageObjectModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<IQueryable<PageObjectModel>> GetDataByFilter(RequestBase<PageObjectFilterModel> request)
    {
        var data = cache.GetAllData();
        var response = data.Where(GetPredicate(request.RequestData));
        return new ResponseBase<IQueryable<PageObjectModel>>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }
    public ResponseBase<PageObjectModel> GetSingleDataByFilter(RequestBase<PageObjectFilterModel> request)
    {
        var data = cache.GetAllData();
        var response = data.FirstOrDefault(GetPredicate(request.RequestData));
        var page = pageCache.GetSingleDataById(response.PageID);
        if (page != null)
            response.Page = page;

        return new ResponseBase<PageObjectModel>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }
    private Expression<Func<PageObjectModel, bool>> GetPredicate(PageObjectFilterModel request)
    {
        var predicate = PredicateBuilderHelper.False<PageObjectModel>();
        if (!request.ActivationStatus.HasValue)
            predicate = predicate.And(q => q.ActivationStatus == (int)ActivationStatusEnum.Active);
        else
            predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);

        predicate = predicate.And(q => q.PageObjectName.ToLower().Contains(request.PageObjectName.ToLower()));
        return predicate;
    }
    private string DataValidation(string pageObjectName, int? id)
    {
        var data = cache.GetAllData();
        var predicate = PredicateBuilderHelper.False<PageObjectModel>();
        predicate = predicate.And(q => q.PageObjectName.ToLower() == pageObjectName.ToLower());
        predicate = predicate.And(q => q.ActivationStatus == (int)ActivationStatusEnum.Active);
        var result = data.FirstOrDefault(predicate);
        if (result != null)
        {
            if (id.HasValue && id == result.ID)
                return string.Empty;
            return ExceptionMessageHelper.IsInUse("Page Object Name");
        }
        return string.Empty;
    }
    #endregion
}
