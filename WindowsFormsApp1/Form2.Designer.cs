
namespace WindowsFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.funcionárioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviçoDeQuartoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.almoxarifadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fornecedorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.almoxarifadoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.funcionárioToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.clienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reservaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviçoDeQuartoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.achadoEPerdidoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.relatorioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.financeiroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contaDeUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.funcionárioToolStripMenuItem,
            this.serviçoDeQuartoToolStripMenuItem,
            this.almoxarifadoToolStripMenuItem,
            this.ajudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(599, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // funcionárioToolStripMenuItem
            // 
            this.funcionárioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.funcionárioToolStripMenuItem1,
            this.clienteToolStripMenuItem,
            this.contaDeUsuarioToolStripMenuItem});
            this.funcionárioToolStripMenuItem.Name = "funcionárioToolStripMenuItem";
            this.funcionárioToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.funcionárioToolStripMenuItem.Text = "Entrar";
            this.funcionárioToolStripMenuItem.Click += new System.EventHandler(this.funcionárioToolStripMenuItem_Click);
            // 
            // serviçoDeQuartoToolStripMenuItem
            // 
            this.serviçoDeQuartoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reservaToolStripMenuItem,
            this.serviçoDeQuartoToolStripMenuItem1,
            this.achadoEPerdidoToolStripMenuItem1});
            this.serviçoDeQuartoToolStripMenuItem.Name = "serviçoDeQuartoToolStripMenuItem";
            this.serviçoDeQuartoToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.serviçoDeQuartoToolStripMenuItem.Text = "Hospedagem";
            // 
            // almoxarifadoToolStripMenuItem
            // 
            this.almoxarifadoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fornecedorToolStripMenuItem,
            this.almoxarifadoToolStripMenuItem1,
            this.sacToolStripMenuItem,
            this.relatorioToolStripMenuItem,
            this.financeiroToolStripMenuItem});
            this.almoxarifadoToolStripMenuItem.Name = "almoxarifadoToolStripMenuItem";
            this.almoxarifadoToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.almoxarifadoToolStripMenuItem.Text = "Outros";
            this.almoxarifadoToolStripMenuItem.Click += new System.EventHandler(this.almoxarifadoToolStripMenuItem_Click);
            // 
            // fornecedorToolStripMenuItem
            // 
            this.fornecedorToolStripMenuItem.Name = "fornecedorToolStripMenuItem";
            this.fornecedorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fornecedorToolStripMenuItem.Text = "Fornecedor";
            // 
            // almoxarifadoToolStripMenuItem1
            // 
            this.almoxarifadoToolStripMenuItem1.Name = "almoxarifadoToolStripMenuItem1";
            this.almoxarifadoToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.almoxarifadoToolStripMenuItem1.Text = "Almoxarifado";
            // 
            // sacToolStripMenuItem
            // 
            this.sacToolStripMenuItem.Name = "sacToolStripMenuItem";
            this.sacToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sacToolStripMenuItem.Text = "Sac";
            // 
            // funcionárioToolStripMenuItem1
            // 
            this.funcionárioToolStripMenuItem1.Name = "funcionárioToolStripMenuItem1";
            this.funcionárioToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.funcionárioToolStripMenuItem1.Text = "Funcionário";
            // 
            // clienteToolStripMenuItem
            // 
            this.clienteToolStripMenuItem.Name = "clienteToolStripMenuItem";
            this.clienteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clienteToolStripMenuItem.Text = "Cliente";
            // 
            // reservaToolStripMenuItem
            // 
            this.reservaToolStripMenuItem.Name = "reservaToolStripMenuItem";
            this.reservaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reservaToolStripMenuItem.Text = "Reserva";
            // 
            // serviçoDeQuartoToolStripMenuItem1
            // 
            this.serviçoDeQuartoToolStripMenuItem1.Name = "serviçoDeQuartoToolStripMenuItem1";
            this.serviçoDeQuartoToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.serviçoDeQuartoToolStripMenuItem1.Text = "Serviço de Quarto";
            // 
            // achadoEPerdidoToolStripMenuItem1
            // 
            this.achadoEPerdidoToolStripMenuItem1.Name = "achadoEPerdidoToolStripMenuItem1";
            this.achadoEPerdidoToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.achadoEPerdidoToolStripMenuItem1.Text = "Achado e Perdido";
            // 
            // relatorioToolStripMenuItem
            // 
            this.relatorioToolStripMenuItem.Name = "relatorioToolStripMenuItem";
            this.relatorioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.relatorioToolStripMenuItem.Text = "Relatorio";
            // 
            // financeiroToolStripMenuItem
            // 
            this.financeiroToolStripMenuItem.Name = "financeiroToolStripMenuItem";
            this.financeiroToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.financeiroToolStripMenuItem.Text = "Financeiro";
            // 
            // contaDeUsuarioToolStripMenuItem
            // 
            this.contaDeUsuarioToolStripMenuItem.Name = "contaDeUsuarioToolStripMenuItem";
            this.contaDeUsuarioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.contaDeUsuarioToolStripMenuItem.Text = "Conta de Usuario";
            // 
            // ajudaToolStripMenuItem
            // 
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            this.ajudaToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.ajudaToolStripMenuItem.Text = "Ajuda";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(125, 77);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(351, 223);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 347);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.Text = "Hotel Bennett’s ";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem funcionárioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviçoDeQuartoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem almoxarifadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fornecedorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem almoxarifadoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sacToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem funcionárioToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contaDeUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reservaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviçoDeQuartoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem achadoEPerdidoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem relatorioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem financeiroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajudaToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}