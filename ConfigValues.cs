using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
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

            //Init

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
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("FontColor", "String,");
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("BackgroundImage", "FileOutput,");



        }

        public void LoadConfigSettings()
        {
            //Setup our data parser
            var Parser = new FileIniDataParser();
            Data = Parser.ReadFile("Kuwagata.ini");

            if (File.ReadAllLines("Kuwagata.ini").Length == 0) //if our file is empty
            {
                Parser.WriteFile("Kuwagata.ini", DefaultData); //write the default data to it
                return; //and do not consult the file further
            }
            //TODO HERE: Ask KuwagataSettings.cs to load found data to the UI so we have it when we pull it up

        }

        public void SaveToConfig(Dictionary<String, dynamic> ValuesToSave) //Here we go.
        {
           

            foreach(KeyValuePair<string, dynamic> kvp in ValuesToSave)
            {
                string[] directory = kvp.Key.Split('/'); // So I built this like Category/Value, so we're going to use it like that.
                KeyData newKeyData = new KeyData(directory[1]);
                switch(Type.GetTypeCode(kvp.Value.GetType())) //Case/Switch cases only accept constant values, so here's what we're gonna do:
                {
                    case TypeCode.String: //Write strings for strings, bools for bools, and so on, and so forth.
                        newKeyData.Value = "String," + kvp.Value;
                        break;
                    case TypeCode.Boolean:
                        newKeyData.Value = "Bool," + kvp.Value.ToString();
                        break;
                    default:
                        break;
                }
                Data[directory[0]].SetKeyData(newKeyData);
            }
            FileIniDataParser parser = new FileIniDataParser();
            parser.WriteFile("@Kuwagata.ini", Data);
        }
    }
}
