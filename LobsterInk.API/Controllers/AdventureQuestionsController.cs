using LobsterInk.Application.Adventures;
using LobsterInk.Application.Adventures.ViewModels;
using LobsterInk.Application.Common.Extensions;
using LobsterInk.Application.Common.Interfaces;
using LobsterInk.Domain.Entities;
using LobsterInk.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdventureQuestionsController : ControllerBase
    {
        private readonly IAdventureQuestionService _questionService;

        public AdventureQuestionsController(IAdventureQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("{id}")]
        public async Task<AdventureQuestionViewModel?> Get(string id)
        {
            return await _questionService.GetById(id);
        }

        [HttpGet("adventure/{adventureId}/first-question")]
        public async Task<AdventureQuestionViewModel?> GetFirstByAdventureId(string adventureId)
        {
            var question = await _questionService.GetFirstByAdventureId(adventureId);
            return question;
        }

        [HttpGet("{questionId}/next-question")]
        public async Task<AdventureQuestion?> GetNext(string questionId, QuestionType questionResponse)
        {
            var question = await _questionService.GetNext(questionId, questionResponse);
            return question;
        }
    }
}
