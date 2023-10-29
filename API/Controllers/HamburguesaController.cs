using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")] //obtener solo las n de Hamburguesas
[ApiVersion("1.1")] //obtener las hamburguesas y las ingedientes
public class HamburguesaController : BaseApiController
{
    private readonly IUnitOfWorkInterface _UnitOfWork;
    private readonly IMapper mapper;

    public HamburguesaController(IUnitOfWorkInterface UnitOfWork, IMapper mapper)
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

    public async Task<ActionResult<List<HamburguesaDto>>> Get()
    {
        var hamburguesa = await _UnitOfWork.Hamburguesas.GetAllAsync();
        return this.mapper.Map<List<HamburguesaDto>>(hamburguesa);
    }

    //METODO GET (Para obtener paginacion, registro y busqueda en la entidad)
    [HttpGet("Pag")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pager<HamburguesaXingredienteDto>>> Get1B([FromQuery] Params hamburParams)
    {
        var hamburguesa = await _UnitOfWork.Hamburguesas.GetAllAsync(hamburParams.PageIndex, hamburParams.PageSize, hamburParams.Search);
        var lsthamburDto = this.mapper.Map<List<HamburguesaXingredienteDto>>(hamburguesa.registros);

        return new Pager<HamburguesaXingredienteDto>(lsthamburDto, hamburguesa.totalRegistros, hamburParams.PageIndex, hamburParams.PageSize, hamburParams.Search);
    }

    //METODO GET POR ID (Traer un solo registro de la entidad de la  Db)
    [HttpGet("{id}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HamburguesaXingredienteDto>> Get( int id)
    {
        var hamburguesa = await _UnitOfWork.Hamburguesas.GetByIdAsync(id);

        if (hamburguesa == null) {
            return NotFound();
        }

        return this.mapper.Map<HamburguesaXingredienteDto>(hamburguesa);
    }

    //METODO POST (para enviar registros a la entidad de la Db)
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HamburguesaDto>> Post(HamburguesaDto hamburguesaDto)
    {
        var hamburguesa = this.mapper.Map<Hamburguesa>(hamburguesaDto);
        _UnitOfWork.Hamburguesas.Add(hamburguesa);
        await _UnitOfWork.SaveAsync();

        if (hamburguesa == null) {
            return BadRequest();
        }

        return this.mapper.Map<HamburguesaDto>(hamburguesa);
    }

    //METODO PUT (editar un registro de la entidad de la Db)
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HamburguesaDto>> Put(int id, [FromBody] HamburguesaDto hamburguesaDto)
    {
        if (hamburguesaDto == null) {
            return NotFound();
        }

        var hamburguesa = this.mapper.Map<Hamburguesa>(hamburguesaDto);
        hamburguesa.Id = id;
        _UnitOfWork.Hamburguesas.Update(hamburguesa);
        await _UnitOfWork.SaveAsync();
        return this.mapper.Map<HamburguesaDto>(hamburguesa);
    }

    //METODO DELETE (Eliminar un registro de la entidad de la Db)
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HamburguesaDto>> Delete(int id)
    {
        var hamburguesa = await _UnitOfWork.Hamburguesas.GetByIdAsync(id);

        if (hamburguesa == null) {
            return NotFound();
        }

        _UnitOfWork.Hamburguesas.Remove(hamburguesa);
        await _UnitOfWork.SaveAsync();

        return NoContent();
    }

    //Consulta para obtener las Hamburguesas segun un ingrediente
    [HttpGet("HamburPor/{ingrediente}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<HamburguesaXingredienteDto>>> GetHamburIngredi(string ingrediente)
    {
        if (string.IsNullOrEmpty(ingrediente)) {
            throw new UnauthorizedAccessException("No se ingreso el Ingrediente a Buscar");
        }

        var hamburguesa = await _UnitOfWork.Hamburguesas.GetAllHamburIngredAsync(ingrediente);

        if ((hamburguesa.Count() == 0) || (hamburguesa == null)) {
            throw new UnauthorizedAccessException("No se encontro ningun elemento");
        }

        return this.mapper.Map<List<HamburguesaXingredienteDto>>(hamburguesa);
    }

    //Consulta para determinar que hamburguesas no contiene un ingrediente 
    [HttpGet("HamburSin/{ingrediente}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<HamburguesaXingredienteDto>>> GetHamburSin(string ingrediente)
    {
        if (string.IsNullOrEmpty(ingrediente)) {
            throw new UnauthorizedAccessException("No se ingreso el Ingrediente a Buscar");
        }

        var hamburguesaSin = await _UnitOfWork.Hamburguesas.GetAllHamburguesaSinAsync(ingrediente);

        if ((hamburguesaSin.Count() == 0) || (hamburguesaSin == null)) {
            throw new UnauthorizedAccessException("No se encontro ningun elemento");
        }

        return this.mapper.Map<List<HamburguesaXingredienteDto>>(hamburguesaSin);
    }

    //Consulta para determinar las Hamburguesas que son menores a un precion n
    [HttpGet("HamburMenorA/{precio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<HamburguesaDto>>> GetPrecioHamburMenor(decimal precio)
    {
        if (decimal.IsNegative(precio)) {
            throw new UnauthorizedAccessException("El precio ingresado no puede ser negativo");
        }

        var lstPrecioHambur = await _UnitOfWork.Hamburguesas.GetAllPrecioHamburguesaMenorAsync(precio);
        
        if ((lstPrecioHambur.Count() == 0) || (lstPrecioHambur == null)) {
            throw new UnauthorizedAccessException("No se encontro ningun elemento");
        }

        return this.mapper.Map<List<HamburguesaDto>>(lstPrecioHambur);
    }

    //Ordenar las Hamburguesas en orden Ascendente (menor a mayor) segun su precio
    [HttpGet("OrdenAscendente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<HamburguesaDto>>> GetOrdenarAscendente()
    {
        var lstOrdenadaAsc = await _UnitOfWork.Hamburguesas.GetAllOrdenAscendenteAsync();

        if ((lstOrdenadaAsc.Count() == 0) || (lstOrdenadaAsc == null)) {
            throw new UnauthorizedAccessException("No se encontro ningun elemento");
        }

        return this.mapper.Map<List<HamburguesaDto>>(lstOrdenadaAsc);
    }

  
}
