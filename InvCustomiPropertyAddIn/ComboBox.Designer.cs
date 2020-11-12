using System.Windows.Forms;

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
            this.LblAktWartiProp = new System.Windows.Forms.Label();
            this.TxtBoxAktWartiProp = new System.Windows.Forms.TextBox();
            this.LblNewiPropVal = new System.Windows.Forms.Label();
            this.LblCmbBoxName = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ComboBoxiProp
            // 
            this.ComboBoxiProp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxiProp.FormattingEnabled = true;
            this.ComboBoxiProp.Location = new System.Drawing.Point(15, 29);
            this.ComboBoxiProp.Name = "ComboBoxiProp";
            this.ComboBoxiProp.Size = new System.Drawing.Size(251, 24);
            this.ComboBoxiProp.TabIndex = 0;
            this.ComboBoxiProp.SelectedIndexChanged += new System.EventHandler(this.ComboBoxiProp_SelectedIndexChanged);
            this.ComboBoxiProp.Click += new System.EventHandler(this.ComboBoxiProp_Click);
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Location = new System.Drawing.Point(140, 210);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(126, 30);
            this.BtnUpdate.TabIndex = 1;
            this.BtnUpdate.Text = "Aktualizuj";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // TxtBoxiPropVal
            // 
            this.TxtBoxiPropVal.Location = new System.Drawing.Point(15, 76);
            this.TxtBoxiPropVal.Multiline = true;
            this.TxtBoxiPropVal.Name = "TxtBoxiPropVal";
            this.TxtBoxiPropVal.Size = new System.Drawing.Size(251, 128);
            this.TxtBoxiPropVal.TabIndex = 2;
            // 
            // LblAktWartiProp
            // 
            this.LblAktWartiProp.AutoSize = true;
            this.LblAktWartiProp.Location = new System.Drawing.Point(12, 243);
            this.LblAktWartiProp.Name = "LblAktWartiProp";
            this.LblAktWartiProp.Size = new System.Drawing.Size(115, 17);
            this.LblAktWartiProp.TabIndex = 3;
            this.LblAktWartiProp.Text = "Aktualna wartość";
            // 
            // TxtBoxAktWartiProp
            // 
            this.TxtBoxAktWartiProp.Location = new System.Drawing.Point(15, 263);
            this.TxtBoxAktWartiProp.Multiline = true;
            this.TxtBoxAktWartiProp.Name = "TxtBoxAktWartiProp";
            this.TxtBoxAktWartiProp.ReadOnly = true;
            this.TxtBoxAktWartiProp.Size = new System.Drawing.Size(251, 137);
            this.TxtBoxAktWartiProp.TabIndex = 4;
            // 
            // LblNewiPropVal
            // 
            this.LblNewiPropVal.AutoSize = true;
            this.LblNewiPropVal.Location = new System.Drawing.Point(12, 56);
            this.LblNewiPropVal.Name = "LblNewiPropVal";
            this.LblNewiPropVal.Size = new System.Drawing.Size(95, 17);
            this.LblNewiPropVal.TabIndex = 5;
            this.LblNewiPropVal.Text = "Nowa wartość";
            // 
            // LblCmbBoxName
            // 
            this.LblCmbBoxName.AutoSize = true;
            this.LblCmbBoxName.Location = new System.Drawing.Point(12, 9);
            this.LblCmbBoxName.Name = "LblCmbBoxName";
            this.LblCmbBoxName.Size = new System.Drawing.Size(116, 17);
            this.LblCmbBoxName.TabIndex = 6;
            this.LblCmbBoxName.Text = "Wybierz zmienną";
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(15, 210);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(122, 30);
            this.AddButton.TabIndex = 7;
            this.AddButton.Text = "Dodaj";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // CmbBoxiProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 416);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.LblCmbBoxName);
            this.Controls.Add(this.LblNewiPropVal);
            this.Controls.Add(this.TxtBoxAktWartiProp);
            this.Controls.Add(this.LblAktWartiProp);
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
        private Label LblAktWartiProp;
        private TextBox TxtBoxAktWartiProp;
        private Label LblNewiPropVal;
        private Label LblCmbBoxName;
        private Button AddButton;
    }
}