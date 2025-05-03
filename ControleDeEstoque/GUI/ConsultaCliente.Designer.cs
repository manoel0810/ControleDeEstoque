namespace GUI
{
    partial class ConsultaCliente
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
            this.dgvDados = new System.Windows.Forms.DataGridView();
            this.btLocalizar = new System.Windows.Forms.Button();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCPF = new System.Windows.Forms.RadioButton();
            this.rbNome = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDados
            // 
            this.dgvDados.AllowUserToAddRows = false;
            this.dgvDados.AllowUserToDeleteRows = false;
            this.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDados.Location = new System.Drawing.Point(20, 129);
            this.dgvDados.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDados.Name = "dgvDados";
            this.dgvDados.ReadOnly = true;
            this.dgvDados.RowHeadersWidth = 51;
            this.dgvDados.RowTemplate.Height = 24;
            this.dgvDados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDados.Size = new System.Drawing.Size(747, 412);
            this.dgvDados.TabIndex = 7;
            this.dgvDados.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Dados_CellMouseClick);
            // 
            // btLocalizar
            // 
            this.btLocalizar.Location = new System.Drawing.Point(637, 74);
            this.btLocalizar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btLocalizar.Name = "btLocalizar";
            this.btLocalizar.Size = new System.Drawing.Size(128, 32);
            this.btLocalizar.TabIndex = 6;
            this.btLocalizar.Text = "Localizar";
            this.btLocalizar.UseVisualStyleBackColor = true;
            this.btLocalizar.Click += new System.EventHandler(this.Localizar_Click);
            // 
            // txtValor
            // 
            this.txtValor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtValor.Location = new System.Drawing.Point(199, 42);
            this.txtValor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(567, 22);
            this.txtValor.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Valor";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCPF);
            this.groupBox1.Controls.Add(this.rbNome);
            this.groupBox1.Location = new System.Drawing.Point(20, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(173, 94);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Consultar pelo:";
            // 
            // rbCPF
            // 
            this.rbCPF.AutoSize = true;
            this.rbCPF.Location = new System.Drawing.Point(16, 57);
            this.rbCPF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbCPF.Name = "rbCPF";
            this.rbCPF.Size = new System.Drawing.Size(93, 20);
            this.rbCPF.TabIndex = 1;
            this.rbCPF.Text = "CPF/CNPJ";
            this.rbCPF.UseVisualStyleBackColor = true;
            // 
            // rbNome
            // 
            this.rbNome.AutoSize = true;
            this.rbNome.Checked = true;
            this.rbNome.Location = new System.Drawing.Point(16, 30);
            this.rbNome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbNome.Name = "rbNome";
            this.rbNome.Size = new System.Drawing.Size(65, 20);
            this.rbNome.TabIndex = 0;
            this.rbNome.TabStop = true;
            this.rbNome.Text = "Nome";
            this.rbNome.UseVisualStyleBackColor = true;
            // 
            // ConsultaCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 553);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDados);
            this.Controls.Add(this.btLocalizar);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsultaCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Consulta de cliente";
            this.Load += new System.EventHandler(this.ConsultaCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDados;
        private System.Windows.Forms.Button btLocalizar;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCPF;
        private System.Windows.Forms.RadioButton rbNome;
    }
}