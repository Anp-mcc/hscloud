using System;
using System.Collections.Generic;

namespace DecksGrabber
{
    public class DeckInfo
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public string Type { get; set; }
        public string Author { get; set; }
        public int? Rating { get; set; }
        public int? ViewsCount { get; set; }
        public int? CommentsCount { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }

        public List<string> Cards { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string AuthorUrl { get; set; }
        public string DeckUrl { get; set; }
    }
}
