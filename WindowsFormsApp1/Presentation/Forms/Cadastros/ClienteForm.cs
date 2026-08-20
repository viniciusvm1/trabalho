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
    public partial class ClienteForm : Form
    {
        private readonly ClienteRepository repositorio;
        private readonly DataGridView tabelaClientes = new DataGridView();
        private long clienteSelecionadoId;

        public ClienteForm()
        {
            InitializeComponent();
            repositorio = new ClienteRepository();
            PrepararTela();
            AtualizarTabela();
        }

        private void PrepararTela()
        {
            Text = "Cadastro de Clientes - Hotel Bennett's";
            ClientSize = new Size(760, 520);
            tabelaClientes.Location = new Point(9, 295);
            tabelaClientes.Size = new Size(742, 215);
            tabelaClientes.ReadOnly = true;
            tabelaClientes.AllowUserToAddRows = false;
            tabelaClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabelaClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabelaClientes.MultiSelect = false;
            tabelaClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.Anchor = button2.Anchor = button3.Anchor = button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tabelaClientes.CellDoubleClick += tabelaClientes_CellDoubleClick;
            Controls.Add(tabelaClientes);

            button4.Click += buttonSalvar_Click;
            button3.Click += buttonEditar_Click;
            button2.Click += buttonExcluir_Click;
            button1.Click += (sender, e) => Close();
        }

        private Cliente ObterCliente()
        {
            return new Cliente {
                Id = clienteSelecionadoId, Nome = textBox1.Text.Trim(), Cpf = textBox3.Text.Trim(), Rg = textBox9.Text.Trim(),
                DataNascimento = textBox10.Text.Trim(), Sexo = Masculino.Checked ? "Masculino" : radioButton1.Checked ? "Feminino" : radioButton2.Checked ? "Outro" : "",
                Rua = textBox2.Text.Trim(), Cep = textBox4.Text.Trim(), Bairro = textBox6.Text.Trim(), Cidade = textBox7.Text.Trim(),
                Estado = comboBox1.Text, Pais = textBox8.Text.Trim(), Celular = textBox15.Text.Trim(),
                Telefone1 = textBox16.Text.Trim(), Telefone2 = textBox17.Text.Trim()
            };
        }

        private bool Validar(Cliente cliente)
        {
            if (cliente.Nome.Length < 3) { Avisar("Informe o nome completo do cliente."); return false; }
            string cpf = new string(cliente.Cpf.Where(char.IsDigit).ToArray());
            if (cpf.Length != 11) { Avisar("O CPF deve conter 11 números."); return false; }
            DateTime data;
            if (cliente.DataNascimento.Length > 0 && !DateTime.TryParse(cliente.DataNascimento, out data)) { Avisar("Informe uma data de nascimento válida."); return false; }
            return true;
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            Cliente cliente = ObterCliente();
            if (!Validar(cliente)) return;
            try { repositorio.Salvar(cliente); Concluir("Cliente cadastrado com sucesso."); }
            catch (System.Data.SQLite.SQLiteException ex) when (ex.ResultCode == System.Data.SQLite.SQLiteErrorCode.Constraint)
            { Avisar("Já existe um cliente cadastrado com este CPF."); }
            catch (Exception ex) { Avisar("Não foi possível salvar: " + ex.Message); }
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if (clienteSelecionadoId == 0) { Avisar("Clique duas vezes em um cliente da tabela para editá-lo."); return; }
            Cliente cliente = ObterCliente();
            if (!Validar(cliente)) return;
            try { repositorio.Atualizar(cliente); Concluir("Cliente atualizado com sucesso."); }
            catch (Exception ex) { Avisar("Não foi possível editar: " + ex.Message); }
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (clienteSelecionadoId == 0) { Avisar("Selecione um cliente na tabela."); return; }
            if (MessageBox.Show("Deseja realmente excluir este cliente?", "Confirmar exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            repositorio.Excluir(clienteSelecionadoId); Concluir("Cliente excluído com sucesso.");
        }

        private void tabelaClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Cliente c = (Cliente)tabelaClientes.Rows[e.RowIndex].DataBoundItem;
            clienteSelecionadoId = c.Id; textBox1.Text = c.Nome; textBox3.Text = c.Cpf; textBox9.Text = c.Rg;
            textBox10.Text = c.DataNascimento; textBox2.Text = c.Rua; textBox4.Text = c.Cep; textBox6.Text = c.Bairro;
            textBox7.Text = c.Cidade; comboBox1.Text = c.Estado; textBox8.Text = c.Pais; textBox15.Text = c.Celular;
            textBox16.Text = c.Telefone1; textBox17.Text = c.Telefone2;
            Masculino.Checked = c.Sexo == "Masculino"; radioButton1.Checked = c.Sexo == "Feminino"; radioButton2.Checked = c.Sexo == "Outro";
        }

        private void AtualizarTabela() { tabelaClientes.DataSource = null; tabelaClientes.DataSource = repositorio.Listar(); }
        private void Concluir(string mensagem) { MessageBox.Show(mensagem, "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information); Limpar(); AtualizarTabela(); }
        private void Avisar(string mensagem) { MessageBox.Show(mensagem, "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        private void Limpar() { clienteSelecionadoId = 0; foreach (TextBox campo in groupBox1.Controls.OfType<TextBox>()) campo.Clear(); foreach (TextBox campo in groupBox3.Controls.OfType<TextBox>()) campo.Clear(); foreach (TextBox campo in groupBox5.Controls.OfType<TextBox>()) campo.Clear(); comboBox1.SelectedIndex = -1; Masculino.Checked = radioButton1.Checked = radioButton2.Checked = false; }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}
