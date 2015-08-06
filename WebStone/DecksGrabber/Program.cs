using System;

namespace DecksGrabber
{
    class Program
    {
        static bool DeckParsed(DeckInfo deckInfo)
        {
            bool changed = true;

            Console.WriteLine(deckInfo.Updated.ToShortDateString() + " : " + deckInfo.Class + " : " + deckInfo.Name);

            return changed;
        }

        static void Main(string[] args)
        {
            try
            {
                var hearthPwdParser = new HearthpwnParser();
                hearthPwdParser.DeckParsedCallback = new DeckParsedDelegate(DeckParsed);
                hearthPwdParser.StartParsing(10);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception.ToString());
            }
        }
    }
}
