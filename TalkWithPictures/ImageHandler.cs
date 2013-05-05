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
        BlobImageRepository m_blobStorage = new BlobImageRepository();

        public ImageHandler() : base() { }

        public ImageHandler(RequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            // Parse out image name and other details
            ImageRequest imageRequest = GetImageRequest(context);

            // Check if image is already in cloud blob by checking DB
            // TODO:

            // If it's not in the blob, search for image
            var uriOfImage = GetFirstSearchURL(imageRequest);
            // Download image from search results
            MemoryStream downloadedFile = DownloadRemoteImageFile(uriOfImage);
            
            // Serve up image from cloud blob, masked as original image
            ReturnDownloadedImage(downloadedFile, context, imageRequest);

            // Store image in cloud blob
            StoreImageInBlob(imageRequest, downloadedFile);
            // Store record in DB with link back to blob for future searches
            // TODO:

            // TEST CODE TO SEE IF IMAGE RETURN IS WORKING
            //ReturnTestImage(context);
        }

        private void StoreImageInBlob(ImageRequest imageRequest, MemoryStream downloadedFile)
        {
            downloadedFile.Seek(0, SeekOrigin.Begin);
            var imageLocation = m_blobStorage.Save("image/" + imageRequest.Extension, downloadedFile).AbsoluteUri;
        }

        private void ReturnDownloadedImage(Stream downloadedFile, HttpContext context, ImageRequest request)        
        {
            using (var image = Image.FromStream(downloadedFile))
            using (var bmp = new Bitmap(image.Width, image.Height))
            using (var gr = Graphics.FromImage(bmp))
            {

                gr.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
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

        private MemoryStream DownloadRemoteImageFile(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
                // Something bad happened so just die.
                // TODO: This could log somewhere.
                return null;
            }

            // Check that the remote file was found and that it is an image
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {

                // if the remote file was found, download it
                // returning as memory stream so we have something to work with
                // TODO: there must be a better way to do this
                var stream = response.GetResponseStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                memoryStream.Position = 0;
                return memoryStream;
            }
            else
                return null;
        }


    }
}
