namespace Api.PersistenceModels
{
    public class PagoModel : BaseModel
    {
        public string Legajo { get; set; }

        public int CantCuota { get; set; }

        public int Monto { get; set; }

        public int NroRecibo { get; set; }

        public DateTime FechaCarga { get; set; }

        public DateTime FechaRecibo { get; set; }

    }
}
