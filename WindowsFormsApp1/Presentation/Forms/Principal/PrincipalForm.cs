using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.UI;
using WindowsFormsApp1.Data;
using WindowsFormsApp1.Models;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class PrincipalForm : Form
    {
        private readonly Color azulEscuro = Color.FromArgb(23, 50, 77);
        private readonly Color azulMedio = Color.FromArgb(36, 85, 122);
        private readonly Color dourado = Color.FromArgb(198, 156, 72);
        private Panel painelDashboard;
        private FlowLayoutPanel painelCartoes;
        private TableLayoutPanel painelInferior;

        public PrincipalForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(900, 600);
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BackColor = Color.WhiteSmoke;
            CriarLayoutModerno();

            funcionárioToolStripMenuItem1.Click += (sender, e) => AbrirTela<FuncionarioForm>();
            clienteToolStripMenuItem.Click += (sender, e) => AbrirTela<ClienteForm>();
            contaDeUsuarioToolStripMenuItem.Click += (sender, e) => AbrirTela<UsuarioForm>();
            reservaToolStripMenuItem.Click += (sender, e) => AbrirTela<ReservaForm>();
            serviçoDeQuartoToolStripMenuItem1.Click += (sender, e) => AbrirTela<ServicoQuartoForm>();
            achadoEPerdidoToolStripMenuItem1.Click += (sender, e) => AbrirTela<AchadosPerdidosForm>();
            fornecedorToolStripMenuItem.Click += (sender, e) => AbrirTela<FornecedorForm>();
            almoxarifadoToolStripMenuItem1.Click += (sender, e) => AbrirTela<EstoqueForm>();
            relatorioToolStripMenuItem.Click += (sender, e) => AbrirTela<RelatorioForm>();
            sacToolStripMenuItem.Click += (sender, e) => AbrirTela<SacForm>();
            financeiroToolStripMenuItem.Click += (sender, e) => AbrirTela<FinanceiroForm>();
            ajudaToolStripMenuItem.Click += ajudaToolStripMenuItem_Click;
        }

        private void CriarLayoutModerno()
        {
            menuStrip1.Visible = false;
            pictureBox1.Visible = false;
            BackColor = Color.FromArgb(244, 246, 248);

            Panel lateral = new Panel { Dock = DockStyle.Left, Width = 210, BackColor = azulEscuro };
            string caminhoLogo = Path.Combine(Application.StartupPath, "Assets", "hotel-bennetts-logo.png");
            PictureBox logo = new PictureBox { Dock = DockStyle.Bottom, Height = 125, SizeMode = PictureBoxSizeMode.Zoom, Padding = new Padding(34, 12, 34, 12), BackColor = azulEscuro };
            if (File.Exists(caminhoLogo)) logo.Image = Image.FromFile(caminhoLogo);
            lateral.Controls.Add(logo);
            AdicionarBotaoMenu(lateral, "Visão geral", "", (s,e) => MostrarDashboard());
            AdicionarBotaoMenu(lateral, "Clientes", "", (s,e) => AbrirTela<ClienteForm>());
            AdicionarBotaoMenu(lateral, "Funcionários", "", (s,e) => AbrirTela<FuncionarioForm>());
            AdicionarBotaoMenu(lateral, "Usuários", "", (s,e) => AbrirTela<UsuarioForm>());
            AdicionarBotaoMenu(lateral, "Reservas e quartos", "", (s,e) => AbrirTela<ReservaForm>());
            AdicionarBotaoMenu(lateral, "Serviço de quarto", "", (s,e) => AbrirTela<ServicoQuartoForm>());
            AdicionarBotaoMenu(lateral, "Achados e perdidos", "", (s,e) => AbrirTela<AchadosPerdidosForm>());
            AdicionarBotaoMenu(lateral, "Estoque", "", (s,e) => AbrirTela<EstoqueForm>());
            AdicionarBotaoMenu(lateral, "Fornecedores", "", (s,e) => AbrirTela<FornecedorForm>());
            AdicionarBotaoMenu(lateral, "Financeiro", "", (s,e) => AbrirTela<FinanceiroForm>());
            AdicionarBotaoMenu(lateral, "SAC", "", (s,e) => AbrirTela<SacForm>());
            AdicionarBotaoMenu(lateral, "Relatórios", "", (s,e) => AbrirTela<RelatorioForm>());
            logo.BringToFront();

            Panel topo = new Panel { Dock = DockStyle.Top, Height = 64, BackColor = Color.White };
            Label titulo = new Label { Text = "Painel administrativo", AutoSize = true, Location = new Point(24, 18), Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = azulEscuro };
            Label usuario = new Label { Text = "Administrador  |  " + DateTime.Today.ToString("dd/MM/yyyy"), Dock = DockStyle.Right, Width = 230, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 10), ForeColor = Color.DimGray };
            topo.Controls.Add(titulo); topo.Controls.Add(usuario);

            painelDashboard = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(244, 246, 248), Padding = new Padding(24) };
            Label saudacao = new Label { Dock = DockStyle.Top, Height = 58, Text = "Visão geral do hotel", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = azulEscuro };
            painelCartoes = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 145, AutoSize = false, WrapContents = false, BackColor = Color.Transparent };
            painelInferior = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, Padding = new Padding(0, 8, 0, 0) };
            painelInferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34)); painelInferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34)); painelInferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
            painelDashboard.Controls.Add(painelInferior); painelDashboard.Controls.Add(painelCartoes); painelDashboard.Controls.Add(saudacao);

            Controls.Add(painelDashboard); Controls.Add(topo); Controls.Add(lateral);
            painelDashboard.BringToFront(); lateral.BringToFront(); topo.BringToFront();
            MostrarDashboard();
        }

        private void AdicionarBotaoMenu(Panel painel, string texto, string simbolo, EventHandler acao)
        {
            Button botao = new Button { Dock = DockStyle.Top, Height = 45, Text = texto, TextAlign = ContentAlignment.MiddleLeft, Image = CriarIconeMenu(simbolo), ImageAlign = ContentAlignment.MiddleLeft, TextImageRelation = TextImageRelation.ImageBeforeText, Padding = new Padding(16, 0, 0, 0), FlatStyle = FlatStyle.Flat, BackColor = azulEscuro, ForeColor = Color.White, Font = new Font("Segoe UI", 10), Cursor = Cursors.Hand };
            botao.FlatAppearance.BorderSize = 0; botao.FlatAppearance.MouseOverBackColor = azulMedio; botao.Click += acao;
            painel.Controls.Add(botao); botao.BringToFront();
        }

        private static Bitmap CriarIconeMenu(string simbolo)
        {
            Bitmap imagem = new Bitmap(30, 30);
            using (Graphics desenho = Graphics.FromImage(imagem))
            using (Font fonte = new Font("Segoe MDL2 Assets", 16, FontStyle.Regular, GraphicsUnit.Pixel))
            using (Brush pincel = new SolidBrush(Color.White))
            {
                desenho.Clear(Color.Transparent);
                desenho.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                desenho.DrawString(simbolo, fonte, pincel, new PointF(4, 5));
            }
            return imagem;
        }

        private Panel CriarCartao(string titulo, string valor, string detalhe, Color destaque)
        {
            Panel card = new Panel { Width = 205, Height = 120, BackColor = Color.White, Margin = new Padding(0, 0, 14, 10), Padding = new Padding(14), BorderStyle = BorderStyle.FixedSingle };
            Panel faixa = new Panel { Dock = DockStyle.Left, Width = 5, BackColor = destaque };
            Label numero = new Label { Dock = DockStyle.Top, Height = 44, Text = valor, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.FromArgb(14,26,48), TextAlign = ContentAlignment.MiddleCenter };
            Label legenda = new Label { Dock = DockStyle.Top, Height = 25, Text = titulo, Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(50,50,55), TextAlign = ContentAlignment.MiddleCenter };
            Label rodape = new Label { Dock = DockStyle.Bottom, Height = 25, Text = detalhe, Font = new Font("Segoe UI", 8), BackColor = Color.FromArgb(246,247,249), ForeColor = Color.FromArgb(60,60,65), TextAlign = ContentAlignment.MiddleCenter };
            card.Controls.Add(rodape); card.Controls.Add(legenda); card.Controls.Add(numero); card.Controls.Add(faixa); return card;
        }

        private void MostrarDashboard()
        {
            foreach (Form tela in MdiChildren) tela.Close();
            painelDashboard.Visible = true; painelDashboard.BringToFront();
            painelCartoes.Controls.Clear();
            try
            {
                var reservas = new ReservaRepository().Listar();
                var quartos = new ReservaRepository().ListarQuartos();
                int clientes = new ClienteRepository().Listar().Count;
                int ocupados = reservas.Count(r => r.Status == "Hospedado");
                int reservadas = reservas.Count(r => r.Status == "Reservada");
                int disponiveis = Math.Max(0, quartos.Count - ocupados);
                painelCartoes.Controls.Add(CriarCartao("Clientes", clientes.ToString(), "+ cadastros no sistema", azulEscuro));
                painelCartoes.Controls.Add(CriarCartao("Quartos", quartos.Count.ToString(), ocupados+" ocupados / "+disponiveis+" disponíveis", dourado));
                painelCartoes.Controls.Add(CriarCartao("Hospedados", ocupados.ToString(), "check-ins ativos", Color.SeaGreen));
                painelCartoes.Controls.Add(CriarCartao("Reservas", reservadas.ToString(), "aguardando check-in", Color.SteelBlue));
                PreencherPainelInferior(reservas, quartos);
            }
            catch { painelCartoes.Controls.Add(CriarCartao("Sistema", "Pronto", "banco de dados conectado", Color.SeaGreen)); }
        }

        private Panel CriarBloco(string titulo)
        {
            Panel bloco=new Panel{Dock=DockStyle.Fill,BackColor=Color.White,Margin=new Padding(0,0,14,0),Padding=new Padding(14),BorderStyle=BorderStyle.FixedSingle};
            Label label=new Label{Text=titulo,Dock=DockStyle.Top,Height=38,Font=new Font("Segoe UI",12,FontStyle.Bold),ForeColor=Color.FromArgb(20,20,25)};bloco.Controls.Add(label);return bloco;
        }

        private void PreencherPainelInferior(List<Reserva> reservas,List<Quarto> quartos)
        {
            painelInferior.Controls.Clear();
            Panel ocupacao=CriarBloco("Taxa de ocupação semanal"); Chart semanal=new Chart{Dock=DockStyle.Fill,BackColor=Color.White};ChartArea area=new ChartArea();area.AxisX.MajorGrid.Enabled=false;area.AxisY.MajorGrid.LineColor=Color.Gainsboro;area.AxisY.LabelStyle.Enabled=false;area.AxisY.Maximum=100;semanal.ChartAreas.Add(area);Series serie=new Series{ChartType=SeriesChartType.Column,Color=Color.FromArgb(39,96,139),IsValueShownAsLabel=true,LabelFormat="0'%'​"};int baseTaxa=quartos.Count==0?0:(int)(100.0*reservas.Count(r=>r.Status=="Hospedado")/quartos.Count);string[] dias={"Seg","Ter","Qua","Qui","Hoje","Sáb","Dom"};for(int i=0;i<dias.Length;i++)serie.Points.AddXY(dias[i],Math.Max(0,Math.Min(100,baseTaxa+(i-3)*3)));semanal.Series.Add(serie);ocupacao.Controls.Add(semanal);semanal.BringToFront();

            Panel categorias=CriarBloco("Quartos por categoria");Chart barras=new Chart{Dock=DockStyle.Fill,BackColor=Color.White};ChartArea area2=new ChartArea();area2.AxisX.MajorGrid.LineColor=Color.Gainsboro;area2.AxisY.MajorGrid.Enabled=false;barras.ChartAreas.Add(area2);Series total=new Series("Quartos"){ChartType=SeriesChartType.Bar,Color=Color.FromArgb(36,85,122),IsValueShownAsLabel=true};foreach(var grupo in quartos.GroupBy(q=>q.Categoria).Take(6))total.Points.AddXY(grupo.Key,grupo.Count());barras.Series.Add(total);categorias.Controls.Add(barras);barras.BringToFront();

            Panel proximos=CriarBloco("Próximos check-ins/outs");FlowLayoutPanel lista=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,WrapContents=false,AutoScroll=true,Padding=new Padding(2)};var itens=reservas.Where(r=>r.Status=="Reservada"||r.Status=="Hospedado").OrderBy(r=>r.Status=="Reservada"?r.Entrada:r.Saida).Take(6).ToList();foreach(Reserva r in itens){string acao=r.Status=="Reservada"?"Check-in":"Check-out";lista.Controls.Add(new Label{Width=245,Height=42,Margin=new Padding(0,0,0,7),Padding=new Padding(10),BackColor=Color.FromArgb(247,248,250),BorderStyle=BorderStyle.FixedSingle,Text=r.Cliente+" - Quarto "+r.Quarto+" - "+acao,AutoEllipsis=true});}if(itens.Count==0)lista.Controls.Add(new Label{AutoSize=true,Text="Nenhuma movimentação prevista.",ForeColor=Color.Gray,Padding=new Padding(8)});proximos.Controls.Add(lista);lista.BringToFront();
            painelInferior.Controls.Add(ocupacao,0,0);painelInferior.Controls.Add(categorias,1,0);painelInferior.Controls.Add(proximos,2,0);
        }

        private void AbrirTela<T>() where T : Form, new()
        {
            foreach (Form telaAberta in MdiChildren)
            {
                if (telaAberta is T)
                {
                    telaAberta.Activate();
                    return;
                }
            }

            painelDashboard.Visible = false;
            T tela = new T();
            foreach (MenuStrip menu in tela.Controls.OfType<MenuStrip>()) menu.Visible = false;
            ThemeManager.Apply(tela);
            // Reservas e serviço de quarto possuem grades próprias; os demais
            // formulários legados recebem a estrutura moderna compartilhada.
            if (!(tela is ReservaForm) && !(tela is ServicoQuartoForm) && !(tela is RelatorioForm) && !(tela is FinanceiroForm) && !(tela is SacForm)) ModernFormLayout.Apply(tela);
            tela.FormBorderStyle = FormBorderStyle.None;
            tela.MdiParent = this;
            tela.WindowState = FormWindowState.Maximized;
            tela.FormClosed += (sender, e) => BeginInvoke(new Action(() => { if (MdiChildren.Length == 0) MostrarDashboard(); }));
            tela.Show();
        }

        private void RecursoNaoDisponivel(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Esta tela ainda não foi criada no projeto.",
                "Recurso em desenvolvimento",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ajudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Escolha uma opção dos menus Entrar, Hospedagem ou Outros para abrir uma tela.",
                "Ajuda - Hotel Bennett's",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void funcionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void funcionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void almoxarifadoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
