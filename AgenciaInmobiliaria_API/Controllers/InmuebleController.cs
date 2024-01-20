using AgenciaInmobiliaria_API.Datos;
using AgenciaInmobiliaria_API.Dto;
using AgenciaInmobiliaria_API.Modelos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgenciaInmobiliaria_API.Controllers
{
    [Route("api/[controller]")] // Formato de las rutas
    [ApiController] // atributo que indica que el controlador es de tipo API
    [ProducesResponseType(StatusCodes.Status200OK)]


    /*
     ControllerBase: Clase que al ser heredada, convierte a
     la clase hija en una de tipo controlador
     */
    public class InmuebleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InmuebleController> _logger;
        private readonly IMapper _mapper;
        public InmuebleController(ApplicationDbContext applicationDbContext ,ILogger<InmuebleController> logger, IMapper mapper)
        {
            _context = applicationDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        // Los metodos de los controladores tipo API son EndPoints
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        /*
         ProduceResponseType se utiliza para documentar los
        diferentes tipos de codigos de estado que se manejaran
         */
        public async Task<ActionResult<IEnumerable<InmueblesDto>>> GetInmuebles()
        {
            _logger.LogInformation("Obtener los inmuebles");

            IEnumerable<Inmuebles> inmueblesList = await _context.Inmuebles.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<InmueblesDto>>(inmueblesList));
        }

        [HttpGet("id:int", Name = "GetInmueble")] // Nombre con el cual nos dirijimos a esta ruta
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InmueblesDto>> GetInmueble(int id)
        {
            if(id==0)
            {
                _logger.LogError("Error al traer la propiedad con id " + id);
                return BadRequest();
            }

            //var inmueble = InmuebleStore.inmuebleList.FirstOrDefault(i => i.id == id);
            var inmueble = await _context.Inmuebles.FirstOrDefaultAsync(i => i.id==id);

            if (inmueble==null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<InmueblesDto>(inmueble));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        /*
         [FromBody]: Atributo de C# que le indica a la Web API que debe de
         buscar el parametro del usuario en el cuerpo de la solicitud http
         */
        public async Task<ActionResult<InmueblesDto>> CrearInmueble([FromBody] InmueblesCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(createDto); // Si algunas de las propiedades no es valida (esta vacia, ej) se evitara que siga el flujo del codigo                                                                 
            }

            // ModelState Personalizado: En este caso, si existe una villa con el mismo nombre
            if (await _context.Inmuebles.FirstOrDefaultAsync(i=>i.Nombre.ToLower() == createDto.Nombre.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreYaExistente","El inmueble con ese nombre ya existe.");
                return BadRequest(ModelState);
            }

            if (createDto==null)
            {
                return BadRequest(createDto);
            }

            Inmuebles modelo = _mapper.Map<Inmuebles>(createDto);

            // Proceso para realizar un insert en la tabla de la base de datos
            await _context.Inmuebles.AddAsync(modelo);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetInmueble", new {id= modelo.id }, modelo);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // Se usa IActionResult y no su implementacion debido a que no es necesario
        // el modelo, porque cuando se trabaja don Delete, se debe retornar un No Content
        public async Task<IActionResult> DeleteInmueble(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }

            var inmueble = await _context.Inmuebles.FirstOrDefaultAsync(i=>i.id==id);

            if (inmueble==null)
            {
                return NotFound();
            }

            _context.Inmuebles.Remove(inmueble);
            _context.SaveChanges();

            return NoContent();

        }

        [HttpPut("{id:int}")] // La ruta de este EndPoint recibira un id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateInmueble(int id, [FromBody] InmueblesUpdateDto updateDto)
        {
            if (updateDto==null || id!=updateDto.id )
            {
                return BadRequest();
            }

            //var inmueble = InmuebleStore.inmuebleList.FirstOrDefault(i=>i.id==id);
            //inmueble.Nombre = inmueblesDto.Nombre;
            //inmueble.Ocupantes = inmueblesDto.Ocupantes;
            //inmueble.MetrosCuadrados = inmueblesDto.MetrosCuadrados;

            Inmuebles modelo = _mapper.Map<Inmuebles>(updateDto);

            _context.Update(modelo);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpPatch("{id:int}")] // La ruta de este EndPoint recibira un id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialInmueble(int id, JsonPatchDocument<InmueblesUpdateDto> patchDto)
        {
            if (patchDto == null || id != id)
            {
                return BadRequest();
            }

            var inmueble = await _context.Inmuebles.AsNoTracking().FirstOrDefaultAsync(i => i.id == id);
            // Se coloca el metodo AsNoTracking() debido a que mas adelante EF core trackeara
            // otra entidad de tipo inmueble con el mismo id y esto no es valido a la hora de 
            // de hacer cambios en la Base de datos. A groso modo, no se pueden instanciar 2
            // registros con el mismo id.

            InmueblesUpdateDto inmueblesDto = _mapper.Map<InmueblesUpdateDto>(inmueble);



            patchDto.ApplyTo(inmueblesDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Crear modelo de tipo inmueble que tenga las mismas
            // propiedades que el dto, pero luego de pasar el patchDto.ApplyTo
            // que contiene lo unico que se va modificar y se lo devuelve
            // al modelo de tipo inmueble, que es el modelo de la Base de datos
            // y el que es de tipo DbSet, que tiene un metodo para Update

            Inmuebles modelo = _mapper.Map<Inmuebles>(inmueblesDto);

            _context.Inmuebles.Update(modelo);
            _context.SaveChanges();
            return NoContent();


        }

    }
}