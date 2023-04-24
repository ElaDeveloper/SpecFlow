using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace D365Demo.TestCases
{
    public class Test_Case_Create_Account_Single
    {
        //[Test]
        public async Task CreateAccount()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            var page = await browser.NewPageAsync();
            page.SetDefaultTimeout(60000);
            await page.GotoAsync("https://org2b94446f.crm4.dynamics.com/main.aspx");
            Console.WriteLine("Launching the browser");
            await page.FillAsync(selector: "input[type='email']", "admin@CRM253142.onmicrosoft.com");
            await page.ClickAsync(selector: "input[type='submit'][class*='primary']");
            await page.FillAsync(selector: "input[type='password']", "^9eM(LFc872yK$~s");
            await page.ClickAsync(selector: "input[type='submit'][class*='primary']");
            Console.WriteLine("Entered credentials");
            var signInLocator = await page.Locator(selector: "#lightbox").IsVisibleAsync();
            Console.WriteLine("Stay signed popup status - " + signInLocator);
            if (signInLocator == true)
            {
                Console.WriteLine("Stay signed popup displayed");
                await page.GetByLabel("Don't show this again").CheckAsync();
                await page.ClickAsync(selector: "input[type='submit']");
            }
            else
            {
                Console.WriteLine("No stay signed pop up not displayed");
                await page.GetByLabel("Don't show this again").CheckAsync();
                await page.ClickAsync(selector: "input[type='submit']");
            }
            var locator = page.FrameLocator(selector: "#AppLandingPage");
            await locator.Locator("div[data-lp-id*='saleshub'][role='listitem']").ClickAsync();
            await page.ClickAsync("div[title='Accounts']");
            await page.ClickAsync("button[aria-label='New']");
            await page.FillAsync(selector: "input[aria-label='Account Name']", "Ela-Example1");
            await page.FillAsync(selector: "input[aria-label='Parent Account, Lookup']", "A Datum Corporation");
            await page.ClickAsync(selector: "button[data-id='parentaccountid.fieldControl-LookupResultsDropdown_parentaccountid_search']");
            await page.Locator(selector: "div[aria-label='Dropdown panel']").IsVisibleAsync();
            await page.GetByText("A Datum Corporation").ClickAsync();
            await page.ClickAsync(selector: "button[aria-label='Save (CTRL+S)']");
            await page.ClickAsync("div[title='Accounts']");
            await page.ClickAsync(selector: "input[aria-label = 'Account Filter by keyword']");
            await page.FillAsync(selector: "input[aria-label = 'Account Filter by keyword']", "Ela-Example1");
            await page.ClickAsync(selector: "button[aria-label='Start search']");
            await page.ClickAsync(selector: "//span[text()='Ela-Example1']");
            String accountName = await page.Locator(selector: "input[aria-label='Account Name']").InnerTextAsync();
            Console.WriteLine("The name of the account - " + accountName);
            await page.ClickAsync(selector: "button[aria-label='More commands for Account']");
            await page.ClickAsync(selector: "button[aria-label='Delete']");
            await page.ClickAsync(selector: "button[data-id='confirmButton']");
            Thread.Sleep(3000);
            await browser.CloseAsync();
        }

        public String DirProject()
        {
            String DirDebug = System.IO.Directory.GetCurrentDirectory();
            String DirProject = DirDebug;

            for (int counter_slash = 0; counter_slash < 3; counter_slash++)
            {
                DirProject = DirProject.Substring(0, DirProject.LastIndexOf(@"\"));
            }

            return DirProject;
        }

        [Test]
        public void ExamplePath()
        {
            Console.WriteLine("Hello path");
            String MyProjectDir = DirProject();
            String filePath = MyProjectDir + "\\TestData\\JsonFiles\\Cases.json";
            String inputFilePath = File.ReadAllText(filePath);
            Console.WriteLine("Hello1123 - " + inputFilePath);            
        }
    }
}
