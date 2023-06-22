namespace IServicios.Persona.DTO_s
{
    public class FiltroAlumnosDTO 
    {
        public string Legajo { get; set; }

        public string Carrera { get; set; }

        public string Apynom { get; set; }

        public string NroDoc { get; set; }

        public string Mail { get; set; }

        public string Extension { get; set; }

        public bool Activo { get; set; }

        public string ActivoStr => Activo ? "PUEDE RENDIR" : "NO PUEDE RENDIR";
    }
}
