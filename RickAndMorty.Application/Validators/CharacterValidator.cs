using FluentValidation;
using RickAndMorty.Application.DTOs;
using RickAndMorty.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Application.Validators
{
    public class CharacterValidator : AbstractValidator<CharacterSaveDTO>
    {
        public CharacterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Species).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Status).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Status).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Gender).NotEmpty()
           .Must(BeAValidGender).WithMessage("Gender must be one of the following values: Male, Female, Unknown, Genderless, Other.");
        }

        private bool BeAValidGender(string gender)
        {
            return Enum.TryParse(typeof(Gender), gender, true, out _);
        }
    }
}
