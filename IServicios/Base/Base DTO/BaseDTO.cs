namespace IServicios.Base.Base_DTO
{
    public class BaseDTO
    {
        public long Id { get; set; }
        public bool Eliminado { get; set; }
        public string EliminadoStr => Eliminado ? "SI" : "NO";
    }
}
