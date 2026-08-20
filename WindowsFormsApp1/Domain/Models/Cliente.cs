namespace WindowsFormsApp1.Models
{
    public class Cliente
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Rua { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Celular { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public override string ToString() => Nome + " - " + Cpf;
    }
}
