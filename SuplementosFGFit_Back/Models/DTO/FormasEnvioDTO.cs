namespace SuplementosFGFit_Back.Models.DTO;

public partial class FormasEnvioDTO
{
    public int IdFormaEnvio { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal? Precio { get; set; }

    public virtual ICollection<OrdenesCompra> OrdenesCompras { get; } = new List<OrdenesCompra>();

    public virtual ICollection<ProveedoresXformaEnvio> ProveedoresXformaEnvios { get; } = new List<ProveedoresXformaEnvio>();
}
