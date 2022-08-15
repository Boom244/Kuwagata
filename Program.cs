using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**In the interest of knowing what I'm doing when I come back to this project after a break of any length,
 * I have decided to litter this entire solution with extremely specific comments.
 */
namespace Kuwagata
{
    class Program
    {
       public static void Main(string[] args)
        {   //Create an OSISReader object to give you verses from the requests you put in.
            OSISReader osisReader = new OSISReader(@"C:\Users\bolum\source\repos\VerseScraper CSharp Edition\OSISBibles\kjv\verses.json");
            //Prompt the user for a verse.
            Console.WriteLine("Enter a verse...");
            string verseRequest = Console.ReadLine();
            int[] verseIds = osisReader.GetReferencesFromString(verseRequest);
            string[] verses = osisReader.GetVersesFromReferences(verseIds);
            foreach(string j in verses)
            {
                Console.WriteLine(j);
            }
            Console.ReadLine();
            //Far future: Hook this up to a GUI so I don't lose my mind.

        }

    }
}
