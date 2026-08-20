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
using System.IO;
using WindowsFormsApp1.Data;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
            AcceptButton = button1;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            label3.Cursor = Cursors.Hand;
            button1.Cursor = Cursors.Hand;
            CriarVisualLogin();
        }

        private void CriarVisualLogin()
        {
            ClientSize = new Size(520, 390);
            BackColor = ThemeManager.Navy;
            string caminhoLogo = Path.Combine(Application.StartupPath, "Assets", "hotel-bennetts-logo.png");
            PictureBox marca = new PictureBox { Location = new Point(175, 10), Size = new Size(170, 82), SizeMode = PictureBoxSizeMode.Zoom, BackColor = ThemeManager.Navy };
            if (File.Exists(caminhoLogo)) marca.Image = Image.FromFile(caminhoLogo);
            Label subtitulo = new Label { Text = "Painel administrativo", ForeColor = Color.FromArgb(205, 215, 225), Font = new Font("Segoe UI", 10), AutoSize = true, Location = new Point(195, 79) };
            Panel card = new Panel { BackColor = Color.White, Location = new Point(85, 100), Size = new Size(350, 235), Padding = new Padding(30) };
            Controls.Add(marca); Controls.Add(subtitulo); Controls.Add(card);
            card.Controls.Add(label1); card.Controls.Add(textBox1); card.Controls.Add(label2); card.Controls.Add(textBox2); card.Controls.Add(label3); card.Controls.Add(button1);
            label1.Location = new Point(35, 25); textBox1.Location = new Point(35, 48); textBox1.Size = new Size(280, 25);
            label2.Location = new Point(35, 82); textBox2.Location = new Point(35, 105); textBox2.Size = new Size(280, 25);
            label3.Location = new Point(35, 140); label3.ForeColor = ThemeManager.Gold;
            button1.Location = new Point(195, 172); button1.Size = new Size(120, 36); button1.Text = "Entrar";
            ThemeManager.Apply(this);
            BackColor = ThemeManager.Navy; marca.BackColor = ThemeManager.Navy; subtitulo.ForeColor = Color.FromArgb(205, 215, 225); label3.ForeColor = ThemeManager.Gold;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Para acessar a demonstração, use o usuário 'admin' e a senha 'admin'.",
                "Recuperação de senha",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = textBox1.Text.Trim();
            string senha = textBox2.Text;

            if (usuario.Length == 0 || senha.Length == 0)
            {
                MessageBox.Show(
                    "Preencha o login e a senha.",
                    "Dados obrigatórios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // A aplicação ainda não possui banco de dados. Estas credenciais
            // permitem testar com segurança a navegação do protótipo.
            if (!new UsuarioRepository().Validar(usuario, senha))
            {
                MessageBox.Show(
                    "Login ou senha inválidos.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                textBox2.Clear();
                textBox2.Focus();
                return;
            }

            Hide();
            using (PrincipalForm menuPrincipal = new PrincipalForm())
            {
                menuPrincipal.ShowDialog();
            }
            Close();
        }
    }
}
