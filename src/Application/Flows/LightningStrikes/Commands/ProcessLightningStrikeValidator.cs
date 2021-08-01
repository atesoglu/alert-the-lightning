using FluentValidation;

namespace Application.Flows.LightningStrikes.Commands
{
    public class ProcessLightningStrikeValidator : AbstractValidator<ProcessLightningStrikeCommand>
    {
        public ProcessLightningStrikeValidator()
        {
            RuleFor(t => t.StrikeTime)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");
            RuleFor(t => t.Latitude)
                .GreaterThanOrEqualTo(-90).WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}.")
                .LessThanOrEqualTo(90).WithMessage("{PropertyName} must be less than or equal to {ComparisonValue}.");
            RuleFor(t => t.Longitude)
                .GreaterThanOrEqualTo(-180).WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}.")
                .LessThanOrEqualTo(180).WithMessage("{PropertyName} must be less than or equal to {ComparisonValue}.");
            RuleFor(t => t.NumberOfSensors)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");
        }
    }
}