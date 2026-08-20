using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WindowsFormsApp1.Data;

namespace WindowsFormsApp1
{
    public partial class RelatorioForm : Form
    {
        private readonly DataGridView tabela=new DataGridView();
        public RelatorioForm()
        {
            InitializeComponent();
            PrepararTela();
        }
        private void PrepararTela(){foreach(Control c in Controls)c.Visible=false;ClientSize=new Size(900,600);MinimumSize=new Size(760,520);TableLayoutPanel raiz=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(24),ColumnCount=1,RowCount=3};raiz.RowStyles.Add(new RowStyle(SizeType.Absolute,65));raiz.RowStyles.Add(new RowStyle(SizeType.Percent,100));raiz.RowStyles.Add(new RowStyle(SizeType.Absolute,58));FlowLayoutPanel filtro=new FlowLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(0,10,0,0)};filtro.Controls.Add(new Label{Text="Relatório:",AutoSize=true,Margin=new Padding(0,8,8,0)});comboBox1.Items.Clear();comboBox1.Items.AddRange(new object[]{"Resumo geral","Clientes","Funcionários","Reservas","Estoque","Fornecedores","Achados e perdidos"});comboBox1.SelectedIndex=0;comboBox1.Width=220;comboBox1.Visible=true;filtro.Controls.Add(comboBox1);button1.Text="Gerar";button1.Visible=true;button1.Size=new Size(100,34);button1.Click+=(s,e)=>Gerar();filtro.Controls.Add(button1);tabela.Dock=DockStyle.Fill;tabela.ReadOnly=true;tabela.AllowUserToAddRows=false;tabela.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;tabela.SelectionMode=DataGridViewSelectionMode.FullRowSelect;FlowLayoutPanel acoes=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.RightToLeft,Padding=new Padding(0,10,0,0)};button3.Text="Fechar";button3.Visible=true;button3.Size=new Size(100,34);button3.Click+=(s,e)=>Close();button2.Text="Exportar CSV";button2.Visible=true;button2.Size=new Size(125,34);button2.Click+=(s,e)=>Exportar();acoes.Controls.Add(button3);acoes.Controls.Add(button2);raiz.Controls.Add(filtro,0,0);raiz.Controls.Add(tabela,0,1);raiz.Controls.Add(acoes,0,2);Controls.Add(raiz);raiz.Visible=true;Gerar();}
        private void Gerar(){string tipo=comboBox1.Text;object dados;if(tipo=="Clientes")dados=new ClienteRepository().Listar();else if(tipo=="Funcionários")dados=new FuncionarioRepository().Listar();else if(tipo=="Reservas")dados=new ReservaRepository().Listar();else if(tipo=="Estoque")dados=new AdministracaoRepository().ListarProdutos();else if(tipo=="Fornecedores")dados=new AdministracaoRepository().ListarFornecedores();else if(tipo=="Achados e perdidos")dados=new AdministracaoRepository().ListarAchados();else{DataTable resumo=new DataTable();resumo.Columns.Add("Indicador");resumo.Columns.Add("Quantidade");resumo.Rows.Add("Clientes",new ClienteRepository().Listar().Count);resumo.Rows.Add("Funcionários",new FuncionarioRepository().Listar().Count);var reservas=new ReservaRepository().Listar();resumo.Rows.Add("Reservas",reservas.Count);resumo.Rows.Add("Hospedados",reservas.Count(r=>r.Status=="Hospedado"));resumo.Rows.Add("Quartos",new ReservaRepository().ListarQuartos().Count);resumo.Rows.Add("Produtos",new AdministracaoRepository().ListarProdutos().Count);dados=resumo;}tabela.DataSource=null;tabela.DataSource=dados;}
        private void Exportar(){if(tabela.Columns.Count==0)return;using(SaveFileDialog s=new SaveFileDialog{Filter="Arquivo CSV|*.csv",FileName="relatorio.csv"})if(s.ShowDialog()==DialogResult.OK){StringBuilder csv=new StringBuilder();csv.AppendLine(string.Join(";",tabela.Columns.Cast<DataGridViewColumn>().Where(c=>c.Visible).Select(c=>c.HeaderText)));foreach(DataGridViewRow linha in tabela.Rows)csv.AppendLine(string.Join(";",tabela.Columns.Cast<DataGridViewColumn>().Where(c=>c.Visible).Select(c=>(linha.Cells[c.Index].Value??"").ToString().Replace(";",","))));File.WriteAllText(s.FileName,csv.ToString(),Encoding.UTF8);MessageBox.Show("Relatório exportado com sucesso.");}}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
