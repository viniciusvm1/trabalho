using System;using System.Collections.Generic;using System.Data.SQLite;using System.IO;using System.Security.Cryptography;using System.Text;using System.Windows.Forms;using WindowsFormsApp1.Models;
namespace WindowsFormsApp1.Data
{
    public class UsuarioRepository
    {
        private readonly string cs;
        public UsuarioRepository(){string p=Path.Combine(Application.StartupPath,"Data");Directory.CreateDirectory(p);cs="Data Source="+Path.Combine(p,"hotel.db")+";Version=3;";using(var c=Abrir())using(var q=c.CreateCommand()){q.CommandText="CREATE TABLE IF NOT EXISTS Usuarios(Id INTEGER PRIMARY KEY AUTOINCREMENT,Nome TEXT NOT NULL UNIQUE,SenhaHash TEXT NOT NULL,Tipo TEXT NOT NULL)";q.ExecuteNonQuery();}if(Listar("admin").Count==0)Cadastrar("admin","admin","Master");}
        private SQLiteConnection Abrir(){var c=new SQLiteConnection(cs);c.Open();return c;}
        private static string Hash(string senha){using(var sha=SHA256.Create())return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(senha)));}
        public bool Validar(string nome,string senha){using(var c=Abrir())using(var q=new SQLiteCommand("SELECT COUNT(*) FROM Usuarios WHERE lower(Nome)=lower(@n) AND SenhaHash=@s",c)){q.Parameters.AddWithValue("@n",nome);q.Parameters.AddWithValue("@s",Hash(senha));return Convert.ToInt32(q.ExecuteScalar())==1;}}
        public void Cadastrar(string nome,string senha,string tipo){using(var c=Abrir())using(var q=new SQLiteCommand("INSERT INTO Usuarios(Nome,SenhaHash,Tipo) VALUES(@n,@s,@t)",c)){q.Parameters.AddWithValue("@n",nome);q.Parameters.AddWithValue("@s",Hash(senha));q.Parameters.AddWithValue("@t",tipo);q.ExecuteNonQuery();}}
        public List<Usuario> Listar(string busca=""){var l=new List<Usuario>();using(var c=Abrir())using(var q=new SQLiteCommand("SELECT Id,Nome,Tipo FROM Usuarios WHERE Nome LIKE @b ORDER BY Nome",c)){q.Parameters.AddWithValue("@b","%"+busca+"%");using(var r=q.ExecuteReader())while(r.Read())l.Add(new Usuario{Id=Convert.ToInt64(r["Id"]),Nome=r["Nome"].ToString(),Tipo=r["Tipo"].ToString()});}return l;}
    }
}
