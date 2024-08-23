using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Kuwa = KuwagataDLL.Main;
using System.Runtime.InteropServices;
/*In the interest of knowing what I'm doing when I come back to this project after a break of any length,
* I have decided to add comments that accurately describe the control flow of this program.
*/


namespace Kuwagata
{
    class Program
    {

        
        public static ConfigValues cv;
        public static List<Form> activeForms;
        public static int currentIndex;
        public static KuwagataMainWindow MainWindow;
        

        [STAThread] //Program does NOT like it when you try to open file dialogs without this.
        public static void Main(string[] args)
        {
            //Initialize all configuration values
            cv = new ConfigValues();
            cv.LoadConfigSettings();

            //Create an OSISReader object to give you verses from the requests you put in.
      
            Kuwa.Initialize(cv.ExecDirectory + @"\OSISBibles\kjv\verses.json");
            //Create a list of active forms that can be hidden when I send the program to the tray
            //also the verse thingy as well
            activeForms = new List<Form>();
            
            //Finally, run the main window.

            MainWindow = new KuwagataMainWindow();
            activeForms.Add(MainWindow);
            System.Windows.Forms.Application.EnableVisualStyles(); //For use of CTRL-A, CTRL-X, etc....
            System.Windows.Forms.Application.Run(MainWindow);

            

        }

        public static void StartNewRequest(string Verse, string Version)
        {
            //Reset the current index
            currentIndex = 0;

            //If the version is different than the preload, swap over.
            if (Version != Kuwa.osisReader.Version)
            {
                // osisReader = new OSISReader(cv.ExecDirectory + @"\OSISBibles\" + Version.ToLower() + @"\verses.json");
                Kuwa.ChangeOSISPath(cv.ExecDirectory + @"\OSISBibles\" + Version.ToLower() + @"\verses.json");
            }

            //Give the user their verses.
            Kuwa.StartNewRequest(Verse, Version);

            File.WriteAllText(cv.VerseOutput, Kuwa.verses[currentIndex]);
            File.WriteAllText(cv.VersionOutput, Kuwa.plainVerseReferences[currentIndex] + ", " + Kuwa.osisReader.Version.ToUpper());
            MainWindow.vs.AssignNewCells(Kuwa.plainVerseReferences, Kuwa.verses);

        }

        //Really didn't want to do this but a lack of a better option has forced my hand.
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        //We won't need protection on this one because it /should/ be impossible the way it's set up to pass it an index out of bounds.
        public static void JumpQueue(int index)
        {
            currentIndex = index;
            File.WriteAllText(cv.VerseOutput, Kuwa.verses[currentIndex]);
            File.WriteAllText(cv.VersionOutput, Kuwa.plainVerseReferences[currentIndex] + "," + Kuwa.osisReader.Version.ToUpper());
        }

        public static void TransformQueue(bool way, bool highlight=true)
        {
            if (Kuwa.verses == null) { return; }
       
            //True is for forward, False is for backward.
            //this could be a nested ternary, but it's not for the sake of readability.
            if (way)
            {
                if (currentIndex == Kuwa.verses.Length - 1) { return; }
                currentIndex++;
            }
            else
            {
                if (currentIndex == 0) { return; }
                currentIndex--;
            }
            File.WriteAllText(cv.VerseOutput, Kuwa.verses[currentIndex]);
            File.WriteAllText(cv.VersionOutput, Kuwa.plainVerseReferences[currentIndex] + "," + Kuwa.osisReader.Version.ToUpper());
            if (MainWindow.isShowingVerses)
            {
                MainWindow.vs.HighlightRow(currentIndex);
            }
        }
    }
}
