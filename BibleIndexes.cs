using System.Text.RegularExpressions;
using System;
namespace Kuwagata
{
    public class BibleIndexes
    {

        public BibleIndexes()
        {

        }

        public int IncreaseBibleReference(int currentReference, AddSelectionOptions option)
        {

            //forcing order of operations here because I don't trust C#
            //      base value minus the remainder of value div by option plus option
            return (currentReference - (currentReference % (int)option)) + (int)option;
        }

        //btw all of these regexes are ripped from Avendesora's pythonbible
        //avendesora if you end up seeing this pls no bully
        public string[] BibleRegexArray = {
            @"Gen\.*(?:esis)?",
            @"Exo\.*(?:d\.*)?(?:us)?",
            @"Lev\.*(?:iticus)?",
            @"Num\.*(?:bers)?",
            @"Deu\.*(?:t\.*)?(?:eronomy)?",
            @"(Joshua|Josh\.*|Jos\.*|Jsh\.*)",
            @"(Judges|Judg\.*|Jdgs\.*|Jdg\.*)",
            @"(Ruth|Rut\.*|Rth\.*)",
            @"1 (Samuel|Sam\.*|Sa\.*|Sm\.*)",
            @"2 (Samuel|Sam\.*|Sa\.*|Sm\.*)",
            @"1 (Kings|Kgs\.*|Kin\.*|Ki\.*)",
            @"2 (Kings|Kgs\.*|Kin\.*|Ki\.*)",
            @"1 (Chronicles|Chron\.*|Chro\.*|Chr\.*|Ch\.*)",
            @"2 (Chronicles|Chron\.*|Chro\.*|Chr\.*|Ch\.*)",
            @"Ezr\.*(?:a)?",
            @"Neh\.*(?:emiah)?",
            @"Est\.*(?:h\.*)?(?:er)?",
            @"Job",
            @"(Psalms|Psalm|Pslm\.*|Psa\.*|Psm\.*|Pss\.*|Ps\.*)",
            @"(Proverbs|Prov\.*|Pro\.*|Prv\.*)",
            @"(Ecclesiastes(?:\s+or\,\s+the\s+Preacher)?|Eccles\.*|Eccle\.*|Eccl\.*|Ecc\.*|Ec\.*|Qoh\.*)$",
            @"(Song(?: of (Solomon|Songs|Sol\.*))?)|Canticles|(Canticle(?: of Canticles)?)|SOS|Cant",
            @"Is\.*(?:aiah)?",
            @"Jer\.*(?:emiah)?",
            @"Lam\.*(?:entations)?",
            @"(Ezekiel|Ezek\.*|Eze\.*|Ezk\.*)",
            @"Dan\.*(?:iel)?",
            @"Hos\.*(?:ea)?",
            @"Joe\.*(?:l)?",
            @"Amo\.*(?:s)?",
            @"Oba\.*(?:d\.*(?:iah)?)?",
            @"Jonah|Jon\.*|Jnh\.*",
            @"Mic\.*(?:ah)?",
            @"Nah\.*(?:um)?",
            @"Hab\.*(?:akkuk)?",
            @"Zep\.*(?:h\.*(?:aniah)?)?",
            @"Hag\.*(?:gai)?",
            @"Zec\.*(?:h\.*(?:ariah)?)?",
            @"Mal\.*(?:achi)?",
            @"Mat\.*(?:t\.*(?:hew)?)?",
            @"Mark|Mar\.*|Mrk\.*",
            @"Luk\.*(?:e)?",
            @"^((?!1|2|3).)*(John|Joh\.*|Jhn\.*|Jo\.*|Jn\.*)",
            @"Act\.*(?:s)?",
            @"Rom\.*(?:ans)?",
            @"1 Co\.*(?:r\.*(?:inthians)?)?",
            @"2 Co\.*(?:r\.*(?:inthians)?)?",
            @"Gal\.*(?:atians)?",
            @"Eph\.*(?:es\.*(?:ians)?)?",
            @"Ph(?:(p\.*)|(?:il\.*(?!e\.*(?:m\.*(?:on)?)?)(?:ippians)?))",
            @"Col\.*(?:ossians)?",
            @"1 Th\.*(?:(s|(es(?:s)?))\.*(?:alonians)?)?",
            @"2 Th\.*(?:(s|(es(?:s)?))\.*(?:alonians)?)?",
            @"1 Ti\.*(?:m\.*(?:othy)?)?",
            @"2 Ti\.*(?:m\.*(?:othy)?)?",
            @"Tit\.*(?:us)?",
            @"(Philemon|Philem\.*|Phile\.*|Phlm\.*|Phi\.*|Phm\.*)",
            @"Heb\.*(?:rews)?",
            @"Ja(?:me)?s\.*",
            @"1 (Pe\.*(?:t\.*(?:er)?)?|Pt\.*)",
            @"2 (Pe\.*(?:t\.*(?:er)?)?|Pt\.*)",
            @"1 (John|Joh\.*|Jhn\.*|Jo\.*|Jn\.*)",
            @"2 (John|Joh\.*|Jhn\.*|Jo\.*|Jn\.*)",
            @"3 (John|Joh\.*|Jhn\.*|Jo\.*|Jn\.*)",
            @"Jud\.*(:?e)?",
            @"Rev\.*(?:elation)?" };

        //Plain array for decoding use.
        public string[] BiblePlainArray = {
                "Genesis",
                "Exodus",
                "Leviticus",
                "Numbers",
                "Deuteronomy",
                "Joshua",
                "Judges",
                "Ruth",
                "1 Samuel",
                "2 Samuel",
                "1 Kings",
                "2 Kings",
                "1 Chronicles",
                "2 Chronicles",
                "Ezra",
                "Nehemiah",
                "Esther",
                "Job",
                "Psalm",
                "Proverbs",
                "Ecclesiastes",
                "Song of Solomon",
                "Isaiah",
                "Jeremiah",
                "Lamentations",
                "Ezekiel",
                "Daniel",
                "Hosea",
                "Joel",
                "Amos",
                "Obadiah",
                "Jonah",
                "Micah",
                "Nahum",
                "Habakkuk",
                "Zephaniah",
                "Haggai",
                "Zechariah",
                "Malachi",
                "Matthew",
                "Mark",
                "Luke",
                "John",
                "Acts",
                "Romans",
                "1 Corinthians",
                "2 Corinthians",
                "Galatians",
                "Ephesians",
                "Philippians",
                "Colossians",
                "1 Thessalonians",
                "2 Thessalonians",
                "1 Timothy",
                "2 Timothy",
                "Titus",
                "Philemon",
                "Hebrews",
                "James",
                "1 Peter",
                "2 Peter",
                "1 John",
                "2 John",
                "3 John",
                "Jude",
                "Revelation"
        };
        //Secret tool for later:       Obadiah,  3 John,   2 John,   Philemon, Jude
        public int[] OneChapterBooks = { 31000000, 64000000, 63000000, 57000000, 65000000 };

        public int GetBibleIndexFromArray(string element)
        {
            for (int i = 0; i < BibleRegexArray.Length; i++)
            {
                if (Regex.IsMatch(element, BibleRegexArray[i], RegexOptions.IgnoreCase)) //This function is likely comp. expensive, but that's a problem for another day 
                {
                    return i + 1; // Indexes start at 0, so we gotta pump up those numbers.
                }
            }
            return 0;
        }



    }






    public enum AddSelectionOptions : int
    {
        Verse = 1,
        Chapter = 1000,
        Book = 1000000,
    }

}