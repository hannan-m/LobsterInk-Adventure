using AutoMapper;
using LobsterInk.Application.Adventures.Models;
using LobsterInk.Application.Adventures.ViewModels;
using LobsterInk.Application.Common.Exceptions;
using LobsterInk.Application.Common.Extensions;
using LobsterInk.Application.Common.Interfaces;
using LobsterInk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LobsterInk.Application.Adventures
{
    public class AdventureService : IAdventureService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AdventureService(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<string> CreateAdventure(CreateAdventureModel model)
        {
            var adventure = _mapper.Map<CreateAdventureModel, Adventure>(model);

            adventure.Id = Guid.NewGuid().ToString();
            AddIdAndLevel(adventure.Questions.First(), adventure.Id, 0);
            _dbContext.Adventures.Add(adventure);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
            return adventure.Id;
        }

        private void AddIdAndLevel(AdventureQuestion node, string adventureId, int currentLevel)
        {
            if (node == null)
                return;

            node.Id = Guid.NewGuid().ToString();
            node.AdventureId = adventureId;
            node.Level += currentLevel + 1;

            if (!node.Children.Any())
                return;


            foreach (var child in node.Children)
                AddIdAndLevel(child, adventureId, node.Level);
        }


        public async Task<List<AdventureViewModel>> List()
        {
            var adventures = await _dbContext.Adventures
                .Select(adventure => new AdventureViewModel
                {
                    Id = adventure.Id,
                    Name = adventure.Name
                })
                .ToListAsync();
            return adventures;
        }

        public async Task<AdventureViewModel?> GetById(string id)
        {
            var adventures = await _dbContext.Adventures
                .Where(adventure => adventure.Id == id)
                .Select(adventure => new AdventureViewModel
                {
                    Id = adventure.Id,
                    Name = adventure.Name
                })
                .FirstOrDefaultAsync();

            if (adventures == null)
            {
                throw new NotFoundException(nameof(Adventure), id);
            }

            return adventures;
        }

        public async Task<IEnumerable<TreeItem<AdventureQuestionViewModel>>> GetByIdWithQuestion(string id)
        {
            var adventure = await _dbContext.Adventures.FindAsync(id);
            if (adventure == null)
            {
                throw new NotFoundException(nameof(Adventure), id);
            }

            var adventureQuestions = await _dbContext.AdventureQuestions
                .Where(adventure => adventure.AdventureId == id)
                .Select(question => new AdventureQuestionViewModel
                {
                    Id = question.Id,
                    ParentNavigationId = question.ParentNavigationId,
                    Level = question.Level,
                    Question = question.Question,
                    Type = question.Type
                })
                .OrderBy(question => question.Level)
                .ThenBy(question => question.ParentNavigationId)
                .ToListAsync();
            var tree = adventureQuestions.GenerateTree(model => model.Id,
                model => model.ParentNavigationId);
            return tree;
        }
    }
}