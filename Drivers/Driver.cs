using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace D365Demo.Drivers
{
    public class Driver
    {
        private readonly Task<IPage> page;
        private IBrowser? browser;

        public Driver() => page = Task.Run(InitializePlaywright);

        public IPage Page => page.Result;

        public void Dispose() => browser?.CloseAsync();

        private async Task<IPage> InitializePlaywright()
        {
            var playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            return await browser.NewPageAsync();
        }
    }
}