﻿using AutoMapper;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.ViewModels.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>()
                .ReverseMap();

            CreateMap<CreateUserCommand, User>();

            CreateMap<UpdateUserCommand, User>();
                
        }
    }
}
