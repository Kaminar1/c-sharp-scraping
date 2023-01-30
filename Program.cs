//missing some of the font scripts. kind of presentable if fixing font, and relative paths.
using HtmlAgilityPack;

HtmlWeb web = new HtmlWeb();
HtmlDocument doc = web.Load(
    "https://norgesnett.no/aktuelt/elavgiften-er-lavere-i-januar-februar-og-mars-2023/"
);

// fix font, icons and relative paths

// add fontawesome import
var fontAwesomeImport = doc.CreateElement("link");
fontAwesomeImport.SetAttributeValue("rel", "stylesheet");
fontAwesomeImport.SetAttributeValue(
    "href",
    "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css"
);
doc.DocumentNode.PrependChild(fontAwesomeImport);

// add font to body
var body = doc.DocumentNode.SelectSingleNode("/html/head");
var bodyStyles = body.GetAttributeValue("style", null);
var separator = (bodyStyles == null ? null : "; ");
body.SetAttributeValue("style", bodyStyles + separator);

// nav items
var navBarUl = doc.DocumentNode.SelectSingleNode("//*[@id=\"x-site\"]/header/div[3]/div/div/ul");
var navItems = navBarUl.SelectNodes("li");
foreach (var li in navItems)
{
    var icon = li.SelectSingleNode("a/div/i");

    FixDownArrowIcons(icon);
}

// footer cards items
var footerCardContainer = doc.DocumentNode.SelectSingleNode(
    "//*[@id=\"x-site\"]/footer/div[2]/div/div/div/div"
);
var footerCards = footerCardContainer.SelectNodes("div[contains(@class, \"kolonnefooter\")]");
foreach (var card in footerCards)
{
    var icon = card.SelectSingleNode("div/div/span/i");

    FixDownArrowIcons(icon);
}

// min side icon
var minsideIcon = doc.DocumentNode.SelectSingleNode(
    "//*[@id=\"login_button-anchor-toggle\"]/div/span/i"
);
var classesProfile = minsideIcon.GetAttributeValue("class", "");
minsideIcon.SetAttributeValue("class", $"{classesProfile} fa-regular fa-user");
minsideIcon.Attributes.Remove("data-x-icon-s");
minsideIcon.Attributes.Remove("data-x-icon-o");

// footer contact phone
var footerContactPhone = doc.DocumentNode.SelectSingleNode(
    "//*[@id=\"x-site\"]/footer/div[3]/div/div/div[2]/div/a[1]/i"
);
var classesPhone = footerContactPhone.GetAttributeValue("class", "");
footerContactPhone.SetAttributeValue("class", $"{classesPhone} fa fa-phone");
footerContactPhone.Attributes.Remove("data-x-icon-s");
footerContactPhone.Attributes.Remove("data-x-icon-o");

// footer contact schema message
var footerContactMessage = doc.DocumentNode.SelectSingleNode(
    "//*[@id=\"x-site\"]/footer/div[3]/div/div/div[2]/div/a[2]/i"
);
var classesMessage = footerContactMessage.GetAttributeValue("class", "");
footerContactMessage.SetAttributeValue("class", $"{classesMessage} fa-regular fa-message");
footerContactMessage.Attributes.Remove("data-x-icon-s");
footerContactMessage.Attributes.Remove("data-x-icon-o");

// footer contact email
var footerContactEmail = doc.DocumentNode.SelectSingleNode(
    "//*[@id=\"x-site\"]/footer/div[3]/div/div/div[2]/div/a[3]/i"
);
var classesEmail = footerContactEmail.GetAttributeValue("class", "");
footerContactEmail.SetAttributeValue("class", $"{classesEmail} fa-regular fa-envelope");
footerContactEmail.Attributes.Remove("data-x-icon-s");
footerContactEmail.Attributes.Remove("data-x-icon-o");

//TODO: fix relative paths
var basePath = "https://norgesnett.no";
var images = doc.DocumentNode.SelectNodes("//img[starts-with(@src, '/wp-content')]");
foreach (var item in images)
{
    var relative = item.GetAttributeValue("src", "");
    System.Console.WriteLine(relative);
    item.SetAttributeValue("src", $"{basePath}{relative}");
}

//TODO: fix font
// - cera-round-pro, "Helvetica Neue", "Helvetica", "Arial", sans-serif

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





void FixDownArrowIcons(HtmlNode icon)
{
    icon.SetAttributeValue("class", "fa-solid fa-angle-down");
    icon.SetAttributeValue("style", "color: white;");
    icon.Attributes.Remove("data-x-icon-s");
    icon.Attributes.Remove("data-x-icon-o");
}
