namespace SarcIntelService.PersonView
{
    partial class PersonEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonEditor));
            this.SaveButton = new System.Windows.Forms.Button();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.moradaTextBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.birthPicker = new System.Windows.Forms.DateTimePicker();
            this.nacioBoxText = new System.Windows.Forms.TextBox();
            this.nacioBox = new System.Windows.Forms.TextBox();
            this.birthBox = new System.Windows.Forms.TextBox();
            this.nameBoxText = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.photoButton = new System.Windows.Forms.Button();
            this.photo = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.adicionarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.infoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photo)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(612, 259);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Guardar";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // infoPanel
            // 
            this.infoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoPanel.Controls.Add(this.textBox3);
            this.infoPanel.Controls.Add(this.textBox1);
            this.infoPanel.Controls.Add(this.moradaTextBox);
            this.infoPanel.Controls.Add(this.textBox2);
            this.infoPanel.Controls.Add(this.birthPicker);
            this.infoPanel.Controls.Add(this.nacioBoxText);
            this.infoPanel.Controls.Add(this.nacioBox);
            this.infoPanel.Controls.Add(this.birthBox);
            this.infoPanel.Controls.Add(this.nameBoxText);
            this.infoPanel.Controls.Add(this.nameBox);
            this.infoPanel.Location = new System.Drawing.Point(270, 27);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(521, 149);
            this.infoPanel.TabIndex = 3;
            // 
            // moradaTextBox
            // 
            this.moradaTextBox.BackColor = System.Drawing.Color.White;
            this.moradaTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moradaTextBox.Location = new System.Drawing.Point(133, 94);
            this.moradaTextBox.Name = "moradaTextBox";
            this.moradaTextBox.Size = new System.Drawing.Size(368, 21);
            this.moradaTextBox.TabIndex = 10;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(12, 94);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(115, 21);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "RESIDENCIA";
            // 
            // birthPicker
            // 
            this.birthPicker.Location = new System.Drawing.Point(133, 42);
            this.birthPicker.Name = "birthPicker";
            this.birthPicker.Size = new System.Drawing.Size(368, 20);
            this.birthPicker.TabIndex = 8;
            this.birthPicker.ValueChanged += new System.EventHandler(this.birthPicker_ValueChanged);
            // 
            // nacioBoxText
            // 
            this.nacioBoxText.BackColor = System.Drawing.Color.White;
            this.nacioBoxText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nacioBoxText.Location = new System.Drawing.Point(133, 67);
            this.nacioBoxText.Name = "nacioBoxText";
            this.nacioBoxText.Size = new System.Drawing.Size(368, 21);
            this.nacioBoxText.TabIndex = 5;
            // 
            // nacioBox
            // 
            this.nacioBox.Enabled = false;
            this.nacioBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nacioBox.Location = new System.Drawing.Point(12, 67);
            this.nacioBox.Name = "nacioBox";
            this.nacioBox.Size = new System.Drawing.Size(115, 21);
            this.nacioBox.TabIndex = 4;
            this.nacioBox.Text = "NATURALIDADE";
            // 
            // birthBox
            // 
            this.birthBox.Enabled = false;
            this.birthBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.birthBox.Location = new System.Drawing.Point(12, 41);
            this.birthBox.Name = "birthBox";
            this.birthBox.Size = new System.Drawing.Size(115, 21);
            this.birthBox.TabIndex = 2;
            this.birthBox.Text = "NASCIMENTO";
            // 
            // nameBoxText
            // 
            this.nameBoxText.BackColor = System.Drawing.Color.White;
            this.nameBoxText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameBoxText.Location = new System.Drawing.Point(133, 15);
            this.nameBoxText.Name = "nameBoxText";
            this.nameBoxText.Size = new System.Drawing.Size(368, 21);
            this.nameBoxText.TabIndex = 1;
            this.nameBoxText.Text = "Nome Completo";
            // 
            // nameBox
            // 
            this.nameBox.Enabled = false;
            this.nameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameBox.Location = new System.Drawing.Point(12, 15);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(115, 21);
            this.nameBox.TabIndex = 0;
            this.nameBox.Text = "NOME";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(704, 259);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Cancelar";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // photoButton
            // 
            this.photoButton.Location = new System.Drawing.Point(12, 260);
            this.photoButton.Name = "photoButton";
            this.photoButton.Size = new System.Drawing.Size(252, 23);
            this.photoButton.TabIndex = 8;
            this.photoButton.Text = "Seleccionar Foto Principal";
            this.photoButton.UseVisualStyleBackColor = true;
            this.photoButton.Click += new System.EventHandler(this.photoButton_Click);
            // 
            // photo
            // 
            this.photo.BackColor = System.Drawing.Color.White;
            this.photo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.photo.Location = new System.Drawing.Point(12, 27);
            this.photo.Name = "photo";
            this.photo.Size = new System.Drawing.Size(252, 227);
            this.photo.TabIndex = 1;
            this.photo.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adicionarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(791, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // adicionarToolStripMenuItem
            // 
            this.adicionarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentoToolStripMenuItem});
            this.adicionarToolStripMenuItem.Name = "adicionarToolStripMenuItem";
            this.adicionarToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.adicionarToolStripMenuItem.Text = "Adicionar";
            // 
            // documentoToolStripMenuItem
            // 
            this.documentoToolStripMenuItem.Name = "documentoToolStripMenuItem";
            this.documentoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.documentoToolStripMenuItem.Text = "Documento";
            this.documentoToolStripMenuItem.Click += new System.EventHandler(this.documentoToolStripMenuItem_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 121);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(115, 21);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "NIF";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(133, 121);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(115, 21);
            this.textBox3.TabIndex = 12;
            // 
            // PersonEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 294);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.photoButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.infoPanel);
            this.Controls.Add(this.photo);
            this.Controls.Add(this.SaveButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PersonEditor";
            this.Text = "Editor de Perfil";
            this.Load += new System.EventHandler(this.PersonEditor_Load);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photo)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox photo;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.TextBox nameBoxText;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox birthBox;
        private System.Windows.Forms.TextBox nacioBoxText;
        private System.Windows.Forms.TextBox nacioBox;
        private System.Windows.Forms.DateTimePicker birthPicker;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button photoButton;
        public System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox moradaTextBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem adicionarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentoToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
    }
}

