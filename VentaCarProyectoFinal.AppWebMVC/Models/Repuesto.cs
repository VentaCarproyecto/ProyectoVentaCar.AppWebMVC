using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VentaCarProyectoFinal.AppWebMVC.Models;

public partial class Repuesto
{
    public int Id { get; set; }

    [Display(Name = "Repuesto")]
    [Required(ErrorMessage = "El Nombre del Repuesto es obligatorio.")]
    public string? NombreRepuesto { get; set; } = null!;

    [Display(Name = "Vendedor")]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public int IdVendedor { get; set; }

    [Display(Name = "Departamento")]
    [Required(ErrorMessage = "El departamento es obligatorio.")]
    public int IdDepartamento { get; set; }

    [Display(Name = "Imagen Producto")]
    [Required(ErrorMessage = "La imagen es obligatoria.")]
    public string ImgProducto { get; set; } = null!;


    [Display(Name = "Compatible con")]
    [Required(ErrorMessage = "La Compatibilidad es obligatoria.")]
    public string Compatiblilidad { get; set; } = null!;


    [Display(Name = "Descripción")]
    [Required(ErrorMessage = "La descripción es obligatoria.")]
    public string DescripcionR { get; set; } = null!;


    [Display(Name = "Procedencia")]
    [Required(ErrorMessage = "La Procedencia es obligatoria.")]
    public string Proveniencia { get; set; } = null!;

    [Display(Name = "Estado Fisico")]
    [Required(ErrorMessage = "El estado fisico es obligatorio.")]
    public string EstadoRp { get; set; } = null!;

    [Display(Name = "Precio")]
    [Required(ErrorMessage = "El precio es obligatorio.")]
    public decimal? Precio { get; set; }

    [Display(Name = "Fecha de publicación")]
    [Required(ErrorMessage = "La Fecha de publicación es obligatoria.")]
    public DateTime? FechaRp { get; set; }

    [Display(Name = "Existencia")]
    [Required(ErrorMessage = "La existencia es obligatoria.")]
    public int? Disponibilidad { get; set; }

    [Display(Name = "Estado")]
    public byte? Actividad { get; set; }

    [Display(Name = "Motivo de la Venta")]
    public string? ComentarioR { get; set; }

    public virtual ICollection<DetalleVenta>? DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Departamento? IdDepartamentoNavigation { get; set; } = null!;

    public virtual Vendedore? IdVendedorNavigation { get; set; } = null!;

    public virtual ICollection<ItemsCarrito>? ItemsCarritos { get; set; } = new List<ItemsCarrito>();

    public virtual ICollection<Venta>? Venta { get; set; } = new List<Venta>();
}
