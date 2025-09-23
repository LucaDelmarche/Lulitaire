using System.Text;
using Application.Core.User.Queries.GetById;
using Application.Core.User.Queries.GetByUsernameOrMail;
using Application.Core.User.Queries.GetByUsernameOrMailAndPassword;
using Application.user.commands.create;
using Application.user.commands.put;
using Application.user.queries.getAll;
using AutoMapper;
using Domain;
using Infrastructure.Entities;

namespace Lulitaire;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Mapping pour transformer un user en dbuser en inversement
        CreateMap<DbUser, User>();
        CreateMap<User, DbUser>();

        CreateMap<DbUser, UserGetByUsernameOrMailOutput>();
        CreateMap<UserGetByUsernameOrMailOutput,DbUser>();
        
        CreateMap<DbUser, UserGetCurrentUserOutput>();
        CreateMap<UserGetCurrentUserOutput,DbUser>();

        CreateMap<DbUser, UserGetCurrentUserOutput.CurrentUserDto>();
        CreateMap<UserGetCurrentUserOutput.CurrentUserDto,DbUser>();
        
        CreateMap<DbUser, UserGetByIdOutput>();
        CreateMap<UserGetByIdOutput,DbUser>();
        CreateMap<DbUser, UserGetByIdOutput.UserGetByIdDto>();
        CreateMap<UserGetByIdOutput.UserGetByIdDto,DbUser>();
        
        
        CreateMap<DbUser, UserGetByUsernameOrMailAndPasswordOutput>();
        CreateMap<UserGetByUsernameOrMailAndPasswordOutput,DbUser>();
        CreateMap<DbUser, UserGetByUsernameOrMailAndPasswordOutput.UserGetBytUsernameDto>();
        CreateMap<UserGetByUsernameOrMailAndPasswordOutput.UserGetBytUsernameDto,DbUser>();
        CreateMap<DbUser, UserGetByUsernameOrMailOutput.UserGetByUsernameDto>();
        CreateMap<UserGetByUsernameOrMailOutput.UserGetByUsernameDto,DbUser>();
        
        //Mapping utile pour la création d'utilisateurs
        CreateMap<UserCreateOutput.UserCreateDto, DbUser>();
        CreateMap<DbUser, UserCreateOutput.UserCreateDto>();
        CreateMap<DbUser, UserCreateOutput>();
        CreateMap<UserCreateCommand, DbUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Password)));;

        //Mapping utile pour la modification d'un utilisateur
        CreateMap<UserPutCommand, DbUser>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Password)));
        CreateMap<UserPutCommand, User>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => System.Text.Encoding.UTF8.GetBytes(src.Password)));;

        //Mapping utile pour l'affichage de tous les utilisateurs
        CreateMap<DbUser, UserGetallOutput.UserDto>();
        CreateMap<UserGetallOutput.UserDto,DbUser>();
        


    }
    
    
    
}