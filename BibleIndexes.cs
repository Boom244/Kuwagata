using System;
using System.Text.RegularExpressions;

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

        //btw all of this is ripped from Avendesora's pythonbible
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
            @"(Ecclesiastes(?:\s+or\,\s+the\s+Preacher)?|Eccles\.*|Eccle\.*|Eccl\.*|Ecc\.*|Ec\.*|Qoh\.*)",
            @"(Song(?: of (Solomon|Songs|Sol\.*))?)|Canticles|(Canticle(?: of Canticles)?)|SOS|Cant",
            @"Isa\.*(?:iah)?",
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
            @"(John|Joh\.*|Jhn\.*|Jo\.*|Jn\.*)",
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
            @"1 (John|Joh\.*|Jhn\.*|Jo\.*|Jn\.*)",
            @"2 (John|Joh\.*|Jhn\.*|Jo\.*|Jn\.*)",
            @"3 (John|Joh\.*|Jhn\.*|Jo\.*|Jn\.*)",
            @"Jud\.*(:?e)?",
            @"Rev\.*(?:elation)?" };
        public int GetBibleIndexFromArray(string element)
        {
            for(int i = 0; i < BibleRegexArray.Length; i++)
            {
                if (Regex.IsMatch(element, BibleRegexArray[i], RegexOptions.IgnoreCase)) {
                    return i + 1; // Indexes start at 0, so we gotta pump up those numbers.
                }
            }
            return 1;
        }
    }

  

    public class CannotGetBibleIndexException : Exception
    {
        public CannotGetBibleIndexException()
        {

        }

        public CannotGetBibleIndexException(string message)
        : base(message)
        {

        }
    }

    public enum AddSelectionOptions : int
    {
        Verse = 1,
        Chapter = 1000,
        Book = 1000000,
    }

}