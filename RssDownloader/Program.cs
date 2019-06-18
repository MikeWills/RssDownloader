using RssDownloader.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RssDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            bool download = true;
            string filename = String.Empty;
            string saveToParent = @"C:\Users\ispa2\OneDrive\Podcast Archive\Scott Sigler\";

            if (args[0].Length == 0)
            {
                // Error
            }

            string url = args[0];

            string folderName = saveToParent;

            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                download = true;

                foreach (var category in item.Categories)
                {
                    if (category.Name.ToUpper().Contains("STORY SMACK"))
                    {
                        download = false;
                    }
                    else if (category.Name.ToUpper().Contains("FRIDAY FIX"))
                    {
                        download = false;
                    }
                }
                if (download)
                {
                    String title = item.Title.Text;
                    foreach (var link in item.Links)
                    {
                        if (link.RelationshipType == "enclosure")
                        {
                            String enclosure = link.Uri.ToString();

                            // Download the files
                            using (var webClient = new WebClient())
                            {
                                filename = UrlHelper.GetFileName(enclosure);
                                if (!File.Exists(String.Format("{0}{1}", folderName, filename)))
                                {
                                    try
                                    {
                                        Console.WriteLine(String.Format("Downloading file '{0}'.", filename));
                                        webClient.DownloadFile(enclosure, String.Format("{0}{1}", folderName, filename));
                                    }
                                    catch (WebException e)
                                    {
                                        //File.AppendAllText(errorLogFile, episode.video_audio.mediaUrl.Value + " returned the error: " + e.Message + "\n");
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
