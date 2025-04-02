using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VentaCarProyectoFinal.AppWebMVC.Models;

public partial class Marca
{
    public int Id { get; set; }

    [Display(Name = "Nombre Marca")]
    [Required(ErrorMessage = "La Marca es obligatorio.")]
    public string Marca1 { get; set; } = null!;

    public virtual ICollection<Auto> Autos { get; set; } = new List<Auto>();
}
