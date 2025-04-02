using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VentaCarProyectoFinal.AppWebMVC.Models;

public partial class Cliente
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El Nombre es obligatorio.")]
    public string Nombre { get; set; } = null!;
    [Required(ErrorMessage = "El Apellido es obligatorio.")]

    public string Apellido { get; set; } = null!;
    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "Debe ingresar un número de teléfono válido.")]
    public string Telefono { get; set; } = null!;
    [Required(ErrorMessage = "La dirección es obligatoria.")]
    public string Direccion { get; set; } = null!;
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
    public string Correo { get; set; } = null!;
    [Required(ErrorMessage = "El número de DUI es obligatorio.")]
    [Range(10000000, 99999999, ErrorMessage = "El DUI debe tener 8 dígitos.")]
    public int Dui { get; set; }
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "CLIENTE";


    public virtual ICollection<CarritoCompra> CarritoCompras { get; set; } = new List<CarritoCompra>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
