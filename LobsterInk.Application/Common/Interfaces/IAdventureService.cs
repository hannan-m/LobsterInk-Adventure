using LobsterInk.Application.Adventures.Models;
using LobsterInk.Application.Adventures.ViewModels;
using LobsterInk.Application.Common.Extensions;

namespace LobsterInk.Application.Common.Interfaces;

public interface IAdventureService
{
    Task<string> CreateAdventure(CreateAdventureModel model);

    Task<List<AdventureViewModel>> List();
    Task<AdventureViewModel?> GetById(string id);
    Task<IEnumerable<TreeItem<AdventureQuestionViewModel>>> GetByIdWithQuestion(string id);
}