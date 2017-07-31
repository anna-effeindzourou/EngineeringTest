namespace Lup.Software.Engineering
{
    // I should maybe use a JSON parser for this class to be returned as header of the GET and POST
    public class ResponseHeader
    {
        public ResponseHeader()
		{
			Errors = new Error();
		}
		public string OriginalUrl { get; set; }
		public string ShortUrl { get; set; }
		public Error Errors { get; set; }
    }
	public class Error
	{
		public string OriginalUrl { get; set; }
	}
}
