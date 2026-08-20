using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1.UI
{
    /// <summary>Organiza os formulários legados em conteúdo, tabelas e barra de ações.</summary>
    public static class ModernFormLayout
    {
        public static void Apply(Form form)
        {
            Control[] content = form.Controls.Cast<Control>()
                .Where(c => c.Visible && !(c is MenuStrip) && !(c is Button))
                .OrderBy(c => c.Top).ToArray();
            Button[] buttons = form.Controls.OfType<Button>()
                .Where(b => b.Visible).OrderByDescending(b => b.Left).ToArray();
            if (content.Length == 0) return;

            foreach (Control item in content) PrepareAnchors(item);
            form.AutoScroll = false;
            form.MinimumSize = new Size(760, 540);

            TableLayoutPanel root = new TableLayoutPanel {
                Dock = DockStyle.Fill, BackColor = ThemeManager.Background,
                Padding = new Padding(22), ColumnCount = 1,
                RowCount = content.Length + (buttons.Length > 0 ? 1 : 0)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            float portion = 100f / content.Length;
            foreach (Control item in content)
            {
                root.RowStyles.Add(new RowStyle(SizeType.Percent, portion));
                item.Dock = DockStyle.Fill; item.Margin = new Padding(0, 0, 0, 14);
                root.Controls.Add(item, 0, root.Controls.Count);
            }

            if (buttons.Length > 0)
            {
                root.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
                FlowLayoutPanel actions = new FlowLayoutPanel {
                    Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft,
                    Padding = new Padding(0, 10, 0, 0)
                };
                foreach (Button button in buttons) {
                    button.Dock = DockStyle.None; button.Anchor = AnchorStyles.None;
                    button.Size = new Size(Math.Max(105, button.Width), 36);
                    button.Margin = new Padding(8, 0, 0, 0); actions.Controls.Add(button);
                }
                root.Controls.Add(actions, 0, content.Length);
            }
            form.Controls.Add(root);
            root.BringToFront();
        }

        private static void PrepareAnchors(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                AnchorStyles anchor = AnchorStyles.Top | AnchorStyles.Left;
                if (child.Right >= parent.ClientSize.Width * 0.65) anchor |= AnchorStyles.Right;
                if (child.Bottom >= parent.ClientSize.Height * 0.65) anchor |= AnchorStyles.Bottom;
                child.Anchor = anchor;
                if (child is DataGridView) {
                    child.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    ((DataGridView)child).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                if (child.HasChildren) PrepareAnchors(child);
            }
        }
    }
}
