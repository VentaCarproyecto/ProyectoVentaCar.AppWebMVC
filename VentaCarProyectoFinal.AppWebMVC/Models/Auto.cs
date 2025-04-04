using System;
using System.Collections.Generic;

namespace VentaCarProyectoFinal.AppWebMVC.Models;

public partial class Auto
{
    public int Id { get; set; }

    public int IdVendedor { get; set; }

    public int IdDepartamento { get; set; }

    public int IdMarca { get; set; }

    public string? AnnoFabricacion { get; set; }

    public string Modelo { get; set; } = null!;

    public string DescripcionA { get; set; } = null!;

    public decimal? Kilometraje { get; set; }

    public string? Estado { get; set; }

    public decimal Precio { get; set; }

    public string? Urlimagen { get; set; }

    public DateTime Urt { get; set; }

    public DateTime? FechaRp { get; set; }

    public string? Comentario { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual Vendedore IdVendedorNavigation { get; set; } = null!;
}
