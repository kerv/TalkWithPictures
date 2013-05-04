﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TalkWithPictures
{
    public class BlobImageRepository : IImageRepository
    {
        public Uri Save(string contentType, System.IO.Stream inputStream, string blobContainer = "searchimages")
        {
            var container = GetContainer(blobContainer);
            var blockBlob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            blockBlob.Properties.ContentType = contentType;
            blockBlob.UploadFromStream(inputStream);
            return blockBlob.Uri;

        }
        private static CloudBlobContainer GetContainer(string blobContainer)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(blobContainer);//must be lowercase
            container.CreateIfNotExists();

            SetPublicPermissions(container);

            return container;
        }
        private static void SetPublicPermissions(CloudBlobContainer container)
        {
            var permissions = new BlobContainerPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            container.SetPermissions(permissions);
        }
    }
}