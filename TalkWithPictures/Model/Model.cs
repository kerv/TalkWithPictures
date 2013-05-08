using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TalkWithPictures.Model
{
    public class PictureContext : DbContext
    {
        public PictureContext() : base(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()) { }
        public DbSet<ImageStore> Pictures { get; set; }
    }

    public class ImageStore
    {
        public ImageStore() { ImageStoreKey = Guid.NewGuid().ToString(); }

        [Key]
        public string ImageStoreKey { get; set; }

        public string SearchTermString { get; set; }
        public string Extension { get; set; }
        public string BlobURL { get; set; }
        public int Index { get; set; }
    }

}