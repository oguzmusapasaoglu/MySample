using AutoMapper;

using MySample.RoleDomain.Libraries.Entities;
using MySample.RoleDomain.Libraries.Models;

namespace Helper.Maps;

public class RolesMapProfile : Profile
{
    public RolesMapProfile()
    {
        #region Roles
        CreateMap<RolesModel, RolesEntity>()
           .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => Enum.GetName(typeof(ActivationStatusEnum), s.ActivationStatusName)))
           .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => s.ActivationStatusData))
           .ReverseMap();

        CreateMap<RolesCreateOrUpdateModel, RolesEntity>();
        CreateMap<RolesListModel, RolesEntity>()
            .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => Enum.GetName(typeof(ActivationStatusEnum), s.ActivationStatusName)));
        #endregion

        #region RolePage
        CreateMap<RolePageListModel, RolePageEntity>().ReverseMap();
        #endregion

        #region RolePageObject
        CreateMap<RolePageObjectModel, RolePageObjectEntity>()
           .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => Enum.GetName(typeof(ActivationStatusEnum), s.ActivationStatusName)))
           .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => s.ActivationStatusData))
           .ReverseMap();

        CreateMap<RolePageObjectCreateOrUpdateModel, RolePageObjectEntity>();
        #endregion

        #region Pages
        CreateMap<PagesModel, PagesEntity>()
           .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => Enum.GetName(typeof(ActivationStatusEnum), s.ActivationStatusName)))
           .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => s.ActivationStatusData))
           .ReverseMap();

        CreateMap<PagesCreateOrUpdateModel, PagesEntity>();
        #endregion

        #region PageObject
        CreateMap<PageObjectModel, PageObjectEntity>()
           .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => Enum.GetName(typeof(ActivationStatusEnum), s.ActivationStatusName)))
           .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => s.ActivationStatusData))
           .ReverseMap();

        CreateMap<PageObjectCreateOrUpdateModel, PageObjectEntity>();
        #endregion
    }
}
