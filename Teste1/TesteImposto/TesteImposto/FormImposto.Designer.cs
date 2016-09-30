namespace TesteImposto
{
    partial class frmImposto
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
            this.lblNomeCliente = new System.Windows.Forms.Label();
            this.lblEstadoOrigem = new System.Windows.Forms.Label();
            this.lblEstadoDestino = new System.Windows.Forms.Label();
            this.txtNomeCliente = new System.Windows.Forms.TextBox();
            this.txtEstadoOrigem = new System.Windows.Forms.TextBox();
            this.txtEstadoDestino = new System.Windows.Forms.TextBox();
            this.lblItensPedido = new System.Windows.Forms.Label();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.btnGerarNotaFiscal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNomeCliente
            // 
            this.lblNomeCliente.AutoSize = true;
            this.lblNomeCliente.Location = new System.Drawing.Point(3, 9);
            this.lblNomeCliente.Name = "lblNomeCliente";
            this.lblNomeCliente.Size = new System.Drawing.Size(85, 13);
            this.lblNomeCliente.TabIndex = 0;
            this.lblNomeCliente.Text = "Nome do Cliente";
            // 
            // lblEstadoOrigem
            // 
            this.lblEstadoOrigem.AutoSize = true;
            this.lblEstadoOrigem.Location = new System.Drawing.Point(3, 34);
            this.lblEstadoOrigem.Name = "lblEstadoOrigem";
            this.lblEstadoOrigem.Size = new System.Drawing.Size(76, 13);
            this.lblEstadoOrigem.TabIndex = 1;
            this.lblEstadoOrigem.Text = "Estado Origem";
            // 
            // lblEstadoDestino
            // 
            this.lblEstadoDestino.AutoSize = true;
            this.lblEstadoDestino.Location = new System.Drawing.Point(3, 61);
            this.lblEstadoDestino.Name = "lblEstadoDestino";
            this.lblEstadoDestino.Size = new System.Drawing.Size(79, 13);
            this.lblEstadoDestino.TabIndex = 2;
            this.lblEstadoDestino.Text = "Estado Destino";
            // 
            // txtNomeCliente
            // 
            this.txtNomeCliente.Location = new System.Drawing.Point(95, 9);
            this.txtNomeCliente.Name = "txtNomeCliente";
            this.txtNomeCliente.Size = new System.Drawing.Size(939, 20);
            this.txtNomeCliente.TabIndex = 3;
            // 
            // txtEstadoOrigem
            // 
            this.txtEstadoOrigem.Location = new System.Drawing.Point(95, 31);
            this.txtEstadoOrigem.Name = "txtEstadoOrigem";
            this.txtEstadoOrigem.Size = new System.Drawing.Size(939, 20);
            this.txtEstadoOrigem.TabIndex = 4;
            // 
            // txtEstadoDestino
            // 
            this.txtEstadoDestino.Location = new System.Drawing.Point(95, 53);
            this.txtEstadoDestino.Name = "txtEstadoDestino";
            this.txtEstadoDestino.Size = new System.Drawing.Size(939, 20);
            this.txtEstadoDestino.TabIndex = 5;
            // 
            // lblItensPedido
            // 
            this.lblItensPedido.AutoSize = true;
            this.lblItensPedido.Location = new System.Drawing.Point(2, 93);
            this.lblItensPedido.Name = "lblItensPedido";
            this.lblItensPedido.Size = new System.Drawing.Size(80, 13);
            this.lblItensPedido.TabIndex = 6;
            this.lblItensPedido.Text = "Itens do pedido";
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.AllowUserToOrderColumns = true;
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.Location = new System.Drawing.Point(6, 109);
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.Size = new System.Drawing.Size(1028, 325);
            this.dgvPedidos.TabIndex = 7;
            // 
            // btnGerarNotaFiscal
            // 
            this.btnGerarNotaFiscal.Location = new System.Drawing.Point(907, 440);
            this.btnGerarNotaFiscal.Name = "btnGerarNotaFiscal";
            this.btnGerarNotaFiscal.Size = new System.Drawing.Size(127, 23);
            this.btnGerarNotaFiscal.TabIndex = 8;
            this.btnGerarNotaFiscal.Text = "Gerar Nota Fiscal";
            this.btnGerarNotaFiscal.UseVisualStyleBackColor = true;
            this.btnGerarNotaFiscal.Click += new System.EventHandler(this.btnGerarNotaFiscal_Click);
            // 
            // frmImposto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 477);
            this.Controls.Add(this.btnGerarNotaFiscal);
            this.Controls.Add(this.dgvPedidos);
            this.Controls.Add(this.lblItensPedido);
            this.Controls.Add(this.txtEstadoDestino);
            this.Controls.Add(this.txtEstadoOrigem);
            this.Controls.Add(this.txtNomeCliente);
            this.Controls.Add(this.lblEstadoDestino);
            this.Controls.Add(this.lblEstadoOrigem);
            this.Controls.Add(this.lblNomeCliente);
            this.Name = "frmImposto";
            this.Text = "Calculo de imposto";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNomeCliente;
        private System.Windows.Forms.Label lblEstadoOrigem;
        private System.Windows.Forms.Label lblEstadoDestino;
        private System.Windows.Forms.TextBox txtNomeCliente;
        private System.Windows.Forms.TextBox txtEstadoOrigem;
        private System.Windows.Forms.TextBox txtEstadoDestino;
        private System.Windows.Forms.Label lblItensPedido;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.Button btnGerarNotaFiscal;
    }
}

