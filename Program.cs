
//missing some of the font scripts. kind of presentable if fixing font, and relative paths.
// using HtmlAgilityPack;

// HtmlWeb web = new HtmlWeb();
// HtmlDocument doc = web.Load("https://norgesnett.no/aktuelt/elavgiften-er-lavere-i-januar-februar-og-mars-2023/");

// var content = doc.DocumentNode.InnerHtml;


// await File.WriteAllTextAsync("output/HtmlAgilityPack.html", content);


using PuppeteerSharp;

using var browserFetcher = new BrowserFetcher();
await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
var browser = await Puppeteer.LaunchAsync(new LaunchOptions
{
    Headless = true
});

using (var page = await browser.NewPageAsync())
{
    await page.GoToAsync("https://norgesnett.no/aktuelt/elavgiften-er-lavere-i-januar-februar-og-mars-2023/");
    
    await page.WaitForSelectorAsync("#x-site > header");
    
    var content = await page.GetContentAsync();

    await File.WriteAllTextAsync("output/PuppeteerSharp.html", content);
}


//cera-round-pro, "Helvetica Neue", "Helvetica", "Arial", sans-serif