using System;
using System.Collections.Generic;

namespace VentaCarProyectoFinal.AppWebMVC.Models;

public partial class Departamento
{
    public int Id { get; set; }

    public string Departamento1 { get; set; } = null!;

    public virtual ICollection<Auto> Autos { get; set; } = new List<Auto>();

    public virtual ICollection<Repuesto> Repuestos { get; set; } = new List<Repuesto>();
}
