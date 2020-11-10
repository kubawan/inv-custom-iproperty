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
using Inventor;
using System.Runtime.InteropServices;

namespace InvAddIn
{
    public partial class CmbBoxiProp : Form
    {
        //Obiekty inventora
        Inventor.Application InvApplication;
        Inventor.Document InvDocument;
        Inventor.PropertySet InvPropertySet;
        Inventor.Property InvProperty;

        //Konstruktor klasy inicjalizujacy WinFormsa
        public CmbBoxiProp()
        {
            InitializeComponent();
        }
        #region WinForm Elemens Methods
        //Przycisk aktualizuj
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            //Jezeli wybrano iProperty
            if (ComboBoxiProp.Text != null)
            {
                //Aktualizuj jej wartosc
                UpdateOrCreateCustomiProperty(InvDocument, ComboBoxiProp.Text, TxtBoxiPropVal.Text);
                //Aktualizuj ComBoxa
                GetiPropertyName();
                TxtBoxAktWartiProp.Clear();
            }
            else
            {
                MessageBox.Show("Brak nazwy zmiennej iProperty!", "Custom iProperty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //Przycisk dodaj
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Stworz nowa zmienna
                UpdateOrCreateCustomiProperty(InvDocument, ComboBoxiProp.Text, TxtBoxiPropVal.Text);
                //Aktualizuj ComBoxa
                GetiPropertyName();
                TxtBoxAktWartiProp.Clear();

            }
            catch
            {
                MessageBox.Show("Brak nazwy zmiennej iProperty!", "Custom iProperty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        public void GetiPropertyName()
        {
            string propertyName;
            try
            {
                //Wskaznik na aktywna instancje Inventora
                InvApplication = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
                //Wskaznik na aktywny dokument
                InvDocument = InvApplication.ActiveDocument;
                //Wskaznik na zbior PropertySet custom
                InvPropertySet = InvDocument.PropertySets["Inventor User Defined Properties"];
                //Wyczsc ComBoxa
                ComboBoxiProp.Items.Clear();
                //Wypelnij ComBoxa nazwami zmiennych iProperty ze zbioru PropertySet
                foreach (Inventor.Property propertyLoop in InvPropertySet)
                {
                    propertyName = propertyLoop.Name;
                    ComboBoxiProp.Items.Add(propertyName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void ComboBoxiPropClear()
        {
            ComboBoxiProp.Items.Clear();
        }
        #endregion

        private void TxtBoxAktWartiProp_TextChanged(object sender, EventArgs e)
        {
            object propertyValue;
            object ComboBoxiPropItem = ComboBoxiProp.SelectedItem;
            InvApplication = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
            InvDocument = InvApplication.ActiveDocument;
            InvPropertySet = InvDocument.PropertySets["Inventor User Defined Properties"];
            InvProperty = InvPropertySet[ComboBoxiPropItem];
            TxtBoxAktWartiProp.Clear();
            propertyValue = InvProperty.Value;
            TxtBoxAktWartiProp.Text = propertyValue.ToString();
        }
    }
}
