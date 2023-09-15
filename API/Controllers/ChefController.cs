using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")] //obtener solo las n de chefs
[ApiVersion("1.1")] //obtener las chefs y las hamburguesas
public class ChefController : BaseApiController
{
    private readonly IUnitOfWorkInterface _UnitOfWork;
    private readonly IMapper mapper;

    public ChefController(IUnitOfWorkInterface UnitOfWork, IMapper mapper)
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

    public async Task<ActionResult<List<ChefDto>>> Get()
    {
        var chefs = await _UnitOfWork.Chefs.GetAllAsync();
        return this.mapper.Map<List<ChefDto>>(chefs);
    }

    //METODO GET (Para obtener paginacion, registro y busqueda en la entidad)
    [HttpGet("Pag")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pager<ChefXhamburguesaDto>>> Get1B([FromQuery] Params chefParams)
    {
        var chef = await _UnitOfWork.Chefs.GetAllAsync(chefParams.PageIndex, chefParams.PageSize, chefParams.Search);
        var lstChefDto = this.mapper.Map<List<ChefXhamburguesaDto>>(chef.registros);

        return new Pager<ChefXhamburguesaDto>(lstChefDto, chef.totalRegistros, chefParams.PageIndex, chefParams.PageSize, chefParams.Search);
    }

    //METODO GET POR ID (Traer un solo registro de la entidad de la  Db)
    [HttpGet("{id}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChefXhamburguesaDto>> Get( int id)
    {
        var chef = await _UnitOfWork.Chefs.GetByIdAsync(id);

        if (chef == null) {
            return NotFound();
        }

        return this.mapper.Map<ChefXhamburguesaDto>(chef);
    }

    //METODO POST (para enviar registros a la entidad de la Db)
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChefDto>> Post(ChefDto chefDto)
    {
        var chef = this.mapper.Map<Chef>(chefDto);
        _UnitOfWork.Chefs.Add(chef);
        await _UnitOfWork.SaveAsync();

        if (chef == null) {
            return BadRequest();
        }

        return this.mapper.Map<ChefDto>(chef);
    }

    //METODO PUT (editar un registro de la entidad de la Db)
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChefDto>> Put(int id, [FromBody] ChefDto chefDto)
    {
        if (chefDto == null) {
            return NotFound();
        }

        var chef = this.mapper.Map<Chef>(chefDto);
        chef.Id = id;
        _UnitOfWork.Chefs.Update(chef);
        await _UnitOfWork.SaveAsync();
        return this.mapper.Map<ChefDto>(chef);
    }

    //METODO DELETE (Eliminar un registro de la entidad de la Db)
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChefDto>> Delete(int id)
    {
        var chef = await _UnitOfWork.Chefs.GetByIdAsync(id);

        if (chef == null) {
            return NotFound();
        }

        _UnitOfWork.Chefs.Remove(chef);
        await _UnitOfWork.SaveAsync();

        return NoContent();
    }

    //encontrar el nombre del Chef que trabaja con algun tipo de carne 
    [HttpGet("ChefDeCarnes/{tipoCarne}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<List<ChefDto>>> GetChefDeCarnes(string tipoCarne)
    {
        var chefs = await _UnitOfWork.Chefs.GetAllChefsCarnesAsync(tipoCarne);
        return this.mapper.Map<List<ChefDto>>(chefs);
    }

    //Encontrar las hamburguesas que pertenecen a un chef determinado
    [HttpGet("Chef/{nombre}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<List<ChefXhamburguesaDto>>> GetHamburguesa(string nombre)
    {
        var chefs = await _UnitOfWork.Chefs.GetAllHamburguesasChefAsync(nombre);
        return this.mapper.Map<List<ChefXhamburguesaDto>>(chefs);
    }
        
}
