using AutoMapper;
using LobsterInk.Application.Adventures.Models;
using LobsterInk.Application.Adventures.ViewModels;
using LobsterInk.Domain.Entities;

namespace LobsterInk.Application.Common.AutoMapper
{
    public class ProfileConfiguration : Profile
    {
        public ProfileConfiguration()
        {
            CreateMap<CreateAdventureQuestionModel, AdventureQuestion>();
            CreateMap<CreateAdventureModel, Adventure>();
            CreateMap<AdventureQuestionViewModel, AdventureQuestion>().ReverseMap();
            CreateMap<AdventureViewModel, Adventure>().ReverseMap();
        }

    }
}
