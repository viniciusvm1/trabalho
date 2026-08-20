using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Data
{
    public class ReservaRepository
    {
        private readonly string connectionString;
        public ReservaRepository()
        {
            string pasta = Path.Combine(Application.StartupPath, "Data"); Directory.CreateDirectory(pasta);
            connectionString = "Data Source=" + Path.Combine(pasta, "hotel.db") + ";Version=3;";
            using (SQLiteConnection c = Abrir()) using (SQLiteCommand cmd = c.CreateCommand()) {
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Quartos (Id INTEGER PRIMARY KEY AUTOINCREMENT, Numero TEXT NOT NULL UNIQUE, Categoria TEXT NOT NULL, Capacidade INTEGER NOT NULL, ValorDiaria NUMERIC NOT NULL);
                CREATE TABLE IF NOT EXISTS Reservas (Id INTEGER PRIMARY KEY AUTOINCREMENT, ClienteId INTEGER NOT NULL, QuartoId INTEGER NOT NULL, Entrada TEXT NOT NULL, Saida TEXT NOT NULL, Adultos INTEGER, Criancas INTEGER, ValorTotal NUMERIC, Observacao TEXT, Status TEXT NOT NULL DEFAULT 'Reservada');";
                cmd.ExecuteNonQuery();
            }
        }
        private SQLiteConnection Abrir() { var c = new SQLiteConnection(connectionString); c.Open(); return c; }
        public void SalvarQuarto(Quarto q) { using (var c=Abrir()) using(var cmd=new SQLiteCommand("INSERT INTO Quartos (Numero,Categoria,Capacidade,ValorDiaria) VALUES (@n,@c,@p,@v)",c)){cmd.Parameters.AddWithValue("@n",q.Numero);cmd.Parameters.AddWithValue("@c",q.Categoria);cmd.Parameters.AddWithValue("@p",q.Capacidade);cmd.Parameters.AddWithValue("@v",q.ValorDiaria);cmd.ExecuteNonQuery();} }
        public List<Quarto> ListarQuartos() { var lista=new List<Quarto>(); using(var c=Abrir()) using(var cmd=new SQLiteCommand("SELECT * FROM Quartos ORDER BY Numero",c)) using(var r=cmd.ExecuteReader()) while(r.Read()) lista.Add(new Quarto{Id=Convert.ToInt64(r["Id"]),Numero=r["Numero"].ToString(),Categoria=r["Categoria"].ToString(),Capacidade=Convert.ToInt32(r["Capacidade"]),ValorDiaria=Convert.ToDecimal(r["ValorDiaria"])}); return lista; }
        public bool ExisteConflito(long quartoId, DateTime entrada, DateTime saida, long ignorarId) { using(var c=Abrir()) using(var cmd=new SQLiteCommand("SELECT COUNT(*) FROM Reservas WHERE QuartoId=@q AND Id<>@id AND Status<>'Cancelada' AND Entrada<@saida AND Saida>@entrada",c)){cmd.Parameters.AddWithValue("@q",quartoId);cmd.Parameters.AddWithValue("@id",ignorarId);cmd.Parameters.AddWithValue("@entrada",entrada.ToString("s"));cmd.Parameters.AddWithValue("@saida",saida.ToString("s"));return Convert.ToInt32(cmd.ExecuteScalar())>0;} }
        public void Salvar(Reserva r) { Executar(@"INSERT INTO Reservas (ClienteId,QuartoId,Entrada,Saida,Adultos,Criancas,ValorTotal,Observacao,Status) VALUES (@cliente,@quarto,@entrada,@saida,@adultos,@criancas,@valor,@obs,'Reservada')",r); }
        public void Atualizar(Reserva r) { Executar(@"UPDATE Reservas SET ClienteId=@cliente,QuartoId=@quarto,Entrada=@entrada,Saida=@saida,Adultos=@adultos,Criancas=@criancas,ValorTotal=@valor,Observacao=@obs WHERE Id=@id",r); }
        private void Executar(string sql, Reserva r) { using(var c=Abrir()) using(var cmd=new SQLiteCommand(sql,c)){cmd.Parameters.AddWithValue("@id",r.Id);cmd.Parameters.AddWithValue("@cliente",r.ClienteId);cmd.Parameters.AddWithValue("@quarto",r.QuartoId);cmd.Parameters.AddWithValue("@entrada",r.Entrada.ToString("s"));cmd.Parameters.AddWithValue("@saida",r.Saida.ToString("s"));cmd.Parameters.AddWithValue("@adultos",r.Adultos);cmd.Parameters.AddWithValue("@criancas",r.Criancas);cmd.Parameters.AddWithValue("@valor",r.ValorTotal);cmd.Parameters.AddWithValue("@obs",r.Observacao);cmd.ExecuteNonQuery();} }
        public void Cancelar(long id) { using(var c=Abrir()) using(var cmd=new SQLiteCommand("UPDATE Reservas SET Status='Cancelada' WHERE Id=@id",c)){cmd.Parameters.AddWithValue("@id",id);cmd.ExecuteNonQuery();} }
        public void AtualizarStatus(long id, string status) { using(var c=Abrir()) using(var cmd=new SQLiteCommand("UPDATE Reservas SET Status=@status WHERE Id=@id",c)){cmd.Parameters.AddWithValue("@id",id);cmd.Parameters.AddWithValue("@status",status);cmd.ExecuteNonQuery();} }
        public List<Reserva> Listar() { var l=new List<Reserva>(); const string sql="SELECT r.*,c.Nome Cliente,q.Numero Quarto FROM Reservas r JOIN Clientes c ON c.Id=r.ClienteId JOIN Quartos q ON q.Id=r.QuartoId ORDER BY r.Entrada DESC"; using(var c=Abrir()) using(var cmd=new SQLiteCommand(sql,c)) using(var x=cmd.ExecuteReader()) while(x.Read()) l.Add(new Reserva{Id=Convert.ToInt64(x["Id"]),ClienteId=Convert.ToInt64(x["ClienteId"]),Cliente=x["Cliente"].ToString(),QuartoId=Convert.ToInt64(x["QuartoId"]),Quarto=x["Quarto"].ToString(),Entrada=DateTime.Parse(x["Entrada"].ToString()),Saida=DateTime.Parse(x["Saida"].ToString()),Adultos=Convert.ToInt32(x["Adultos"]),Criancas=Convert.ToInt32(x["Criancas"]),ValorTotal=Convert.ToDecimal(x["ValorTotal"]),Observacao=x["Observacao"].ToString(),Status=x["Status"].ToString()}); return l; }
    }
}
