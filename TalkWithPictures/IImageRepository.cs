using System;
using System.Collections.Generic;
using System.Linq;

namespace TalkWithPictures
{
    public interface IImageRepository
    {
        Uri Save(string contentType, System.IO.Stream inputStream, string blobContainer = "");
    }
}
