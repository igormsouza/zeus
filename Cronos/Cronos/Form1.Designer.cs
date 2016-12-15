namespace Cronos
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnBuscarCaminhoOrigem = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStatusOperacao = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNamespaceOrigem = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtCaminhoDestino = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscarCaminhoDestino = new System.Windows.Forms.Button();
            this.txtCaminhoOrigem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkAjuda = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBuscarCaminhoOrigem
            // 
            this.btnBuscarCaminhoOrigem.Location = new System.Drawing.Point(577, 28);
            this.btnBuscarCaminhoOrigem.Name = "btnBuscarCaminhoOrigem";
            this.btnBuscarCaminhoOrigem.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarCaminhoOrigem.TabIndex = 0;
            this.btnBuscarCaminhoOrigem.Text = "Buscar";
            this.btnBuscarCaminhoOrigem.UseVisualStyleBackColor = true;
            this.btnBuscarCaminhoOrigem.Click += new System.EventHandler(this.btnBuscarCaminhoOrigem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkAjuda);
            this.groupBox1.Controls.Add(this.txtProduto);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblStatusOperacao);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtCliente);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNamespaceOrigem);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnIniciar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLog);
            this.groupBox1.Controls.Add(this.txtCaminhoDestino);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBuscarCaminhoDestino);
            this.groupBox1.Controls.Add(this.txtCaminhoOrigem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnBuscarCaminhoOrigem);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(667, 444);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Diretórios";
            // 
            // txtProduto
            // 
            this.txtProduto.Location = new System.Drawing.Point(394, 142);
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.Size = new System.Drawing.Size(177, 20);
            this.txtProduto.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(338, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Produto :";
            // 
            // lblStatusOperacao
            // 
            this.lblStatusOperacao.AutoSize = true;
            this.lblStatusOperacao.Location = new System.Drawing.Point(379, 184);
            this.lblStatusOperacao.Name = "lblStatusOperacao";
            this.lblStatusOperacao.Size = new System.Drawing.Size(192, 13);
            this.lblStatusOperacao.TabIndex = 14;
            this.lblStatusOperacao.Text = "Aguardando a seleção da pasta origem";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(330, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Status :";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(148, 142);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(177, 20);
            this.txtCliente.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Cliente :";
            // 
            // txtNamespaceOrigem
            // 
            this.txtNamespaceOrigem.BackColor = System.Drawing.SystemColors.Control;
            this.txtNamespaceOrigem.Enabled = false;
            this.txtNamespaceOrigem.Location = new System.Drawing.Point(148, 107);
            this.txtNamespaceOrigem.Name = "txtNamespaceOrigem";
            this.txtNamespaceOrigem.Size = new System.Drawing.Size(423, 20);
            this.txtNamespaceOrigem.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Namespace Origem :";
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(67, 179);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 8;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Log :";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(6, 217);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(653, 221);
            this.txtLog.TabIndex = 6;
            // 
            // txtCaminhoDestino
            // 
            this.txtCaminhoDestino.BackColor = System.Drawing.SystemColors.Control;
            this.txtCaminhoDestino.Enabled = false;
            this.txtCaminhoDestino.Location = new System.Drawing.Point(80, 65);
            this.txtCaminhoDestino.Name = "txtCaminhoDestino";
            this.txtCaminhoDestino.Size = new System.Drawing.Size(491, 20);
            this.txtCaminhoDestino.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destino :";
            // 
            // btnBuscarCaminhoDestino
            // 
            this.btnBuscarCaminhoDestino.Location = new System.Drawing.Point(577, 65);
            this.btnBuscarCaminhoDestino.Name = "btnBuscarCaminhoDestino";
            this.btnBuscarCaminhoDestino.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarCaminhoDestino.TabIndex = 3;
            this.btnBuscarCaminhoDestino.Text = "Buscar";
            this.btnBuscarCaminhoDestino.UseVisualStyleBackColor = true;
            this.btnBuscarCaminhoDestino.Click += new System.EventHandler(this.btnBuscarCaminhoDestino_Click);
            // 
            // txtCaminhoOrigem
            // 
            this.txtCaminhoOrigem.BackColor = System.Drawing.SystemColors.Control;
            this.txtCaminhoOrigem.Enabled = false;
            this.txtCaminhoOrigem.Location = new System.Drawing.Point(80, 28);
            this.txtCaminhoOrigem.Name = "txtCaminhoOrigem";
            this.txtCaminhoOrigem.Size = new System.Drawing.Size(491, 20);
            this.txtCaminhoOrigem.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Origem :";
            // 
            // linkAjuda
            // 
            this.linkAjuda.AutoSize = true;
            this.linkAjuda.Location = new System.Drawing.Point(609, 184);
            this.linkAjuda.Name = "linkAjuda";
            this.linkAjuda.Size = new System.Drawing.Size(43, 13);
            this.linkAjuda.TabIndex = 18;
            this.linkAjuda.TabStop = true;
            this.linkAjuda.Text = "Ajuda ?";
            this.linkAjuda.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAjuda_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 444);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Cronos - Gerador Zeus - v1.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBuscarCaminhoOrigem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCaminhoOrigem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNamespaceOrigem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtCaminhoDestino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscarCaminhoDestino;
        private System.Windows.Forms.Label lblStatusOperacao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProduto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkAjuda;
    }
}

