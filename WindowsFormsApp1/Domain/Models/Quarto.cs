namespace WindowsFormsApp1.Models
{
    public class Quarto
    {
        public long Id { get; set; }
        public string Numero { get; set; }
        public string Categoria { get; set; }
        public int Capacidade { get; set; }
        public decimal ValorDiaria { get; set; }
        public override string ToString() => Numero + " - " + Categoria + " (R$ " + ValorDiaria.ToString("N2") + ")";
    }
}
