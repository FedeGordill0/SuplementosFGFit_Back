using AutoMapper;
using SuplementosFGFit_Back.Models;
using SuplementosFGFit_Back.Models.DTO;

namespace SuplementosFGFit_Back
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaCreateDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaUpdateDTO>().ReverseMap();
            CreateMap<FormasEnvio, FormasEnvioDTO>().ReverseMap();
            CreateMap<FormasEnvio, FormasEnvioCreateDTO>().ReverseMap();
            CreateMap<FormasEnvio, FormasEnvioUpdateDTO>().ReverseMap();
            CreateMap<FormasPago, FormasPagoDTO>().ReverseMap();
            CreateMap<FormasPago, FormasPagoCreateDTO>().ReverseMap();
            CreateMap<FormasPago, FormasPagoUpdateDTO>().ReverseMap();
            CreateMap<UnidadesMedidum, UnidadesMedidumDTO>().ReverseMap();
            CreateMap<UnidadesMedidum, UnidadesMedidumCreateDTO>().ReverseMap();
            CreateMap<UnidadesMedidum, UnidadesMedidumUpdateDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioCreateDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateDTO>().ReverseMap();
            CreateMap<Proveedore, ProveedoreDTO>().ReverseMap();
            CreateMap<Proveedore, ProveedoreCreateDTO>().ReverseMap();
            CreateMap<Proveedore, ProveedoreUpdateDTO>().ReverseMap();
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Producto, ProductoCreateDTO>().ReverseMap();
            CreateMap<Producto, ProductoUpdateDTO>().ReverseMap();
            CreateMap<ProductosXproveedore, ProductosXproveedoreDTO>().ReverseMap();
            CreateMap<ProductosXproveedore, ProductosXproveedoreCreateDTO>().ReverseMap();
            CreateMap<ProductosXproveedore, ProductosXproveedoreUpdateDTO>().ReverseMap();
            CreateMap<ProveedoresXformaPago, ProveedoresXformaPagoDTO>().ReverseMap();
            CreateMap<ProveedoresXformaPago, ProveedoresXformaPagoCreateDTO>().ReverseMap();
            CreateMap<ProveedoresXformaPago, ProveedoresXformaPagoUpdateDTO>().ReverseMap();
            CreateMap<ProveedoresXformaEnvio, ProveedoresXformaEnvioDTO>().ReverseMap();
            CreateMap<ProveedoresXformaEnvio, ProveedoresXformaEnvioCreateDTO>().ReverseMap();
            CreateMap<ProveedoresXformaEnvio, ProveedoresXformaEnvioUpdateDTO>().ReverseMap();
            CreateMap<UsuariosXrole, UsuariosXroleDTO>().ReverseMap();
            CreateMap<UsuariosXrole, UsuariosXroleCreateDTO>().ReverseMap();
            CreateMap<UsuariosXrole, UsuariosXroleUpdateDTO>().ReverseMap();
            CreateMap<OrdenesCompra, OrdenesCompraDTO>().ReverseMap();
            CreateMap<OrdenesCompra, OrdenesCompraCreateDTO>().ReverseMap();
            CreateMap<OrdenesCompra, OrdenesCompraUpdateDTO>().ReverseMap();
            CreateMap<DetalleOrden, DetalleOrdenDTO>().ReverseMap();
            CreateMap<DetalleOrden, DetalleOrdenCreateDTO>().ReverseMap();
            CreateMap<DetalleOrden, DetalleOrdenUpdateDTO>().ReverseMap();
            CreateMap<EstadoOrdenCompra, EstadoOrdenCompraDTO>().ReverseMap();
            CreateMap<EstadoOrdenCompra, EstadoOrdenCompraCreateDTO>().ReverseMap();
            CreateMap<EstadoOrdenCompra, EstadoOrdenCompraUpdateDTO>().ReverseMap();
        }
    }
}
