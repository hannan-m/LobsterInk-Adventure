using AutoMapper;
using LobsterInk.Application.Adventures.ViewModels;
using LobsterInk.Application.Common.Exceptions;
using LobsterInk.Application.Common.Interfaces;
using LobsterInk.Domain.Entities;
using LobsterInk.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LobsterInk.Application.Adventures;

public class AdventureQuestionService : IAdventureQuestionService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public AdventureQuestionService(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<AdventureQuestionViewModel?> GetFirstByAdventureId(string adventureId)
    {
        var adventureExist = await _dbContext.Adventures.AnyAsync(a => a.Id == adventureId);

        if (!adventureExist)
        {
            throw new NotFoundException(nameof(Adventure), adventureId);
        }

        return await _dbContext.AdventureQuestions
            .Where(q => q.AdventureId == adventureId
                        && q.ParentNavigationId == null)
            .Select(a => new AdventureQuestionViewModel
            {
                Id = a.Id,
                Level = a.Level,
                Question = a.Question,
                Type = a.Type,
                ParentNavigationId = a.ParentNavigationId
            })
            .FirstOrDefaultAsync();
    }

    public async Task<AdventureQuestionViewModel?> GetById(string questionId)
    {
        var question = await _dbContext.AdventureQuestions
            .Where(q => q.Id == questionId)
            .Select(a => new AdventureQuestionViewModel
            {
                Id = a.Id,
                Level = a.Level,
                Question = a.Question,
                Type = a.Type,
                ParentNavigationId = a.ParentNavigationId
            })
            .FirstOrDefaultAsync();

        if (question == null)
        {
            throw new NotFoundException(nameof(AdventureQuestion), questionId);
        }

        return question;
    }

    public async Task<AdventureQuestion?> GetNext(string currentQuestionId, QuestionType questionResponse)
    {
        var questionExist = await _dbContext.AdventureQuestions.AnyAsync(a => a.Id == currentQuestionId);

        if (!questionExist)
        {
            throw new NotFoundException(nameof(AdventureQuestion), currentQuestionId);
        }

        var question = await _dbContext.AdventureQuestions
            .Where(q => q.Id == currentQuestionId)
            .SelectMany(a => a.Children)
            .Where(a => a.Type == questionResponse)
            .FirstOrDefaultAsync();

        return question;
    }
}