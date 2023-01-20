
//missing some of the font scripts. kind of presentable if fixing font, and relative paths.
using HtmlAgilityPack;

HtmlWeb web = new HtmlWeb();
HtmlDocument doc = web.Load("https://norgesnett.no/aktuelt/elavgiften-er-lavere-i-januar-februar-og-mars-2023/");

// fix font, icons and relative paths

// add fontawesome import
var fontAwesomeImport = doc.CreateElement("link");
fontAwesomeImport.SetAttributeValue("rel", "stylesheet");
fontAwesomeImport.SetAttributeValue("href", "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css");
doc.DocumentNode.PrependChild(fontAwesomeImport);

// add font to body 
var body = doc.DocumentNode.SelectSingleNode("/html/head");
var bodyStyles = body.GetAttributeValue("style", null);
var separator = (bodyStyles == null ? null : "; ");
body.SetAttributeValue("style", bodyStyles + separator);

/** replace icons
* navbar
* footer
* <i class="fa-solid fa-angle-down" data-x-skip-scroll="true" aria-hidden="true" style="color: white;"></i>
*/
// var downArrowIconNodesParent = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div/header/div[3]/div/div/ul/li");
var downArrowIconNodes = doc.DocumentNode.SelectNodes("//i[contains(@class, 'x-anchor-sub-indicator')]");
// for (var i = 1; i <= 4; i++)
// {
//     var icon = doc.DocumentNode.SelectSingleNode($"//body/div[2]/div/header/div[3]/div/div/ul/li[{i}]/a/div/i");
    
//     // /html/body/div[2]/div/header/div[3]/div/div/ul/li[3]/a/div/i

//     icon.SetAttributeValue("class", "fa-solid fa-angle-down");
//     icon.SetAttributeValue("style", "color: white;");
//     icon.Attributes.Remove("data-x-icon-s");
//     icon.Attributes.Remove("data-x-icon-o");
// }


// var footerIsh = doc.DocumentNode.SelectSingleNode("div.x-bar-content > div.x-bar-container > div.x-row > div.x-row-inner:has(.kolonnefooter)");//*[@id="x-site"]/footer/div[2]/div/div/div/div
var footerIsh = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div/footer/div[2]/div/div/div/div"); 
// var footerIcons = footerIsh.SelectNodes("//i[@class='x-icon x-graphic-child x-graphic-icon x-graphic-primary']");
var footerIcons = footerIsh.SelectNodes("//i[contains(@class, 'x-icon') and contains(@class, 'x-graphic-child') and contains(@class, 'x-graphic-icon') and contains(@class, 'x-graphic-primary')]");

// /html/body/div[2]/div/footer/div[2]/div/div/div/div/div[1]/div[2]/div/span/i
// //*[@id="x-site"]/footer/div[2]/div/div/div/div/div[1]/div[2]/div/span/i


foreach (var icon in footerIcons)
{
    icon.SetAttributeValue("class", "fa-solid fa-angle-down");
    icon.SetAttributeValue("style", "color: white;");
    icon.Attributes.Remove("data-x-icon-s");
    icon.Attributes.Remove("data-x-icon-o");
}

var content = doc.DocumentNode.InnerHtml;


await File.WriteAllTextAsync("output/HtmlAgilityPack.html", content);


// using PuppeteerSharp;

// using var browserFetcher = new BrowserFetcher();
// await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
// var browser = await Puppeteer.LaunchAsync(new LaunchOptions
// {
//     Headless = true
// });

// using (var page = await browser.NewPageAsync())
// {
//     await page.GoToAsync("https://norgesnett.no/aktuelt/elavgiften-er-lavere-i-januar-februar-og-mars-2023/");
    
//     await page.WaitForSelectorAsync("#x-site > header");
    
//     var content = await page.GetContentAsync();

//     await File.WriteAllTextAsync("output/PuppeteerSharp.html", content);
// }


//cera-round-pro, "Helvetica Neue", "Helvetica", "Arial", sans-serif