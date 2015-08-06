using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DecksGrabber
{
    public delegate bool DeckParsedDelegate(DeckInfo deckInfo);

    public abstract class BaseParser
    {
        protected readonly string[] _classNames = { "Druid", "Hunter", "Mage", "Paladin", "Priest", "Rogue", "Shaman", "Warlock", "Warrior" };

        protected abstract string GetDecksUrl(string className, int index);
        protected abstract List<DeckInfo> ParseDecksPage(string pageContent);
        protected abstract DeckInfo ParseDeckPage(string pageContent);

        public DeckParsedDelegate DeckParsedCallback;

        public void StartParsing(int delaySeconds)
        {
            while (true)
            {
                int pageIndex = 0;

                Dictionary<string, bool> allDecksAreLoaded = new Dictionary<string, bool>();
                foreach (var className in _classNames)
                    allDecksAreLoaded[className] = false;

                // Repeat while at least for one class we still see new for us decks
                while (allDecksAreLoaded.Where(x => !x.Value).Count() > 0)
                {
                    List<Task<string>> downloadingDecksTasks = new List<Task<string>>();
                    List<KeyValuePair<DeckInfo, Task<string>>> downloadingDeckTasks = new List<KeyValuePair<DeckInfo, Task<string>>>();

                    // Start downloading first decks page for all classes
                    foreach (var className in _classNames)
                    {
                        if (!allDecksAreLoaded[className])
                        {
                            string url = GetDecksUrl(className, pageIndex);
                            if (!string.IsNullOrEmpty(url))
                            {
                                Task<string> downloadTask = DownloadPage(url);
                                downloadingDecksTasks.Add(downloadTask);
                            }
                        }
                    }

                    // Wait for each response and parse this
                    Task.WaitAll(downloadingDecksTasks.ToArray());
                    foreach (var downloadingDecksTask in downloadingDecksTasks)
                    {
                        string decksPageContent = downloadingDecksTask.Result;

                        // Parse downloaded decks page
                        var decks = ParseDecksPage(decksPageContent);

                        // Start downloading page for each deck
                        foreach (var deckInfo in decks)
                        {
                            if (!string.IsNullOrEmpty(deckInfo.DeckUrl))
                            {
                                Task<string> downloadTask = DownloadPage(deckInfo.DeckUrl);
                                downloadingDeckTasks.Add(new KeyValuePair<DeckInfo, Task<string>>(deckInfo, downloadTask));
                            }
                        }
                    }

                    // Wait for each response and parse this
                    bool nothingUpdated = true;
                    Task.WaitAll(downloadingDeckTasks.Select(x => x.Value).ToArray());
                    foreach (var downloadingDeckTask in downloadingDeckTasks)
                    {
                        string deckPageContent = downloadingDeckTask.Value.Result;

                        // Parse downloaded deck page
                        DeckInfo deckInfoDetailed = ParseDeckPage(deckPageContent);

                        downloadingDeckTask.Key.Cards = deckInfoDetailed.Cards;
                        downloadingDeckTask.Key.Description = deckInfoDetailed.Description;

                        if (ValidateDeckInfo(downloadingDeckTask.Key) && DeckParsedCallback != null)
                        {
                            bool somethingUpdated = DeckParsedCallback(downloadingDeckTask.Key);
                            if (somethingUpdated)
                                nothingUpdated = false;
                        }

                        allDecksAreLoaded[downloadingDeckTask.Key.Class] = nothingUpdated;
                    }

                    ++pageIndex;
                }

                Thread.Sleep(delaySeconds * 1000);
            }
        }

        protected Task<string> DownloadPage(string url)
        {
            return new HttpClient().GetStringAsync(url);
        }

        protected bool ValidateDeckInfo(DeckInfo deckInfo)
        {
            return !string.IsNullOrEmpty(deckInfo.Name);
        }
    }
}
