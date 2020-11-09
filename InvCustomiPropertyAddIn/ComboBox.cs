using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InvCustomiPropertyAddIn;

namespace InvAddIn
{
    public partial class CmbBoxiProp : Form
    {
        public CmbBoxiProp()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Działam");
            string propertyName;
            StandardAddInServer addInServer = new StandardAddInServer();
            Inventor.Document invDoc = addInServer.GetActiveDoc();
            Inventor.PropertySet propertySet = addInServer.GetPropertySet(invDoc);
            foreach (Inventor.PropertySet propertySetLoop in propertySet)
            {
                foreach (Inventor.Property propertyLoop in propertySetLoop)
                {
                    propertyName = propertyLoop.Name;
                    ComboBoxiProp.Items.Add(propertyName);
                }
            }
        }
    }
}
