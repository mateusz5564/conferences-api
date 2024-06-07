using Conferences.Application.Conferences.Dtos;
using Conferences.Application.ImportantDates.Dtos;
using FluentValidation.TestHelper;
using Xunit;

namespace Conferences.Application.Conferences.Commands.CreateConference.Tests
{
    public class CreateConferenceCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
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
                    Latitude = 50,
                    Longitude = 50
                },
                ImportantDates = [
                        new CreateImportantDateDto {
                            Name = "Important Date 1",
                            Date = DateTime.Parse("2024-06-14T11:30:00"),
                        },
                        new CreateImportantDateDto {
                            Name = "Important Date 2",
                            Date = DateTime.Parse("2024-06-16T17:00:00"),
                        }
                    ],
                WebsiteUrl = "https://www.google.com/",
                CategoryId = 8
            };

            var validator = new CreateConferenceCommandValidator();

            var result = validator.TestValidate(createConferenceCommand);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            var createConferenceCommand = new CreateConferenceCommand
            {
                Title = "T",
                Description = "The best",
                LogoUrl = "placeholdco/600x400",
                StartDate = DateTime.Parse("1900-06-14T11:30:00"),
                EndDate = DateTime.Parse("1899-06-16T17:00:00"),
                Location = new LocationDto
                {
                    Latitude = 250,
                    Longitude = -250
                },
                ImportantDates = [
                        new CreateImportantDateDto {
                            Name = "I"
                        }
                    ],
                WebsiteUrl = "googlecom",
                CategoryId = 8
            };

            var validator = new CreateConferenceCommandValidator();

            var result = validator.TestValidate(createConferenceCommand);

            result.ShouldHaveValidationErrorFor(x => x.Title);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.LogoUrl);
            result.ShouldHaveValidationErrorFor(x => x.StartDate);
            result.ShouldHaveValidationErrorFor(x => x.EndDate);
            result.ShouldHaveValidationErrorFor(x => x.Location.Longitude);
            result.ShouldHaveValidationErrorFor(x => x.Location.Latitude);
            result.ShouldHaveValidationErrorFor("ImportantDates[0].Name");
            result.ShouldHaveValidationErrorFor("ImportantDates[0].Date");
            result.ShouldHaveValidationErrorFor(x => x.WebsiteUrl);
        }

        public static IEnumerable<object[]> InvalidLocations =>
            new List<object[]>
            {
                new object[] { new LocationDto { Latitude = -181, Longitude = 181 } },
                new object[] { new LocationDto { Latitude = -181, Longitude = -181 } },
                new object[] { new LocationDto { Latitude = 181, Longitude = -181 } },
                new object[] { new LocationDto { Latitude = 233, Longitude = -212 } },
                new object[] { new LocationDto { Longitude = -212 } },
                new object[] { new LocationDto { Latitude = 511 } }
            };

        [Theory()]
        [MemberData(nameof(InvalidLocations))]
        public void Validator_ForInvalidLocation_ShouldHaveValidationErrorForLocation(LocationDto locationDto)
        {
            var createConferenceCommand = new CreateConferenceCommand
            {
                Location = locationDto
            };

            var validator = new CreateConferenceCommandValidator();

            var result = validator.TestValidate(createConferenceCommand);

            result.ShouldHaveValidationErrorFor(x => x.Location.Longitude);
            result.ShouldHaveValidationErrorFor(x => x.Location.Latitude);
        }

        [Theory()]
        [InlineData("")]
        [InlineData("google.com")]
        [InlineData("www.google.com")]
        [InlineData("https://www.google.com")]
        public void Validator_ForValidWebsiteUrl_ShouldNotHaveValidationErrorForWebsiteUrl(string websiteUrl)
        {
            var createConferenceCommand = new CreateConferenceCommand
            {
                Location = new LocationDto
                {
                    Latitude = 50,
                    Longitude = 50
                },
                WebsiteUrl = websiteUrl,
            };

            var validator = new CreateConferenceCommandValidator();

            var result = validator.TestValidate(createConferenceCommand);

            result.ShouldNotHaveValidationErrorFor(x => x.WebsiteUrl);
        }
    }
}