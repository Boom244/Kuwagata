using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace Kuwagata
{
    public class ConfigValues
    {

        //Config values:
        public string VerseOutput = @"C:\Users\bolum\Desktop\VerseScraper\VerseToDisplay.txt";
        public string VersionOutput = @"C:\Users\bolum\Desktop\VerseScraper\VerseVersion.txt";
        public string ExecDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public IniFile SettingsFile;

        public ConfigValues()
        {
             SettingsFile = new IniFile(ExecDirectory + "Kuwagata.ini");
        }

       public void LoadAllConfigs()
        {
            PropertyInfo[] propInfo = this.GetType().GetProperties();
            foreach(PropertyInfo prop in propInfo)
            {
                if (prop.Name == "ExecDirectory" || prop.Name == "SettingsFile") { continue; }
                prop.SetValue(this, SettingsFile.Read(prop.Name));
            }

        }
    
        public void SaveToConfig(Form SettingsForm)
        {

        }
    }
}
