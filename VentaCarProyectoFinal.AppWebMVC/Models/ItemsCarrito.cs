using System;
using System.Collections.Generic;

namespace VentaCarProyectoFinal.AppWebMVC.Models;

public partial class ItemsCarrito
{
    public int Id { get; set; }

    public int IdCarrito { get; set; }

    public int IdRepuesto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual CarritoCompra IdCarritoNavigation { get; set; } = null!;

    public virtual Repuesto IdRepuestoNavigation { get; set; } = null!;
}
