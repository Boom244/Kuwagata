using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
/*In the interest of knowing what I'm doing when I come back to this project after a break of any length,
* I have decided to add comments that accurately describe the control flow of this program.
*/


namespace Kuwagata
{
    class Program
    {

        
        public static OSISReader osisReader;
        public static ConfigValues cv;
        public static string[] verses;
        public static string[] plainVerseReferences;
        public static int[] verseIds;
        public static List<Form> activeForms;
        public static int currentIndex;
        public static KuwagataMainWindow MainWindow;
        public static VerseHolder verseHolder;
    

        [STAThread] //Program does NOT like it when you try to open file dialogs without this.
        public static void Main(string[] args)
        {
            //Initialize all configuration values
            cv = new ConfigValues();
            cv.LoadConfigSettings();

            //Create an OSISReader object to give you verses from the requests you put in.
            osisReader = new OSISReader(cv.ExecDirectory + @"\OSISBibles\kjv\verses.json");

            //Create a list of active forms that can be hidden when I send the program to the tray
            //also the verse thingy as well
            activeForms = new List<Form>();
            verseHolder = new VerseHolder();
            //Finally, run the main window.

            MainWindow = new KuwagataMainWindow();
            Application.EnableVisualStyles(); //For use of CTRL-A, CTRL-X, etc....
            Application.Run(MainWindow);

            activeForms.Add(MainWindow);
            activeForms.Add(verseHolder);



        }

        public static void StartNewRequest(string Verse, string Version)
        {
            //Reset the current index
            currentIndex = 0;

            //If the version is different than the preload, swap over.
            if (Version != osisReader.Version)
            {
                osisReader = new OSISReader(cv.ExecDirectory + @"\OSISBibles\" + Version.ToLower() + @"\verses.json"); ;
            }

            //Give the user their verses.
            verseIds = osisReader.GetReferencesFromString(Verse, false);
            verses = osisReader.GetVersesFromReferences(verseIds);

            plainVerseReferences = osisReader.batchDecodeAllReferences(verseIds);

            File.WriteAllText(cv.VerseOutput, verses[currentIndex]);
            File.WriteAllText(cv.VersionOutput, plainVerseReferences[currentIndex] + ", " + osisReader.Version.ToUpper());

        }

        public static void TransformQueue(bool way)
        {
            if (verses == null) { return; }

            //True is for forward, False is for backward.
            //this could be a nested ternary, but it's not for the sake of readability.
            if (way)
            {
                if (currentIndex == verses.Length - 1) { return; }
                currentIndex++;
            }
            else
            {
                if (currentIndex == 0) { return; }
                currentIndex--;
            }
            File.WriteAllText(cv.VerseOutput, verses[currentIndex]);
            File.WriteAllText(cv.VersionOutput, plainVerseReferences[currentIndex] + "," + osisReader.Version.ToUpper());
        }
    }
}
