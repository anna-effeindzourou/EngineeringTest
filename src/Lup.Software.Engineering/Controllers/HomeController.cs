namespace Lup.Software.Engineering.Controllers             
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Configuration;

	public class HomeController : Controller
    {
        private AzureTable m_aTable;

        public HomeController(IConfiguration configuration)
        {
            // Instance of our Azure Table
            m_aTable = new AzureTable(configuration);
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Route("error")]
        public IActionResult Error()
        {
            return this.View();
        }
    }
}