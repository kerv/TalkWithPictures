using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Xml.Linq;

namespace TalkWithPictures
{
    public class BingRequest : IImageRequest
    {
        private string AppId = ConfigurationManager.AppSettings["BingSearchAPIKey"];

        public BingRequest()
        {
            Count = 50;
            Offset = 0;
        }

        private string searchTerm;
        public string SearchTerm
        {
            get { return searchTerm; }
            set { searchTerm = value; }
        }

        public string Url
        {
            get
            {
                return string.Format("https://api.datamarket.azure.com/Bing/Search/v1/Composite?Sources=%27image%27&Query=%27{0}%27&Adult=%27Strict%27&$top={1}&$skip={2}&$format=JSON", SearchTerm, Count, Offset);
            }
        }

        public int Count { get; set; }
        public int Offset { get; set; }

        public IEnumerable<PictureInformation> Parse(string json)
        {
            var dynamicObject = Json.Decode(json);

            List<PictureInformation> listOfPictures = new List<PictureInformation>();
            for (int i = 0; i < 50; i++)
            {
                var item = dynamicObject.d.results[0].Image[i];

                if (item != null)
                {
                    listOfPictures.Add(new PictureInformation
                    {
                        //Title = new String(item.Element(d + "Title").Value.Cast<char>().Take(50).ToArray()),
                        Url = item.MediaURL,
                        //ThumbnailUrl = item.Element(d + "Thumbnail").Element(d + "MediaUrl").Value,
                        Source = "Bing"
                    }); 
                }
            }

            return listOfPictures;
        }

        //public IEnumerable<PictureInformation> Parse(string xml)
        //{
        //    XElement respXml = XElement.Parse(xml);
        //    XNamespace d = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices");
        //    XNamespace m = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");

        //    return (from item in respXml.Descendants(m + "properties")
        //            select new PictureInformation
        //            {
        //                Title = new String(item.Element(d + "Title").Value.Cast<char>().Take(50).ToArray()),
        //                Url = item.Element(d + "MediaUrl").Value,
        //                ThumbnailUrl = item.Element(d + "Thumbnail").Element(d + "MediaUrl").Value,
        //                Source = "Bing"
        //            }).ToList();
        //}

        public ICredentials Credentials
        {
            get
            {
                return new NetworkCredential(AppId, AppId);
            }
        }
    }
}