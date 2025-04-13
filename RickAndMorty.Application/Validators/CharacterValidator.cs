using FluentValidation;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Application.Enums;

namespace RickAndMorty.Application.Validators
{
    public class CharacterValidator : AbstractValidator<CharacterSaveDTO>
    {
        public CharacterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Species).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Status).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Type).MaximumLength(50);
            RuleFor(x => x.Gender).NotEmpty().MaximumLength(20)
           .Must(BeAValidGender).WithMessage("Gender must be one of the following values: Male, Female, Unknown, Genderless, Other.");
        }

        private bool BeAValidGender(string gender)
        {
            return Enum.TryParse(typeof(Gender), gender, true, out _);
        }
    }
}