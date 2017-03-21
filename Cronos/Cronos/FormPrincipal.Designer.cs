namespace Cronos
{
    partial class FormPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.btnSearchOriginPath = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkHelp = new System.Windows.Forms.LinkLabel();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNamespaceOrigin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtDestinyPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearchDestinyPath = new System.Windows.Forms.Button();
            this.txtOriginPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearchOriginPath
            // 
            this.btnSearchOriginPath.Location = new System.Drawing.Point(577, 28);
            this.btnSearchOriginPath.Name = "btnSearchOriginPath";
            this.btnSearchOriginPath.Size = new System.Drawing.Size(75, 23);
            this.btnSearchOriginPath.TabIndex = 0;
            this.btnSearchOriginPath.Text = "Search";
            this.btnSearchOriginPath.UseVisualStyleBackColor = true;
            this.btnSearchOriginPath.Click += new System.EventHandler(this.btnSearchOriginPath_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkHelp);
            this.groupBox1.Controls.Add(this.txtProduct);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtClient);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNamespaceOrigin);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLog);
            this.groupBox1.Controls.Add(this.txtDestinyPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSearchDestinyPath);
            this.groupBox1.Controls.Add(this.txtOriginPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSearchOriginPath);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(667, 444);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Directories";
            // 
            // linkHelp
            // 
            this.linkHelp.AutoSize = true;
            this.linkHelp.Location = new System.Drawing.Point(609, 184);
            this.linkHelp.Name = "linkHelp";
            this.linkHelp.Size = new System.Drawing.Size(38, 13);
            this.linkHelp.TabIndex = 18;
            this.linkHelp.TabStop = true;
            this.linkHelp.Text = "Help ?";
            this.linkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHelp_LinkClicked);
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(394, 142);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(177, 20);
            this.txtProduct.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(338, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Product :";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(379, 184);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(166, 13);
            this.lblStatus.TabIndex = 14;
            this.lblStatus.Text = "You have to select the origin path";
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
            // txtClient
            // 
            this.txtClient.Location = new System.Drawing.Point(148, 142);
            this.txtClient.Name = "txtClient";
            this.txtClient.Size = new System.Drawing.Size(177, 20);
            this.txtClient.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Client :";
            // 
            // txtNamespaceOrigin
            // 
            this.txtNamespaceOrigin.BackColor = System.Drawing.SystemColors.Control;
            this.txtNamespaceOrigin.Enabled = false;
            this.txtNamespaceOrigin.Location = new System.Drawing.Point(148, 107);
            this.txtNamespaceOrigin.Name = "txtNamespaceOrigin";
            this.txtNamespaceOrigin.Size = new System.Drawing.Size(423, 20);
            this.txtNamespaceOrigin.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Namespace Origin :";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(67, 179);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnIniciar_Click);
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
            // txtDestinyPath
            // 
            this.txtDestinyPath.BackColor = System.Drawing.SystemColors.Control;
            this.txtDestinyPath.Enabled = false;
            this.txtDestinyPath.Location = new System.Drawing.Point(80, 65);
            this.txtDestinyPath.Name = "txtDestinyPath";
            this.txtDestinyPath.Size = new System.Drawing.Size(491, 20);
            this.txtDestinyPath.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destiny Path :";
            // 
            // btnSearchDestinyPath
            // 
            this.btnSearchDestinyPath.Location = new System.Drawing.Point(577, 65);
            this.btnSearchDestinyPath.Name = "btnSearchDestinyPath";
            this.btnSearchDestinyPath.Size = new System.Drawing.Size(75, 23);
            this.btnSearchDestinyPath.TabIndex = 3;
            this.btnSearchDestinyPath.Text = "Search";
            this.btnSearchDestinyPath.UseVisualStyleBackColor = true;
            this.btnSearchDestinyPath.Click += new System.EventHandler(this.btnSearchDestinyPath_Click);
            // 
            // txtOriginPath
            // 
            this.txtOriginPath.BackColor = System.Drawing.SystemColors.Control;
            this.txtOriginPath.Enabled = false;
            this.txtOriginPath.Location = new System.Drawing.Point(80, 28);
            this.txtOriginPath.Name = "txtOriginPath";
            this.txtOriginPath.Size = new System.Drawing.Size(491, 20);
            this.txtOriginPath.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Origin Path :";
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 444);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPrincipal";
            this.Text = "Cronos - Generator Zeus - v1.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSearchOriginPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOriginPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClient;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNamespaceOrigin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtDestinyPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearchDestinyPath;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkHelp;
    }
}

