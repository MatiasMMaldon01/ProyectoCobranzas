namespace Api.PersistenceModels
{
    public class AlumnoModel : BaseModel
    {
        public string Apellido { get; set; }

        public string Nombre { get; set; }

        public string Dni { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Mail { get; set; }

        public int Legajo { get; set; }

        public decimal PorcBeca { get; set; }

        public DateTime FechaIngreso { get; set; }

        public List<long> CarrerasId { get; set; }
    }
}
