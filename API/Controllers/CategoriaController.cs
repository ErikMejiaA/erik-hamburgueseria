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
public class CategoriaController : BaseApiController
{
    private readonly IUnitOfWorkInterface _UnitOfWork;
    private readonly IMapper mapper;

    public CategoriaController(IUnitOfWorkInterface UnitOfWork, IMapper mapper)
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

    public async Task<ActionResult<List<CategoriaDto>>> Get()
    {
        var categorias = await _UnitOfWork.Categorias.GetAllAsync();
        return this.mapper.Map<List<CategoriaDto>>(categorias);
    }

    //METODO GET (Para obtener paginacion, registro y busqueda en la entidad)
    [HttpGet("Pag")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pager<CategoriaXhamburguesaDto>>> Get1B([FromQuery] Params categoParams)
    {
        var categoria = await _UnitOfWork.Categorias.GetAllAsync(categoParams.PageIndex, categoParams.PageSize, categoParams.Search);
        var lstCategiDto = this.mapper.Map<List<CategoriaXhamburguesaDto>>(categoria.registros);

        return new Pager<CategoriaXhamburguesaDto>(lstCategiDto, categoria.totalRegistros, categoParams.PageIndex, categoParams.PageSize, categoParams.Search);
    }

    //METODO GET POR ID (Traer un solo registro de la entidad de la  Db)
    [HttpGet("{id}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaXhamburguesaDto>> Get( int id)
    {
        var categoria = await _UnitOfWork.Categorias.GetByIdAsync(id);

        if (categoria == null) {
            return NotFound();
        }

        return this.mapper.Map<CategoriaXhamburguesaDto>(categoria);
    }

    //METODO POST (para enviar registros a la entidad de la Db)
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaDto>> Post(CategoriaDto CategoriaDto)
    {
        var categoria = this.mapper.Map<Categoria>(CategoriaDto);
        _UnitOfWork.Categorias.Add(categoria);
        await _UnitOfWork.SaveAsync();

        if (categoria == null) {
            return BadRequest();
        }

        return this.mapper.Map<CategoriaDto>(categoria);
    }

    //METODO PUT (editar un registro de la entidad de la Db)
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaDto>> Put(int id, [FromBody] CategoriaDto categoriaDto)
    {
        if (categoriaDto == null) {
            return NotFound();
        }

        var categoria = this.mapper.Map<Categoria>(categoriaDto);
        categoria.Id = id;
        _UnitOfWork.Categorias.Update(categoria);
        await _UnitOfWork.SaveAsync();
        return this.mapper.Map<CategoriaDto>(categoria);
    }

    //METODO DELETE (Eliminar un registro de la entidad de la Db)
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaDto>> Delete(int id)
    {
        var categoria = await _UnitOfWork.Categorias.GetByIdAsync(id);

        if (categoria == null) {
            return NotFound();
        }

        _UnitOfWork.Categorias.Remove(categoria);
        await _UnitOfWork.SaveAsync();

        return NoContent();
    }

    //metodos para dos ID, entidades de muchos a muchos
    //METODO GET POR ID (Traer un solo registro de la entidad de la  Db)
    /*[HttpGet('{idUsua}/{idRol}')]
    [Authorize]


    public async Task<ActionResult<UsuariosRolesDto>> Get( int idUsua, int idRol)
    {
        var usuarioRol = await _UnitOfWork.UsuariosRoles.GetByIdAsync(idUsua, idRol);

        if (usuarioRol == null) {
            return NotFound();
        }

        return this.mapper.Map<UsuariosRolesDto>(usuarioRol);
    }

    //METODO PUT (editar un registro de la entidad de la Db)
    [HttpPut('{idUsua}/{idRol}')]
    [Authorize(Roles = 'Administrador')]


    public async Task<ActionResult<UsuariosRolesDto>> Put(int idUsua, int idRol, [FromBody] UsuariosRolesDto usuariosRolesDto)
    {
        if (usuariosRolesDto == null) {
            return NotFound();
        }

        var usuarioRol = this.mapper.Map<UsuariosRoles>(usuariosRolesDto);
        usuarioRol.UsuarioId = idUsua;
        usuarioRol.RolId = idRol;
        _UnitOfWork.UsuariosRoles.Update(usuarioRol);
        await _UnitOfWork.SaveAsync();

        return this.mapper.Map<UsuariosRolesDto>(usuarioRol);
    }

    //METODO DELETE (Eliminar un registro de la entidad de la Db)
    [HttpDelete('{idUsua}/{idRol}')]
    [Authorize(Roles = 'Administrador')]


    public async Task<ActionResult<UsuariosRolesDto>> Delete(int idUsua, int idRol)
    {
        var usuarioRol = await _UnitOfWork.UsuariosRoles.GetByIdAsync (idUsua, idRol);

        if (usuarioRol == null) {
            return NotFound();
        }

        _UnitOfWork.UsuariosRoles.Remove(usuarioRol);
        await _UnitOfWork.SaveAsync();

        return NoContent();
    }*/
}

