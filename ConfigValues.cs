using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
namespace Kuwagata
{
    public class ConfigValues
    {

        //Config values:
        public string VerseOutput = @"C:\Users\bolum\Desktop\VerseScraper\VerseToDisplay.txt";
        public string VersionOutput = @"C:\Users\bolum\Desktop\VerseScraper\VerseVersion.txt";
        public string ExecDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public IniData SettingsFile;

        public ConfigValues()
        {
            var Parser = new FileIniDataParser();
            if (!File.Exists("Kuwagata.ini"))
            {
                File.Create("Kuwagata.ini");
            }
            SettingsFile = Parser.ReadFile("Kuwagata.ini");
        }

        void SetupBlankConfig()
        {

        }

      /** public void LoadAllConfigs()
        {
            PropertyInfo[] propInfo = this.GetType().GetProperties();
            foreach(PropertyInfo prop in propInfo)
            {
                if (prop.Name == "ExecDirectory" || prop.Name == "SettingsFile") { continue; }
                prop.SetValue(this, SettingsFile.Read(prop.Name));
            }

        } **/
    
        public void SaveToConfig(Form SettingsForm)
        {

        }
    }
}
