using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuwagataDLL
{
    public static class Main
    {
        public static OSISReader osisReader;
        public static string[] verses;
        public static string[] plainVerseReferences;
        public static int[] verseIds;
        public static int currentIndex;

        public static void Initialize(string OSISPath)
        {
            osisReader = new OSISReader(OSISPath);
        }

        public static void ChangeOSISPath(string newOSISPath)
        {
            osisReader.ChangeOSISPath(newOSISPath);
        }

        public static void StartNewRequest(string Verse, string Version)
        {
            //Reset the current index
            currentIndex = 0;

            //Give the user their verses.
            verseIds = osisReader.GetReferencesFromString(Verse, false);
            verses = osisReader.GetVersesFromReferences(verseIds);

            plainVerseReferences = osisReader.batchDecodeAllReferences(verseIds);
        }


    }
}
