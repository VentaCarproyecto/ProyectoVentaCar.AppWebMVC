using System;
using System.Collections.Generic;

namespace VentaCarProyectoFinal.AppWebMVC.Models;

public partial class DetalleVenta
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public int? IdRepuesto { get; set; }

    public int? IdAuto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Auto? IdAutoNavigation { get; set; }

    public virtual Repuesto? IdRepuestoNavigation { get; set; }

    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
