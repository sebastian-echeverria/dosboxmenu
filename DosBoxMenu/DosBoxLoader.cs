// *********************************************************************************
// Title:		DosBoxLoader.cs
// Description:	Clase que contiene los m�todos con la funcionalidad de los menus de
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
        protected string m_sDosBoxPath = "C:\\Program Files (x86)\\DOSBox-0.74\\dosbox.exe";
        protected string m_sDosBoxPathFile = "C:\\dosboxpath.txt";

        public string m_sFileName;
        public string m_sFolder;

        #endregion

        #region Metodos

        //*******************************************************************************
        // Loads the path from a specific file
        //*******************************************************************************
        public void loadDosBoxPath()
        {
            // If path config exists, load it
            if(File.Exists(m_sDosBoxPathFile))
            {
                FileStream fs = new FileStream(m_sDosBoxPathFile, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                
                // Obtenemos y almacenamos ruta
                String sDBPath = sr.ReadLine();
                if (sDBPath != "")
                    m_sDosBoxPath = sDBPath;

                // Cerramos
                sr.Close();
                fs.Close();
            }
        }

        //*******************************************************************************
        // Sets the path to the DosBox executable
        //*******************************************************************************
        public void setDosBoxPath(string sPath)
        {
            m_sDosBoxPath = sPath;
        }

        //*******************************************************************************
        // Ejecuta un archivo en DosBox
        //*******************************************************************************
        public void RunInDosBox(bool fullscreen)
        {
            loadDosBoxPath();

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
            loadDosBoxPath();

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
