using System;
using System.Runtime.InteropServices;
using Inventor;
using Microsoft.Win32;
using System.Windows.Forms;
using stdole;
using System.Drawing;
using InvAddIn;
using InvAddIn.Properties;

namespace InvCustomiPropertyAddIn
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("889caee5-3c6a-436f-b6a6-07731ed72ac3")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {
        //Obiekt aplikacji inventor.
        private Inventor.Application InvApplication;
        //private Inventor.ApplicationEvents InvAppEventsSave;
        private Inventor.ApplicationEvents PartDocCreate;
        private Inventor.Document InvDocument;
        private Inventor.PropertySet InvPropertySet;
        private Inventor.Property InvProperty;
        private Inventor.DocumentTypeEnum InvDocumentTypePart;
        private Inventor.DocumentTypeEnum InvDocumentTypeAssembly;
        private Inventor.ButtonDefinition AddiPropertyButtonDef;
        private Inventor.UserInterfaceEvents InvUiEvents;
        private readonly string m_ClientID = "{311a4c02-49df-4947-a01c-47765ec06b27}";
        //Utworz isntancje klasy WinFormsa
        private readonly CmbBoxiProp cmbBoxiProp = new CmbBoxiProp();
        //Ikony przycisku
        private readonly stdole.IPictureDisp  smallIconAddBtn = PictureConverter.ImageToPictureDisp(Resources._16_x_16);
        private readonly stdole.IPictureDisp largeIconAddBtn = PictureConverter.ImageToPictureDisp(Resources._32_x_32);

        //Delegaty wydarzen
        //private Inventor.DocumentEventsSink_OnSaveEventHandler DocumentEventsSink_OnSaveEventHandlerDelegate;
        //private Inventor.ApplicationEventsSink_OnNewDocumentEventHandler ApplicationEventsSink_OnNewDocumentEventHandler;
        //private Inventor.UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler;

        public StandardAddInServer()
        {
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.
            try
            {
                //Inicjalizuj wtyczkê
                InvApplication = addInSiteObject.Application;

                /*
                //Inicjalizuj delegate wydarzenia "Save"
                m_appEventsSave = InvApplication.ApplicationEvents;
                m_appEventsSave.OnSaveDocument += new ApplicationEventsSink_OnSaveDocumentEventHandler(ApplicationEvents_OnSaveDocument);
                */
                //Inicjalizuj delegate wydarzenia "NewDocument:
                PartDocCreate = InvApplication.ApplicationEvents;
                PartDocCreate.OnNewDocument += new ApplicationEventsSink_OnNewDocumentEventHandler(ApplicationEvents_OnNewDocument);

                //Ustaw wskaznik UIManager na UserInterfaceManager
                Inventor.UserInterfaceManager UIManager = InvApplication.UserInterfaceManager;

                //Ustaw wskaxnik controlDef na ControlDefinition
                Inventor.ControlDefinitions contorlDefs = InvApplication.CommandManager.ControlDefinitions;

                //Utworz definicje przycisku Add
                AddiPropertyButtonDef = contorlDefs.AddButtonDefinition("Add iProperty", "AddCustomiPropertiesButton", CommandTypesEnum.kFilePropertyEditCmdType, m_ClientID,"Dodaj now¹ zmienn¹ iProperty","Kliknij aby dodaæ now¹ zmienn¹ iProperty", smallIconAddBtn, largeIconAddBtn);

                //Wywolaj przy uruchomieniu wtyczki aby utworzyc UI wtyczki
                if (firstTime == true)
                {
                    CreateUserInterface();
                }

                //Inicjalizuj delegate wydarzenia "RibbonReset"
                InvUiEvents = InvApplication.UserInterfaceManager.UserInterfaceEvents;
                InvUiEvents.OnResetRibbonInterface += new UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler(InvUiEvents_OnResetRibbonInterface);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "AddIn Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // TODO: Add ApplicationAddInServer.Activate implementation.
            // e.g. event initialization, command creation etc.
        }
        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
            InvApplication = null;

            /*
            //Deaktywuj delegate "Save"
            m_appEventsSave.OnSaveDocument -= new ApplicationEventsSink_OnSaveDocumentEventHandler(ApplicationEvents_OnSaveDocument);
            m_appEventsSave = null;
            */

            //Deaktywuj delegate "NewDocument"
            PartDocCreate.OnNewDocument -= new ApplicationEventsSink_OnNewDocumentEventHandler(ApplicationEvents_OnNewDocument);
            PartDocCreate = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get
            {
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }

        #endregion
        #region Methods
        //Metoda tworzenia UI wtyczki
        private void CreateUserInterface()
        {
            //Ustaw wskaznik UIManager na UserInterfaceManager
            Inventor.UserInterfaceManager UIManager = InvApplication.UserInterfaceManager;

            //Ustaw wskaznik na Ribbon Part
            Inventor.Ribbon partRibbon = UIManager.Ribbons["Part"];

            //Ustaw wskaznik na RibbonTab Tools
            Inventor.RibbonTab toolsTab = partRibbon.RibbonTabs["id_TabTools"];

            //Utworz nowy panel o nazwie iPropery w RibbonTab Tools
            Inventor.RibbonPanel newFeaturePanel = toolsTab.RibbonPanels.Add("Custom iProperty", "ToolsTabCustomiProperty", m_ClientID, "id_PanelP_ToolsFind");

            //Utworz przycisk w nowym panelu wykorzystujac definicje przycisku
            newFeaturePanel.CommandControls.AddButton(AddiPropertyButtonDef, true);

            //Utworz delegate na wydarzenie Button Click
            AddiPropertyButtonDef.OnExecute += new ButtonDefinitionSink_OnExecuteEventHandler(AddiPropertyButtonDef_OnExecute);
        }
        //Metoda wywo³ywana klikniêciem na przycisk Add
        private void AddiPropertyButtonDef_OnExecute(NameValueMap Context)
        {
            //wywo³anie metody NewiPropertyItem
            NewiPropertyItem();
        }
        //Metoda wydarzenia NewDocument
        private void ApplicationEvents_OnNewDocument(_Document DocumentObject, EventTimingEnum BeforeOrAfter, NameValueMap Context, out HandlingCodeEnum HandlingCode)
        {
            HandlingCode = HandlingCodeEnum.kEventNotHandled;
            if (BeforeOrAfter != EventTimingEnum.kAfter)
            {
                return;
            }

            //Uzyskaj dostep do plikow typu Part 
            InvDocumentTypePart = DocumentTypeEnum.kPartDocumentObject;
            if (InvApplication.ActiveDocumentType == InvDocumentTypePart)
            {
                //Wywolaj metode CreateBasiciProperty
                CreateBasiciProperty();
            }
            //Uzyskaj dostep do plikow typu Assemblu
            InvDocumentTypeAssembly = DocumentTypeEnum.kAssemblyDocumentObject;
            if (InvApplication.ActiveDocumentType == InvDocumentTypeAssembly)
            {              
                //Wywolaj metode CreateBasiciProperty
                CreateBasiciProperty();
            }

            //W momencie utworzenia nowego dokumentu dodaj DockWindow
            CreateDockableWindow();

            //W momencie utworzenia nowego dokumentu wyczysc ComBoxa w DockWindow
            cmbBoxiProp.ComboBoxiPropClear();

            HandlingCode = HandlingCodeEnum.kEventHandled;
        }
        //Metoda wydarzenia RibbonReset
        private void InvUiEvents_OnResetRibbonInterface(NameValueMap Context)
        {
            //Utworz na nowo UserInterface
            CreateUserInterface();
        }
        //Metoda AddiPropertySetName zwraca string z nazwa zmiennej iProperty
        private string AddiPropertySetName()
        {
            //Popros uzytkownika o wprowadzenie nazwy nowej iProperty
            string getPropertyName = Microsoft.VisualBasic.Interaction.InputBox("WprowadŸ nazwê: ", "Custom iProperty", "Nazwa zmiennej iProperty");
            return getPropertyName;
        }
        //Metoda AddiPropertySetValue zwraca string z wartoscia zmiennej iProperty
        private string AddiPropertySetValue()
        {
            //Popros uzytkownika o wprowadzenie wartosci nowej iProperty
            string getPropertyValue = Microsoft.VisualBasic.Interaction.InputBox("WporwadŸ wartoœæ: ", "Custom iProperty", "Wartoœæ zmiennej iProperty");
            return getPropertyValue;
        }
        //Metoda NewiPropertyItem tworzy now¹ zmienn¹ iProperty
        public void NewiPropertyItem()
        {
            //Ustaw wskaznik na aktywny dokument
            InvDocument = InvApplication.ActiveDocument;
            //Wywoluje metode UpdateOrCreateCustomiProperty / dodaje nowa zmienna iProperty
            UpdateOrCreateCustomiProperty(InvDocument, AddiPropertySetName(), AddiPropertySetValue());
        }
        //Metoda tworzy cztery podstawowe iProperties
        private void CreateBasiciProperty()
        {
            //Ustaw wskaznik na aktywny dokument
            InvDocument = InvApplication.ActiveDocument;
            //Tworzy cztery podstawowe iProperties wywolujac funkcje UpdateOrCreateCustomiProperty
            UpdateOrCreateCustomiProperty(InvDocument, "Malowanie", "");
            UpdateOrCreateCustomiProperty(InvDocument, "Obróbka plastyczna", "");
            UpdateOrCreateCustomiProperty(InvDocument, "Obróbka skrawaniem", "");
            UpdateOrCreateCustomiProperty(InvDocument, "Obróka powierzchniowa", "");
        }
        //Metoda sprawdza czy istnieje i dodaje lub aktualizuje zmienna iProperty
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
        //Metoda tworzy DockWindow
        public void CreateDockableWindow()
        {
            //Ustawia wskaznik na UserInterfaceManager
            Inventor.UserInterfaceManager uiManager = InvApplication.UserInterfaceManager;
            //Dodaje nowe DockWindow o nazwie iProperty
            Inventor.DockableWindow dockWindow = uiManager.DockableWindows.Add(m_ClientID, "CustomiProperty", "iProperty");
            //Prametry DockWindow
            //Nie widoczne przy starcie
            dockWindow.Visible = false;
            //Nie mozna zadokowac przy gornej i dolnej krawedzi
            dockWindow.DisabledDockingStates = DockingStateEnum.kDockTop;
            dockWindow.DisabledDockingStates = DockingStateEnum.kDockBottom;
            //Tworzy instancje klasy z WinForm
            InvAddIn.CmbBoxiProp FrmComboBox = new InvAddIn.CmbBoxiProp();
            //Dodaje do DockWindow WinForma
            dockWindow.AddChild(FrmComboBox.Handle);
            //Wyswietla utworzonego WinForma
            FrmComboBox.Show();
            //Wywolaj metode GetiPropertyName z WinFormsa, pierwsze wypelnienie ComBoxa tym co jest w pliku na dzien dobry
            cmbBoxiProp.GetiPropertyName();
        }
        #endregion
    }
}
