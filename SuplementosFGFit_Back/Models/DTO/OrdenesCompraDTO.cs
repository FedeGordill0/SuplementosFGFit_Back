using SuplementosFGFit_Back.Models.DTO;
using System;
using System.Collections.Generic;

namespace SuplementosFGFit_Back.Models;

public partial class OrdenesCompraDTO
{
    public int? IdOrdenCompra { get; set; }
    public DateTime? FechaRegistro { get; set; }

    public int? IdEstadoOrden { get; set; }

    public int? IdFormaEnvio { get; set; }

    public int? IdFormaPago { get; set; }

    public int? IdProveedor { get; set; }
    public virtual ICollection<DetalleOrdenDTO> DetalleOrdens { get; set; } = new List<DetalleOrdenDTO>();

    public virtual EstadoOrdenCompra? IdEstadoOrdenNavigation { get; set; }

    public virtual FormasEnvio? IdFormaEnvioNavigation { get; set; }

    public virtual FormasPago? IdFormaPagoNavigation { get; set; }

    public virtual Proveedore? IdProveedorNavigation { get; set; }
}
