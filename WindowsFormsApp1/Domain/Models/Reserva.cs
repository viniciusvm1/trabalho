using System;

namespace WindowsFormsApp1.Models
{
    public class Reserva
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }
        public string Cliente { get; set; }
        public long QuartoId { get; set; }
        public string Quarto { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }
        public int Adultos { get; set; }
        public int Criancas { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }
        public string Status { get; set; }
    }
}
