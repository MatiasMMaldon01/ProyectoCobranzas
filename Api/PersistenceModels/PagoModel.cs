namespace Api.PersistenceModels
{
    public class PagoModel : BaseModel
    {
        public decimal Monto { get; set; }

        public long CuotaId { get; set; }
    }
}
