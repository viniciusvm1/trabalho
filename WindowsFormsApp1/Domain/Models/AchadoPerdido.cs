using System;
namespace WindowsFormsApp1.Models
{
    public class AchadoPerdido
    {
        public long Id { get; set; }
        public string Item { get; set; }
        public string Quarto { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Status { get; set; }
    }
}
