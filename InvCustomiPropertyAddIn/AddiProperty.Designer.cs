namespace InvAddIn
{
    partial class AddiProperty
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
            this.TxtBoxiPropName = new System.Windows.Forms.TextBox();
            this.LbliPropName = new System.Windows.Forms.Label();
            this.BtniPropAdd = new System.Windows.Forms.Button();
            this.TxtBoxiPropVal = new System.Windows.Forms.TextBox();
            this.LbliPropVal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TxtBoxiPropName
            // 
            this.TxtBoxiPropName.Location = new System.Drawing.Point(12, 33);
            this.TxtBoxiPropName.Name = "TxtBoxiPropName";
            this.TxtBoxiPropName.Size = new System.Drawing.Size(249, 22);
            this.TxtBoxiPropName.TabIndex = 0;
            // 
            // LbliPropName
            // 
            this.LbliPropName.AutoSize = true;
            this.LbliPropName.Location = new System.Drawing.Point(12, 9);
            this.LbliPropName.Name = "LbliPropName";
            this.LbliPropName.Size = new System.Drawing.Size(111, 17);
            this.LbliPropName.TabIndex = 1;
            this.LbliPropName.Text = "iPropery: Nazwa";
            // 
            // BtniPropAdd
            // 
            this.BtniPropAdd.Location = new System.Drawing.Point(12, 163);
            this.BtniPropAdd.Name = "BtniPropAdd";
            this.BtniPropAdd.Size = new System.Drawing.Size(249, 38);
            this.BtniPropAdd.TabIndex = 2;
            this.BtniPropAdd.Text = "Dodaj";
            this.BtniPropAdd.UseVisualStyleBackColor = true;
            this.BtniPropAdd.Click += new System.EventHandler(this.BtniPropAdd_Click);
            // 
            // TxtBoxiPropVal
            // 
            this.TxtBoxiPropVal.Location = new System.Drawing.Point(12, 78);
            this.TxtBoxiPropVal.Multiline = true;
            this.TxtBoxiPropVal.Name = "TxtBoxiPropVal";
            this.TxtBoxiPropVal.Size = new System.Drawing.Size(249, 79);
            this.TxtBoxiPropVal.TabIndex = 0;
            // 
            // LbliPropVal
            // 
            this.LbliPropVal.AutoSize = true;
            this.LbliPropVal.Location = new System.Drawing.Point(12, 58);
            this.LbliPropVal.Name = "LbliPropVal";
            this.LbliPropVal.Size = new System.Drawing.Size(125, 17);
            this.LbliPropVal.TabIndex = 1;
            this.LbliPropVal.Text = "iProperty: Wartość";
            // 
            // AddiProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 215);
            this.Controls.Add(this.BtniPropAdd);
            this.Controls.Add(this.LbliPropVal);
            this.Controls.Add(this.TxtBoxiPropVal);
            this.Controls.Add(this.LbliPropName);
            this.Controls.Add(this.TxtBoxiPropName);
            this.Name = "AddiProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add iProperty";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtBoxiPropName;
        private System.Windows.Forms.Label LbliPropName;
        private System.Windows.Forms.Button BtniPropAdd;
        private System.Windows.Forms.TextBox TxtBoxiPropVal;
        private System.Windows.Forms.Label LbliPropVal;
    }
}