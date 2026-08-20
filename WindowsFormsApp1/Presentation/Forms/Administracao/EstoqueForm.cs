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
    public partial class EstoqueForm : Form
    {
        private readonly AdministracaoRepository repositorio;
        private long produtoId;
        public EstoqueForm()
        {
            InitializeComponent();
            repositorio=new AdministracaoRepository();dataGridView1.AutoGenerateColumns=true;dataGridView1.Columns.Clear();dataGridView1.ReadOnly=true;dataGridView1.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
            button1.Click+=(s,e)=>Salvar();button2.Click+=(s,e)=>Editar();button3.Click+=(s,e)=>Limpar();button4.Click+=(s,e)=>Carregar(textBox2.Text.Trim());dataGridView1.CellDoubleClick+=(s,e)=>Selecionar(e.RowIndex);Carregar();
        }
        private void Carregar(string busca=""){dataGridView1.DataSource=null;dataGridView1.DataSource=repositorio.ListarProdutos(busca);}
        private Produto Obter()=>new Produto{Id=produtoId,Nome=textBox1.Text.Trim(),Categoria=textBox2.Text.Trim(),Estoque=(int)numericUpDown1.Value};
        private void Salvar(){Produto p=Obter();if(p.Nome.Length<2){MessageBox.Show("Informe o nome do produto.");return;}try{repositorio.SalvarProduto(p);Limpar();Carregar();}catch(Exception ex){MessageBox.Show("Não foi possível salvar: "+ex.Message);}}
        private void Editar(){if(produtoId==0){MessageBox.Show("Clique duas vezes em um produto para editar.");return;}repositorio.AtualizarProduto(Obter());Limpar();Carregar();}
        private void Selecionar(int linha){if(linha<0)return;Produto p=(Produto)dataGridView1.Rows[linha].DataBoundItem;produtoId=p.Id;textBox1.Text=p.Nome;textBox2.Text=p.Categoria;numericUpDown1.Value=Math.Max(numericUpDown1.Minimum,Math.Min(numericUpDown1.Maximum,p.Estoque));}
        private void Limpar(){produtoId=0;textBox1.Clear();textBox2.Clear();numericUpDown1.Value=0;}

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
