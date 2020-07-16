﻿using AutoMapper;
using KellermanSoftware.CompareNetObjects;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class LeagueServiceTests
    {
        private readonly CompareLogic _compareLogic;
        private readonly IMapper _mapper;
        private readonly Mock<ILeagueRepository> _repositoryMock;
        private readonly ILeagueService _service;

        public LeagueServiceTests()
        {
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperLeagueProfile>());

            _mapper = new Mapper(configuration);
            _compareLogic = new CompareLogic();
            _repositoryMock = new Mock<ILeagueRepository>();
            _service = new LeagueService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async void ReturnAllLeaguesWhichExistsInDb()
        {
            IEnumerable<League> leagues = new List<League>
            {

                new League()
                {
                  Id = 1,
                  Name = "Primera Division",
                  Country = "Spain",
                  Season = "2018/2019",
                  MVP = "Lionel Messi",
                  Winner = "FC Barcelona",
                  Rounds = null,
                  Teams = null
                },
                new League()
                {
                    Id = 2,
                    Name = "Serie A",
                    Country = "Italia",
                    Season = "2017/2018",
                    MVP = "Mauro Icardi",
                    Winner = "Juventus",
                    Rounds = null,
                    Teams = null
                },
                new League()
                {
                    Id = 3,
                    Name = "Lotto Ekstraklasa",
                    Country = "Poland",
                    Season = "2018/2019",
                    MVP = "Igor Angulo",
                    Winner = "Piast Gliwice",
                    Rounds = null,
                    Teams = null
                }
            };

            IEnumerable<LeagueDTO> testLeagues = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(leagues);            

            var expectedLeagues = _mapper.Map<IEnumerable<LeagueDTO>>(leagues);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeagues = await _service.GetAllAsync());

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testLeagues);
            Assert.Equal(leagues.Count(), testLeagues.Count());

            for (int i = 0; i < expectedLeagues.Count(); i++)
            {
                Assert.True(_compareLogic.Compare(expectedLeagues.ElementAt(i), testLeagues.ElementAt(i)).AreEqual);
            }
        }

        [Fact]
        public async void ReturnLeagueWhichExistsInDbByGivenId()
        {
            // Assert
            var league = new League()
            {
                Id = 1,
                Name = "Primera Division",
                Country = "Spain",
                Season = "2018/2019",
                MVP = "Lionel Messi",
                Winner = "FC Barcelona",
            };

            LeagueDTO testLeague = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(league);

            var expectedLeague = _mapper.Map<LeagueDTO>(league);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await _service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testLeague);

            Assert.True(_compareLogic.Compare(expectedLeague, testLeague).AreEqual);
        }


        [Fact]
        public async void ReturnNullWhenLeagueDoNotExistsInDbByGivenId()
        {
            // Assert
            LeagueDTO testLeague = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((League)null);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await _service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.Null(testLeague);
        }
    }
}
