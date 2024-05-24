using FluentValidation;

namespace Conferences.Application.ImportantDates.Commands
{
    public class CreateImportantDateValidator : AbstractValidator<CreateImportantDateCommand>
    {
        public CreateImportantDateValidator()
        {
            RuleFor(c => c.Name).Length(2, 100);
            RuleFor(c => c.Date).NotEmpty();
        }
    }
}
