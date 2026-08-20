using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1.UI
{
    /// <summary>Adapta formulários antigos, criados com coordenadas fixas, ao espaço disponível.</summary>
    public sealed class ResponsiveLayout
    {
        private sealed class ControlState
        {
            public Control Control;
            public Rectangle Bounds;
            public bool TopLevel;
        }

        private readonly Form form;
        private readonly Size originalSize;
        private readonly List<ControlState> states = new List<ControlState>();
        private bool updating;

        private ResponsiveLayout(Form target)
        {
            form = target;
            originalSize = target.ClientSize;
            Capture(target.Controls, true);
            target.AutoScroll = true;
            target.Resize += OnResize;
        }

        public static void Attach(Form form)
        {
            if (form == null || form.ClientSize.Width == 0 || form.ClientSize.Height == 0) return;
            new ResponsiveLayout(form);
        }

        private void Capture(Control.ControlCollection controls, bool topLevel)
        {
            foreach (Control control in controls)
            {
                // O menu do formulário principal continua com o comportamento nativo.
                if (!(control is MenuStrip))
                {
                    control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    states.Add(new ControlState { Control = control, Bounds = control.Bounds, TopLevel = topLevel });
                    if (control.HasChildren) Capture(control.Controls, false);
                }
            }
        }

        private void OnResize(object sender, EventArgs e)
        {
            if (updating || originalSize.Width == 0 || originalSize.Height == 0) return;
            updating = true;
            try
            {
                float availableX = (float)form.ClientSize.Width / originalSize.Width;
                float availableY = (float)form.ClientSize.Height / originalSize.Height;
                float scale = Math.Max(0.65f, Math.Min(availableX, availableY));
                int offsetX = Math.Max(0, (form.ClientSize.Width - (int)(originalSize.Width * scale)) / 2);
                int offsetY = Math.Max(0, (form.ClientSize.Height - (int)(originalSize.Height * scale)) / 2);
                form.SuspendLayout();
                foreach (ControlState state in states)
                {
                    state.Control.Bounds = new Rectangle(
                        (int)Math.Round(state.Bounds.X * scale) + (state.TopLevel ? offsetX : 0),
                        (int)Math.Round(state.Bounds.Y * scale) + (state.TopLevel ? offsetY : 0),
                        Math.Max(20, (int)Math.Round(state.Bounds.Width * scale)),
                        Math.Max(18, (int)Math.Round(state.Bounds.Height * scale)));
                }
                form.AutoScrollMinSize = new Size(
                    (int)Math.Round(originalSize.Width * scale),
                    (int)Math.Round(originalSize.Height * scale));
                form.ResumeLayout(true);
            }
            finally { updating = false; }
        }
    }
}
