using D365Demo.PageObjects;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using D365Demo.Hooks;
using D365Demo.TestData;
using D365Demo.Utilities;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace D365Demo.TestCases
{
    public class Test_Case_Manage_Cases : PageTest
    {
        private readonly PageObjectCaseEntity pageObjectCaseEntity;
        private readonly PageObjectAccountEntity pageObjectAccountEntity;
        private readonly Test_Data_Cases? caseData;
        private readonly IPage iPage;
        private readonly Test_Case_Manage_Accounts test_Case_Manage_Accounts;
        private String? caseIDGenerated;
        private String filePath;
        private readonly FormatJsonFile formatJsonFile;

        public Test_Case_Manage_Cases(IPage Page)
        {
            iPage = Page;
            pageObjectCaseEntity = new PageObjectCaseEntity(iPage);
            pageObjectAccountEntity = new PageObjectAccountEntity(iPage);
            filePath = "C:\\Ela\\PlayWright\\SpecFlowLearn\\TestData\\JsonFiles\\Cases.json";
            String inputFilePath = File.ReadAllText(filePath);
            caseData = JsonSerializer.Deserialize<Test_Data_Cases>(inputFilePath);
            test_Case_Manage_Accounts = new Test_Case_Manage_Accounts(iPage);
            formatJsonFile = new FormatJsonFile();
        }

        public async Task SelectSubject(String subject)
        {
            await pageObjectCaseEntity.ClickSubjectDrpDwn();
            await pageObjectCaseEntity.ExpandSubjectValues();
            await pageObjectCaseEntity.SelectSubject(subject);
        }

        public async Task SelectCustomer(String customerName)
        {
            await pageObjectCaseEntity.TxtInputCustomerName.FillAsync(customerName);
            await pageObjectCaseEntity.ClickCustomerSearch();
            await pageObjectCaseEntity.WaitCustomerPnlVisiblity();
            await pageObjectCaseEntity.GetCustomerByText(customerName);
        }

        public async Task SelectContact(String contactEmail)
        {
            await pageObjectCaseEntity.EnterContact(contactEmail);
            await pageObjectCaseEntity.ClickContactSearch();
            await pageObjectCaseEntity.WaitCustomerPnlVisiblity();
            await pageObjectCaseEntity.GetContactByText(contactEmail);
        }

        public async Task SelectProduct(String productName)
        {
            await pageObjectCaseEntity.EnterProduct(productName);
            await pageObjectCaseEntity.ClickProductSearch();
            await pageObjectCaseEntity.WaitCustomerPnlVisiblity();
            await pageObjectCaseEntity.GetProductByText(productName);
        }

        public async Task SelectQueue(String queueName)
        {
            await pageObjectCaseEntity.EnterQueue(queueName);
            await pageObjectCaseEntity.ClickQueueSearch();
            await pageObjectCaseEntity.WaitCustomerPnlVisiblity();
            await pageObjectCaseEntity.SelectQueueFromPanel(queueName);
        }

        public async Task SelectAssingee(String asigneeType, String asigneeTeamName)
        {
            await pageObjectCaseEntity.SelectAssingee(asigneeType);
            await pageObjectCaseEntity.EnterAsignee(asigneeTeamName);
            await pageObjectCaseEntity.SearchAsignee();
            await pageObjectCaseEntity.PnlAsigneeSearch.WaitForAsync();
            await pageObjectCaseEntity.SelectSearchedTeam(asigneeTeamName);
            await pageObjectCaseEntity.AddAsignedUser();
        }

        public async Task CreateCase()
        {
            await test_Case_Manage_Accounts.NavigateToAnEntity("CASES");
            await pageObjectCaseEntity.ClickNewCase();
            await pageObjectCaseEntity.EnterCaseTitle(caseData!.CaseTitle!);
            await SelectSubject(caseData.Subject);
            await SelectCustomer(caseData!.CustomerName!);
            await pageObjectCaseEntity.SelectOrigin(caseData!.Origin!);
            await SelectContact(caseData!.ContactEmail!);
            await pageObjectCaseEntity.SelectSatisfaction(caseData!.Satisfaction!);
            await SelectProduct(caseData!.Product!);
            await pageObjectCaseEntity.EnterDescription(caseData!.Description!);
            await pageObjectAccountEntity.SaveForm();
            caseIDGenerated = await pageObjectCaseEntity.GetCaseId();
            caseData.CaseId = caseIDGenerated!;
            Console.WriteLine($"Case id that is generated is - {caseIDGenerated}");
            formatJsonFile.updateJsonFile(filePath, "CaseId", caseIDGenerated!);
        }

        public async Task SearchCase()
        {
            Console.WriteLine($"Searching case id - {caseIDGenerated}");
            await test_Case_Manage_Accounts.NavigateToAnEntity("CASES");
            await pageObjectCaseEntity.SelectView("All Cases");
            await pageObjectAccountEntity.searchEntity(caseIDGenerated!);
            await pageObjectCaseEntity.SelectCaseSearchResult(caseIDGenerated!);
            await pageObjectCaseEntity.WaitForCaseTitle();
        }

        public async Task VerifyCaseAttributeValues()
        {
            try
            {
                Console.WriteLine($"Starting to validate case attribute values for case id - {caseIDGenerated}");
                await pageObjectCaseEntity.TxtCaseTitle.WaitForAsync();
                Console.WriteLine(await pageObjectCaseEntity.TxtCaseTitle.GetAttributeAsync(name: "title"));
                await Expect(pageObjectCaseEntity.TxtCaseTitle).ToHaveValueAsync($"{caseData!.CaseTitle}");
                await Expect(pageObjectCaseEntity.TxtCaseID).ToHaveValueAsync($"{caseData!.CaseId}");
                await Expect(pageObjectCaseEntity.TxtSubject).ToHaveAttributeAsync("title", $"{caseData!.Subject}");
                await Expect(pageObjectCaseEntity.TxtCustomerName).ToHaveAttributeAsync("title", $"{caseData!.CustomerName}");
                await Expect(pageObjectCaseEntity.DrpDwnOrigin).ToHaveAttributeAsync("title", $"{caseData!.Origin}");
                await Expect(pageObjectCaseEntity.TxtContactName).ToHaveAttributeAsync("title", $"{caseData!.ContactName}");
                await Expect(pageObjectCaseEntity.DrpDwnSatisfaction).ToHaveAttributeAsync("title", $"{caseData!.Satisfaction}");
                await Expect(pageObjectCaseEntity.TxtProductValue).ToHaveAttributeAsync("title", $"{caseData!.Product}");
                await Expect(pageObjectCaseEntity.TxtDescription).ToHaveValueAsync($"{caseData!.Description}");
                Console.WriteLine("Completed validating applicaiton data");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception - {ex.Message}");
            }
        }

        public async Task AddCaseToQueue()
        {
            Console.WriteLine($"Add case case id - {caseIDGenerated} to the queue");
            await pageObjectCaseEntity.TxtCaseTitle.WaitForAsync();
            Console.WriteLine(await pageObjectCaseEntity.TxtCaseTitle.GetAttributeAsync(name: "title"));
            await pageObjectAccountEntity.ClickMoreCommands();
            await pageObjectCaseEntity.MenuAddToQueue.ClickAsync();
            await pageObjectCaseEntity.PnlAddToQueue.WaitForAsync();
            await SelectQueue(caseData!.QueueName);
            await pageObjectCaseEntity.BtnAddToQueuePnl.WaitForAsync();
            await pageObjectCaseEntity.BtnAddToQueuePnl.ClickAsync();
            await pageObjectCaseEntity.TxtCaseTitle.WaitForAsync();
            Console.WriteLine($"Completed adding case - {caseIDGenerated} to the queue");
        }

        public async Task AssignCaseToUser()
        {
            Console.WriteLine($"Assign case case id - {caseIDGenerated} to the user");
            await pageObjectCaseEntity.TxtCaseTitle.WaitForAsync();
            Console.WriteLine(await pageObjectCaseEntity.TxtCaseTitle.GetAttributeAsync(name: "title"));
            await pageObjectAccountEntity.ClickMoreCommands();
            await pageObjectCaseEntity.MenuAssignCaseBtn();
            await pageObjectCaseEntity.PnlAssignCase.WaitForAsync();
            await SelectAssingee(caseData!.AssigneeType, caseData.AssigneeTeamName);
            await pageObjectCaseEntity.TxtCaseTitle.WaitForAsync();
            Console.WriteLine($"Completed adding case - {caseIDGenerated} to the queue");
        }

        public async Task VerifyCaseQueueAndOwner()
        {
            Console.WriteLine($"Assign case case id - {caseIDGenerated} to the user");
            await Expect(pageObjectCaseEntity.TxtOwnerValue).ToHaveAttributeAsync("aria-label", $"{caseData!.AssigneeTeamName}");
            Console.WriteLine($"Completed adding case - {caseIDGenerated} to the queue");
        }

        public async Task DeleteCase()
        {
            Console.WriteLine($"Delete the case - {caseIDGenerated}");
            await pageObjectAccountEntity.ClickMoreCommands();
            await pageObjectAccountEntity.ClickDelete();
            await pageObjectAccountEntity.ClickConfirm(); 
        }
    }
}
