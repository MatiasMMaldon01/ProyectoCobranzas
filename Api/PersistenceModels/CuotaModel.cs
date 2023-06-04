namespace Api.PersistenceModels
{
    public class CuotaModel : BaseModel
    {
        public int Numero { get; set; }

        public int AlumnoId { get; set; }

        public int PrecioCuotaId { get; set; }
    }
}
