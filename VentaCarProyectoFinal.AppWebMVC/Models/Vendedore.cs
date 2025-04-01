using System;
using System.Collections.Generic;

namespace VentaCarProyectoFinal.AppWebMVC.Models;

public partial class Vendedore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Telefono { get; set; }

    public string Direccion { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Dui { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Auto> Autos { get; set; } = new List<Auto>();

    public virtual ICollection<Repuesto> Repuestos { get; set; } = new List<Repuesto>();
}
