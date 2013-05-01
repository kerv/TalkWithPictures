using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TalkWithPictures
{
    public interface IImageRequest
    {
        string SearchTerm { get; set; }
        string Url { get; }

        IEnumerable<PictureInformation> Parse(string xml);

        ICredentials Credentials { get; }
    }
}
