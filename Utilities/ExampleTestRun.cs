using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.Playwright;
using System.IO;
using D365Demo.TestCases;
using D365Demo.Drivers;
using D365Demo.PageObjects;
using D365Demo.Hooks;

namespace D365Demo.Utilities
{
    public class ExampleTestRun
    {
        public readonly Driver driver;
        private readonly PageObjectCaseEntity pageObjectCaseEntity;
        private readonly PageObjectAccountEntity pageObjectAccountEntity;
        private readonly Test_Case_Manage_Accounts test_Case_Manage_Accounts;
        private readonly Test_Case_Manage_Cases test_Case_Manage_Cases;

        public ExampleTestRun()
        {
            driver = new Driver();
            pageObjectCaseEntity = new PageObjectCaseEntity(driver.Page);
            pageObjectAccountEntity = new PageObjectAccountEntity(driver.Page);
            test_Case_Manage_Accounts = new Test_Case_Manage_Accounts(driver.Page);
            test_Case_Manage_Cases = new Test_Case_Manage_Cases(driver.Page);
        }

        [SetUp]
        public async Task Setup()
        {
            Console.WriteLine("Setting up the browser");
            driver.Page.SetDefaultTimeout(60000);
            await driver.Page.GotoAsync("https://org2b94446f.crm4.dynamics.com/main.aspx");
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
            await test_Case_Manage_Accounts.NavigateToSalesHubApp();
        }

        [Test]
        public async Task ManageCase()
        {
            Console.WriteLine("Executing creating case scenario");
            await test_Case_Manage_Cases.CreateCase();
            Console.WriteLine("Searching for the case created already");
            await test_Case_Manage_Cases.SearchCase();
            await test_Case_Manage_Cases.VerifyCaseAttributeValues();
            await test_Case_Manage_Cases.AddCaseToQueue();
            await test_Case_Manage_Cases.AssignCaseToUser();
            await test_Case_Manage_Cases.DeleteCase();
            Console.WriteLine("Completed case management");
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("Closing the browser");
            driver.Dispose();
        }
    }
}