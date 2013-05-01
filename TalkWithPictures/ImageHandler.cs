using Bing;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Web;
using System.Web.Routing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TalkWithPictures
{
    /// <summary>
    /// Handles all JPG, PNG, or GIF images requested for http://mydomain.com/*.jpg|png|gif
    /// </summary>
    public class ImageHandler : IHttpHandler
    {
        public bool IsReusable { get { return false; } }
        protected RequestContext RequestContext { get; set; }

        public ImageHandler() : base() { }

        public ImageHandler(RequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            // Parse out image name and other details
            ImageRequest imageRequest = GetImageRequest(context);

            // Check if image is already in cloud blob

            // Search for image
            var uriOfImage = GetFirstSearchURL(imageRequest);
            // Download image
            Stream downloadedFile = DownloadRemoteImageFile(uriOfImage);
            // Store image in cloud blob
            // Serve up image from cloud blob, masked as original image
            ReturnDownloadedImage(downloadedFile, 400, 400, context, imageRequest);

            // TEST CODE TO SEE IF IMAGE RETURN IS WORKING
            //ReturnTestImage(context);
        }

        private void ReturnDownloadedImage(Stream downloadedFile, int width, int height, HttpContext context, ImageRequest request)
        {
            using (var image = Image.FromStream(downloadedFile))
            using (var bmp = new Bitmap(width, height))
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.DrawImage(image, new Rectangle(0, 0, width, height));
                context.Response.ContentType = "image/png";
                bmp.Save(context.Response.OutputStream, ImageFormat.Png);
            }
        }

        #region Test Image

        /// <summary>
        /// FROM http://www.hanselman.com/blog/BackToBasicsDynamicImageGenerationASPNETControllersRoutingIHttpHandlersAndRunAllManagedModulesForAllRequests.aspx
        /// </summary>
        /// <param name="context"></param>
        private static void ReturnTestImage(HttpContext context)
        {
            using (var rectangleFont = new Font("Arial", 14, FontStyle.Bold))
            using (var bitmap = new Bitmap(320, 110, PixelFormat.Format24bppRgb))
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var backgroundColor = Color.Bisque;
                g.Clear(backgroundColor);
                g.DrawString("This PNG was totally generated", rectangleFont, SystemBrushes.WindowText, new PointF(10, 40));
                context.Response.ContentType = "image/png";
                bitmap.Save(context.Response.OutputStream, ImageFormat.Png);
            }
        }

        #endregion

        /// <summary>
        /// Parses out context request to see what the user was looking for.
        /// 
        /// TODO: Add image resolution and other stuff as allowed params.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>image
        private ImageRequest GetImageRequest(HttpContext context)
        {
            var fileNameWithoutAppPath = context.Request.CurrentExecutionFilePath.TrimStart(context.Request.ApplicationPath);
            var fileNameWithoutExtension = fileNameWithoutAppPath.TrimEnd(context.Request.CurrentExecutionFilePathExtension);
            var searchQueries = fileNameWithoutExtension.Split('_'); // <-- HAHA a '_' face! *cute*

            ImageRequest image = new ImageRequest()
                {
                    Extension = context.Request.CurrentExecutionFilePathExtension.TrimStart('.'), // remove leading period
                    SearchTerms = searchQueries
                };

            return image;
        }

        public string GetFirstSearchURL(ImageRequest request)
        {
            var searchTerm = string.Join(" ", request.SearchTerms);

            var bingRequest = new BingRequest();
            bingRequest.SearchTerm = searchTerm;

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(bingRequest.Url);
            httpRequest.MaximumAutomaticRedirections = 4;
            httpRequest.MaximumResponseHeadersLength = 4;
            httpRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["BingSearchAPIKey"], ConfigurationManager.AppSettings["BingSearchAPIKey"]);
            HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();

            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();

            string content;
            using (StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8))
            {
                content = reader.ReadToEnd();
            }

            var images = bingRequest.Parse(content);
            
            return images.First().Url;

        }

        private Stream DownloadRemoteImageFile(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
                return null;
            }

            // Check that the remote file was found. The ContentType
            // check is performed since a request for a non-existent
            // image file might be redirected to a 404-page, which would
            // yield the StatusCode "OK", even though the image was not
            // found.
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {

                // if the remote file was found, download it
                return response.GetResponseStream();
            }
            else
                return null;
        }


    }
}
