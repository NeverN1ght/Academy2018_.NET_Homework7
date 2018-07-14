using Academy2018_.NET_Homework5.Shared.DTOs;
using FluentValidation;

namespace Academy2018_.NET_Homework5.Core.Validation
{
    public class CrewDtoValidator: AbstractValidator<CrewDto>
    {
        public CrewDtoValidator()
        {
            RuleFor(c => c.Pilot)
                .NotNull();
            RuleForEach(c => c.Stewardesses)
                .NotNull();
        }
    }
}
