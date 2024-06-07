using AutoMapper;
using Conferences.Application.Categories.Dtos;
using Conferences.Application.Conferences.Commands.CreateConference;
using Conferences.Application.Conferences.Commands.UpdateConference;
using Conferences.Application.ImportantDates.Dtos;
using Conferences.Domain.Entities;
using FluentAssertions;
using NetTopologySuite.Geometries;
using Xunit;

namespace Conferences.Application.Conferences.Dtos.Tests
{
    public class ConferencesProfileTests
    {
        private IMapper _mapper;

        public ConferencesProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ConferencesProfile>();
                cfg.AddProfile<CategoriesProfile>();
                cfg.AddProfile<ImportantDatesProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact()]
        public void CreateMap_ForConferenceToConferenceDto_MapsCorrectly()
        {
            var conference = new Conference
            {
                Id = 5,
                Title = "Test Conference",
                Description = "The best conference in the world",
                IsAccepted = true,
                LogoUrl = "https://placehold.co/600x400",
                StartDate = DateTime.Parse("2024-06-14T11:30:00"),
                EndDate = DateTime.Parse("2024-06-16T17:00:00"),
                Location = new Point(20, 20),
                ImportantDates = [
                        new ImportantDate {
                            Id = 1,
                            Name = "Important Date 1",
                            Date = DateTime.Parse("2024-06-14T11:30:00"),
                        }
                    ],
                WebsiteUrl = "https://www.google.com/",
                Category = new Category { Id = 8, Name = "Ekonomia" },
                CategoryId = 8
            };


            var conferenceDto = _mapper.Map<ConferenceDto>(conference);

            conferenceDto.Should().NotBeNull();
            conferenceDto.Id.Should().Be(conference.Id);
            conferenceDto.Title.Should().Be(conference.Title);
            conferenceDto.Description.Should().Be(conference.Description);
            conferenceDto.IsAccepted.Should().Be(conference.IsAccepted);
            conferenceDto.LogoUrl.Should().Be(conference.LogoUrl);
            conferenceDto.StartDate.Should().Be(conference.StartDate);
            conferenceDto.EndDate.Should().Be(conference.EndDate);
            conferenceDto.Location.Longitude.Should().Be(conference.Location.X);
            conferenceDto.Location.Latitude.Should().Be(conference.Location.Y);
            conferenceDto.ImportantDates[0].Id.Should().Be(conference.ImportantDates[0].Id);
            conferenceDto.ImportantDates[0].Name.Should().Be(conference.ImportantDates[0].Name);
            conferenceDto.ImportantDates[0].Date.Should().Be(conference.ImportantDates[0].Date);
            conferenceDto.WebsiteUrl.Should().Be(conference.WebsiteUrl);
            conferenceDto.Category.Id.Should().Be(conference.Category.Id);
            conferenceDto.Category.Name.Should().Be(conference.Category.Name);
        }

        [Fact()]
        public void CreateMap_ForCreateConferenceCommandToConference_MapsCorrectly()
        {
            var createConferenceCommand = new CreateConferenceCommand
            {
                Title = "Test Conference",
                Description = "The best conference in the world",
                LogoUrl = "https://placehold.co/600x400",
                StartDate = DateTime.Parse("2024-06-14T11:30:00"),
                EndDate = DateTime.Parse("2024-06-16T17:00:00"),
                Location = new LocationDto
                {
                    Longitude = 10,
                    Latitude = 10,
                },
                ImportantDates = [
                        new CreateImportantDateDto {
                            Name = "Important Date 1",
                            Date = DateTime.Parse("2024-06-14T11:30:00"),
                        }
                    ],
                WebsiteUrl = "https://www.google.com/",
                CategoryId = 8
            };


            var conference = _mapper.Map<Conference>(createConferenceCommand);

            conference.Should().NotBeNull();
            conference.Title.Should().Be(createConferenceCommand.Title);
            conference.Description.Should().Be(createConferenceCommand.Description);
            conference.LogoUrl.Should().Be(createConferenceCommand.LogoUrl);
            conference.StartDate.Should().Be(createConferenceCommand.StartDate);
            conference.EndDate.Should().Be(createConferenceCommand.EndDate);
            conference.Location.X.Should().Be(createConferenceCommand.Location.Longitude);
            conference.Location.Y.Should().Be(createConferenceCommand.Location.Latitude);
            conference.ImportantDates[0].Name.Should().Be("Important Date 1");
            conference.ImportantDates[0].Date.Should().Be(DateTime.Parse("2024-06-14T11:30:00"));
            conference.WebsiteUrl.Should().Be(createConferenceCommand.WebsiteUrl);
            conference.CategoryId.Should().Be(createConferenceCommand.CategoryId);
        }

        [Fact()]
        public void CreateMap_ForUpdateConferenceCommandToConference_MapsCorrectly()
        {
            var updateConferenceCommand = new UpdateConferenceCommand
            {
                Id = 5,
                Description = "The best conference in the world updated",
                LogoUrl = "https://placehold.com/600x400",
                StartDate = DateTime.Parse("2024-06-20T08:30:00"),
                EndDate = DateTime.Parse("2024-06-22T15:00:00"),
                Location = new UpdateLocationDto
                {
                    Longitude = 10
                },
                WebsiteUrl = "https://www.bing.com/",
                CategoryId = 9
            };

            var conference = new Conference
            {
                Id = 5,
                Title = "Test Conference",
                Description = "The best conference in the world",
                IsAccepted = true,
                LogoUrl = "https://placehold.co/600x400",
                StartDate = DateTime.Parse("2024-06-14T11:30:00"),
                EndDate = DateTime.Parse("2024-06-16T17:00:00"),
                Location = new Point(20, 20),
                ImportantDates = [
                    new ImportantDate {
                                    Id = 1,
                                    Name = "Important Date 1",
                                    Date = DateTime.Parse("2024-06-14T11:30:00"),
                                }
                ],
                WebsiteUrl = "https://www.google.com/",
                Category = new Category { Id = 8, Name = "Ekonomia" },
                CategoryId = 8
            };

            _mapper.Map(updateConferenceCommand, conference);

            conference.Should().NotBeNull();
            conference.Id.Should().Be(5);
            conference.Title.Should().Be("Test Conference");
            conference.Description.Should().Be(updateConferenceCommand.Description);
            conference.LogoUrl.Should().Be(updateConferenceCommand.LogoUrl);
            conference.StartDate.Should().Be(updateConferenceCommand.StartDate);
            conference.EndDate.Should().Be(updateConferenceCommand.EndDate);
            conference.Location.X.Should().Be(updateConferenceCommand.Location.Longitude);
            conference.Location.Y.Should().Be(20);
            conference.ImportantDates[0].Id.Should().Be(1);
            conference.ImportantDates[0].Name.Should().Be("Important Date 1");
            conference.ImportantDates[0].Date.Should().Be(DateTime.Parse("2024-06-14T11:30:00"));
            conference.WebsiteUrl.Should().Be(updateConferenceCommand.WebsiteUrl);
            conference.CategoryId.Should().Be(updateConferenceCommand.CategoryId);
        }
    }
}