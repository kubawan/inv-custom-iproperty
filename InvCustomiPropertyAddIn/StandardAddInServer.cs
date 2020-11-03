using System;
using System.Runtime.InteropServices;
using Inventor;
using Microsoft.Win32;
using System.Windows.Forms;
using stdole;

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

        // Inventor application object.
        private Inventor.Application m_inventorApplication;
        private Inventor.ApplicationEvents m_appEventsSave;
        private Inventor.Document invDocument;
        private Inventor.PropertySet invPropertySet;
        private Inventor.Property invProperty;
        private Inventor.DocumentTypeEnum invDocumentTypePart;
        private Inventor.DocumentTypeEnum invDocumentTypeAssembly;
        private Inventor.ButtonDefinition m_buttonDef;
        private Inventor.UserInterfaceEvents m_uiEvents;
        private string m_ClientID = "{311a4c02-49df-4947-a01c-47765ec06b27}";


        //Events handler delegates
        private Inventor.DocumentEventsSink_OnSaveEventHandler DocumentEventsSink_OnSaveEventHandlerDelegate;
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
                // Initialize AddIn members.
                m_inventorApplication = addInSiteObject.Application;

                //Initialize save event delegate
                m_appEventsSave = m_inventorApplication.ApplicationEvents;
                m_appEventsSave.OnSaveDocument += new ApplicationEventsSink_OnSaveDocumentEventHandler(ApplicationEvents_OnSaveDocument);

                //Get a reference to the UserInterfaceManager object
                Inventor.UserInterfaceManager UIManager = m_inventorApplication.UserInterfaceManager;

                //Get a reference to the ControlDefinitions object
                Inventor.ControlDefinitions contorlDefs = m_inventorApplication.CommandManager.ControlDefinitions;

                //Create the button definition
                m_buttonDef = contorlDefs.AddButtonDefinition("One", "UIRibbonSampleOne", CommandTypesEnum.kNonShapeEditCmdType, m_ClientID);

                //Call the function to add information to the userinterface
                if(firstTime == true)
                {
                    CreateUserInterface();
                }

                //Connect to UI events to be able to handle UI reset
                m_uiEvents = m_inventorApplication.UserInterfaceManager.UserInterfaceEvents;


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
            m_inventorApplication = null;

            //Deactivate save event delegate
            m_appEventsSave.OnSaveDocument -= new ApplicationEventsSink_OnSaveDocumentEventHandler(ApplicationEvents_OnSaveDocument);
            m_appEventsSave = null;

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
        //Create AddIn UI ribbon method
        private void CreateUserInterface()
        {
            //Get a reference to the UserManagerInterface object
            Inventor.UserInterfaceManager UIManager = m_inventorApplication.UserInterfaceManager;

            //Get the zero doc ribbon
            Inventor.Ribbon zeroRibbon = UIManager.Ribbons["ZeroDoc"];

            //Get the getting started tab
            Inventor.RibbonTab startedTab = zeroRibbon.RibbonTabs["id_GetStarted"];

            //Get the new feature panel
            Inventor.RibbonPanel newFeaturePanel = startedTab.RibbonPanels["id_Panel_GetStartedWhatsNew"];

            //Add a button to the panel, using previously created button def
            newFeaturePanel.CommandControls.AddButton(m_buttonDef, true);
        }

        //Save event method
        private void ApplicationEvents_OnSaveDocument(_Document DocumentObject, EventTimingEnum BeforeOrAfter, NameValueMap Context, out HandlingCodeEnum HandlingCode)
        {
            HandlingCode = HandlingCodeEnum.kEventNotHandled;
            if (BeforeOrAfter != EventTimingEnum.kAfter)
            {
                return;
            }

            //Acces part or assembly doc
            invDocumentTypePart = DocumentTypeEnum.kPartDocumentObject;
            if (m_inventorApplication.ActiveDocumentType == invDocumentTypePart)
            {
                //MessageBox.Show("Wtyczka weszla w plik typu part.", "Wtyczka - cutom iProperties", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Create custom iPoperty
                TestiPropertyUpdate();
            }
            invDocumentTypeAssembly = DocumentTypeEnum.kAssemblyDocumentObject;
            if (m_inventorApplication.ActiveDocumentType == invDocumentTypeAssembly)
            {
                //MessageBox.Show("Wtyczka weszla w plik typu assembly.", "Wtyczka - cutom iProperties", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Create custom iPoperty
                TestiPropertyUpdate();
            }

            HandlingCode = HandlingCodeEnum.kEventHandled;
        }

        //Add custom iProperty methods
        private void TestiPropertyUpdate()
        {
            //Get active document
            invDocument = m_inventorApplication.ActiveDocument;
            //MessageBox.Show("Wywolano TestiPropertyUpdate.", "Wtyczka - custom iProperty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateCustomiProperties(invDocument, "Malowanie", "");
            UpdateCustomiProperties(invDocument, "Obróbka plastyczna", "");
            UpdateCustomiProperties(invDocument, "Obróbka skrawaniem", "");
            UpdateCustomiProperties(invDocument, "Obróka powierzchniowa", "");
        }
        public void UpdateCustomiProperties(Inventor.Document Document, string PropertyName, string PropertyValue)
        {
            //MessageBox.Show("Wywolano UpdateCustomiProperties", "Wtyczka - custom iProperty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                //Get custom property set
                invPropertySet = invDocument.PropertySets["Inventor User Defined Properties"];

                //Get existing property if exsist
                invProperty = null;
                Boolean propertyExists = true;
                try
                {
                    invProperty = invPropertySet[PropertyName];
                }
                catch (Exception ex)
                {
                    propertyExists = false;
                }

                //Check to see if the property was obtained succesfully
                if (!propertyExists)
                {
                    //Failed to get the exsisting property so create new one
                    invProperty = invPropertySet.Add(PropertyValue, PropertyName, null);
                }
                else
                {
                    invProperty.Value = PropertyValue;
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
