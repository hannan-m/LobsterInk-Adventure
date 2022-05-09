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
        public async Task<IActionResult> CreateHistory(string adventureId, string userId)
        {
            await _adventureQuestionHistoryService.Add(adventureId, userId);
            return NoContent();
        }

        [HttpGet]
        public async Task<List<UserAdventureQuestionHistoryViewModel>> GetFirstByAdventureId(string adventureId,
            string userId)
        {
            var question = await _adventureQuestionHistoryService.GetByAdventureId(adventureId, userId);
            return question;
        }
    }
}