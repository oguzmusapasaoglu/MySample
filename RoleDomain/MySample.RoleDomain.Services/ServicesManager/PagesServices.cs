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

public class PagesServices : IPagesServices
{
    #region private
    private IPagesRepository pagesRepository;
    private IPagesCache cache;
    private IPageObjectCache pageObjectCache;
    private IValidateManager validate;
    #endregion

    #region Ctor
    public PagesServices(
        IPagesRepository _pagesRepository,
        IPagesCache _cache,
        IPageObjectCache _pageObjectCache,
        IValidateManager _validate)
    {
        pagesRepository = _pagesRepository;
        cache = _cache;
        pageObjectCache = _pageObjectCache;
        validate = _validate;
    }
    #endregion

    #region Methods
    public ResponseBase<PagesModel> CreateOrUpdate(RequestBase<PagesCreateOrUpdateModel> request)
    {
        var rData = request.RequestData;
        if (DataValidation(rData.PagesName, rData.ID).IsNotNullOrEmpty())
            return new ResponseBase<PagesModel>
            {
                Message = ExceptionMessageHelper.IsInUse("Pages"),
                Result = ResultEnum.Warning
            };

        var entity = MapperInstance.Instance.Map<PagesCreateOrUpdateModel, PagesEntity>(rData);

        var validateResult = validate.RolesValidate(entity).Result;
        if (validateResult.Any())
            throw new NotificationException(validateResult, ExceptionTypeEnum.Validation);

        var result = (rData.ID.HasValue)
            ? pagesRepository.Update(entity, request.RequestUserId)
            : pagesRepository.Create(entity, request.RequestUserId);

        if (result.IsCompletedSuccessfully && !result.Id.IsNullOrLessOrEqToZero())
        {
            var returnModel = MapperInstance.Instance.Map<PagesEntity, PagesModel>(result.Result);
            cache.AddSingleData(returnModel);
            return new ResponseBase<PagesModel>
            {
                Data = returnModel,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<PagesModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<PagesModel> ChangeStatus(RequestBase<PagesStatusChangeModel> request)
    {
        var rData = request.RequestData;
        if (rData.ID.IsNullOrLessOrEqToZero())
            return ResponseHelper.ReturnErrorResponse<PagesModel>
                (ExceptionMessageHelper.RequiredField("ID"), ResultEnum.Warning);
        var predicate = PredicateBuilderHelper.False<PagesModel>();
        predicate = predicate.And(q => q.ID == rData.ID);
        predicate = predicate.And(q => q.ActivationStatus == rData.ActivationStatus);

        var model = cache.GetSingleDataByFilter(predicate);
        model.ActivationStatus = rData.ActivationStatus;
        var entity = MapperInstance.Instance.Map<PagesModel, PagesEntity>(model);
        var result = pagesRepository.Update(entity, request.RequestUserId);
        if (result.IsNotNullOrEmpty())
        {
            cache.ReFillCache();
            return new ResponseBase<PagesModel>
            {
                Data = model,
                Result = ResultEnum.Success
            };
        }
        return new ResponseBase<PagesModel>
        {
            Message = ExceptionMessageHelper.UnexpectedSystemError,
            Result = ResultEnum.Error
        };
    }
    public ResponseBase<IQueryable<PagesModel>> GetDataByFilter(RequestBase<PagesFilterModel> request)
    {
        var data = cache.GetAllData();
        var response = data.Where(GetPredicate(request.RequestData));
        return new ResponseBase<IQueryable<PagesModel>>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }
    public ResponseBase<PagesModel> GetSingleDataByFilter(RequestBase<PagesFilterModel> request)
    {
        var data = cache.GetAllData();
        var response = data.FirstOrDefault(GetPredicate(request.RequestData));
        var pageObjects = pageObjectCache.GetDataByPageId(request.RequestData.Id);
        if (pageObjects.Any())
            response.PagesObjects = pageObjects;

        return new ResponseBase<PagesModel>
        {
            Data = response,
            Result = ResultEnum.Success
        };
    }
    public Task<IQueryable<UserRolesPagesRepository>> GetPagesByUserID(int userID)
    {
        return pagesRepository.GetPagesByUserID(userID);
    }
    private Expression<Func<PagesModel, bool>> GetPredicate(PagesFilterModel request)
    {
        var predicate = PredicateBuilderHelper.False<PagesModel>();
        if (!request.ActivationStatus.HasValue)
            predicate = predicate.And(q => q.ActivationStatus == (int)ActivationStatusEnum.Active);
        else
            predicate = predicate.And(q => q.ActivationStatus == request.ActivationStatus);

        predicate = predicate.And(q => q.PageName.ToLower().Contains(request.PageName.ToLower()));
        return predicate;
    }
    private string DataValidation(string pageName, int? id)
    {
        var data = cache.GetAllData();
        var predicate = PredicateBuilderHelper.False<PagesModel>();
        predicate = predicate.And(q => q.PageName.ToLower() == pageName.ToLower());
        predicate = predicate.And(q => q.ActivationStatus == (int)ActivationStatusEnum.Active);
        var result = data.FirstOrDefault(predicate);
        if (result != null)
        {
            if (id.HasValue && id == result.ID)
                return string.Empty;
            return ExceptionMessageHelper.IsInUse("Pages");
        }
        return string.Empty;
    }
    #endregion
}
