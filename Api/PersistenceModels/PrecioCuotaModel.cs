namespace Api.PersistenceModels
{
    public class PrecioCuotaModel : BaseModel
    {
        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        public int CarreraId { get; set; }
    }
}
