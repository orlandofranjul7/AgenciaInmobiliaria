﻿using System.ComponentModel.DataAnnotations;

namespace AgenciaInmobiliaria_API.Dto
{
    public class InmueblesDto
    {
        // DTO: Dta Transfer Object: Se usa para NO mostrar todas las propiedades
        // de un modelo.

        // Data Annotations solo son agregados a propiedades de los modelos
        public int id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public decimal Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
