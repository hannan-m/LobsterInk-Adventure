using LobsterInk.Application.Adventures;
using LobsterInk.Application.Adventures.Models;
using LobsterInk.Application.Adventures.ViewModels;
using LobsterInk.Application.Common.Extensions;
using LobsterInk.Application.Common.Interfaces;
using LobsterInk.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdventuresController : ControllerBase
    {
        private readonly IAdventureService _adventureService;

        public AdventuresController(IAdventureService adventureService)
        {
            _adventureService = adventureService;
        }

        [HttpGet("list")]
        public async Task<List<AdventureViewModel>> Get()
        {
            return await _adventureService.List();
        }

        [HttpGet("{id}")]
        public async Task<List<AdventureViewModel>> Get(string id)
        {
            return await _adventureService.GetById(id);
        }

        [HttpGet("{id}/questions")]
        public async Task<IEnumerable<TreeItem<AdventureQuestionViewModel>>> GetByIdWithQuestions(string id)
        {
            var question = await _adventureService.GetByIdWithQuestion(id);
            return question;
        }

        [HttpPost]
        public async Task<string> Create(CreateAdventureModel model)
        {
            return await _adventureService.CreateAdventure(model);
        }
    }
}
