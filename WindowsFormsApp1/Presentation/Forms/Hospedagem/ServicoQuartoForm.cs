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
    public partial class ServicoQuartoForm : Form
    {
        private readonly ConsumoRepository repositorio;
        private readonly ComboBox comboReserva = new ComboBox();
        private List<Produto> produtos;
        private CheckBox[] seletores;
        private NumericUpDown[] quantidades;

        public ServicoQuartoForm()
        {
            InitializeComponent();
            repositorio = new ConsumoRepository();
            PrepararTela();
        }

        private void PrepararTela()
        {
            Text="Serviço de quarto - Hotel Bennett's";ClientSize=new Size(850,600);MinimumSize=new Size(720,520);
            foreach(Control antigo in Controls)antigo.Visible=false;
            comboReserva.DropDownStyle=ComboBoxStyle.DropDownList;
            seletores=new[]{Agua,checkBox2,checkBox4,checkBox5,checkBox7,checkBox13};
            quantidades=new[]{numericUpDown1,numericUpDown2,numericUpDown3,numericUpDown4,numericUpDown5,numericUpDown6};
            produtos=repositorio.ListarProdutos();
            for(int i=0;i<seletores.Length;i++){int indice=i;seletores[i].Text=produtos[i].Nome+" - R$ "+produtos[i].Preco.ToString("N2")+" ("+produtos[i].Estoque+")";seletores[i].CheckedChanged+=(s,e)=>Calcular();quantidades[i].Minimum=0;quantidades[i].Maximum=100;quantidades[i].ValueChanged+=(s,e)=>Calcular();}
            TableLayoutPanel raiz=new TableLayoutPanel{Dock=DockStyle.Fill,Padding=new Padding(28),BackColor=Color.FromArgb(244,246,248),ColumnCount=1,RowCount=4};raiz.RowStyles.Add(new RowStyle(SizeType.Absolute,95));raiz.RowStyles.Add(new RowStyle(SizeType.Percent,100));raiz.RowStyles.Add(new RowStyle(SizeType.Absolute,65));raiz.RowStyles.Add(new RowStyle(SizeType.Absolute,58));
            GroupBox hospedagem=new GroupBox{Text="Hospedagem ativa",Dock=DockStyle.Fill,Padding=new Padding(18)};TableLayoutPanel linha=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=2};linha.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,130));linha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));linha.Controls.Add(new Label{Text="Cliente / quarto:",Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleLeft},0,0);comboReserva.Dock=DockStyle.Fill;linha.Controls.Add(comboReserva,1,0);hospedagem.Controls.Add(linha);
            GroupBox consumo=new GroupBox{Text="Produtos e quantidades",Dock=DockStyle.Fill,Padding=new Padding(18)};TableLayoutPanel lista=new TableLayoutPanel{Dock=DockStyle.Fill,ColumnCount=2,RowCount=6};lista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,75));lista.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,25));
            for(int i=0;i<6;i++){lista.RowStyles.Add(new RowStyle(SizeType.Percent,16.66f));seletores[i].Dock=DockStyle.Fill;seletores[i].Visible=true;quantidades[i].Dock=DockStyle.Fill;quantidades[i].Visible=true;lista.Controls.Add(seletores[i],0,i);lista.Controls.Add(quantidades[i],1,i);}consumo.Controls.Add(lista);
            Panel total=new Panel{Dock=DockStyle.Fill,BackColor=Color.White};label3.Text="Total:";label3.AutoSize=true;label3.Location=new Point(18,22);label3.Visible=true;label4.AutoSize=true;label4.Location=new Point(75,20);label4.Visible=true;total.Controls.Add(label3);total.Controls.Add(label4);
            FlowLayoutPanel acoes=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.RightToLeft,Padding=new Padding(0,10,0,0)};button2.Text="Lançar consumo";button2.Size=new Size(150,36);button2.Visible=true;button2.Click+=Finalizar_Click;button1.Text="Fechar";button1.Size=new Size(105,36);button1.Visible=true;button1.Click+=(s,e)=>Close();button3.Visible=false;acoes.Controls.Add(button2);acoes.Controls.Add(button1);
            raiz.Controls.Add(hospedagem,0,0);raiz.Controls.Add(consumo,0,1);raiz.Controls.Add(total,0,2);raiz.Controls.Add(acoes,0,3);Controls.Add(raiz);raiz.Visible=true;
            comboReserva.DataSource=repositorio.ListarHospedagens();comboReserva.Format+=(s,e)=>{Reserva r=e.ListItem as Reserva;if(r!=null)e.Value=r.Cliente+" - quarto "+r.Quarto;};comboReserva.SelectedIndexChanged+=(s,e)=>Calcular();
            Calcular();
        }

        private void Calcular(){decimal total=0;for(int i=0;i<produtos.Count;i++)if(seletores[i].Checked)total+=produtos[i].Preco*quantidades[i].Value;Reserva r=comboReserva.SelectedItem as Reserva;decimal anterior=r==null?0:repositorio.TotalReserva(r.Id);label4.Text="R$ "+total.ToString("N2")+" | acumulado: R$ "+anterior.ToString("N2");label4.AutoSize=true;}
        private void Finalizar_Click(object sender,EventArgs e)
        {
            Reserva reserva=comboReserva.SelectedItem as Reserva;if(reserva==null){Avisar("Faça o check-in de uma reserva antes de lançar o consumo.");return;}
            var itens=new List<Tuple<Produto,int>>();for(int i=0;i<produtos.Count;i++)if(seletores[i].Checked&&quantidades[i].Value>0)itens.Add(Tuple.Create(produtos[i],(int)quantidades[i].Value));
            if(itens.Count==0){Avisar("Selecione um produto e informe a quantidade.");return;}
            try{repositorio.Registrar(reserva.Id,itens);MessageBox.Show("Consumo lançado e estoque atualizado.","Serviço de quarto",MessageBoxButtons.OK,MessageBoxIcon.Information);produtos=repositorio.ListarProdutos();for(int i=0;i<seletores.Length;i++){seletores[i].Checked=false;quantidades[i].Value=0;seletores[i].Text=produtos[i].Nome+" - R$ "+produtos[i].Preco.ToString("N2")+" ("+produtos[i].Estoque+")";}Calcular();}catch(Exception ex){Avisar(ex.Message);}
        }
        private void Avisar(string m){MessageBox.Show(m,"Serviço de quarto",MessageBoxButtons.OK,MessageBoxIcon.Warning);}

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
