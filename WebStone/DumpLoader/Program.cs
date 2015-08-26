using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using DataAccess;
using Newtonsoft.Json;
using Raven.Client.Document;
using System.Net;

namespace DumpLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any() || string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("There are no any arguments");
                return;
            }

            bool downloadImages = args.Contains("--download-images");

            Console.WriteLine("Parsing JSON cards files started...");

            // Initialise DB stuff
            var dbDocument = InitDbContext();
            var dbCore = new DatabaseCore(dbDocument);

            var cardsDirectory = new DirectoryInfo(args[0]);

            // Create Images directory for downloaded images
            Directory.CreateDirectory(Path.Combine(cardsDirectory.FullName, "Images"));

            foreach (var cardFile in cardsDirectory.GetFiles("*.*.json"))
            {
                Console.WriteLine("  Parsing file: " + cardFile.Name);

                // Determine a language
                string language = string.Empty;
                var subwords = cardFile.Name.Split('.');
                if (subwords.Count() == 3)
                    language = subwords[1];

                // Check that language is determined
                if (string.IsNullOrEmpty(language))
                    continue;

                string filePath = Path.Combine(args[0], cardFile.Name);

                // Read JSON file content
                var cardJsonContent = File.ReadAllText(filePath);

                // Parse JSON file content
                var cards = JsonConvert.DeserializeObject<List<CardJsonModel>>(cardJsonContent);

                using (var dbSession = dbCore.OpenSession())
                {
                    // Add all cards to DB
                    foreach (var card in cards)
                    {
                        var dbCard = card.Map(language);

                        // Download image
                        if (downloadImages)
                        {
                            string dstImagePath = Path.Combine(Path.Combine(cardsDirectory.FullName, "Images"), string.Format("{0}_{1}.png", card.Id, language));
                            DownloadImage(dbCard.ImageUrl, dstImagePath);
                        }

                        // Add translated card
                        dbSession.Store(dbCard);

                        // Add enUS card as default if necessary
                        if (string.Compare(language, "enUS", true) == 0)
                        {
                            dbCard = card.Map();

                            // Download image
                            if (downloadImages)
                            {
                                string dstImagePath = Path.Combine(Path.Combine(cardsDirectory.FullName, "Images"), card.Id + ".png");
                                DownloadImage(dbCard.ImageUrl, dstImagePath);
                            }

                            dbSession.Store(dbCard);
                        }
                    }

                    dbSession.SaveChanges();
                }
            }

            Console.WriteLine("Parsing JSON cards files is done.");
        }

        private static DocumentStore InitDbContext()
        {
            var documentStore = new DocumentStore
            {
                Url = ConfigurationManager.AppSettings["DatabaseUrl"],
                DefaultDatabase = ConfigurationManager.AppSettings["DefaultDatabase"]
            };

            documentStore.Initialize();

            return documentStore;
        }

        private static void DownloadImage(string srcImageUrl, string dstImagePath)
        {
            string dstImageInfoPath = dstImagePath.Replace(".png", ".json");

            ImageInfoJsonModel imageInfo = new ImageInfoJsonModel();
            DateTime? lastModified = null;
            string eTag = null;

            if (File.Exists(dstImagePath) && File.Exists(dstImageInfoPath))
            {
                // Read image info
                string imageInfoJsonContent = File.ReadAllText(dstImageInfoPath);

                // Parse JSON file content
                imageInfo = JsonConvert.DeserializeObject<ImageInfoJsonModel>(imageInfoJsonContent);

                lastModified = DateTime.FromBinary(imageInfo.LastModified);
                eTag = imageInfo.ETag;
            }

            var request = (HttpWebRequest)WebRequest.Create(srcImageUrl);

            if (lastModified.HasValue)
                request.IfModifiedSince = lastModified.Value;

            if (!string.IsNullOrEmpty(eTag))
                request.Headers.Add("If-None-Match", eTag);

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    lastModified = response.LastModified;
                    eTag = response.GetResponseHeader("ETag");

                    // Save image
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var fileStream = File.Create(dstImagePath))
                        {
                            responseStream.CopyTo(fileStream);

                            if (lastModified.HasValue && !string.IsNullOrEmpty(eTag))
                            {
                                imageInfo.LastModified = lastModified.Value.ToBinary();
                                imageInfo.ETag = eTag;

                                // Get JSON file content
                                string imageInfoJsonContent = JsonConvert.SerializeObject(imageInfo);

                                // Write to file
                                File.WriteAllText(dstImageInfoPath, imageInfoJsonContent);
                            }

                            Console.WriteLine("    Downloaded card: " + Path.GetFileName(dstImagePath));
                        }
                    }
                }
            }
            catch (WebException webException)
            {
                var httpException = webException.Response as HttpWebResponse;

                if (httpException != null && httpException.StatusCode == HttpStatusCode.NotModified)
                {
                }
            }
        }

        private class ImageInfoJsonModel
        {
            public long LastModified { get; set; }

            public string ETag { get; set; }
        }
    }
}
