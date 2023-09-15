using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")] //obtener solo las n de categorias
[ApiVersion("1.1")] //obtener las categorias y las hamburguesas
public class Hamburguesa_ingredientesController : BaseApiController
{
    private readonly IUnitOfWorkInterface _UnitOfWork;
    private readonly IMapper mapper;

    public Hamburguesa_ingredientesController(IUnitOfWorkInterface UnitOfWork, IMapper mapper)
    {
        _UnitOfWork = UnitOfWork;
        this.mapper = mapper;
    }

    //peticiones o EndPoint
    //METODO GET (obtener todos los registros)
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<List<Hamburguesa_ingredientesDto>>> Get()
    {
        var hamburIngre = await _UnitOfWork.Hamburguesa_Ingredientes.GetAllAsync();
        return this.mapper.Map<List<Hamburguesa_ingredientesDto>>(hamburIngre);
    }

    //METODO GET (Para obtener paginacion, registro y busqueda en la entidad)
    [HttpGet("Pag")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pager<Hamburguesa_ingredientesDto>>> Get1B([FromQuery] Params ingrParams)
    {
        var ingreHambur = await _UnitOfWork.Hamburguesa_Ingredientes.GetAllAsync(ingrParams.PageIndex, ingrParams.PageSize, ingrParams.Search);
        var lstIngrDto = this.mapper.Map<List<Hamburguesa_ingredientesDto>>(ingreHambur.registros);

        return new Pager<Hamburguesa_ingredientesDto>(lstIngrDto, ingreHambur.totalRegistros, ingrParams.PageIndex, ingrParams.PageSize, ingrParams.Search);
    }
        
}

