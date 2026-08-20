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
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class AchadosPerdidosForm : Form
    {
        private readonly AdministracaoRepository repositorio;
        private readonly DataGridView tabela=new DataGridView();
        private long itemId;
        public AchadosPerdidosForm()
        {
            InitializeComponent();
            repositorio=new AdministracaoRepository();textBox3.ReadOnly=true;
            tabela.Location=new Point(10,250);tabela.Size=new Size(560,170);tabela.Anchor=AnchorStyles.Left|AnchorStyles.Right|AnchorStyles.Top|AnchorStyles.Bottom;tabela.ReadOnly=true;tabela.AllowUserToAddRows=false;tabela.AutoGenerateColumns=true;tabela.SelectionMode=DataGridViewSelectionMode.FullRowSelect;tabela.CellDoubleClick+=(s,e)=>Selecionar(e.RowIndex);Controls.Add(tabela);
            ClientSize=new Size(600,500);button1.Click+=(s,e)=>Salvar();button2.Text="Devolver";button2.Click+=(s,e)=>Devolver();button3.Click+=(s,e)=>Limpar();Carregar();
        }
        private void Carregar(){tabela.DataSource=null;tabela.DataSource=repositorio.ListarAchados();}
        private void Salvar(){DateTime data;if(!DateTime.TryParse(maskedTextBox1.Text,out data))data=DateTime.Today;if(textBox1.Text.Trim().Length<2){MessageBox.Show("Informe o item encontrado.");return;}repositorio.SalvarAchado(new AchadoPerdido{Item=textBox1.Text.Trim(),Quarto=textBox2.Text.Trim(),DataEntrada=data});Limpar();Carregar();}
        private void Selecionar(int linha){if(linha<0)return;AchadoPerdido a=(AchadoPerdido)tabela.Rows[linha].DataBoundItem;itemId=a.Id;textBox3.Text=a.Id.ToString();textBox1.Text=a.Item;textBox2.Text=a.Quarto;maskedTextBox1.Text=a.DataEntrada.ToShortDateString();}
        private void Devolver(){if(itemId==0){MessageBox.Show("Selecione um item na tabela.");return;}repositorio.DevolverAchado(itemId);Limpar();Carregar();}
        private void Limpar(){itemId=0;textBox1.Clear();textBox2.Clear();textBox3.Clear();maskedTextBox1.Clear();}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
