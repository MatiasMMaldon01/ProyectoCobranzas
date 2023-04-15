namespace Api.PersistenceModels
{
    public class CuotaModel : BaseModel
    {
        public int Numero { get; set; }

        public long AlumnoId { get; set; }

        public long PrecioCuotaId { get; set; }
    }
}
