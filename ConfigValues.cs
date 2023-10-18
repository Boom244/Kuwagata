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
        
        public string ExecDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public string VerseOutput;
        public string VersionOutput;
        private FileIniDataParser Parser;
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
            VerseOutput = ExecDirectory + @"\VerseToDisplay.txt";
            VersionOutput = ExecDirectory + @"\VerseVersion.txt";

            //Sections:
            DefaultData.Sections.AddSection("Output");
            DefaultData.Sections.AddSection("VerseConfig");
            DefaultData.Sections.AddSection("KuwagataDiscreteWindowOutput");

            //Keys:
            DefaultData["Output"].AddKey("VerseOutput", "FileOutput," + VerseOutput);
            DefaultData["Output"].AddKey("VersionOutput", "FileOutput," + VersionOutput);
            DefaultData["VerseConfig"].AddKey("DefaultLoadedVersion", "String,KJV");
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("Fullscreen", "Bool,false");
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("Font", "String,");
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("FontSize", "String,");
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("FontColor", "String,");
            DefaultData["KuwagataDiscreteWindowOutput"].AddKey("BackgroundImage", "FileOutput,");

        }

        public void LoadConfigSettings()
        {
            //Setup our data parser
            Parser = new FileIniDataParser();
            try
            {
                Data = Parser.ReadFile("Kuwagata.ini");
            } catch // If the file does indeed not exist
            {
                File.Create("Kuwagata.ini");
                Data = Parser.ReadFile("Kuwagata.ini");
            }

            if (File.ReadAllLines("Kuwagata.ini").Length == 0) //if our file is empty
            {
                Parser.WriteFile("Kuwagata.ini", DefaultData); //write the default data to it
                Data = DefaultData; //set that to our working data
                return; //and do not consult the file further
            }
            VerseOutput = Data["Output"]["VerseOutput"].Split(',')[1]; //set these values because we'll be using them
            VersionOutput = Data["Output"]["VersionOutput"].Split(',')[1];
            VerifyOutputExistence();
        }

        public void VerifyOutputExistence()
        {
            //Ensure the files that we're going to write to throughout the lifetime of the program exist
            if (!File.Exists(VerseOutput))
            {
                File.Create(VerseOutput);
            }
            if (!File.Exists(VersionOutput))
            {
                File.Create(VersionOutput);
            }
        }

        public void SaveToConfig(Dictionary<String, dynamic> ValuesToSave) //Here we go.
        {
           

            foreach(KeyValuePair<string, dynamic> kvp in ValuesToSave)
            {
                string[] directory = kvp.Key.Split('/'); // So I built this like Category/Value, so we're going to use it like that.
                KeyData newKeyData = new KeyData(directory[1]);
                switch(Type.GetTypeCode(kvp.Value.GetType())) //Case/Switch only accepts constant values, so here's what we're gonna do:
                {
                    case TypeCode.String: //Write strings for strings, bools for bools, and so on, and so forth.
                        newKeyData.Value = (kvp.Key.Contains("Output") ? "FileOutput," : "String,") + kvp.Value;
                        break;
                    case TypeCode.Boolean:
                        newKeyData.Value = "Bool," + kvp.Value.ToString();
                        break;
                    default:
                        break;
                }
                Data[directory[0]].SetKeyData(newKeyData);
            }
            Parser.WriteFile("Kuwagata.ini", Data);
            VerifyOutputExistence(); //Also want to do this when we save new data
        }
    }
}
