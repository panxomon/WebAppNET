using FluentValidation;
using FluentValidation.Attributes;
using Web.Base.Cqrs.Command;

namespace Web1.Models.Actividades
{
    [Validator(typeof(ValidarActividadPorId))]
    public class ActividadPorId : ICommand
    {
        public long Id { get; set; }
    }

    public class ValidarActividadPorId : AbstractValidator<ActividadPorId>
    {
        public ValidarActividadPorId()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Debe ingresar un id.");             
        }
    }
}
