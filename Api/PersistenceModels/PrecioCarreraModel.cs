namespace Api.PersistenceModels
{
    public class PrecioCarreraModel : BaseModel
    {
        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Matricula { get; set; }

        public int CarreraId { get; set; }
    }
}
