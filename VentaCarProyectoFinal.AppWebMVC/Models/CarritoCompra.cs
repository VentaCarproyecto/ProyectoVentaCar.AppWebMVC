using System;
using System.Collections.Generic;

namespace VentaCarProyectoFinal.AppWebMVC.Models;

public partial class CarritoCompra
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public DateTime FechaCreacion { get; set; }

    public byte Estado { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual ICollection<ItemsCarrito> ItemsCarritos { get; set; } = new List<ItemsCarrito>();
}
