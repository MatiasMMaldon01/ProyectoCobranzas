namespace Api.PersistenceModels
{
    public class PagoModel : BaseModel
    {
        public string Legajo { get; set; }

        public int CantCuota { get; set; }

        public decimal Monto { get; set; }

        public long NroRecibo { get; set; }

        public DateTime FechaCarga { get; set; }

        public DateTime FechaRecibo { get; set; }

    }
}
