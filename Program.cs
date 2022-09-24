using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*In the interest of knowing what I'm doing when I come back to this project after a break of any length,
* I have decided to litter this entire solution with extremely specific comments.
* 
* Also, side note: Having the forms directly reference Program.cs for the vital functions of the program is probably
* not a good thing. Revisit later.
*/


namespace Kuwagata
{
    class Program
    {

        //I get the strange feeling I'm doing something wrong here....
        public static OSISReader osisReader;
        public static ConfigValues cv;
        public static string[] verses;
        public static string[] plainVerseReferences;
        public static int[] verseIds;
        public static List<Form> activeForms;
        public static int currentIndex;
        public static KuwagataMainWindow MainWindow;
        //Must be the wind.

        [STAThread] //prevent C# from freaking out when I open a file dialog
        public static void Main(string[] args)
        {
            //Initialize all configuration values
            cv = new ConfigValues();
            cv.LoadConfigSettings();

            //Create an OSISReader object to give you verses from the requests you put in.
            osisReader = new OSISReader(cv.ExecDirectory + @"\OSISBibles\kjv\verses.json");

            //Create a list of active forms that can be hidden when I send the program to the tray
            activeForms = new List<Form>();

            //Finally, run the main window.

            MainWindow = new KuwagataMainWindow();
            Application.EnableVisualStyles(); //For use of CTRL-A, CTRL-X, etc....
            Application.Run(MainWindow);

            activeForms.Add(MainWindow);
             
            
            
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
            //Oh, and an addendum; give them their normalized verse references.
            plainVerseReferences = osisReader.batchDecodeAllReferences(verseIds);

            File.WriteAllText(cv.VerseOutput, verses[currentIndex]);
            File.WriteAllText(cv.VersionOutput, plainVerseReferences[currentIndex] + ", " + osisReader.Version.ToUpper());

        }

        public static void TransformQueue(bool way)
        {
            if (verses == null ) { return; }
                
            //True is for forward, False is for backward.
            if (way)
            {
                if (currentIndex == verses.Length - 1) { return;  }
                currentIndex++;
            }
            else
            {
                if(currentIndex == 0) { return; }   
                currentIndex--;
            }
            File.WriteAllText(cv.VerseOutput, verses[currentIndex]);
            File.WriteAllText(cv.VersionOutput, plainVerseReferences[currentIndex] + "," + osisReader.Version.ToUpper());
        }
    }
}
