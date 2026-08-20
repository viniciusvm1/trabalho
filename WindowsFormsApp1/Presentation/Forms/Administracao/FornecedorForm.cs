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
    public partial class FornecedorForm : Form
    {
        private readonly AdministracaoRepository repositorio;
        public FornecedorForm()
        {
            InitializeComponent();
            repositorio=new AdministracaoRepository();
            button1.Text="Buscar";button1.Click+=(s,e)=>Carregar();textBox1.KeyDown+=(s,e)=>{if(e.KeyCode==Keys.Enter)Carregar();};
            Button novo=new Button{Text="Cadastrar",Location=new Point(10,410),Size=new Size(110,32)};novo.Click+=(s,e)=>Cadastrar();
            Button excluir=new Button{Text="Excluir",Location=new Point(130,410),Size=new Size(100,32)};excluir.Click+=(s,e)=>Excluir();Controls.Add(novo);Controls.Add(excluir);
            dataGridView1.AutoGenerateColumns=true;dataGridView1.Columns.Clear();dataGridView1.ReadOnly=true;dataGridView1.SelectionMode=DataGridViewSelectionMode.FullRowSelect;Carregar();
        }
        private void Carregar(){dataGridView1.DataSource=null;dataGridView1.DataSource=repositorio.ListarFornecedores(textBox1.Text.Trim());}
        private void Cadastrar(){using(Form f=new Form{Text="Novo fornecedor",Size=new Size(390,340),StartPosition=FormStartPosition.CenterParent}){TableLayoutPanel p=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(20),ColumnCount=2,RowCount=6};p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,110));p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));TextBox nome=new TextBox(),tipo=new TextBox(),email=new TextBox(),telefone=new TextBox();NumericUpDown valor=new NumericUpDown{Maximum=1000000,DecimalPlaces=2};Button salvar=new Button{Text="Salvar",DialogResult=DialogResult.OK,Size=new Size(100,34)};Control[] campos={nome,tipo,valor,email,telefone};string[] labels={"Nome:","Produto:","Valor:","E-mail:","Telefone:"};for(int i=0;i<5;i++){p.Controls.Add(new Label{Text=labels[i],TextAlign=ContentAlignment.MiddleLeft,Dock=DockStyle.Fill},0,i);campos[i].Dock=DockStyle.Fill;p.Controls.Add(campos[i],1,i);}p.Controls.Add(salvar,1,5);f.Controls.Add(p);f.AcceptButton=salvar;if(f.ShowDialog()==DialogResult.OK){if(nome.Text.Trim().Length<2){MessageBox.Show("Informe o nome do fornecedor.");return;}repositorio.SalvarFornecedor(new Fornecedor{Nome=nome.Text.Trim(),TipoProduto=tipo.Text.Trim(),ValorProduto=valor.Value,Email=email.Text.Trim(),Telefone=telefone.Text.Trim()});Carregar();}}}
        private void Excluir(){if(dataGridView1.CurrentRow?.DataBoundItem is Fornecedor f&&MessageBox.Show("Excluir o fornecedor selecionado?","Fornecedores",MessageBoxButtons.YesNo)==DialogResult.Yes){repositorio.ExcluirFornecedor(f.Id);Carregar();}}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
