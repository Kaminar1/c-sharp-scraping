
//missing some of the font scripts. kind of presentable if fixing font, and relative paths.
using HtmlAgilityPack;

HtmlWeb web = new HtmlWeb();
HtmlDocument doc = web.Load("https://norgesnett.no/aktuelt/elavgiften-er-lavere-i-januar-februar-og-mars-2023/");

var content = doc.DocumentNode.InnerHtml;

await File.WriteAllTextAsync("HtmlAgilityPack.html", content);

