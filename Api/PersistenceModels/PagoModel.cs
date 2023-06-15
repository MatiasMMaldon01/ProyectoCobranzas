namespace Api.PersistenceModels
{
    public class PagoModel : BaseModel
    {
        public decimal Monto { get; set; }

        public long NroRecibo { get; set; }

        public DateTime FechaCarga { get; set; }

        public DateTime FechaRecibo { get; set; }

        public int CuotaId { get; set; }

        public int AlumnoId { get; set; }

    }
}
