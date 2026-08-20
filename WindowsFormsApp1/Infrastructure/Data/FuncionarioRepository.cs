using System;using System.Collections.Generic;using System.Data.SQLite;using System.IO;using System.Windows.Forms;using WindowsFormsApp1.Models;
namespace WindowsFormsApp1.Data
{
    public class FuncionarioRepository
    {
        private readonly string cs;
        public FuncionarioRepository(){string p=Path.Combine(Application.StartupPath,"Data");Directory.CreateDirectory(p);cs="Data Source="+Path.Combine(p,"hotel.db")+";Version=3;";using(var c=Abrir())using(var q=c.CreateCommand()){q.CommandText=@"CREATE TABLE IF NOT EXISTS Funcionarios(Id INTEGER PRIMARY KEY AUTOINCREMENT,Nome TEXT NOT NULL,Cpf TEXT NOT NULL UNIQUE,Rg TEXT,DataNascimento TEXT,Sexo TEXT,Funcao TEXT,Admissao TEXT,Demissao TEXT,TipoContrato TEXT,Rua TEXT,Cep TEXT,Bairro TEXT,Cidade TEXT,Estado TEXT,Pais TEXT,Celular TEXT,Telefone1 TEXT,Telefone2 TEXT)";q.ExecuteNonQuery();}}
        private SQLiteConnection Abrir(){var c=new SQLiteConnection(cs);c.Open();return c;}
        public List<Funcionario> Listar(){var l=new List<Funcionario>();using(var c=Abrir())using(var q=new SQLiteCommand("SELECT * FROM Funcionarios ORDER BY Nome",c))using(var r=q.ExecuteReader())while(r.Read())l.Add(Ler(r));return l;}
        public void Salvar(Funcionario f){Executar(@"INSERT INTO Funcionarios(Nome,Cpf,Rg,DataNascimento,Sexo,Funcao,Admissao,Demissao,TipoContrato,Rua,Cep,Bairro,Cidade,Estado,Pais,Celular,Telefone1,Telefone2) VALUES(@Nome,@Cpf,@Rg,@DataNascimento,@Sexo,@Funcao,@Admissao,@Demissao,@TipoContrato,@Rua,@Cep,@Bairro,@Cidade,@Estado,@Pais,@Celular,@Telefone1,@Telefone2)",f);}
        public void Atualizar(Funcionario f){Executar(@"UPDATE Funcionarios SET Nome=@Nome,Cpf=@Cpf,Rg=@Rg,DataNascimento=@DataNascimento,Sexo=@Sexo,Funcao=@Funcao,Admissao=@Admissao,Demissao=@Demissao,TipoContrato=@TipoContrato,Rua=@Rua,Cep=@Cep,Bairro=@Bairro,Cidade=@Cidade,Estado=@Estado,Pais=@Pais,Celular=@Celular,Telefone1=@Telefone1,Telefone2=@Telefone2 WHERE Id=@Id",f);}
        public void Excluir(long id){using(var c=Abrir())using(var q=new SQLiteCommand("DELETE FROM Funcionarios WHERE Id=@Id",c)){q.Parameters.AddWithValue("@Id",id);q.ExecuteNonQuery();}}
        private void Executar(string sql,Funcionario f){using(var c=Abrir())using(var q=new SQLiteCommand(sql,c)){foreach(var p in typeof(Funcionario).GetProperties())q.Parameters.AddWithValue("@"+p.Name,p.GetValue(f,null)??"");q.ExecuteNonQuery();}}
        private static Funcionario Ler(SQLiteDataReader r)=>new Funcionario{Id=Convert.ToInt64(r["Id"]),Nome=r["Nome"].ToString(),Cpf=r["Cpf"].ToString(),Rg=r["Rg"].ToString(),DataNascimento=r["DataNascimento"].ToString(),Sexo=r["Sexo"].ToString(),Funcao=r["Funcao"].ToString(),Admissao=r["Admissao"].ToString(),Demissao=r["Demissao"].ToString(),TipoContrato=r["TipoContrato"].ToString(),Rua=r["Rua"].ToString(),Cep=r["Cep"].ToString(),Bairro=r["Bairro"].ToString(),Cidade=r["Cidade"].ToString(),Estado=r["Estado"].ToString(),Pais=r["Pais"].ToString(),Celular=r["Celular"].ToString(),Telefone1=r["Telefone1"].ToString(),Telefone2=r["Telefone2"].ToString()};
    }
}
