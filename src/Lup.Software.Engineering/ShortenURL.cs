namespace Lup.Software.Engineering
{
	using System.Collections.Generic;

    // Class to create a shorten from a Url
    public class ShortenURL
    {
        public static string Create(string originalURL)
        {
            // List of possible character for the shorten
            List<string> alphaNum = new List<string>()
            {
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
                "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F",
                "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
                "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
            };

            // Transform indice (base 10) to base 62
            ulong idUrl = AzureTable.GetIndice();
            int r = (int)(idUrl % 62);
            ulong q = idUrl / 62;
            string shortUrl = alphaNum[r];

            int l = 1;
            while (q > 62)
            {
                q = q / 62;
                r = (int)(q % 62);
                shortUrl += alphaNum[r];
                l += 1;
            }

            shortUrl += alphaNum[(int)q];
            for (int i = l; i <= 7; i++)
            {
                shortUrl += alphaNum[0];
            }

            // Store in database
            AzureTable.InsertUrlDrawer(new UrlDrawer(originalURL, shortUrl));

            return shortUrl;
        }
    }
}
