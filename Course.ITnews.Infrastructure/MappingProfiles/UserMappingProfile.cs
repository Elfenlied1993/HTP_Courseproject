using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Course.ITnews.Data.Contracts.Entities;
using Course.ITnews.Domain.Contracts.ViewModels;

namespace Course.ITnews.Infrastructure.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            MapUserToUserViewModel();
            MapUserViewModelToUser();
        }

        private void MapUserToUserViewModel()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccessFailedCount, c => c.MapFrom(src => src.AccessFailedCount))
                .ForMember(dest => dest.ConcurrencyStamp, c => c.MapFrom(src => src.ConcurrencyStamp))
                .ForMember(dest => dest.Email, c => c.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmailConfirmed, c => c.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.LockoutEnabled, c => c.MapFrom(src => src.LockoutEnabled))
                .ForMember(dest => dest.LockoutEnd, c => c.MapFrom(src => src.LockoutEnd))
                .ForMember(dest => dest.NormalizedEmail, c => c.MapFrom(src => src.NormalizedEmail))
                .ForMember(dest => dest.NormalizedUserName, c => c.MapFrom(src => src.NormalizedUserName))
                .ForMember(dest => dest.PasswordHash, c => c.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.PhoneNumber, c => c.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PhoneNumberConfirmed, c => c.MapFrom(src => src.PhoneNumberConfirmed))
                .ForMember(dest => dest.TwoFactorEnabled, c => c.MapFrom(src => src.TwoFactorEnabled))
                .ForMember(dest => dest.SecurityStamp, c => c.MapFrom(src => src.SecurityStamp))
                .ForMember(dest => dest.UserName, c => c.MapFrom(src => src.UserName))
                .ForAllOtherMembers(c=>c.Ignore());
        }

        private void MapUserViewModelToUser()
        {
            CreateMap<UserViewModel,User>()
                .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccessFailedCount, c => c.MapFrom(src => src.AccessFailedCount))
                .ForMember(dest => dest.ConcurrencyStamp, c => c.MapFrom(src => src.ConcurrencyStamp))
                .ForMember(dest => dest.Email, c => c.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmailConfirmed, c => c.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.LockoutEnabled, c => c.MapFrom(src => src.LockoutEnabled))
                .ForMember(dest => dest.LockoutEnd, c => c.MapFrom(src => src.LockoutEnd))
                .ForMember(dest => dest.NormalizedEmail, c => c.MapFrom(src => src.NormalizedEmail))
                .ForMember(dest => dest.NormalizedUserName, c => c.MapFrom(src => src.NormalizedUserName))
                .ForMember(dest => dest.PasswordHash, c => c.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.PhoneNumber, c => c.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PhoneNumberConfirmed, c => c.MapFrom(src => src.PhoneNumberConfirmed))
                .ForMember(dest => dest.TwoFactorEnabled, c => c.MapFrom(src => src.TwoFactorEnabled))
                .ForMember(dest => dest.SecurityStamp, c => c.MapFrom(src => src.SecurityStamp))
                .ForMember(dest => dest.UserName, c => c.MapFrom(src => src.UserName))
                .ForAllOtherMembers(c => c.Ignore());
        }
    }
}
