namespace IServicios.Pago.CargasMasivas
{
    public interface IPagoCargaMasiva
    {
        Task CargaMasivaPago();
        Task EliminacionMasivaPagos(DateTime desde, DateTime hasta);
    }
}
