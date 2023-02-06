using AutoMapper;

using MySample.UserDomain.Libraries.Entities;
using MySample.UserDomain.Libraries.Models;

namespace Helper.Maps;

public class UsersMapProfile : Profile
{
    public UsersMapProfile()
    {
        #region User Info
        CreateMap<UserInfoModel, UserInfoEntity>()
           .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => Enum.GetName(typeof(ActivationStatusEnum), s.ActivationStatusName)))
           .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => s.ActivationStatusData))
           .ReverseMap();

        CreateMap<UserInfoCreateModel, UserInfoEntity>();
        CreateMap<UserInfoUpdateModel, UserInfoEntity>();

        CreateMap<UserInfoListModel, UserInfoEntity>()
            .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => Enum.GetName(typeof(ActivationStatusEnum), s.ActivationStatusName)));
        #endregion

        #region UserGroup
        CreateMap<UserGroupModel, UserGroupEntity>()
            .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => Enum.GetName(typeof(ActivationStatusEnum), s.ActivationStatusName)))
            .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => s.ActivationStatusData))
            .ReverseMap();

        CreateMap<UserGroupCreateOrUpdateModel, UserGroupEntity>();
        #endregion

        #region UsersRoles
        CreateMap<UsersRolesModel, UsersRolesEntity>()
          .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => Enum.GetName(typeof(ActivationStatusEnum), s.ActivationStatusName)))
          .ForMember(d => d.ActivationStatus, o => o.MapFrom(s => s.ActivationStatusData))
          .ReverseMap();

        CreateMap<UsersRolesCreateOrUpdateModel, UsersRolesEntity>();
        #endregion
    }
}
