namespace Modelo.DTO
{
    public class DTOCuenta
    {
        public DTOCuenta() { }
        public int NumeroCuenta { get; set; }
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public string Cliente { get; set; }
        
    }
}
