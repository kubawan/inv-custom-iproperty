using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;
using InvCustomiPropertyAddIn;
using System.Runtime.InteropServices;

namespace InvAddIn
{
    public partial class AddiProperty : Form
    {
        //Obiekty inventora
        Inventor.Application InvApplication;
        Inventor.Document InvDocument;
        Inventor.PropertySet InvPropertySet;
        Inventor.Property InvProperty;

        public AddiProperty()
        {
            InitializeComponent();

        }
        #region WinForms
        private void BtniPropAdd_Click(object sender, EventArgs e)
        {
            //Wskaznik na aktywna instancje Inventora
            InvApplication = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
            InvDocument = InvApplication.ActiveDocument;
            if (String.IsNullOrWhiteSpace(TxtBoxiPropName.Text) == false)
            {
                //jezeli pole iProperty: Wartosc nie jest puste
                UpdateOrCreateCustomiProperty(InvDocument, TxtBoxiPropName.Text, TxtBoxiPropVal.Text.ToString());
                //po dodaniu zmiennej wyczyść pola
                TxtBoxiPropName.Clear();
                TxtBoxiPropVal.Clear();
                Close();
            }
            else
            {
                MessageBox.Show("Uzupełnij pole iProperty: Nazwa!", "Add iProperty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
        #region OtherMethods
        public void UpdateOrCreateCustomiProperty(Inventor.Document Document, string PropertyName, string PropertyValue)
        {
            try
            {
                //Wskaznik na PropertySet Custom iProperty
                InvPropertySet = Document.PropertySets["Inventor User Defined Properties"];

                //Sprawdza czy istnieje wskazana zmienna jezeli tak ustawia na nia wskaznik, jezeli nie ustawia Bool propertyExist na false 
                InvProperty = null;
                Boolean propertyExists = true;
                try
                {
                    InvProperty = InvPropertySet[PropertyName];
                }
                catch
                {
                    propertyExists = false;
                }

                //Sprawdza czy w poprzednim kroku pozyskano nazwe zmiennej
                if (!propertyExists)
                {
                    //Nie udalo sie pozyskac nazwy istniejacej zmiennej wiec tworzy ja
                    InvProperty = InvPropertySet.Add(PropertyValue, PropertyName, null);
                }
                else
                {
                    //Udalo sie pozyskac nazwe istniejacej zmiennej wiec aktualizuje jedynie jej wartosc
                    InvProperty.Value = PropertyValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "AddIn Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
