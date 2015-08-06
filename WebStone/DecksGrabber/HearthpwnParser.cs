using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DecksGrabber
{
    public class HearthpwnParser : BaseParser
    {
        private static readonly string _domen = @"www.hearthpwn.com";

        protected override string GetDecksUrl(string className, int index)
        {
            if (!_classNames.Contains(className))
                return string.Empty;

            int filterClass = 1 << (Array.IndexOf(_classNames, className) + 2);

            return @"http://www.hearthpwn.com/decks?filter-class=" + filterClass.ToString() + @"&filter-deck-tag=2" + @"&page=" + (index + 1).ToString();
        }

        protected override List<DeckInfo> ParseDecksPage(string pageContent)
        {
            List<DeckInfo> decks = new List<DeckInfo>();

            // Parse downloaded page
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageContent);

            var trNodes = htmlDocument.GetElementbyId("decks").Descendants().Where(x => x.Name == "tr");
            foreach (var trNode in trNodes)
            {
                DeckInfo deckInfo = new DeckInfo() { Source = _domen };

                var tdNodes = trNode.Descendants().Where(x => x.Name == "td");
                foreach (var tdNode in tdNodes)
                {
                    string attributeClass = tdNode.GetAttributeValue("class", string.Empty);

                    if (attributeClass.Contains("col-name"))
                    {
                        var aNodes = tdNode.Descendants().Where(x => x.Name == "a").ToArray();
                        if (aNodes.Count() == 2)
                        {
                            deckInfo.Name = WebUtility.HtmlDecode(aNodes[0].InnerText);
                            deckInfo.Author = WebUtility.HtmlDecode(aNodes[1].InnerText);
                            deckInfo.DeckUrl = "http://" + _domen + aNodes[0].GetAttributeValue("href", string.Empty);
                            deckInfo.AuthorUrl = "http://" + _domen + aNodes[1].GetAttributeValue("href", string.Empty);
                        }
                    }
                    else if (attributeClass.Contains("col-deck-type"))
                    {
                        deckInfo.Type = WebUtility.HtmlDecode(tdNode.InnerText);
                    }
                    else if (attributeClass.Contains("col-class"))
                    {
                        deckInfo.Class = WebUtility.HtmlDecode(tdNode.InnerText);
                    }
                    else if (attributeClass.Contains("col-ratings"))
                    {
                        var divNodes = tdNode.Descendants().Where(x => x.Name == "div").ToArray();
                        if (divNodes.Count() == 1)
                        {
                            deckInfo.Rating = int.Parse(divNodes[0].InnerText);
                        }
                    }
                    else if (attributeClass.Contains("col-views"))
                    {
                        deckInfo.ViewsCount = int.Parse(tdNode.InnerText);
                    }
                    else if (attributeClass.Contains("col-comments"))
                    {
                        deckInfo.CommentsCount = int.Parse(tdNode.InnerText);
                    }
                    else if (attributeClass.Contains("col-updated"))
                    {
                        var abbrNodes = tdNode.Descendants().Where(x => x.Name == "abbr").ToArray();
                        if (abbrNodes.Count() == 1)
                        {
                            deckInfo.Updated = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(double.Parse(abbrNodes[0].GetAttributeValue("data-epoch", string.Empty)));
                        }
                    }
                }

                decks.Add(deckInfo);
            }

            return decks;
        }

        protected override DeckInfo ParseDeckPage(string pageContent)
        {
            DeckInfo deckInfo = new DeckInfo() { Source = _domen, Cards = new List<string>() };

            // Parse downloaded page
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageContent);

            var tableNodes = htmlDocument.DocumentNode.Descendants().Where(x => (x.Name == "table" && x.GetAttributeValue("id", string.Empty) == "cards"));
            foreach (var tableNode in tableNodes)
            {
                var trNodes = tableNode.Descendants().Where(x => x.Name == "tr");
                foreach (var trNode in trNodes)
                {
                    var tdNodes = trNode.Descendants().Where(x => x.Name == "td");
                    foreach (var tdNode in tdNodes)
                    {
                        string attributeClass = tdNode.GetAttributeValue("class", string.Empty);

                        if (attributeClass.Contains("col-name"))
                        {
                            var bNodes = tdNode.Descendants().Where(x => x.Name == "b").ToArray();
                            if (bNodes.Count() == 1)
                            {
                                deckInfo.Cards.Add(WebUtility.HtmlDecode(bNodes[0].InnerText));
                                if (WebUtility.HtmlDecode(tdNode.InnerText).Contains("2"))
                                {
                                    deckInfo.Cards.Add(WebUtility.HtmlDecode(bNodes[0].InnerText));
                                }
                            }
                        }
                    }
                }
            }

            return deckInfo;
        }
    }
}
