using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {


        static void Main(string[] args)
        {
            my n = new my();
            UseSqlLite uql = new UseSqlLite();
            uql.LoadData();
            //n.DB(@"https://proklondike.net/books.html");
            Console.WriteLine("Test");
            Console.ReadLine();
        }


    }

    class my
    {
        
        public async Task DBR(string website)
        {
            A:

            HttpClient http = new HttpClient();
            var response = await http.GetByteArrayAsync(website);
            String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            source = WebUtility.HtmlDecode(source);
            HtmlDocument resultat = new HtmlDocument();
            resultat.LoadHtml(source);

            List<HtmlNode> toftitle = resultat.DocumentNode.Descendants().Where
            (x => (x.Name == "div" && x.Attributes["class"] != null &&
            x.Attributes["class"].Value.Contains("book-list-item"))).ToList();


            //var li = toftitle[6].Descendants("div").ToList();
            foreach (var item in toftitle)
            {
                var link = item.Descendants("a").ToList()[0].GetAttributeValue("href", null);
                var img = item.Descendants("img").ToList()[0].GetAttributeValue("src", null);
                var title = item.Descendants("h3").ToList()[0].InnerText;
                Console.WriteLine($"Link: {link}; IMG: {img}, TITLE: {title}"  );
            }

            for (int i = 2; i < 129; i++)
            {
                website = $"/books/page/{i}.html";
                if (website != "https://proklondike.net/books.html")
                {
                    website = $"https://proklondike.net/books/page/{i}.html";
                    goto A;
                }
            }

        }

        


    }
}
