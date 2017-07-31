namespace Lup.Software.Engineering             
{
	using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class HitController : Controller
    {
		[HttpGet("{shortUrl}")]
		public IActionResult RedirectToExternalUrl(string shortUrl)
		{
            // Verify that this Url is in the database
            string originalUrl = AzureTable.GetOriginalUrl(shortUrl);

			// If the shorten is correct
			// Increment the number of hits for this shorten
			// And redirect the user to the original Url 
			if (originalUrl == null)
			{
				return Redirect("api/error");
			}
			else
			{
                AzureTable.IncrementHit(shortUrl);
                return Redirect(originalUrl);
			}
		}
    }
}
