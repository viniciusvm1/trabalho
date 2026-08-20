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
    public partial class ReservaForm : Form
    {
        private readonly ReservaRepository repositorio;
        private readonly ClienteRepository clientes;
        private readonly ComboBox comboCliente = new ComboBox();
        private readonly DataGridView tabela = new DataGridView();
        private long reservaId;

        public ReservaForm()
        {
            InitializeComponent();
            repositorio = new ReservaRepository(); clientes = new ClienteRepository(); PrepararTela(); CarregarListas();
        }

        private void PrepararTela()
        {
            Text = "Reservas - Hotel Bennett's"; ClientSize = new Size(980, 650); MinimumSize = new Size(820, 580);
            foreach (Control antigo in Controls) antigo.Visible = false;
            comboCliente.DropDownStyle = ComboBoxStyle.DropDownList; comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; textBox2.ReadOnly = true;
            dateTimePicker1.Value = DateTime.Today; dateTimePicker2.Value = DateTime.Today.AddDays(1);
            dateTimePicker1.ValueChanged += Calcular; dateTimePicker2.ValueChanged += Calcular; comboBox1.SelectedIndexChanged += Calcular;
            button4.Click += Salvar_Click; button3.Click += Editar_Click; button2.Click += Cancelar_Click; button1.Click += (s,e) => Close();

            TableLayoutPanel raiz = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(22), BackColor = Color.FromArgb(244,246,248), ColumnCount = 1, RowCount = 4 };
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 205)); raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 55)); raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            GroupBox dados = new GroupBox { Text = "Dados da reserva", Dock = DockStyle.Fill, Padding = new Padding(16) };
            TableLayoutPanel campos = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 5, Padding = new Padding(8) };
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105)); campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50)); campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105)); campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for(int i=0;i<5;i++) campos.RowStyles.Add(new RowStyle(SizeType.Percent,20));
            AdicionarCampo(campos,"Cliente:",comboCliente,0,0); AdicionarCampo(campos,"Quarto:",comboBox1,2,0);
            AdicionarCampo(campos,"Entrada:",dateTimePicker1,0,1); AdicionarCampo(campos,"Saída:",dateTimePicker2,2,1);
            AdicionarCampo(campos,"Adultos:",numericUpDown1,0,2); AdicionarCampo(campos,"Crianças:",numericUpDown2,2,2);
            campos.Controls.Add(new Label{Text="Observação:",Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleLeft},0,3); textBox3.Dock=DockStyle.Fill; campos.Controls.Add(textBox3,1,3); campos.SetColumnSpan(textBox3,3);
            campos.Controls.Add(new Label{Text="Valor total:",Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleLeft},0,4); label10.Dock=DockStyle.Fill;label10.TextAlign=ContentAlignment.MiddleLeft;label10.Font=new Font("Segoe UI",11,FontStyle.Bold);campos.Controls.Add(label10,1,4);campos.SetColumnSpan(label10,3);
            dados.Controls.Add(campos);

            FlowLayoutPanel acoes = new FlowLayoutPanel { Dock=DockStyle.Fill,FlowDirection=FlowDirection.RightToLeft,Padding=new Padding(0,8,0,5) };
            foreach(Button b in new[]{button1,button2,button3,button4}){b.Visible=true;b.Size=new Size(105,36);b.Margin=new Padding(8,0,0,0);acoes.Controls.Add(b);}
            tabela.Dock=DockStyle.Fill;tabela.Visible=true;tabela.ReadOnly=true;tabela.AllowUserToAddRows=false;tabela.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;tabela.SelectionMode=DataGridViewSelectionMode.FullRowSelect;tabela.MultiSelect=false;tabela.CellDoubleClick+=Tabela_CellDoubleClick;
            FlowLayoutPanel rodape = new FlowLayoutPanel { Dock=DockStyle.Fill,Padding=new Padding(0,10,0,0) };
            Button btnQuarto=new Button{Text="Cadastrar quarto",Size=new Size(140,36)};btnQuarto.Click+=CadastrarQuarto_Click;
            Button btnCheckIn=new Button{Text="Check-in",Size=new Size(105,36)};btnCheckIn.Click+=(s,e)=>AlterarStatus("Hospedado");
            Button btnCheckOut=new Button{Text="Check-out",Size=new Size(105,36)};btnCheckOut.Click+=(s,e)=>AlterarStatus("Finalizada");rodape.Controls.AddRange(new Control[]{btnQuarto,btnCheckIn,btnCheckOut});
            raiz.Controls.Add(dados,0,0);raiz.Controls.Add(acoes,0,1);raiz.Controls.Add(tabela,0,2);raiz.Controls.Add(rodape,0,3);Controls.Add(raiz);raiz.Visible=true;
        }

        private static void AdicionarCampo(TableLayoutPanel painel,string titulo,Control controle,int coluna,int linha){painel.Controls.Add(new Label{Text=titulo,Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleLeft},coluna,linha);controle.Dock=DockStyle.Fill;controle.Visible=true;painel.Controls.Add(controle,coluna+1,linha);}

        private void CarregarListas()
        {
            comboCliente.DataSource = clientes.Listar(); comboCliente.DisplayMember = "Nome"; comboCliente.ValueMember = "Id";
            comboBox1.DataSource = repositorio.ListarQuartos();
            tabela.DataSource = null; tabela.DataSource = repositorio.Listar();
            if (tabela.Columns["ClienteId"] != null) tabela.Columns["ClienteId"].Visible = false;
            if (tabela.Columns["QuartoId"] != null) tabela.Columns["QuartoId"].Visible = false;
            if (tabela.Columns["Observacao"] != null) tabela.Columns["Observacao"].Visible = false;
            Calcular(null, EventArgs.Empty);
        }

        private Reserva ObterReserva()
        {
            Cliente c = comboCliente.SelectedItem as Cliente; Quarto q = comboBox1.SelectedItem as Quarto;
            int dias = Math.Max(1, (dateTimePicker2.Value.Date-dateTimePicker1.Value.Date).Days);
            return new Reserva { Id=reservaId, ClienteId=c?.Id??0, QuartoId=q?.Id??0, Entrada=dateTimePicker1.Value.Date, Saida=dateTimePicker2.Value.Date, Adultos=(int)numericUpDown1.Value, Criancas=(int)numericUpDown2.Value, ValorTotal=(q?.ValorDiaria??0)*dias, Observacao=textBox3.Text.Trim() };
        }

        private bool Validar(Reserva r)
        {
            if(r.ClienteId==0){Avisar("Cadastre e selecione um cliente.");return false;} if(r.QuartoId==0){Avisar("Cadastre e selecione um quarto.");return false;}
            if(r.Saida<=r.Entrada){Avisar("A data de saída deve ser posterior à entrada.");return false;}
            Quarto q=comboBox1.SelectedItem as Quarto; if(r.Adultos+r.Criancas<1){Avisar("Informe pelo menos um hóspede.");return false;} if(r.Adultos+r.Criancas>q.Capacidade){Avisar("A quantidade de hóspedes excede a capacidade do quarto.");return false;}
            if(repositorio.ExisteConflito(r.QuartoId,r.Entrada,r.Saida,r.Id)){Avisar("Este quarto já possui reserva no período escolhido.");return false;} return true;
        }

        private void Salvar_Click(object s,EventArgs e){Reserva r=ObterReserva();if(!Validar(r))return;repositorio.Salvar(r);Concluir("Reserva cadastrada com sucesso.");}
        private void Editar_Click(object s,EventArgs e){if(reservaId==0){Avisar("Clique duas vezes em uma reserva para editá-la.");return;}Reserva r=ObterReserva();if(!Validar(r))return;repositorio.Atualizar(r);Concluir("Reserva atualizada.");}
        private void Cancelar_Click(object s,EventArgs e){if(reservaId==0){Avisar("Selecione uma reserva para cancelar.");return;}if(MessageBox.Show("Deseja cancelar esta reserva?","Reservas",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes){repositorio.Cancelar(reservaId);Concluir("Reserva cancelada.");}}
        private void Calcular(object s,EventArgs e){Quarto q=comboBox1.SelectedItem as Quarto;int dias=Math.Max(1,(dateTimePicker2.Value.Date-dateTimePicker1.Value.Date).Days);label10.Text="R$ "+((q?.ValorDiaria??0)*dias).ToString("N2")+" ("+dias+" diária(s))";}
        private void Tabela_CellDoubleClick(object s,DataGridViewCellEventArgs e){if(e.RowIndex<0)return;Reserva r=(Reserva)tabela.Rows[e.RowIndex].DataBoundItem;reservaId=r.Id;textBox2.Text=r.Id.ToString();comboCliente.SelectedValue=r.ClienteId;SelecionarQuarto(r.QuartoId);dateTimePicker1.Value=r.Entrada;dateTimePicker2.Value=r.Saida;numericUpDown1.Value=r.Adultos;numericUpDown2.Value=r.Criancas;textBox3.Text=r.Observacao;}
        private void SelecionarQuarto(long id){for(int i=0;i<comboBox1.Items.Count;i++)if(((Quarto)comboBox1.Items[i]).Id==id){comboBox1.SelectedIndex=i;break;}}
        private void CadastrarQuarto_Click(object s,EventArgs e){using(Form f=new Form{Text="Novo quarto",Size=new Size(330,260),StartPosition=FormStartPosition.CenterParent}){TextBox n=new TextBox{Location=new Point(130,20)},cat=new TextBox{Location=new Point(130,55)};NumericUpDown cap=new NumericUpDown{Location=new Point(130,90),Minimum=1,Maximum=20,Value=2},valor=new NumericUpDown{Location=new Point(130,125),Minimum=1,Maximum=100000,DecimalPlaces=2};Button ok=new Button{Text="Salvar",Location=new Point(130,165),DialogResult=DialogResult.OK};f.Controls.AddRange(new Control[]{new Label{Text="Número:",Location=new Point(20,23)},n,new Label{Text="Categoria:",Location=new Point(20,58)},cat,new Label{Text="Capacidade:",Location=new Point(20,93)},cap,new Label{Text="Valor da diária:",Location=new Point(20,128)},valor,ok});f.AcceptButton=ok;if(f.ShowDialog()==DialogResult.OK){if(string.IsNullOrWhiteSpace(n.Text)||string.IsNullOrWhiteSpace(cat.Text)){Avisar("Preencha o número e a categoria.");return;}try{repositorio.SalvarQuarto(new Quarto{Numero=n.Text.Trim(),Categoria=cat.Text.Trim(),Capacidade=(int)cap.Value,ValorDiaria=valor.Value});CarregarListas();}catch(Exception ex){Avisar("Não foi possível cadastrar o quarto: "+ex.Message);}}}}
        private void AlterarStatus(string status){if(reservaId==0){Avisar("Selecione uma reserva na tabela.");return;}repositorio.AtualizarStatus(reservaId,status);Concluir(status=="Hospedado"?"Check-in realizado.":"Check-out realizado.");}
        private void Concluir(string m){MessageBox.Show(m,"Reservas",MessageBoxButtons.OK,MessageBoxIcon.Information);reservaId=0;textBox2.Clear();CarregarListas();}
        private void Avisar(string m){MessageBox.Show(m,"Reservas",MessageBoxButtons.OK,MessageBoxIcon.Warning);}

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged_2(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
