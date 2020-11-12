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
        //Definiuje nowa liste
        List<string> PropertyNameList = new List<string>();
        //Konstruktor klasy inicjalizujacy WinFormsa
        public CmbBoxiProp()
        {
            InitializeComponent();
        }
        #region WinForm Elemens Methods
        //Przycisk aktualizuj
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            InvApplication = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
            InvDocument = InvApplication.ActiveDocument;
            //Jezeli wybrano iProperty
            if (String.IsNullOrWhiteSpace(ComboBoxiProp.Text) == false)
            {
                //Aktualizuj jej wartosc
                UpdateOrCreateCustomiProperty(InvDocument, ComboBoxiProp.Text, TxtBoxiPropVal.Text);
            }
            else
            {
                MessageBox.Show("Brak nazwy zmiennej iProperty!", "Custom iProperty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //Czysc TextBoxa
            TxtBoxiPropVal.Clear();
            //Ustaw nowa wartosc pola Aktualna Wartosc
            TxtBoxAktWartFill();
        }
        //Przycisk dodaj
        private void AddButton_Click(object sender, EventArgs e)
        {
            AddiProperty addiProperty = new AddiProperty();
            addiProperty.ShowDialog();
        }
        private void ComboBoxiProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtBoxAktWartFill();
        }
        private void ComboBoxiProp_Click(object sender, EventArgs e)
        {
            ComboBoxiPropFill();
        }
        #endregion
        #region OtherMethods
        public void UpdateOrCreateCustomiProperty(Inventor.Document Document, string PropertyName, string PropertyValue)
        {
            InvDocument = InvApplication.ActiveDocument;
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
                MessageBox.Show(ex.Message);
            }
        }
        public void GetiPropertyName()
        {
            InvApplication = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
            InvDocument = InvApplication.ActiveDocument;
            string propertyName;
            try
            {
                //Wskaznik na zbior PropertySet custom
                InvPropertySet = InvDocument.PropertySets["Inventor User Defined Properties"];
                PropertyNameList.Clear();
                //Wypelnij ComBoxa nazwami zmiennych iProperty ze zbioru PropertySet
                foreach (Inventor.Property propertyLoop in InvPropertySet)
                {
                    propertyName = propertyLoop.Name;
                    PropertyNameList.Add(propertyName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ComboBoxiPropFill()
        {
            ComboBoxiProp.Items.Clear();
            GetiPropertyName();
            foreach(string element in PropertyNameList)
            {
                ComboBoxiProp.Items.Add(element);
            }
        }
        public void TxtBoxAktWartFill()
        {
            try
            {
                TxtBoxAktWartiProp.Clear();
                //Wskaznik na zbior PropertySet custom
                InvPropertySet = InvDocument.PropertySets["Inventor User Defined Properties"];
                foreach (Inventor.Property propertyLoop in InvPropertySet)
                {
                    if (ComboBoxiProp.SelectedItem.ToString() == propertyLoop.Name)
                    {
                        TxtBoxAktWartiProp.Text = propertyLoop.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
