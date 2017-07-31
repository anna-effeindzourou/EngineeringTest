namespace Lup.Software.Engineering.Tests.Controllers
{
	using Lup.Software.Engineering.Controllers;
	using Microsoft.AspNetCore.Mvc;
	using Xunit;
    public class HitControllerTest
    {
        private HitController controller;
        public HitControllerTest()
        {
            controller = new HitController();
        }

        [Fact]
		public void ApiShortenShouldReturnNullShortenOnBadUrl()
		{
            // Get the number of hit on this shorten
            ulong nbOfHitsBeforeHit = AzureTable.GetNumbeOfHit("www.google.com");

			this.controller.RedirectToExternalUrl("1234aaa");

			// Test that the 
            ulong nbOfHitsAfterHit = AzureTable.GetNumbeOfHit("www.google.com");
            Assert.Equal(nbOfHitsBeforeHit + 1, nbOfHitsAfterHit);
		}
    }
}
