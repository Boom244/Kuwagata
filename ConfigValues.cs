using IniParser;
using IniParser.Model;
using System;
using System.IO;
using System.Windows.Forms;
namespace Kuwagata
{
    public class ConfigValues
    {

        //Config values:
        public string VerseOutput = @"C:\Users\bolum\Desktop\VerseScraper\VerseToDisplay.txt";
        public string VersionOutput = @"C:\Users\bolum\Desktop\VerseScraper\VerseVersion.txt";
        public string ExecDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        public IniData DefaultData;

        public IniData Data { get; set; }

        public ConfigValues()
        {

            if (!File.Exists("Kuwagata.ini"))
            {
                File.Create("Kuwagata.ini");
            }

            //Okay, I don't like this, especially not in the constructor.
            //I'm going to either move this to another file or do it another way. Eventually.

            DefaultData = new IniData();

            //Sections:
            DefaultData.Sections.AddSection("Output");
            DefaultData.Sections.AddSection("VerseConfig");
            DefaultData.Sections.AddSection("KuwagataDiscreteWindowOutput");

            //Keys:
            DefaultData["Output"].AddKey("VerseOutput", "FileOutput,");
            DefaultData["Output"].AddKey("VersionOutput", "FileOutput,");
            DefaultData["VerseConfig"].AddKey("DefaultLoadedVersion", "String,");
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("Fullscreen", "Bool,false");
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("Font", "String,");
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("FontSize", "String,");
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("FontColor", "ColorWheel,");
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("BackgroundImage", "FileOutput,");



        }

        public void LoadConfigSettings()
        {
            //Setup our data parser
            var Parser = new FileIniDataParser();
            Data = Parser.ReadFile("Kuwagata.ini");

            if (File.ReadAllLines(@"Kuwagata.ini").Length == 0) //if our file is empty
            {
                Parser.WriteFile(@"Kuwagata.ini", DefaultData); //write the default data to it
                return; //and do not consult the file further
            }


        }

        public void SaveToConfig(Form SettingsForm)
        {

        }
    }
}
