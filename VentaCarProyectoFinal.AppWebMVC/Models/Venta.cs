using System;
using System.Collections.Generic;

namespace VentaCarProyectoFinal.AppWebMVC.Models;

public partial class Venta
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public int IdRepuesto { get; set; }

    public int NumeroTarjeta { get; set; }

    public string? NombreTrajeta { get; set; }

    public DateTime FechaExpTrajeta { get; set; }

    public byte[] RimgProducto { get; set; } = null!;

    public decimal TotalPago { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Repuesto IdRepuestoNavigation { get; set; } = null!;
}
