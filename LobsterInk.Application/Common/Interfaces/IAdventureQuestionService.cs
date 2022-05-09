using LobsterInk.Application.Adventures.ViewModels;
using LobsterInk.Domain.Entities;
using LobsterInk.Domain.Enums;

namespace LobsterInk.Application.Common.Interfaces;

public interface IAdventureQuestionService
{
    Task<AdventureQuestionViewModel?> GetById(string questionId);
    Task<AdventureQuestion?> GetNext(string currentQuestionId, QuestionType questionResponse);
    Task<AdventureQuestionViewModel?> GetFirstByAdventureId(string adventureId);
}