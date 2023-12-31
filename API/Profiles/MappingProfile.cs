using API.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //CReamos el mapeo de las entidades a los Dtos
        CreateMap<Categoria, CategoriaDto>().ReverseMap();
        CreateMap<Categoria, CategoriaXhamburguesaDto>().ReverseMap();


        CreateMap<Chef, ChefDto>().ReverseMap();
        CreateMap<Chef, ChefXhamburguesaDto>().ReverseMap();

        CreateMap<Hamburguesa, HamburguesaDto>().ReverseMap();
        CreateMap<Hamburguesa, HamburguesaXingredienteDto>().ReverseMap();

        CreateMap<Ingrediente, IngredienteDto>().ReverseMap();
        CreateMap<Ingrediente, IngredienteXhamburguesaDto>().ReverseMap();
        CreateMap<Ingrediente, IngredientesEnStockDto>().ReverseMap();
        CreateMap<Ingrediente, IngredienteActualizarDto>().ReverseMap();
        CreateMap<Ingrediente, NuevoIngredienteDto>().ReverseMap();

        CreateMap<Hamburguesa_ingredientes, Hamburguesa_ingredientesDto>().ReverseMap();
        CreateMap<Hamburguesa_ingredientes, AgregarHamburguesaIngredienteDto>().ReverseMap();
    }

}