using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalkWithPictures
{
    public class ImageRequest
    {
        public string Extension { get; set; }
        public IEnumerable<string> SearchTerms { get; set; }

        public ImageRequest()
        {
        }
    }
}
