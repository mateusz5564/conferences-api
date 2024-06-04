using Conferences.Domain.Entities;
using FluentValidation;

namespace Conferences.Application.Conferences.Queries.GetAllConferences
{
    public class GetAllConferencesQueryValidator : AbstractValidator<GetAllConferencesQuery>
    {
        private int[] allowedPageSizes = [5, 10, 15, 30];
        private string[] allowedSortByColumnNames = [nameof(Conference.Title), nameof(Conference.StartDate),
           nameof(Conference.Category) + nameof(Conference.Category.Name)];

        public GetAllConferencesQueryValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(q => q.PageSize)
                .Must(value => allowedPageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(", ", allowedPageSizes)}]");

            RuleFor(q => q.SortBy)
                .Must(value => allowedSortByColumnNames.Contains(value))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(", ", allowedSortByColumnNames)}]");
        }
    }
}
