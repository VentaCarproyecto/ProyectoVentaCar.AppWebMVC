using System;
using System.Collections.Generic;

namespace VentaCarProyectoFinal.AppWebMVC.Models;

public partial class Repuesto
{
    public int Id { get; set; }

    public string NombreRepuesto { get; set; } = null!;

    public int IdVendedor { get; set; }

    public int IdDepartamento { get; set; }

    public byte[] ImgProducto { get; set; } = null!;

    public string Compatiblilidad { get; set; } = null!;

    public string DescripcionR { get; set; } = null!;

    public string Proveniencia { get; set; } = null!;

    public string EstadoRp { get; set; } = null!;

    public decimal Precio { get; set; }

    public DateTime FechaRp { get; set; }

    public int Disponibilidad { get; set; }

    public byte? Actividad { get; set; }

    public string? ComentarioR { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual Vendedore IdVendedorNavigation { get; set; } = null!;

    public virtual ICollection<ItemsCarrito> ItemsCarritos { get; set; } = new List<ItemsCarrito>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
