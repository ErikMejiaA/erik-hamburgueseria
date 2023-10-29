using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")] //obtener solo las n de Ingredientes 
[ApiVersion("1.1")] //obtener las ingredientes y las hamburguesas
public class IngredientesController : BaseApiController
{
    private readonly IUnitOfWorkInterface _UnitOfWork;
    private readonly IMapper mapper;

    public IngredientesController(IUnitOfWorkInterface UnitOfWork, IMapper mapper)
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

    public async Task<ActionResult<List<IngredienteDto>>> Get()
    {
        var ingredientes = await _UnitOfWork.Ingredientes.GetAllAsync();
        return this.mapper.Map<List<IngredienteDto>>(ingredientes);
    }

    //METODO GET (Para obtener paginacion, registro y busqueda en la entidad)
    [HttpGet("Pag")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pager<IngredienteXhamburguesaDto>>> Get1B([FromQuery] Params ingrediParams)
    {
        var ingredientes = await _UnitOfWork.Ingredientes.GetAllAsync(ingrediParams.PageIndex, ingrediParams.PageSize, ingrediParams.Search);
        var lstIngrediDto = this.mapper.Map<List<IngredienteXhamburguesaDto>>(ingredientes.registros);

        return new Pager<IngredienteXhamburguesaDto>(lstIngrediDto, ingredientes.totalRegistros, ingrediParams.PageIndex, ingrediParams.PageSize, ingrediParams.Search);
    }

    //METODO GET POR ID (Traer un solo registro de la entidad de la  Db)
    [HttpGet("{id}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredienteXhamburguesaDto>> Get( int id)
    {
        var ingrediente = await _UnitOfWork.Ingredientes.GetByIdAsync(id);

        if (ingrediente == null) {
            return NotFound();
        }

        return this.mapper.Map<IngredienteXhamburguesaDto>(ingrediente);
    }

    //METODO POST (para enviar registros a la entidad de la Db)
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredienteDto>> Post(IngredienteDto ingredienteDto)
    {
        var ingrediente = this.mapper.Map<Ingrediente>(ingredienteDto);
        _UnitOfWork.Ingredientes.Add(ingrediente);
        await _UnitOfWork.SaveAsync();

        if (ingrediente == null) {
            return BadRequest();
        }

        return this.mapper.Map<IngredienteDto>(ingrediente);
    }

    //METODO PUT (editar un registro de la entidad de la Db)
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredienteDto>> Put(int id, [FromBody] IngredienteDto ingredienteDto)
    {
        if (ingredienteDto == null) {
            return NotFound();
        }

        var ingrediente = this.mapper.Map<Ingrediente>(ingredienteDto);
        ingrediente.Id = id;
        _UnitOfWork.Ingredientes.Update(ingrediente);
        await _UnitOfWork.SaveAsync();
        return this.mapper.Map<IngredienteDto>(ingrediente);
    }

    //METODO DELETE (Eliminar un registro de la entidad de la Db)
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredienteDto>> Delete(int id)
    {
        var ingrediente = await _UnitOfWork.Ingredientes.GetByIdAsync(id);

        if (ingrediente == null) {
            return NotFound();
        }

        _UnitOfWork.Ingredientes.Remove(ingrediente);
        await _UnitOfWork.SaveAsync();

        return NoContent();
    }

    //Condulta traer los ingredientes con un stock < a 400
    [HttpGet("BuscarStockMenorA/{stock}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<List<IngredientesEnStockDto>>> GetStock(int stock)
    {
        if (int.IsNegative(stock)) {
            throw new UnauthorizedAccessException("el stock igresado es invalido");
        }

        var ingredientes = await _UnitOfWork.Ingredientes.GetAllIngredientesAsync(stock);

        if ((ingredientes == null) || (ingredientes.Count() == 0)) {
            throw new UnauthorizedAccessException("No se encotro ningun elemento");
        }

        return this.mapper.Map<List<IngredientesEnStockDto>>(ingredientes);
    }

    //Consulta para determinar las hambuquesas que tiene un determinado ingrediente
    [HttpGet("HamburguesaDe/{ingrediente}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<IngredienteXhamburguesaDto>>> GetHamburIngredi(string ingrediente)
    {
        if (string.IsNullOrEmpty(ingrediente)) {
            throw new UnauthorizedAccessException("El parametro Ingrediente esta Nulo");
        }
        
        var lstHamburIngred = await _UnitOfWork.Ingredientes.GetAllIngrediXhamburAsync(ingrediente);

        if ((lstHamburIngred == null) || (lstHamburIngred.Count() == 0)) {
            throw new UnauthorizedAccessException("No se encotro ningun elemento");
        }

        return this.mapper.Map<List<IngredienteXhamburguesaDto>>(lstHamburIngred);
    }

    //Consulta para determinar el ingrediente mas caro que hay 
    [HttpGet("IngredienteMasCaro")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredienteDto>> GetIngredienteMasCaro()
    {
        var ingredieneCaro = await _UnitOfWork.Ingredientes.GetIndredienteMasCaroAsync();

        if (ingredieneCaro == null) {
            throw new UnauthorizedAccessException("No se encotro ningun elemento");
        }
        
        return this.mapper.Map<IngredienteDto>(ingredieneCaro);
    }

    //Consulta para determinar los ingredientes que esta dentro de un rango determinado segun un precio inferior y superior
    [HttpGet("RangoPrecio/{limInferior}/{LimSuperior}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<IngredienteDto>>> GetRangoPrecio(decimal limInferior, decimal limSuperior)
    {
        if (decimal.IsNegative(limInferior) || decimal.IsNegative(limSuperior) || (limInferior > limSuperior)) {
            throw new UnauthorizedAccessException("Uno de los limites ingresados en negativo o estan invertidos los limites");
        }

        var lstIngredRangoPrecio = await _UnitOfWork.Ingredientes.GetAllPrecioRangoAsync(limInferior, limSuperior);

        if ((lstIngredRangoPrecio == null) || (lstIngredRangoPrecio.Count() == 0)) {
            throw new UnauthorizedAccessException("No se encotro ningun elemento");
        }

        return this.mapper.Map<List<IngredienteDto>>(lstIngredRangoPrecio);
    }

    //Consulta para buscar por nombre de ingrediente y editar su descripcion 
    [HttpPut("EditDescrip/{nombre}/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredienteDto>> Put(int id, string nombre, [FromBody] IngredienteDto ingredienteDto)
    {
        var editarDescrip = await _UnitOfWork.Ingredientes.GetEditarDescripcionAsync(nombre);
        ingredienteDto.Id = editarDescrip.Id;
        ingredienteDto.Nombre = editarDescrip.Nombre;
        ingredienteDto.Precio = editarDescrip.Precio;
        ingredienteDto.Stock = editarDescrip.Stock;

        var editado =  this.mapper.Map<Ingrediente>(ingredienteDto);
        _UnitOfWork.Ingredientes.Update(editado);
        await _UnitOfWork.SaveAsync();
        return this.mapper.Map<IngredienteDto>(editado);
    }

        
}
