using LobsterInk.Application.Adventures;
using LobsterInk.Application.Adventures.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LobsterInk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAdventureQuestionHistoriesController : ControllerBase
    {
        private readonly IAdventureQuestionHistoryService _adventureQuestionHistoryService;

        public UserAdventureQuestionHistoriesController(
            IAdventureQuestionHistoryService adventureQuestionHistoryService)
        {
            _adventureQuestionHistoryService = adventureQuestionHistoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHistory(Guid adventureId, Guid userId)
        {
            await _adventureQuestionHistoryService.Add(adventureId.ToString(), userId.ToString());
            return NoContent();
        }

        [HttpGet]
        public async Task<List<UserAdventureQuestionHistoryViewModel>> GetFirstByAdventureId(Guid adventureId,
            Guid userId)
        {
            var question = 
                await _adventureQuestionHistoryService.GetByAdventureId(adventureId.ToString(), userId.ToString());
            return question;
        }
    }
}