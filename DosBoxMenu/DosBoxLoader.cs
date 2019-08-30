// *********************************************************************************
// Title:		DosBoxLoader.cs
// Description:	Clase que contiene los métodos con la funcionalidad de los menus de
//              DosBox. 
// *********************************************************************************

using System;
using System.Collections;
using System.Text;
using System.IO;

namespace DosBoxMenu3
{
    class DosBoxLoader
    {
        #region Atributos

        // Ruta de DOSBOX
        protected static string DEFAULT_PATH = "C:\\Program Files (x86)\\DOSBox-0.74\\dosbox.exe";
        protected static string PATH_FILE = "C:\\dosboxpath.txt";

        protected string m_sDosBoxPath;

        public string m_sFileName;

        #endregion

        #region Metodos

        //*******************************************************************************
        // Loads the path from a specific file
        //*******************************************************************************
        private void LoadDosBoxPath()
        {
            // If path config exists, load it
            if(File.Exists(PATH_FILE))
            {
                FileStream fs = new FileStream(PATH_FILE, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                
                // Obtenemos y almacenamos ruta
                String sDBPath = sr.ReadLine();
                if (sDBPath != "")
                    SetDosBoxPath(sDBPath);

                // Cerramos
                sr.Close();
                fs.Close();
            }
            else
            {
                SetDosBoxPath(DEFAULT_PATH);
            }
        }

        //*******************************************************************************
        // Sets the path to the DosBox executable
        //*******************************************************************************
        public void SetDosBoxPath(string sPath)
        {
            m_sDosBoxPath = sPath;
        }

        //*******************************************************************************
        // Ejecuta un archivo en DosBox
        //*******************************************************************************
        public void RunInDosBox(bool fullscreen)
        {
            LoadDosBoxPath();

            string sCurrFolder = Path.GetDirectoryName(m_sFileName);

            // Comando a ejecutar
            //System.Windows.Forms.MessageBox.Show(m_sDosBoxPath + GetDosBoxArguments(m_sFileName), "Hola");
            System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo();
            procInfo.FileName = m_sDosBoxPath;
            procInfo.Arguments = GetDosBoxArguments(m_sFileName, fullscreen);
            procInfo.WorkingDirectory = sCurrFolder;
            System.Diagnostics.Process.Start(procInfo);
        }

        //*******************************************************************************
        // Crea un archivo bat con comandos de DosBox
        //*******************************************************************************
        public void CreateBatFile(bool bFullscreen)
        {
            LoadDosBoxPath();

            // Obtenemos datos del archivo y comando
            string sFilenameOnly = Path.GetFileNameWithoutExtension(m_sFileName);
            string sCurrFolder = Path.GetDirectoryName(m_sFileName);
            string sCommand = GetDosBoxCommand(m_sFileName, bFullscreen);

            // Escribimos archivo
            String sSuffix = "_db";
            if (bFullscreen)
                sSuffix += "f";
            sSuffix += ".bat";
            FileStream fs = new FileStream(sCurrFolder + "\\" + sFilenameOnly + sSuffix, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(sCommand);

            // Cerramos
            sw.Close();
            fs.Close();
        }

        //*******************************************************************************
        // Arma comando de dosbox
        //*******************************************************************************
        private string GetDosBoxCommand(string sFilePath, bool bFullscreen)
        {
            // Comando de DOSBOX
            string sCommand = "\"" + m_sDosBoxPath + "\" ";
            sCommand += GetDosBoxArguments(sFilePath, bFullscreen);
            return sCommand;
        }

        //*******************************************************************************
        // Arma parametros de dosbox
        //*******************************************************************************
        private string GetDosBoxArguments(string sFilePath, bool bFullscreen)
        {
            // Nombre del archivo
            string sFilename = Path.GetFileName(sFilePath);
            // string sCurrDrive = Path.GetPathRoot(sFilePath);
            // string sCurrPath = Helpers.ToShortPathName(Path.GetDirectoryName(sFilePath));
            // string sFolder = Helpers.ToShortPathName(sCurrPath.Substring(Path.GetPathRoot(sCurrPath).Length));

            // Retornamos argumentos
            string sArguments = "";
            if (bFullscreen)
                sArguments += " -fullscreen";
            sArguments += " -c \"mount c .\" -c \"c: \" -c \"" + sFilename + " \"";
            return sArguments;
        }

        #endregion
    }
}
