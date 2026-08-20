using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Data;

namespace WindowsFormsApp1
{
    public partial class UsuarioForm : Form
    {
        private readonly UsuarioRepository repositorio;
        public UsuarioForm()
        {
            InitializeComponent();
            repositorio=new UsuarioRepository();textBox1.UseSystemPasswordChar=true;dataGridView1.AutoGenerateColumns=true;dataGridView1.Columns.Clear();dataGridView1.ReadOnly=true;
            button1.Click+=(s,e)=>Carregar();button2.Click+=(s,e)=>Cadastrar();button3.Click+=(s,e)=>Limpar();Carregar();
        }
        private void Carregar(){dataGridView1.DataSource=null;dataGridView1.DataSource=repositorio.Listar(textBox3.Text.Trim());}
        private void Cadastrar(){string nome=textBox2.Text.Trim(),senha=textBox1.Text;if(nome.Length<3||senha.Length<4){MessageBox.Show("Informe um usuário e uma senha com pelo menos 4 caracteres.");return;}string tipo=radioButton1.Checked?"Master":radioButton2.Checked?"Padrão":"Comum";try{repositorio.Cadastrar(nome,senha,tipo);Limpar();Carregar();}catch(Exception ex){MessageBox.Show("Não foi possível cadastrar: "+ex.Message);}}
        private void Limpar(){textBox1.Clear();textBox2.Clear();radioButton1.Checked=radioButton2.Checked=radioButton3.Checked=false;}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
