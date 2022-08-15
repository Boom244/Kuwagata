using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kuwagata
{
    class OSISReader
    {
        //Constructor.
        public OSISReader(string OSISpath)
        {
            OSISPath = OSISpath;
            string JSONText = File.ReadAllText(OSISPath);
            verses = (JObject)JsonConvert.DeserializeObject(JSONText);
            BI = new BibleIndexes();
        }

        public int[] GetReferencesFromString(string request)
        {   
            //First, split the string by semicolons into individual requests;
            string[] requests = request.Split(';');

            //Next, initialize an array of numbers to return after processing the requests.
            List<int> returnList = new List<int>();

            string[] elements;
            string[] chapterAndVerse;
            string[] firstandPossSecond;
            int returnNumber = 0;
            for(int i = 0; i < requests.Length; i++)
            {
                /**TODO features:
                 * Implement whole-chapter picking
                 * Implement cross-book picking (gonna need to implement a scan function in BibleIndexes so I can do this efficiently)
                 **/

                //First, split the resulting string further by its spaces to get the book and chapter/verses. 
                elements = requests[i].Split(' '); 
                //Second, turn the first element of *that* resulting string into a number using BibleIndexes' GetBibleIndexFromArray.
                try
                {
                    returnNumber = BI.GetBibleIndexFromArray(elements[0]) * 1000000; //x1000000 because that's the scheme the JSON uses.
                }
                catch
                {
                    //Like it? It's my own useless exception. At least I'll know where stuff is going wrong. 
                    throw new CannotGetBibleIndexException("Error in getting bible reference. Could not parse book name.");
                }

                //This is a little part I reworked because the first time I did this I made a planning error structurally
                firstandPossSecond = elements[1].Split('-');


                //and now this becomes that
                chapterAndVerse = firstandPossSecond[0].Split(':'); // This is now left level 3 of the diagram

                //So if we have something like "Genesis 2:4-3:7" as input, the above should contain {"2:4", "3:7"}

                //And now, we take a little detour in case this is a cross-chapter reference:
                returnNumber += Int32.Parse(chapterAndVerse[0]) * 1000; //Again, scheme.

                //Fourth is where the loop branches.
                if (firstandPossSecond.Length >= 2) //If a second element exists, branch and get all the verses between the first and second number. 
                {



                    string[] nums = chapterAndVerse[1].Split('-'); //YES, YES, SPLIT IT WIDE OPEN
                    //Now comes the part where I lose all of my remaining sanity:

                    if (firstandPossSecond[1].Contains(':')) // If the split string contains a reference to another verse, in another chapter:
                    {
                        // dudedudedude I got this

                        //Step 1, set the trap, now laced with direcursional-trisinglelineide-based ricin
                        string startToken = GetReferencesFromString(elements[0] + " " + firstandPossSecond[0])[0].ToString();

                        //Step 2, hatch a terrible idea
                        string endToken = GetReferencesFromString(elements[0] + " " + firstandPossSecond[1])[0].ToString();

                        //Step 3, cross your fingers and hope it works
                        //I really didn't want to have to do this, but it will be what this is for a lack of a better solution.
                        for (int m = Int32.Parse(startToken); m < Int32.Parse(endToken)+1; m++)
                        {
                            if (verses.ContainsKey(m.ToString()))
                            {
                                returnList.Add(m);
                            }
                            else
                            {
                                //prevent the program from processing more indexes than it needs to
                                m = BI.IncreaseBibleReference(m, AddSelectionOptions.Chapter); 
                            }
                        }
                        


                    }
                    else //if the user (probably me) decides to spare my sanity
                    {
                        for (int k = Int32.Parse(firstandPossSecond[0]); k < Int32.Parse(firstandPossSecond[1]) + 1; k++) //Loop through the resulting numbers
                        {
                            returnList.Add(returnNumber + k); //And add this junk
                            //Surprisingly, this fits with the scheme.
                        }
                    }

                   
                }
                else
                {
                    returnList.Add(returnNumber + Int32.Parse(chapterAndVerse[1]));
                    
                }
                //And this is all still just one iteration of a loop.... ARE WE HAVING FUN YET?!
                //This is either going to take a large steaming crap all over allocated memory or run up CPU usage. Revisit if either is true.

            }

            return returnList.ToArray();
            //So hopefully, as a result of this function, an input of {"Genesis 1:1", "Genesis 1:2"} SHOULD return {1001001, 1001002}.
        }

        public string[] GetVersesFromReferences(int[] references)
        {
            List<string> def = new List<string>();
            foreach(int reference in references)
            {
                def.Add(verses[reference.ToString()].ToString()); //I don't know if the fact that this works means I'm stupid or a genius
            }
            return def.ToArray();
        }

        public string OSISPath { get; set; }
        public JObject verses;
        public BibleIndexes BI;
    }
}
