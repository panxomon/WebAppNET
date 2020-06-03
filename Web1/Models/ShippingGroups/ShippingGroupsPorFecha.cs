using FluentValidation;
using System;
using Web.Base.Cqrs.Query;

namespace Web1.Models.ShippingGroups
{
    public class ShippingGroupsPorFecha : IQuery, IShippingGroupsPorFecha
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public ShippingGroupsPorFecha(string fechaInicio, string fechaFin)
        {
            FechaInicio = ConvertStringToDate(fechaInicio);
            FechaFin = ConvertStringToDate(fechaFin);
        }

        public DateTime ConvertStringToDate(string date)
        {
            var fecha = Convert.ToDateTime(date);

            return fecha;
        }
    }

    public class ShippingGroupsPorFechaValidador : AbstractValidator<ShippingGroupsPorFecha>
    {
        public ShippingGroupsPorFechaValidador()
        {
            RuleFor(x => x.FechaInicio).NotEmpty().WithMessage("Debe ingresar una fecha de inicio.");
            RuleFor(x => x.FechaFin).NotEmpty().WithMessage("Debe ingresar una fecha de fin.");         
        }

        //private bool IsValidDateTime(string )
        //{
        //}
    }
}
