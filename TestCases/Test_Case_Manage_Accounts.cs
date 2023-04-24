//using D365Demo.Drivers;
using D365Demo.Drivers;
using D365Demo.PageObjects;
using D365Demo.TestData;
using D365Demo.Utilities;
using Dynamitey.DynamicObjects;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Linq.Expressions;
using System.Text.Json;

namespace D365Demo.TestCases
{

    public class Test_Case_Manage_Accounts
    {

        private readonly PageObjectAccountEntity pageObjectAccountEntity;
        private readonly Test_Data_Accounts? accountsData;
        private readonly IPage Page;
        private readonly FormatJsonFile formatJsonFile;

        public Test_Case_Manage_Accounts(IPage Page)
        {
            this.Page = Page;
            formatJsonFile = new FormatJsonFile();
            String MyProjectDir = formatJsonFile.DirProject();
            String filePath = MyProjectDir + "\\TestData\\JsonFiles\\Cases.json";
            pageObjectAccountEntity = new PageObjectAccountEntity(Page);
            string text = File.ReadAllText(filePath);
            accountsData = JsonSerializer.Deserialize<Test_Data_Accounts>(text);
        }

        public async Task NavigateToSalesHubApp()
        {
            await pageObjectAccountEntity.NavigateSalesHub();
        }

        public async Task NavigateToAnEntity(String entityName)
        {
            switch (entityName.ToUpper())
            {
                case "ACCOUNTS":
                    Console.WriteLine("Navigating to Accounts Entity");
                    await pageObjectAccountEntity.NavigateAccounts();
                    break;
                case "CONTACTS":
                    Console.WriteLine("Navigating to ContactsAccounts Entity");
                    break;
                case "CASES":
                    Console.WriteLine("Navigating to Cases Entity");
                    await pageObjectAccountEntity.NavigateCases();
                    break;
                default:
                    Console.WriteLine("No matching data");
                    break;
            }
        }

        public async Task CreateAccount()
        {
            Console.WriteLine(accountsData!.AccountName);
            await pageObjectAccountEntity.AddNewAccount();
            await pageObjectAccountEntity.EnterAccountName(accountsData.AccountName);
            await pageObjectAccountEntity.EnterParentAccountName(accountsData.ParentAccountName);
            await pageObjectAccountEntity.ClickParentSearch();
            await pageObjectAccountEntity.WaitParentPnlVisiblity();
            await pageObjectAccountEntity.GetParentAccountByText(accountsData.ParentAccountName);
            await pageObjectAccountEntity.SaveForm();
            Thread.Sleep(6000);
        }

        public async Task SearchAndDeleteAccount()
        {
            Console.WriteLine("Search and delete account - " + accountsData!.AccountName);
            await pageObjectAccountEntity.NavigateAccounts();
            await pageObjectAccountEntity.ClickAccountSearchField();
            await pageObjectAccountEntity.SearchAccount(accountsData.AccountName);
            await pageObjectAccountEntity.ClickAccountSearch();
            await pageObjectAccountEntity.SelectAccountSearch(accountsData.AccountName);
            String accountNameField = await pageObjectAccountEntity.GetAccountName().InnerTextAsync();
            Console.WriteLine("The name of the account - " + accountNameField);
            await pageObjectAccountEntity.ClickMoreCommands();
            await pageObjectAccountEntity.ClickDelete();
            await pageObjectAccountEntity.ClickConfirm();
            Thread.Sleep(3000);
        }
    }
}
