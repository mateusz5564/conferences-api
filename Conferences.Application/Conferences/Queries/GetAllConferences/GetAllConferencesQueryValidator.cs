using FluentValidation;

namespace Conferences.Application.Conferences.Queries.GetAllConferences
{
    public class GetAllConferencesQueryValidator : AbstractValidator<GetAllConferencesQuery>
    {
        private int[] allowedPageSizes = [5, 10, 15, 30];

        public GetAllConferencesQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(q => q.PageSize)
                .Must(value => allowedPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(", ", allowedPageSizes)}]");
        }
    }
}
