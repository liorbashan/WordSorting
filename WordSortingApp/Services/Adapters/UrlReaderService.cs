using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordSortingApp.Interfaces;

namespace WordSortingApp.Services
{
    public class UrlReaderService : ITextCollector
    {
        public void ReadAndAddToCollection(string[] source)
        {
            GetTextFromUrl(source);
        }

        private void GetTextFromUrl(string[] sources)
        {
            var words = new BlockingCollection<string>();

            var getWebPageContent = Task.Run(async () =>
            {
                try
                {
                    foreach (var url in sources)
                    {
                        try
                        {
                            using (var client = new HttpClient())
                            {
                                var content = await client.GetStringAsync(url);
                                string clean = HTMLToText(content);
                                string decodedText = System.Net.WebUtility.HtmlDecode(clean).Trim();

                                string[] wordsInLine = decodedText.Split(' ');
                                foreach (var singleWord in wordsInLine)
                                {
                                    // Hand over to addWordsToWordCounter and continue reading.
                                    words.Add(singleWord);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                }
                finally
                {
                    words.CompleteAdding();
                }
            });

            var addWordsToWordCounter = Task.Run(() =>
            {
                foreach (var word in words.GetConsumingEnumerable())
                {
                    WordCounterService.AddWordToCollection(word);
                }
            });

            Task.WaitAll(getWebPageContent, addWordsToWordCounter);
        }

        public string HTMLToText(string HTMLCode)
        {
            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
            Regex bodyTagRegex = new Regex("<body.*>(?<theBody>.*)</body>", options);
            Match match = bodyTagRegex.Match(HTMLCode);
            if (match.Success)
            {
                HTMLCode = match.Groups[0].Value;
            }

            const string tagWhiteSpace = @"\t|\n|\r";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, options);
            var stripFormattingRegex = new Regex(stripFormatting, options);

            //Decode html specific characters
            HTMLCode = System.Net.WebUtility.HtmlDecode(HTMLCode);
            HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", "", options);
            HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", "", options);
            HTMLCode = Regex.Replace(HTMLCode, "<style.*?</style>", "", options);
            HTMLCode = Regex.Replace(HTMLCode, tagWhiteSpace, " ");
            HTMLCode = lineBreakRegex.Replace(HTMLCode, Environment.NewLine);
            HTMLCode = stripFormattingRegex.Replace(HTMLCode, string.Empty);

            return HTMLCode;



            //// Remove new lines since they are not visible in HTML
            //HTMLCode = HTMLCode.Replace("\n", " ");

            //// Remove tab spaces
            //HTMLCode = HTMLCode.Replace("\t", " ");

            //// Remove multiple white spaces from HTML
            //HTMLCode = Regex.Replace(HTMLCode, "\\s+", " ");

            //// Remove HEAD tag
            //HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            //// Remove any JavaScript
            //HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            //HTMLCode = Regex.Replace(HTMLCode, "<style.*?</style>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            //// Replace special characters like &, <, >, " etc.
            //StringBuilder sbHTML = new StringBuilder(HTMLCode);
            //// Note: There are many more special characters, these are just
            //// most common. You can add new characters in this arrays if needed
            //string[] OldWords = { "&nbsp;", "&amp;", "&quot;", "&lt;", "&gt;", "&reg;", "&copy;", "&bull;", "&trade;" };
            //string[] NewWords = { " ", "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢" };
            //for (int i = 0; i < OldWords.Length; i++)
            //{
            //    sbHTML.Replace(OldWords[i], NewWords[i]);
            //}

            //// Check if there are line breaks (<br>) or paragraph (<p>)
            //sbHTML.Replace("<br>", "\n<br>");
            //sbHTML.Replace("<br ", "\n<br ");
            //sbHTML.Replace("<p ", "\n<p ");

            //// Finally, remove all HTML tags and return plain text
            //return Regex.Replace(sbHTML.ToString(), "<[^>]*>", "");
        }
    }
}
