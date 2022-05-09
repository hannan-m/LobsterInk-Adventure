using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LobsterInk.Application.Adventures.Models;
using LobsterInk.Application.Adventures.ViewModels;
using LobsterInk.Domain.Entities;

namespace LobsterInk.Application.UnitTests
{
    public class AutoMapperProfileConfigurations : Profile
    {
        public AutoMapperProfileConfigurations()
        {
            CreateMap<CreateAdventureQuestionModel, AdventureQuestion>();
            CreateMap<CreateAdventureModel, Domain.Entities.Adventure>();
            CreateMap<AdventureQuestionViewModel, AdventureQuestion>().ReverseMap();
            CreateMap<AdventureViewModel, Domain.Entities.Adventure>().ReverseMap();
        }
    }
}
