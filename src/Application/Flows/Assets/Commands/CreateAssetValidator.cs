using FluentValidation;

namespace Application.Flows.Assets.Commands
{
    public class CreateAssetValidator : AbstractValidator<CreateAssetCommand>
    {
        public CreateAssetValidator()
        {
            RuleFor(t => t.AssetOwner)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(2).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MinLength}.")
                .MaximumLength(15).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MaxLength}.");
            RuleFor(t => t.AssetName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(2).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MinLength}.")
                .MaximumLength(15).WithMessage("{PropertyName} is {TotalLength} characters length, must be minimum {MaxLength}.");
            RuleFor(t => t.QuadKey)
                .Length(12).WithMessage("{PropertyName} is required and must be exactly {ExactLength} characters length.");
        }
    }
}