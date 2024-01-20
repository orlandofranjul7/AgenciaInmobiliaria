using System.ComponentModel.DataAnnotations;

namespace AgenciaInmobiliaria_API.Dto
{
    public class InmueblesUpdateDto
    {
        // DTO: Dta Transfer Object: Se usa para NO mostrar todas las propiedades
        // de un modelo.

        // Data Annotations solo son agregados a propiedades de los modelos
        [Required]
        public int id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public decimal Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        [Required]
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
