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
            Version = Directory.GetParent(OSISPath).Name.ToString().ToUpper();
            string JSONText = File.ReadAllText(OSISPath);
            verses = (JObject)JsonConvert.DeserializeObject(JSONText);
            BI = new BibleIndexes();
        }

        //Okay so since I keep seeing a bunch of similar code in GetReferencesFromString I'ma keep this function DRY
        private int[] GetVersesBetweenMarkers(int startMarker, int endMarker, AddSelectionOptions skipOption, bool escalate)
        {
            List<int> returnList = new List<int>();
           

            //ok so I realized people are beyond idiotic (including myself) so I'm going to do this
           if (startMarker > endMarker)
            {
                //I don't wanna use a temp so I'm just going to carbon-copy this programming interview question
                startMarker += endMarker;
                endMarker = startMarker - endMarker;
                startMarker -=  endMarker;
            }

            for (int j = startMarker; j < endMarker; j++)
            {
                if (verses.ContainsKey(j.ToString()))
                {
                    returnList.Add(j);
                }
                else
                {
                    //prevent the program from processing more indexes than it needs to
                    j = BI.IncreaseBibleReference(j, skipOption) + 1;
                    if (verses.ContainsKey(j.ToString()) == false && escalate)
                    {
                        j = BI.IncreaseBibleReference(j, AddSelectionOptions.Book);
                    }
                    if (verses.ContainsKey(j.ToString()))
                    {
                        returnList.Add(j);
                    }
                    
                }
            }
            return returnList.ToArray(); //I might be a little rarted but this makes me less rarted
        }

        //Reference decoder.
        public string[] batchDecodeAllReferences(int[] references)
        {
            List<string> returnList = new List<string>();

            foreach (int reference in references)
            {
                //Discern the book.
                double preBookIdentifier = reference / 1000000;
                double bookIdentifier = Math.Floor(preBookIdentifier);
                string Book = BI.BiblePlainArray[(int)bookIdentifier - 1];

                //Discern the chapter.
                string ChapterIdentifier = Math.Floor((reference - (bookIdentifier * 1000000)) / 1000).ToString();

                //Discern the verse.
                string VerseIdentifier = (reference - ((bookIdentifier * 1000000) + (Int32.Parse(ChapterIdentifier) * 1000))).ToString();

                //Lastly, smack 'em all together to get something a human can reliably reference.
                string finalAddition = Book + " " + ChapterIdentifier + ":" + VerseIdentifier;

                returnList.Add(finalAddition);

            }

            return returnList.ToArray();
        }

        public int[] GetReferencesFromString(string request, bool specialRecurse)
        {   
            //First, split the string by semicolons into individual requests;
            string[] requests = request.Split(';');

            //Next, initialize an array of numbers to return after processing the requests.
            List<int> returnList = new List<int>();

            string[] elements;
            string[] chapterAndVerse;
            string[] firstandPossSecond;
            int returnNumber = 0;
            bool multiWordBook = false;
            for(int i = 0; i < requests.Length; i++)
            {
                //Okay so a weird error happens if someone does something like put a space between separated references so I'm doing this:
                while (requests[i][0] == ' ')
                  {
                     requests[i] = requests[i].Remove(0, 1);
                  }
               

                //First, split the resulting string further by its spaces to get the book and chapter/verses. 
                elements = requests[i].Split(' '); 
                //Second, turn the first element of *that* resulting string into a number using BibleIndexes' GetBibleIndexFromArray.
                

               returnNumber = BI.GetBibleIndexFromArray(elements[0]) * 1000000; //x1000000 because that's the scheme the JSON uses.

                //ok so I forgor initially that books like 2 Corinthians and 1 John exist so here's what we're gonna do
                if (returnNumber == 0)
                {
                    returnNumber = BI.GetBibleIndexFromArray(elements[0] + " " + elements[1]) * 1000000;
                    multiWordBook = true; //flag the next subscript to shift down one element
                }

                //If we are simply referencing an entire book
                if (((elements.Length == 1) || (elements.Length == 2 && multiWordBook)) && !requests[i].Contains("-"))
                {
                    //gotta check for the special condition (explained later)
                    if (specialRecurse)
                    {
                        returnList.Add(returnNumber + 1001);
                        continue; //simply return a starting point (or an end point)
                    }

                    int nextBook = BI.IncreaseBibleReference(returnNumber, AddSelectionOptions.Book);
                    foreach (int GVBMref in GetVersesBetweenMarkers(returnNumber, nextBook, AddSelectionOptions.Chapter, false))
                    {
                        returnList.Add(GVBMref);
                    }
                    continue; //Cut off the current iteration there to prevent headache.
                }

                //Let's say, hypothetically, for the sake of the argument, we were referencing across two books with no verses specified;
                string[] potentialCrossBookReference = requests[i].Split('-');
                if (potentialCrossBookReference.Length > 1)
                {
                    if (BI.GetBibleIndexFromArray(potentialCrossBookReference[0]) != 0 && BI.GetBibleIndexFromArray(potentialCrossBookReference[1]) != 0)
                    {
                        int startPos = GetReferencesFromString(potentialCrossBookReference[0], true)[0];
                        int endPos = GetReferencesFromString(potentialCrossBookReference[1], true)[0];

                        foreach(int GVBMref in GetVersesBetweenMarkers(startPos, endPos, AddSelectionOptions.Chapter, true))
                        {
                            returnList.Add(GVBMref);
                        }

                        continue; //And that's a wrap, folks!
                                                     //Remember when this function was clean and elegant? Me neither.
                    }
                }
                




                //This is a little part I reworked because the first time I did this I made a planning error structurally
                firstandPossSecond = multiWordBook ? elements[2].Split('-') : elements[1].Split('-'); //ternary operator 



                //and now this becomes that
                chapterAndVerse = firstandPossSecond[0].Split(':'); 

                returnNumber += Int32.Parse(chapterAndVerse[0]) * 1000; //Again, scheme.

                if (firstandPossSecond.Length > 1) // Gotta do this to prevent "index outta range"
                {
                    if ((BI.GetBibleIndexFromArray(elements[0]) != 0 && BI.GetBibleIndexFromArray(firstandPossSecond[1]) != 0)
                        || (multiWordBook && BI.GetBibleIndexFromArray(elements[0] + " " + elements[1]) != 0 && BI.GetBibleIndexFromArray(firstandPossSecond[1]) != 0))
                    {
                        int startPos; //intentionally left vague for now
                        int endPos;

                        //Step 1, stitch back together the reference you had in the first book
                        string startRef = elements[0] + " " + firstandPossSecond[0];
                        //Step 2, Craft the second reference from the edges of requests[i]
                        string endRef = requests[i].Remove(0, startRef.Length + 1);
                        //Step 3, smack 'em both together and get the numerical references by running this function again
                        int[] numRef = GetReferencesFromString(startRef + ";" + endRef, true); //Adding a special condition just for this

                        //By all means this shouldn't happen but just in case it does
                        startPos = numRef[0] < numRef[1] ? numRef[0] : numRef[1]; //if the end position is somehow smaller than the start, swap em around
                        endPos = numRef[0] < numRef[1] ? numRef[1] : numRef[0]; //and so on

                        for (int j = startPos; j < endPos; j++)
                        {
                            if (verses.ContainsKey(j.ToString()))
                            {
                                returnList.Add(j);
                            }
                            else
                            {
                                //prevent the program from processing more indexes than it needs to
                                j = BI.IncreaseBibleReference(j, AddSelectionOptions.Chapter) + 1;
                                if (verses.ContainsKey(j.ToString()) == false)
                                {
                                    j = BI.IncreaseBibleReference(j, AddSelectionOptions.Book);
                                }
                            }
                        }
                        returnList.Add(endPos);
                        continue; 

                    }
                }
               

                //If there's just a chapter and no verse:
                if (chapterAndVerse.Length == 1)
                {
                    int nextBook = BI.IncreaseBibleReference(returnNumber, AddSelectionOptions.Chapter);
                    for (int a = returnNumber; a < nextBook; a++)
                    {
                        if (verses.ContainsKey(a.ToString()))
                        {
                            returnList.Add(a);
                        }
                    }
                    continue;
                }


                //Fourth is where the loop branches.
                if (firstandPossSecond.Length >= 2) //If a second element exists, branch and get all the verses between the first and second number. 
                {



                    string[] nums = chapterAndVerse[1].Split('-'); //YES, YES, SPLIT IT WIDE OPEN
                    //Now comes the part where I lose all of my remaining sanity:

                    if (firstandPossSecond[1].Contains(':')) // If the split string contains a reference to another verse, in another chapter:
                    {
                        // dudedudedude I got this

                        //Step 1, set the trap, now laced with direcursional-trisinglelineide-based ricin
                        string startToken = GetReferencesFromString((multiWordBook ? elements[0] + " " + elements[1] : elements[0]) + " " + firstandPossSecond[0], false)[0].ToString();

                        //Step 2, hatch a terrible idea
                        string endToken = GetReferencesFromString((multiWordBook ? elements[0] + " " + elements[1] : elements[0]) + " " + firstandPossSecond[1], false)[0].ToString();

                        //Step 3, cross your fingers and hope it works
                        //I really didn't want to have to do this, but it will be what this is for a lack of a better solution.
                        for (int m = Int32.Parse(startToken); m < Int32.Parse(endToken) + 1; m++)
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
                        //So for our efforts here, it's a little less complicated....
                        string[] firstChapterAndVerse = firstandPossSecond[0].Split(':');

                        var startPosition = multiWordBook ? String.Format("{0} {1} {2}", elements[0], elements[1], firstandPossSecond[0]) : String.Format("{0} {1}", elements[0], firstandPossSecond[0]);
                        var endPosition = multiWordBook ? String.Format("{0} {1} {2}:{3}", elements[0], elements[1], firstChapterAndVerse[0], firstandPossSecond[1]) : String.Format("{0} {1}:{2}", elements[0], firstChapterAndVerse[0], firstandPossSecond[1]);

                        startPosition = String.Format("{0};{1}", startPosition, endPosition);

                        int[] tempHolder = GetReferencesFromString(startPosition, false);
                        for (int k = tempHolder[0]; k < tempHolder[1]+1; k++) //Loop through the resulting numbers
                        {
                            returnList.Add(k); //And add this junk
                            //Surprisingly, this fits with the scheme.
                        }
                    }

                   
                }
                else
                {
                    returnList.Add(returnNumber + Int32.Parse(chapterAndVerse[1]));
                    
                }
                //And this is all still just one iteration of a loop.... ARE WE HAVING FUN YET?!
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
        public string Version;
        public BibleIndexes BI;
    }
}
