namespace Lup.Software.Engineering.Tests.Controllers
{
	using Lup.Software.Engineering.Controllers;
	using Microsoft.AspNetCore.Mvc;
	using Xunit;

	public class ShortenControllerTests
	{
		private CreateShortenURLController controller;

		public ShortenControllerTests()
		{
			this.controller = new CreateShortenURLController();
		}

		[Fact]
		public void ApiShortenShouldReturnResponseHeader()
		{
            var result = this.controller.ShortenUrl("www.google.com");

			Assert.IsType<ResponseHeader>(result);
            Assert.Equal(result.OriginalUrl, "www.google.com");
            // Should also test that the shorten is correct and stored in the database
		}

		[Fact]
		public void ApiShortenShouldReturnNullShortenOnBadUrl()
		{
            var result = this.controller.ShortenUrl("ivwEVFABHVIERYGVAY");

            Assert.IsType<ResponseHeader>(result);
            Assert.Equal(result.OriginalUrl, "ivwEVFABHVIERYGVAY");
            Assert.Equal(result.ShortUrl, string.Empty);
            Assert.Equal(result.Errors.OriginalUrl, "The OriginalUrl field is not a valid fully-qualified http,   https,  or ftp URL");
		}
	}
}
