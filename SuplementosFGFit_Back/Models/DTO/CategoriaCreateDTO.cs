namespace SuplementosFGFit_Back.Models.DTO;

public partial class CategoriaCreateDTO
{
    public string Descripcion { get; set; }

    public string Nombre { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
