﻿using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{

    public class LeagueRepository : ILeagueRepository
    {
        private readonly SoccerStatisticsDbContext _context;

        public LeagueRepository(SoccerStatisticsDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<League>> GetAllAsync()
        {
            var result = _context.Leagues;
            return await result.ToListAsync();

        }



        public async Task<League> GetByIdAsync(uint id)
            => await _context.Leagues.SingleOrDefaultAsync(x => x.Id == id);


    }
}
