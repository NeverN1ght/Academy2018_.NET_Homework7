using Academy2018_.NET_Homework5.Shared.DTOs;
using FluentValidation;

namespace Academy2018_.NET_Homework5.Core.Validation
{
    public class TicketDtoValidator: AbstractValidator<TicketDto>
    {
        public TicketDtoValidator()
        {
            RuleFor(t => t.FlightNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.Price)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
