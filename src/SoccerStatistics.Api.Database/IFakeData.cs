﻿using Bogus;
using SoccerStatistics.Api.Database.Entities;
using System.Collections.Generic;

namespace SoccerStatistics.Api.Database
{
    public interface IFakeData
    {
        Faker<Player> GetFakePlayer();
        Faker<Stadium> GetFakeStadium();
        Faker<Team> GetFakeTeam();
        Faker<Transfer> GetFakeTransfer(IEnumerable<Player> players, IEnumerable<Team> teams);
        Faker<Match> GetFakeMatch(IEnumerable<Team> teams);
        Faker<Activity> GetFakeActivity(IEnumerable<Player> players);
        Faker<InteractionBetweenPlayers> GetFakeInteraction(IEnumerable<Player> players);
        Faker<TeamInMatchStats> GetFakeTeamInMatchStats(Team team);
        Faker<Round> GetFakeRound(League league);
        Faker<League> GetFakeLeague(IEnumerable<Team> teams);
        Faker<TeamInLeague> GetFakeTeamInLeague(League league, Team team);
    }
}