using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1.UI
{
    public static class ThemeManager
    {
        public static readonly Color Navy = Color.FromArgb(23, 50, 77);
        public static readonly Color Blue = Color.FromArgb(36, 85, 122);
        public static readonly Color Gold = Color.FromArgb(198, 156, 72);
        public static readonly Color Background = Color.FromArgb(244, 246, 248);
        public static readonly Color Success = Color.FromArgb(46, 139, 87);
        public static readonly Color Danger = Color.FromArgb(201, 76, 76);

        public static void Apply(Form form)
        {
            form.BackColor = Background;
            form.Font = new Font("Segoe UI", 9.5f);
            form.ForeColor = Navy;
            form.AutoScroll = true;
            form.Text = GetTitle(form);
            ApplyControls(form.Controls);
        }

        private static string GetTitle(Form form)
        {
            if (form is FuncionarioForm) return "Funcionários - Hotel Bennett's";
            if (form is ClienteForm) return "Clientes - Hotel Bennett's";
            if (form is ReservaForm) return "Reservas e quartos - Hotel Bennett's";
            if (form is ServicoQuartoForm) return "Serviço de quarto - Hotel Bennett's";
            if (form is AchadosPerdidosForm) return "Achados e perdidos - Hotel Bennett's";
            if (form is FornecedorForm) return "Fornecedores - Hotel Bennett's";
            if (form is EstoqueForm) return "Estoque - Hotel Bennett's";
            if (form is UsuarioForm) return "Usuários - Hotel Bennett's";
            if (form is RelatorioForm) return "Relatórios - Hotel Bennett's";
            if (form is FinanceiroForm) return "Financeiro - Hotel Bennett's";
            if (form is SacForm) return "SAC - Hotel Bennett's";
            return form.Text;
        }

        private static void ApplyControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is MenuStrip) { control.Visible = false; continue; }
                if (control is Button) StyleButton((Button)control);
                else if (control is DataGridView) StyleGrid((DataGridView)control);
                else if (control is GroupBox) StyleGroup((GroupBox)control);
                else if (control is TextBoxBase || control is ComboBox || control is NumericUpDown || control is DateTimePicker)
                    StyleInput(control);
                else if (control is Label) control.ForeColor = Navy;
                else if (control is CheckBox || control is RadioButton) control.ForeColor = Navy;

                if (control.HasChildren) ApplyControls(control.Controls);
            }
        }

        private static void StyleInput(Control control)
        {
            control.BackColor = Color.White;
            control.ForeColor = Color.FromArgb(45, 55, 65);
            control.Font = new Font("Segoe UI", 9.5f);
            if (control is TextBoxBase) ((TextBoxBase)control).BorderStyle = BorderStyle.FixedSingle;
        }

        private static void StyleGroup(GroupBox group)
        {
            group.BackColor = Color.White;
            group.ForeColor = Navy;
            group.Padding = new Padding(12, 18, 12, 12);
        }

        private static void StyleButton(Button button)
        {
            string text = (button.Text ?? "").ToLowerInvariant();
            Color color = Blue;
            if (text.Contains("excluir") || text.Contains("cancelar")) color = Danger;
            else if (text.Contains("salvar") || text.Contains("cadastrar") || text.Contains("finalizar") || text.Contains("lançar") || text.Contains("check-in")) color = Success;
            else if (text.Contains("fechar") || text.Contains("voltar") || text.Contains("check-out")) color = Color.FromArgb(95, 105, 115);
            else if (text.Contains("editar")) color = Gold;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
            button.MinimumSize = new Size(78, 30);
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Color.FromArgb(225, 230, 235);
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Navy;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            grid.ColumnHeadersHeight = 36;
            grid.RowTemplate.Height = 32;
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(45, 55, 65);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(218, 232, 242);
            grid.DefaultCellStyle.SelectionForeColor = Navy;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
        }
    }
}
