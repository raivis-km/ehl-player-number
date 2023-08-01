using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ehl_numbers
{
    public static class Utilities
    {
        public static List<string> GetTeamUrls()
        {
            var listMatches = new List<string>();

            using (WebClient client = new WebClient())
            {
                string komandasHtml = client.DownloadString("https://ehl.entuziasti.com/komandas");
                string pattern = @"\""\/komandas\/(([a-z]|-)+)\/\d+\""";

                var matches = Regex.Matches(komandasHtml, pattern);

                foreach (Match match in matches)
                {
                    if (match.Success && match.Groups.Count > 0)
                    {
                        var url = match.Value;
                        listMatches.AddUnique(url.Substring(2, url.Length - 3));
                    }
                }
            }

            return listMatches;
        }

        public static List<Number> GetTeamNumbers(string teamUrl)
        {
            var result = new List<Number>();

            using (WebClient client = new WebClient())
            {
                string komandasHtml = client.DownloadString($"https://ehl.entuziasti.com/{teamUrl}");
                var patern = @"<a href=\""\/personas\/(([a-z]|-)+)\/\d+\/\d+"">\d+</a>";

                var matches = Regex.Matches(komandasHtml, patern);

                foreach (Match match in matches)
                {
                    if (match.Success && match.Groups.Count > 0)
                    {
                        var numberMatch = Regex.Match(match.Value, @">\d+<");
                        if (numberMatch.Success)
                        {
                            var number = numberMatch.Value.Substring(1, numberMatch.Value.Length - 2);
                            var numberInt = int.Parse(number);

                            if (numberInt > 0)
                                result.Add(new Number()
                                {
                                    number = numberInt,
                                    name = match.Groups[1].Value
                                });
                        }
                    }
                }
            }
            return result;
        }

        private static void AddUnique(this List<string> list, string url)
        {
            if (!list.Contains(url))
                list.Add(url);
        }
    }
}
