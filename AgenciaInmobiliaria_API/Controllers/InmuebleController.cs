using AgenciaInmobiliaria_API.Datos;
using AgenciaInmobiliaria_API.Dto;
using AgenciaInmobiliaria_API.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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
        public InmuebleController(ApplicationDbContext applicationDbContext ,ILogger<InmuebleController> logger)
        {
            _context = applicationDbContext;
            _logger = logger;
        }

        // Los metodos de los controladores tipo API son EndPoints
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        /*
         ProduceResponseType se utiliza para documentar los
        diferetnes tipos de codigos de estado que se manejaran
         */
        public ActionResult<IEnumerable<InmueblesDto>> GetInmuebles()
        {
            _logger.LogInformation("Obtener los inmuebles");
            return Ok(_context.Inmuebles.ToList());
        }

        [HttpGet("id:int", Name = "GetInmueble")] // Nombre con el cual nos dirijimos a esta ruta
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<InmueblesDto> GetInmueble(int id)
        {
            if(id==0)
            {
                _logger.LogError("Error al traer la propiedad con id " + id);
                return BadRequest();
            }

            //var inmueble = InmuebleStore.inmuebleList.FirstOrDefault(i => i.id == id);
            var inmueble = _context.Inmuebles.FirstOrDefault(i => i.id==id);

            if (inmueble==null) 
            {
                return NotFound();
            }

            return Ok(inmueble);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        /*
         [FromBody]: Atributo de C# que le indica a la Web API que debe de
         buscar el parametro del usuario en el cuerpo de la solicitud http
         */
        public ActionResult<InmueblesDto> CrearInmueble([FromBody] InmueblesDto inmueblesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(inmueblesDto); // Si algunas de las propiedades no es valida (esta vacia, ej) se evitara que siga el flujo del codigo                                                                 
            }

            // ModelState Personalizado: En este caso, si existe una villa con el mismo nombre
            if (_context.Inmuebles.FirstOrDefault(i=>i.Nombre.ToLower() == inmueblesDto.Nombre.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreYaExistente","El inmueble con ese nombre ya existe.");
                return BadRequest(ModelState);
            }

            if (inmueblesDto==null)
            {
                return BadRequest(inmueblesDto);
            }
            if (inmueblesDto.id>0) // quiere decir que no necesitamos un id, puesto que se genera automaticamente
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Inmuebles modelo = new()
            {
                Nombre = inmueblesDto.Nombre,
                Detalle = inmueblesDto.Detalle,
                ImagenUrl = inmueblesDto.ImagenUrl,
                Ocupantes = inmueblesDto.Ocupantes,
                Tarifa = inmueblesDto.Tarifa,
                MetrosCuadrados = inmueblesDto.MetrosCuadrados,
                Amenidad = inmueblesDto.Amenidad,
                FechaCreacion = DateTime.Now,
                FechaActualizacion = DateTime.Now
            };

            // Proceso para realizar un insert en la tabla de la base de datos
            _context.Inmuebles.Add(modelo);
            _context.SaveChanges();

            return CreatedAtRoute("GetInmueble", new {id= inmueblesDto.id }, inmueblesDto);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // Se usa IActionResult y no su implementacion debido a que no es necesario
        // el modelo, porque cuando se trabaja don Delete, se debe retornar un No Content
        public IActionResult DeleteInmueble(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }

            var inmueble = _context.Inmuebles.FirstOrDefault(i=>i.id==id);

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
        public IActionResult UpdateInmueble(int id, [FromBody] InmueblesDto inmueblesDto)
        {
            if (inmueblesDto==null || id!=inmueblesDto.id )
            {
                return BadRequest();
            }

            //var inmueble = InmuebleStore.inmuebleList.FirstOrDefault(i=>i.id==id);
            //inmueble.Nombre = inmueblesDto.Nombre;
            //inmueble.Ocupantes = inmueblesDto.Ocupantes;
            //inmueble.MetrosCuadrados = inmueblesDto.MetrosCuadrados;

            Inmuebles modelo = new()
            {
                id = inmueblesDto.id,
                Nombre = inmueblesDto.Nombre,
                Detalle = inmueblesDto.Detalle,
                ImagenUrl = inmueblesDto.ImagenUrl,
                Ocupantes = inmueblesDto.Ocupantes,
                Tarifa = inmueblesDto.Tarifa,
                MetrosCuadrados = inmueblesDto.MetrosCuadrados,
                Amenidad = inmueblesDto.Amenidad
            };

            _context.Update(modelo);
            _context.SaveChanges();

            return NoContent();


        }

        [HttpPatch("{id:int}")] // La ruta de este EndPoint recibira un id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialInmueble(int id, JsonPatchDocument<InmueblesDto> patchDto)
        {
            if (patchDto == null || id != id)
            {
                return BadRequest();
            }

            var inmueble = _context.Inmuebles.FirstOrDefault(i => i.id == id);

            InmueblesDto inmueblesDto = new()
            {
                id = inmueble.id,
                Nombre = inmueble.Nombre,
                Detalle = inmueble.Detalle,
                ImagenUrl = inmueble.ImagenUrl,
                Ocupantes = inmueble.Ocupantes,
                Tarifa = inmueble.Tarifa,
                MetrosCuadrados = inmueble.MetrosCuadrados,
                Amenidad = inmueble.Amenidad
            };

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

            Inmuebles modelo = new()
            {
                id = inmueblesDto.id,
                Nombre = inmueblesDto.Nombre,
                Detalle = inmueblesDto.Detalle,
                ImagenUrl = inmueblesDto.ImagenUrl,
                Ocupantes = inmueblesDto.Ocupantes,
                Tarifa = inmueblesDto.Tarifa,
                MetrosCuadrados = inmueblesDto.MetrosCuadrados,
                Amenidad = inmueblesDto.Amenidad
            };

            return NoContent();


        }

    }
}
