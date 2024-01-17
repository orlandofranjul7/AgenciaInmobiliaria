using Microsoft.EntityFrameworkCore;

namespace AgenciaInmobiliaria_API.Modelos
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)  // aplicar inyeccion de dependencias
        {
            
        }

        // Aqui se detalla que el modelo de inmueble de tipo DbSert
        // se creara como una tabla en la base de datos.
        public DbSet<Inmuebles> Inmuebles { get; set; }

        // El siguiente metodo se usa para crear registros cuando se hace una migracion 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inmuebles>().HasData(
                new Inmuebles()
                {
                    id=1,
                    Nombre="Villa en La Romana",
                    Detalle="Detlle de la Villa",
                    ImagenUrl="",
                    Ocupantes=10,
                    MetrosCuadrados=120,
                    Tarifa=200,
                    Amenidad="",
                    FechaCreacion=DateTime.Now,
                    FechaActualizacion=DateTime.Now
                },
                new Inmuebles()
                {
                    id = 2,
                    Nombre = "Villa en La Romana",
                    Detalle = "Detlle de la Villa",
                    ImagenUrl = "",
                    Ocupantes = 10,
                    MetrosCuadrados = 120,
                    Tarifa = 200,
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                }
            );
            
        }

    }
}
