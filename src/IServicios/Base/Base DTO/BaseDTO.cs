namespace IServicios.Base.Base_DTO
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public bool Eliminado { get; set; }
        public string EliminadoStr => Eliminado ? "SI" : "NO";
    }
}
