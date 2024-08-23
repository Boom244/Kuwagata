﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KuwagataDLL
{
    public class OSISReader
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

       public void ChangeOSISPath(string newOSISPath)
        {
            OSISPath = newOSISPath;
            Version = Directory.GetParent(newOSISPath).Name.ToString().ToUpper();
            verses = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(newOSISPath));
        }

        //DRY concerns
        private int[] GetVersesBetweenMarkers(int startMarker, int endMarker, AddSelectionOptions skipOption, bool escalate)
        {
            List<int> returnList = new List<int>();


            //ok so I realized people are beyond idiotic (including myself) so I'm going to do this
            if (startMarker > endMarker)
            {
                //I don't wanna use a temp so I'm just going to carbon-copy this programming interview question
                startMarker += endMarker;
                endMarker = startMarker - endMarker;
                startMarker -= endMarker;
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

        public List<int> GetVersesBetweenChapters(bool multiWordBook, string[] elements, string[] firstandPossSecond, List<int> returnList)
        {
            int startToken = Int32.Parse(GetReferencesFromString((multiWordBook ? elements[0] + " " + elements[1] : elements[0]) + " " + firstandPossSecond[0], false)[0].ToString());
            int endToken = Int32.Parse(GetReferencesFromString((multiWordBook ? elements[0] + " " + elements[1] : elements[0]) + " " + firstandPossSecond[1], false)[0].ToString());

            returnList = returnList.Concat(
                GetVersesBetweenMarkers(
                    startToken,
                    endToken,
                    AddSelectionOptions.Chapter, false)
                .ToList()
                ).ToList();
            returnList.Add(endToken);
            return returnList;

        }

        //DRY concerns
        public List<int> SplitCommaSeparatedVerses(List<int> returnList, string[] elements, bool multiWordBook)
        {
            string[] subelements;
            string book, chapter;
            /* Concerned about use of if statement as opposed to ternary operators?
             * https://stackoverflow.com/questions/17328641/ternary-operator-is-twice-as-slow-as-an-if-else-block
             * Granted, later versions of NET have closed the gap from ~80ms to 40-50ms, but optimization is a must.
             */
            if (multiWordBook)
            {
                subelements = elements[2].Split(',');
                book = $"{elements[0]} {elements[1]}";
            }
            else
            {
                subelements = elements[1].Split(',');
                book = elements[0];
            }
            chapter = subelements[0].Split(':')[0];

            foreach (string subelement in subelements) //now we loop through
            {
                string submittanceString;
             
                //do a check: does it contain a verse and a chapter or just a verse?
                if (subelement.Contains(":"))
                {
                    submittanceString = $"{book} {subelement}";
                }
                else
                {
                    submittanceString = $"{book} {chapter}:{subelement}";
                }
                List<int> l = GetReferencesFromString(submittanceString, false).ToList(); 
                returnList = returnList.Concat(l).ToList();
            }
            return returnList;
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
           
            for (int i = 0; i < requests.Length; i++)
            {

                bool multiWordBook = false;
                //Error catch that happens if someone does something like put a space between separated references so I'm doing this:
                while (requests[i][0] == ' ')
                {
                    requests[i] = requests[i].Remove(0, 1);
                }

                //Stylistic choice; If you end a reference with a semicolon and put nothing after it, then an error pops up.
                if (requests[i] == "")
                {
                    continue;
                }

                    //First, split the resulting string further by its spaces to get the book and chapter/verses. 
                    elements = requests[i].Split(' ');

                //Second, turn the first element of *that* resulting string into a number using BibleIndexes' GetBibleIndexFromArray.

                returnNumber = BI.GetBibleIndexFromArray(elements[0]) * 1000000; //x1000000 because that's the scheme the JSON uses.

                //Accomodations for multi-word books
                if (returnNumber == 0)
                {
                    returnNumber = BI.GetBibleIndexFromArray(elements[0] + " " + elements[1]) * 1000000;
                    multiWordBook = true; //flag the next subscript to shift down one element
                }

                //New clause; Sometimes you might want to reference a bunch of new verses within the same book, a la, for example,
                //"Jonah 1:3-4,14,17,2:1". So, here's what we're gonna do:
                if (elements.Length > (multiWordBook ? 2 : 1))
                {
                    if (elements[multiWordBook ? 2 : 1].Contains(","))
                    {
                        returnList = SplitCommaSeparatedVerses(returnList, elements, multiWordBook);
                        continue;
                    }
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
                    continue; 
                }

                string[] potentialCrossBookReference = requests[i].Split('-');
                if (potentialCrossBookReference.Length > 1)
                {
                    if (BI.GetBibleIndexFromArray(potentialCrossBookReference[0]) != 0 && BI.GetBibleIndexFromArray(potentialCrossBookReference[1]) != 0)
                    {
                        int startPos = GetReferencesFromString(potentialCrossBookReference[0], true)[0];
                        int endPos = GetReferencesFromString(potentialCrossBookReference[1], true)[0];

                        foreach (int GVBMref in GetVersesBetweenMarkers(startPos, endPos, AddSelectionOptions.Chapter, true))
                        {
                            returnList.Add(GVBMref);
                        }
                        returnList.Add(endPos);
                        continue; 
                    }
                }

                firstandPossSecond = multiWordBook ? elements[2].Split('-') : elements[1].Split('-'); //ternary operator 

                chapterAndVerse = firstandPossSecond[0].Split(':');

                returnNumber += Int32.Parse(chapterAndVerse[0]) * 1000; 

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


                if (firstandPossSecond.Length >= 2) //If a second element exists, branch and get all the verses between the first and second number. 
                {
                    string[] nums = chapterAndVerse[1].Split('-'); 
                    

                    if (firstandPossSecond[1].Contains(':')) // If the split string contains a reference to another verse, in another chapter:
                    {
                        returnList = GetVersesBetweenChapters(multiWordBook, elements, firstandPossSecond, returnList);
                    }
                    else 
                    {
                        string[] firstChapterAndVerse = firstandPossSecond[0].Split(':');

                        var startPosition = multiWordBook ? String.Format("{0} {1} {2}", elements[0], elements[1], firstandPossSecond[0]) : String.Format("{0} {1}", elements[0], firstandPossSecond[0]);
                        var endPosition = multiWordBook ? String.Format("{0} {1} {2}:{3}", elements[0], elements[1], firstChapterAndVerse[0], firstandPossSecond[1]) : String.Format("{0} {1}:{2}", elements[0], firstChapterAndVerse[0], firstandPossSecond[1]);

                        startPosition = String.Format("{0};{1}", startPosition, endPosition);

                        int[] tempHolder = GetReferencesFromString(startPosition, false);
                        for (int k = tempHolder[0]; k < tempHolder[1] + 1; k++) //Loop through the resulting numbers
                        {
                            returnList.Add(k); 
                        }
                    }


                }
                else
                {
                    returnList.Add(returnNumber + Int32.Parse(chapterAndVerse[1]));

                }
            }

            return returnList.ToArray();
            //So hopefully, as a result of this function, an input of {"Genesis 1:1", "Genesis 1:2"} should return {1001001, 1001002}.
        }

        public string[] GetVersesFromReferences(int[] references)
        {
            List<string> def = new List<string>();
            foreach (int reference in references)
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
