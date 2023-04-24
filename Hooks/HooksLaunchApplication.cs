﻿using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using D365Demo.Drivers;
using D365Demo.PageObjects;
using D365Demo.Utilities;

namespace D365Demo.Hooks
{
    [Binding]
    public class HooksLaunchApplication
    {
        public readonly Driver _driver;
        private readonly PageObjectAccountEntity pageObjectAccountEntity;

        public HooksLaunchApplication(Driver driver)
        {
            _driver = driver;
            pageObjectAccountEntity = new PageObjectAccountEntity(_driver.Page);
        }

        [BeforeScenario]
        public async Task Setup()
        {
            _driver.Page.SetDefaultTimeout(60000);
            await _driver.Page.GotoAsync("https://org2b94446f.crm4.dynamics.com/main.aspx");
            await pageObjectAccountEntity.Login("admin@CRM253142.onmicrosoft.com", "^9eM(LFc872yK$~s");
            var signInLocator = pageObjectAccountEntity.StaySigned();
            Console.WriteLine("Stay signed popup status - " + signInLocator);
            if (signInLocator.Equals(true))
            {
                Console.WriteLine("Stay signed popup displayed");
                await pageObjectAccountEntity.StaysignedLabel();
                await pageObjectAccountEntity.StaySignedClickBtn();
            }
            else
            {
                Console.WriteLine("No stay signed pop up not displayed");
                await pageObjectAccountEntity.StaysignedLabel();
                await pageObjectAccountEntity.StaySignedClickBtn();
            }
        }

        [AfterScenario]
        public void TearDown()
        {
            _driver.Dispose();
        }
    }
}
