namespace InvAddIn
{
    partial class CmbBoxiProp
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
            this.ComboBoxiProp = new System.Windows.Forms.ComboBox();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.TxtBoxiPropVal = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ComboBoxiProp
            // 
            this.ComboBoxiProp.FormattingEnabled = true;
            this.ComboBoxiProp.Location = new System.Drawing.Point(12, 12);
            this.ComboBoxiProp.Name = "ComboBoxiProp";
            this.ComboBoxiProp.Size = new System.Drawing.Size(247, 24);
            this.ComboBoxiProp.TabIndex = 0;
            this.ComboBoxiProp.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Location = new System.Drawing.Point(12, 176);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(247, 30);
            this.BtnUpdate.TabIndex = 1;
            this.BtnUpdate.Text = "Aktualizuj";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // TxtBoxiPropVal
            // 
            this.TxtBoxiPropVal.Location = new System.Drawing.Point(12, 42);
            this.TxtBoxiPropVal.Multiline = true;
            this.TxtBoxiPropVal.Name = "TxtBoxiPropVal";
            this.TxtBoxiPropVal.Size = new System.Drawing.Size(247, 128);
            this.TxtBoxiPropVal.TabIndex = 2;
            // 
            // CmbBoxiProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 228);
            this.Controls.Add(this.TxtBoxiPropVal);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.ComboBoxiProp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CmbBoxiProp";
            this.Text = "iProperty";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBoxiProp;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.TextBox TxtBoxiPropVal;
    }
}