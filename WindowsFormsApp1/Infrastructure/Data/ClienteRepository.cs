using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Data
{
    public class ClienteRepository
    {
        private readonly string connectionString;

        public ClienteRepository()
        {
            string pasta = Path.Combine(Application.StartupPath, "Data");
            Directory.CreateDirectory(pasta);
            string arquivo = Path.Combine(pasta, "hotel.db");
            connectionString = "Data Source=" + arquivo + ";Version=3;";
            CriarTabela();
        }

        private SQLiteConnection AbrirConexao()
        {
            SQLiteConnection conexao = new SQLiteConnection(connectionString);
            conexao.Open();
            return conexao;
        }

        private void CriarTabela()
        {
            using (SQLiteConnection conexao = AbrirConexao())
            using (SQLiteCommand comando = conexao.CreateCommand())
            {
                comando.CommandText = @"CREATE TABLE IF NOT EXISTS Clientes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT, Nome TEXT NOT NULL,
                    Cpf TEXT NOT NULL UNIQUE, Rg TEXT, DataNascimento TEXT, Sexo TEXT,
                    Rua TEXT, Cep TEXT, Bairro TEXT, Cidade TEXT, Estado TEXT, Pais TEXT,
                    Celular TEXT, Telefone1 TEXT, Telefone2 TEXT)";
                comando.ExecuteNonQuery();
            }
        }

        public List<Cliente> Listar()
        {
            List<Cliente> clientes = new List<Cliente>();
            using (SQLiteConnection conexao = AbrirConexao())
            using (SQLiteCommand comando = new SQLiteCommand("SELECT * FROM Clientes ORDER BY Nome", conexao))
            using (SQLiteDataReader leitor = comando.ExecuteReader())
                while (leitor.Read()) clientes.Add(Ler(leitor));
            return clientes;
        }

        public void Salvar(Cliente cliente)
        {
            const string sql = @"INSERT INTO Clientes
                (Nome,Cpf,Rg,DataNascimento,Sexo,Rua,Cep,Bairro,Cidade,Estado,Pais,Celular,Telefone1,Telefone2)
                VALUES (@Nome,@Cpf,@Rg,@DataNascimento,@Sexo,@Rua,@Cep,@Bairro,@Cidade,@Estado,@Pais,@Celular,@Telefone1,@Telefone2)";
            Executar(sql, cliente);
        }

        public void Atualizar(Cliente cliente)
        {
            const string sql = @"UPDATE Clientes SET Nome=@Nome,Cpf=@Cpf,Rg=@Rg,
                DataNascimento=@DataNascimento,Sexo=@Sexo,Rua=@Rua,Cep=@Cep,Bairro=@Bairro,
                Cidade=@Cidade,Estado=@Estado,Pais=@Pais,Celular=@Celular,
                Telefone1=@Telefone1,Telefone2=@Telefone2 WHERE Id=@Id";
            Executar(sql, cliente);
        }

        public void Excluir(long id)
        {
            using (SQLiteConnection conexao = AbrirConexao())
            using (SQLiteCommand comando = new SQLiteCommand("DELETE FROM Clientes WHERE Id=@Id", conexao))
            { comando.Parameters.AddWithValue("@Id", id); comando.ExecuteNonQuery(); }
        }

        private void Executar(string sql, Cliente c)
        {
            using (SQLiteConnection conexao = AbrirConexao())
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@Id", c.Id); cmd.Parameters.AddWithValue("@Nome", c.Nome);
                cmd.Parameters.AddWithValue("@Cpf", c.Cpf); cmd.Parameters.AddWithValue("@Rg", c.Rg);
                cmd.Parameters.AddWithValue("@DataNascimento", c.DataNascimento); cmd.Parameters.AddWithValue("@Sexo", c.Sexo);
                cmd.Parameters.AddWithValue("@Rua", c.Rua); cmd.Parameters.AddWithValue("@Cep", c.Cep);
                cmd.Parameters.AddWithValue("@Bairro", c.Bairro); cmd.Parameters.AddWithValue("@Cidade", c.Cidade);
                cmd.Parameters.AddWithValue("@Estado", c.Estado); cmd.Parameters.AddWithValue("@Pais", c.Pais);
                cmd.Parameters.AddWithValue("@Celular", c.Celular); cmd.Parameters.AddWithValue("@Telefone1", c.Telefone1);
                cmd.Parameters.AddWithValue("@Telefone2", c.Telefone2); cmd.ExecuteNonQuery();
            }
        }

        private static Cliente Ler(SQLiteDataReader r) => new Cliente {
            Id = Convert.ToInt64(r["Id"]), Nome = r["Nome"].ToString(), Cpf = r["Cpf"].ToString(), Rg = r["Rg"].ToString(),
            DataNascimento = r["DataNascimento"].ToString(), Sexo = r["Sexo"].ToString(), Rua = r["Rua"].ToString(),
            Cep = r["Cep"].ToString(), Bairro = r["Bairro"].ToString(), Cidade = r["Cidade"].ToString(),
            Estado = r["Estado"].ToString(), Pais = r["Pais"].ToString(), Celular = r["Celular"].ToString(),
            Telefone1 = r["Telefone1"].ToString(), Telefone2 = r["Telefone2"].ToString()
        };
    }
}
