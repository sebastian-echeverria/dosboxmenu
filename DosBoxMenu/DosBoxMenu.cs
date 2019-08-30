// *********************************************************************************
// Title:		DosBoxMenu.cs
// Description:	Clase que implementa un menu de boton derecho de Windows, en este caso,
//              de comandos de DosBox.
//              Deriva de BaseContextMenu, que viene en DLL Utils.ShellExtensions.ContextMenu.dll
//              en .NET, que DEBE estar registrado en sistema
// *********************************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Utils.ShellContextMenu;

namespace DosBoxMenu3
{
    [ComVisible(true), Guid("0056DA96-FFD6-4180-BAB2-8C9B6F552B2D")]
    public class DosBoxMenu : BaseContextMenu
    {
        private DosBoxLoader dbLoader;

        //*******************************************************************************
        // Register with a file type
        //*******************************************************************************
        [ComRegisterFunction]
        public static void RegisterFunction(Type t)
        {
            //Register for files
            RegisterContextMenu.Register("*", t.GUID);
            //RegisterContextMenu.Register("bat", t.GUID);
        }

        //*******************************************************************************
        // UnRegister with a file type
        //*******************************************************************************
        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {
            RegisterContextMenu.Unregister("*", t.GUID);
            //RegisterContextMenu.Unregister("bat", t.GUID);
        }

        //*******************************************************************************
        // Builds the menu itself
        //*******************************************************************************
        public override void AssembleMenu()
        {
            try
            {
                //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(@"c:\test.bmp");
                //System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(@"c:\test1.bmp");

                // Create menu entries
                MenuItems menuItems0 = new MenuItems("DosBox", false);
                MenuItems menuItems1 = new MenuItems("Run on DosBox", false);
                MenuItems menuItems2 = new MenuItems("Run on DosBox fullscreen", false);
                MenuItems separator = new MenuItems("", true);
                MenuItems menuItems3 = new MenuItems("Create DosBox batch file", false);
                MenuItems menuItems4 = new MenuItems("Create DosBox batch file (fullscreen)", false);

                // Associate to methods
                menuItems1.Click += new Utils.ShellContextMenu.MenuItems.MenuClickHandler(EjecutaEnDosBox);
                menuItems2.Click += new Utils.ShellContextMenu.MenuItems.MenuClickHandler(EjecutaEnDosBoxFS);
                menuItems3.Click += new Utils.ShellContextMenu.MenuItems.MenuClickHandler(CrearArchivoBat);
                menuItems4.Click += new Utils.ShellContextMenu.MenuItems.MenuClickHandler(CrearArchivoBatFS);

                // Add menus to main context menu
                // This order is important. Always insert the parent container and then the child!
                InsertMenu(menuItems0);
                AddMenu(menuItems0, menuItems1);
                AddMenu(menuItems0, menuItems2);
                AddMenu(menuItems0, separator);
                AddMenu(menuItems0, menuItems3);
                AddMenu(menuItems0, menuItems4);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + e.Message);
            }
        }

        //*******************************************************************************
        // Runs the command to execute file
        //*******************************************************************************
        private void EjecutaEnDosBox()
        {
            dbLoader = new DosBoxLoader();
            dbLoader.m_sFileName = this.Filename;
            dbLoader.EjecutaEnDosBox();
        }

        //*******************************************************************************
        // Runs the command to execute file
        //*******************************************************************************
        private void EjecutaEnDosBoxFS()
        {
            dbLoader = new DosBoxLoader();
            dbLoader.m_sFileName = this.Filename;
            dbLoader.EjecutaEnDosBoxFullScreen();
        }

        //*******************************************************************************
        // Runs the command to create batch file
        //*******************************************************************************
        private void CrearArchivoBat()
        {
            dbLoader = new DosBoxLoader();
            dbLoader.m_sFileName = this.Filename;
            dbLoader.CrearArchivoBat();
        }

        //*******************************************************************************
        // Runs the command to create batch file
        //*******************************************************************************
        private void CrearArchivoBatFS()
        {
            dbLoader = new DosBoxLoader();
            dbLoader.m_sFileName = this.Filename;
            dbLoader.CrearArchivoBatFullscreen();
        }


    }
}
