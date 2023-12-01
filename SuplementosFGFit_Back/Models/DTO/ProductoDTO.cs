using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models.DTO;

public partial class ProductoDTO
{
    public int IdProducto { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool? Estado { get; set; }

    public string Imagen { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int? IdCategoria { get; set; }

    public int? IdUnidadMedida { get; set; }
    public DateTime? FechaVencimiento { get; set; }

    public virtual ICollection<DetalleOrden> DetalleOrdens { get; } = new List<DetalleOrden>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual UnidadesMedidum? IdUnidadMedidaNavigation { get; set; }
    public virtual ICollection<ProductosXproveedore> ProductosXproveedores { get; set; } = new List<ProductosXproveedore>();
}
