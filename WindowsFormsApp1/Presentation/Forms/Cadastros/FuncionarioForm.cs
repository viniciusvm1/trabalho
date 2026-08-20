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
    public partial class FuncionarioForm : Form
    {
        private readonly FuncionarioRepository repositorio;
        private readonly DataGridView tabela=new DataGridView();
        private long funcionarioId;
        public FuncionarioForm()
        {
            InitializeComponent();
            repositorio=new FuncionarioRepository();ClientSize=new Size(850,680);
            tabela.Location=new Point(10,430);tabela.Size=new Size(810,180);tabela.Anchor=AnchorStyles.Left|AnchorStyles.Right|AnchorStyles.Top|AnchorStyles.Bottom;tabela.ReadOnly=true;tabela.AllowUserToAddRows=false;tabela.AutoGenerateColumns=true;tabela.SelectionMode=DataGridViewSelectionMode.FullRowSelect;tabela.CellDoubleClick+=(s,e)=>Selecionar(e.RowIndex);Controls.Add(tabela);
            button4.Click+=(s,e)=>Salvar();button3.Click+=(s,e)=>Editar();button2.Click+=(s,e)=>Excluir();button1.Click+=(s,e)=>Close();Carregar();
        }
        private Funcionario Obter()=>new Funcionario{Id=funcionarioId,Nome=textBox1.Text.Trim(),Cpf=textBox3.Text.Trim(),Rg=textBox9.Text.Trim(),DataNascimento=textBox10.Text.Trim(),Sexo=Masculino.Checked?"Masculino":radioButton1.Checked?"Feminino":radioButton2.Checked?"Outro":"",Funcao=textBox11.Text.Trim(),Admissao=textBox12.Text.Trim(),Demissao=textBox13.Text.Trim(),TipoContrato=textBox14.Text.Trim(),Rua=textBox2.Text.Trim(),Cep=textBox4.Text.Trim(),Bairro=textBox5.Text.Trim(),Cidade=textBox6.Text.Trim(),Estado=textBox7.Text.Trim(),Pais=textBox8.Text.Trim(),Celular=textBox15.Text.Trim(),Telefone1=textBox16.Text.Trim(),Telefone2=textBox17.Text.Trim()};
        private bool Validar(Funcionario f){if(f.Nome.Length<3){MessageBox.Show("Informe o nome completo.");return false;}if(new string(f.Cpf.Where(char.IsDigit).ToArray()).Length!=11){MessageBox.Show("O CPF deve conter 11 números.");return false;}if(f.Funcao.Length<2){MessageBox.Show("Informe a função do funcionário.");return false;}return true;}
        private void Salvar(){Funcionario f=Obter();if(!Validar(f))return;try{repositorio.Salvar(f);Concluir("Funcionário cadastrado.");}catch(Exception ex){MessageBox.Show("Não foi possível salvar: "+ex.Message);}}
        private void Editar(){if(funcionarioId==0){MessageBox.Show("Clique duas vezes em um funcionário para editar.");return;}Funcionario f=Obter();if(!Validar(f))return;repositorio.Atualizar(f);Concluir("Funcionário atualizado.");}
        private void Excluir(){if(funcionarioId==0){MessageBox.Show("Selecione um funcionário.");return;}if(MessageBox.Show("Excluir o funcionário selecionado?","Funcionários",MessageBoxButtons.YesNo)==DialogResult.Yes){repositorio.Excluir(funcionarioId);Concluir("Funcionário excluído.");}}
        private void Carregar(){tabela.DataSource=null;tabela.DataSource=repositorio.Listar();if(tabela.Columns["Rua"]!=null){foreach(string c in new[]{"Rua","Cep","Bairro","Cidade","Estado","Pais","Telefone1","Telefone2"})if(tabela.Columns[c]!=null)tabela.Columns[c].Visible=false;}}
        private void Selecionar(int linha){if(linha<0)return;Funcionario f=(Funcionario)tabela.Rows[linha].DataBoundItem;funcionarioId=f.Id;textBox1.Text=f.Nome;textBox3.Text=f.Cpf;textBox9.Text=f.Rg;textBox10.Text=f.DataNascimento;textBox11.Text=f.Funcao;textBox12.Text=f.Admissao;textBox13.Text=f.Demissao;textBox14.Text=f.TipoContrato;textBox2.Text=f.Rua;textBox4.Text=f.Cep;textBox5.Text=f.Bairro;textBox6.Text=f.Cidade;textBox7.Text=f.Estado;textBox8.Text=f.Pais;textBox15.Text=f.Celular;textBox16.Text=f.Telefone1;textBox17.Text=f.Telefone2;Masculino.Checked=f.Sexo=="Masculino";radioButton1.Checked=f.Sexo=="Feminino";radioButton2.Checked=f.Sexo=="Outro";}
        private void Concluir(string m){MessageBox.Show(m,"Funcionários");Limpar();Carregar();}
        private void Limpar(){funcionarioId=0;foreach(TextBox t in groupBox1.Controls.OfType<TextBox>())t.Clear();foreach(GroupBox g in groupBox1.Controls.OfType<GroupBox>())foreach(TextBox t in g.Controls.OfType<TextBox>())t.Clear();Masculino.Checked=radioButton1.Checked=radioButton2.Checked=false;}

        private void entrarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
