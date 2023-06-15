namespace Api.PersistenceModels
{
    public class CuotaModel : BaseModel
    {
        public int Numero { get; set; }
              
        public DateTime Fecha { get; set; }

        public int PrecioCarreraId { get; set; }

    }
}
