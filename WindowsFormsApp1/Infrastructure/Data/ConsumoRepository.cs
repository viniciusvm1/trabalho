using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Data
{
    public class ConsumoRepository
    {
        private readonly string cs;
        public ConsumoRepository()
        {
            string pasta=Path.Combine(Application.StartupPath,"Data");Directory.CreateDirectory(pasta);cs="Data Source="+Path.Combine(pasta,"hotel.db")+";Version=3;";
            using(var c=Abrir())using(var cmd=c.CreateCommand()){cmd.CommandText=@"CREATE TABLE IF NOT EXISTS Clientes (Id INTEGER PRIMARY KEY AUTOINCREMENT, Nome TEXT NOT NULL, Cpf TEXT NOT NULL UNIQUE, Rg TEXT, DataNascimento TEXT, Sexo TEXT, Rua TEXT, Cep TEXT, Bairro TEXT, Cidade TEXT, Estado TEXT, Pais TEXT, Celular TEXT, Telefone1 TEXT, Telefone2 TEXT);
            CREATE TABLE IF NOT EXISTS Quartos (Id INTEGER PRIMARY KEY AUTOINCREMENT, Numero TEXT NOT NULL UNIQUE, Categoria TEXT NOT NULL, Capacidade INTEGER NOT NULL, ValorDiaria NUMERIC NOT NULL);
            CREATE TABLE IF NOT EXISTS Reservas (Id INTEGER PRIMARY KEY AUTOINCREMENT, ClienteId INTEGER NOT NULL, QuartoId INTEGER NOT NULL, Entrada TEXT NOT NULL, Saida TEXT NOT NULL, Adultos INTEGER, Criancas INTEGER, ValorTotal NUMERIC, Observacao TEXT, Status TEXT NOT NULL DEFAULT 'Reservada');
            CREATE TABLE IF NOT EXISTS Produtos (Id INTEGER PRIMARY KEY AUTOINCREMENT,Nome TEXT NOT NULL UNIQUE,Preco NUMERIC NOT NULL,Estoque INTEGER NOT NULL);
            CREATE TABLE IF NOT EXISTS Consumos (Id INTEGER PRIMARY KEY AUTOINCREMENT,ReservaId INTEGER NOT NULL,ProdutoId INTEGER NOT NULL,Quantidade INTEGER NOT NULL,ValorUnitario NUMERIC NOT NULL,Data TEXT NOT NULL);
            INSERT OR IGNORE INTO Produtos (Nome,Preco,Estoque) VALUES ('Água',4,50),('Água com gás',5,50),('Refrigerante',7,50),('Suco',8,50),('Chocolate',6,50),('Batata chips',9,50);";cmd.ExecuteNonQuery();}
        }
        private SQLiteConnection Abrir(){var c=new SQLiteConnection(cs);c.Open();return c;}
        public List<Reserva> ListarHospedagens(){var l=new List<Reserva>();using(var c=Abrir())using(var cmd=new SQLiteCommand("SELECT r.Id,c.Nome Cliente,q.Numero Quarto FROM Reservas r JOIN Clientes c ON c.Id=r.ClienteId JOIN Quartos q ON q.Id=r.QuartoId WHERE r.Status='Hospedado' ORDER BY c.Nome",c))using(var x=cmd.ExecuteReader())while(x.Read())l.Add(new Reserva{Id=Convert.ToInt64(x["Id"]),Cliente=x["Cliente"].ToString(),Quarto=x["Quarto"].ToString()});return l;}
        public List<Produto> ListarProdutos(){var l=new List<Produto>();using(var c=Abrir())using(var cmd=new SQLiteCommand("SELECT * FROM Produtos ORDER BY Id",c))using(var x=cmd.ExecuteReader())while(x.Read())l.Add(new Produto{Id=Convert.ToInt64(x["Id"]),Nome=x["Nome"].ToString(),Preco=Convert.ToDecimal(x["Preco"]),Estoque=Convert.ToInt32(x["Estoque"])});return l;}
        public void Registrar(long reservaId,List<Tuple<Produto,int>> itens)
        {
            using(var c=Abrir())using(var tx=c.BeginTransaction())try{foreach(var item in itens){if(item.Item2>item.Item1.Estoque)throw new InvalidOperationException("Estoque insuficiente para "+item.Item1.Nome+".");using(var cmd=new SQLiteCommand("INSERT INTO Consumos (ReservaId,ProdutoId,Quantidade,ValorUnitario,Data) VALUES (@r,@p,@q,@v,@d); UPDATE Produtos SET Estoque=Estoque-@q WHERE Id=@p;",c,tx)){cmd.Parameters.AddWithValue("@r",reservaId);cmd.Parameters.AddWithValue("@p",item.Item1.Id);cmd.Parameters.AddWithValue("@q",item.Item2);cmd.Parameters.AddWithValue("@v",item.Item1.Preco);cmd.Parameters.AddWithValue("@d",DateTime.Now.ToString("s"));cmd.ExecuteNonQuery();}}tx.Commit();}catch{tx.Rollback();throw;}
        }
        public decimal TotalReserva(long reservaId){using(var c=Abrir())using(var cmd=new SQLiteCommand("SELECT COALESCE(SUM(Quantidade*ValorUnitario),0) FROM Consumos WHERE ReservaId=@r",c)){cmd.Parameters.AddWithValue("@r",reservaId);return Convert.ToDecimal(cmd.ExecuteScalar());}}
    }
}
