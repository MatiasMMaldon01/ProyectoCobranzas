namespace Api.PersistenceModels
{
    public class PagoModel : BaseModel
    {
        public decimal Monto { get; set; }

        public int CuotaId { get; set; }
    }
}
