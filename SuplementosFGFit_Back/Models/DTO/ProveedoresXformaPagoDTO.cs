namespace SuplementosFGFit_Back.Models.DTO
{
    public partial class ProveedoresXformaPagoDTO
    {
        public int IdProveedorFormaPago { get; set; }

        public int? IdProveedor { get; set; }

        public int? IdFormaPago { get; set; }

        public virtual FormasPago? IdFormaPagoNavigation { get; set; }

        public virtual Proveedore? IdProveedorNavigation { get; set; }
    }
}

