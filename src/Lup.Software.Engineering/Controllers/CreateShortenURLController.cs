namespace Lup.Software.Engineering.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/shorten")]
    public class CreateShortenURLController : Controller
    {
        [HttpPost]
        [Route("api/shorten")]
        public ResponseHeader ShortenUrl([FromBody]string originalUrl)
        {
            ResponseHeader response = new ResponseHeader();

            // Check whether the url is valid or not.
            // If the url is invalid, return the failure message. 
            // Else return the shorten url
            bool validUrl = CheckURLValid(originalUrl);
            if (validUrl == false)
            {
                response.ShortUrl = null;
                response.OriginalUrl = originalUrl;
                response.Errors.OriginalUrl = "The OriginalUrl field is not a valid fully-qualified http,   https,  or ftp URL";
                return response;
            }

            // Check that orginalUrl is not already in the database
            string shorten = AzureTable.GetShorten(originalUrl);
            if (shorten != null)
            {
				response.ShortUrl = shorten;
				response.OriginalUrl = originalUrl;
				response.Errors = null;

                return response;
            }

            // Create a shorten for this Url -> saved in the database
            // Fill the response Header
            shorten = ShortenURL.Create(originalUrl);
            response.ShortUrl = shorten;
            response.OriginalUrl = originalUrl;
            response.Errors = null;

            return response;
        }

        // Check if the Url is Valid
        public bool CheckURLValid(string strURL)
        {
            return Uri.IsWellFormedUriString(strURL, UriKind.RelativeOrAbsolute); ;
        }

        [HttpGet]
        [Route("api/error")]
        public string Error()
        {
            return "404 Error";
        }
    }
}
