using LobsterInk.Application.Adventures.ViewModels;
using LobsterInk.Application.Common.Exceptions;
using LobsterInk.Application.Common.Interfaces;
using LobsterInk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LobsterInk.Application.Adventures
{
    public class AdventureQuestionHistoryService : IAdventureQuestionHistoryService
    {
        private readonly IApplicationDbContext _dbContext;

        public AdventureQuestionHistoryService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(string questionId, string userId)
        {
            var questionExist = await _dbContext.AdventureQuestions.AnyAsync(q => q.Id == questionId);

            if (!questionExist)
            {
                throw new NotFoundException(nameof(AdventureQuestion), questionId);
            }

            var entity = await _dbContext.UserAdventureQuestionsHistory.FirstOrDefaultAsync(h =>
                h.AdventureQuestionId == questionId && h.UserId == userId);

            if (entity != null)
            {
                throw new DuplicateRecordException(nameof(UserAdventureQuestionHistory),
                    $"QuestionId {questionId}, UserId {userId}");
            }

            entity = new UserAdventureQuestionHistory
            {
                AdventureQuestionId = questionId,
                UserId = userId
            };

            _dbContext.UserAdventureQuestionsHistory.Add(entity);

            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<List<UserAdventureQuestionHistoryViewModel>> GetByAdventureId(string adventureId,
            string userId)
        {
            var history = await _dbContext.AdventureQuestions
                .Where(q => q.AdventureId == adventureId)
                .SelectMany(q => q.AdventureQuestionHistories)
                .Include(a => a.AdventureQuestion)
                .Where(qh => qh.UserId == userId)
                .GroupBy(qh => qh.UserId)
                .Select(qh => new UserAdventureQuestionHistoryViewModel
                {
                    UserId = qh.Key,
                    QuestionHistory = qh.Select(a => new AdventureQuestionHistoryViewModel
                        {
                            Id = a.AdventureQuestionId,
                            Level = a.AdventureQuestion.Level,
                            Question = a.AdventureQuestion.Question,
                            Type = a.AdventureQuestion.Type
                        })
                        .OrderBy(m => m.Level).ToList()
                })
                .ToListAsync();

            return history;
        }
    }

    public interface IAdventureQuestionHistoryService
    {
        Task Add(string questionId, string userId);
        Task<List<UserAdventureQuestionHistoryViewModel>> GetByAdventureId(string adventureId, string userId);
    }
}