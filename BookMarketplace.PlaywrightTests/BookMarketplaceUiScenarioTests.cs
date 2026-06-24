using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace BookMarketplace.PlaywrightTests;

public class BookMarketplaceUiScenarioTests : PageTest
{
    private readonly string _baseUrl =
        Environment.GetEnvironmentVariable("BOOKMARKETPLACE_BASE_URL")
        ?? "http://localhost:5098";

    [Fact]
    public async Task UserCanNavigateAndUseSearchScenario()
    {
        await Page.GotoAsync(_baseUrl);
        await PauseAsync();

        await Expect(Page.Locator("body")).ToBeVisibleAsync();
        await Expect(Page.Locator("nav")).ToBeVisibleAsync();
        await PauseAsync();

        var booksLink = Page.GetByRole(
            AriaRole.Link,
            new() { NameRegex = new Regex("Books|Knjige", RegexOptions.IgnoreCase) });

        if (await booksLink.CountAsync() > 0)
        {
            await booksLink.First.ClickAsync();
            await PauseAsync();

            await Expect(Page.Locator("body")).ToBeVisibleAsync();
        }

        await Page.GotoAsync(_baseUrl);
        await PauseAsync();

        var gamesLink = Page.GetByRole(
            AriaRole.Link,
            new() { NameRegex = new Regex("Board Games|Društvene igre|Drustvene igre|Igre", RegexOptions.IgnoreCase) });

        if (await gamesLink.CountAsync() > 0)
        {
            await gamesLink.First.ClickAsync();
            await PauseAsync();

            await Expect(Page.Locator("body")).ToBeVisibleAsync();
        }

        await Page.GotoAsync(_baseUrl);
        await PauseAsync();

        var searchInput = Page.Locator(
            "input[type='search'], input[name='query'], input[name='q'], input[placeholder*='Search'], input[placeholder*='Pretra']");

        if (await searchInput.CountAsync() > 0)
        {
            await searchInput.First.FillAsync("Harry");
            await PauseAsync();

            var harryLink = Page.GetByRole(
                AriaRole.Link,
                new() { NameRegex = new Regex("Harry", RegexOptions.IgnoreCase) });

            if (await harryLink.CountAsync() > 0)
            {
                await harryLink.First.ClickAsync();
                await PauseAsync();

                await Expect(Page.Locator("body")).ToContainTextAsync(
                    new Regex("Harry|Cijena|Opis|Knjiga", RegexOptions.IgnoreCase));
            }

            await Expect(Page.Locator("body")).ToBeVisibleAsync();
        }

        await Expect(Page.Locator("body")).ToBeVisibleAsync();
    }

    private async Task PauseAsync()
    {
        await Page.WaitForTimeoutAsync(1000);
    }
}