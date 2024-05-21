using FluentValidation;
using System.Text.RegularExpressions;

namespace Conferences.Application.Conferences.Commands.CreateConference
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateConferenceCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Title).Length(2, 100);

            RuleFor(dto => dto.Description).Length(20, 2000);

            RuleFor(dto => dto.LogoUrl)
                .Must(BeAValidUrl)
                .WithMessage("Logo url must be a valid url");

            RuleFor(dto => dto.StartDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Start date must be in the future");

            RuleFor(dto => dto.EndDate)
                .GreaterThan(dto => dto.StartDate)
                .WithMessage("End date must be after start date");

            RuleFor(dto => dto.Location.Longitude)
                .LessThanOrEqualTo(180)
                .WithMessage("Longitude must less than 180 degrees.")
                .GreaterThanOrEqualTo(-180)
                .WithMessage("Longitude must more than -180 degrees.");

            RuleFor(dto => dto.Location.Latitude)
                .LessThanOrEqualTo(180)
                .WithMessage("Latitude must less than 180 degrees.")
                .GreaterThanOrEqualTo(-180)
                .WithMessage("Latitude must more than -180 degrees.");

            RuleFor(dto => dto.WebsiteUrl)
                .Must(BeAValidUrl)
                .WithMessage("Website url must be a valid url");
        }

        private bool BeAValidUrl(string? url)
        {
            if(string.IsNullOrEmpty(url)) return true;

            string urlPattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(urlPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(url);
        }
    }
}
