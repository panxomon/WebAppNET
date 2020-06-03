using FluentValidation;
using FluentValidation.Attributes;
using Web.Base.Cqrs.Command;

namespace Web1.Models.Actividades
{
    [Validator(typeof(ValidarNuevaActividad))]
    public class NuevaActividad : ICommand 
    {   
        public long Id { get; set; }

        public string Nombre { get; set; } 
        public string Codigo { get; set; }
    }

    public class ValidarNuevaActividad : AbstractValidator<NuevaActividad>
    {
        public ValidarNuevaActividad()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Debe ingresar un nombre.");
            RuleFor(x => x.Codigo).NotEmpty().WithMessage("Debe ingresar un codigo.");
        }
    }
}
