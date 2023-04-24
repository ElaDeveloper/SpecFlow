using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace D365Demo.PageObjects
{
    public class PageObjectCaseEntity
    {
        private readonly IPage page;

        public PageObjectCaseEntity(IPage page) => this.page = page;

        public ILocator BtnAddNewCase => page.Locator(selector: "button[aria-label='New Case']");

        public ILocator TxtCaseTitle => page.Locator(selector: "input[aria-label*='Case Title']");

        private ILocator DrpDwnSubject => page.Locator(selector: "div[aria-label*='Subject']");

        private ILocator PnlSubject => page.Locator(selector: "ul[id*='subjectid.fieldControl-subject-tree-dropdown'][role='tree']");

        private ILocator TxtDefaultSlt => page.Locator(selector: "//label[text()='Default Subject']");

        private ILocator TxtQuerySlt => page.Locator(selector: "//label[text()='Query']/preceding-sibling::span");

        private ILocator TxtServiceSlt => page.Locator(selector: "//label[text()='Service']/preceding-sibling::span");

        private ILocator ExpandSubjectParent => page.Locator(selector: "li[id*='subjectid.fieldControl-subject-tree-dropdown'][aria-expanded='false'] span");

        public ILocator TxtInputCustomerName => page.Locator(selector: "input[data-id='customerid.fieldControl-LookupResultsDropdown_customerid_textInputBox_with_filter_new']"); 

        public ILocator TxtSearchCustomer => page.Locator(selector: "button[data-id='customerid.fieldControl-LookupResultsDropdown_customerid_search']");

        public ILocator TxtCustomerName => page.Locator(selector: "div[data-id='customerid.fieldControl-LookupResultsDropdown_customerid_selected_tag_text']");

        private ILocator PnlCustomer => page.Locator(selector: "div[aria-label='Dropdown panel']");

        public ILocator DrpDwnOrigin => page.Locator(selector: "select[aria-label='Origin']");

        public ILocator TxtSearchContact => page.Locator(selector: "input[aria-label='Contact, Lookup']");

        public ILocator TxtContactName => page.Locator(selector: "div[data-id='primarycontactid.fieldControl-LookupResultsDropdown_primarycontactid_selected_tag_text']");

        private ILocator BtnSearchContact => page.Locator(selector: "button[data-id='primarycontactid.fieldControl-LookupResultsDropdown_primarycontactid_search']");

        public ILocator DrpDwnSatisfaction => page.Locator(selector: "select[aria-label='Satisfaction']");

        public ILocator TxtProduct => page.Locator(selector: "input[aria-label='Product, Lookup']");

        public ILocator TxtProductValue => page.Locator(selector: "div[data-id='productid.fieldControl-LookupResultsDropdown_productid_selected_tag_text']");

        private ILocator BtnProduct => page.Locator(selector: "button[data-id='productid.fieldControl-LookupResultsDropdown_productid_search']");

        public ILocator TxtDescription => page.Locator(selector: "textarea[aria-label='Description']");

        public ILocator TxtCaseID => page.Locator(selector: "input[aria-label='ID']");

        private ILocator ChangeFilter => page.Locator(selector: "//span[text()='Open popup to change view.']/following-sibling::i[@data-icon-name='ChevronDown']");

        private ILocator PnlViewSelector => page.Locator(selector: "div[data-id*='ViewSelector']");

        private ILocator TxtSearchFilter => page.Locator(selector: "input[aria-label='Search views']");

        private ILocator LblCaseTitle => page.Locator(selector: "label[id *= 'title-field-label']");

        public ILocator TxtSubject => page.Locator(selector: "div[data-id='subjectid.fieldControl-subject-tree-input'] span");

        public ILocator MenuAddToQueue => page.Locator(selector: "button[aria-label='Add to Queue']");

        public ILocator PnlAddToQueue => page.Locator(selector: "div[data-lp-id='dialogView|AddToQueue']");

        public ILocator TxtAddToQueue => page.Locator(selector: "input[data-id='businessqueues_id.fieldControl-LookupResultsDropdown_businessqueues_id_textInputBox_with_filter_new']");

        public ILocator BtnSearchAddToQueue => page.Locator(selector: "button[data-id='businessqueues_id.fieldControl-LookupResultsDropdown_businessqueues_id_search']");

        public ILocator BtnAddToQueuePnl => page.Locator(selector: "button[aria-label='Add']");

        public ILocator MenuAssignBtn => page.Locator(selector: "button[aria-label='Assign']");

        public ILocator PnlAssignCase => page.Locator(selector: "div[data-id='AssignQueue']");

        public ILocator SltAssignTo => page.Locator(selector: "select[aria-label='Assign To']");

        public ILocator TxtAssignTo => page.Locator(selector: "input[aria-label='User or team, Lookup']");

        public ILocator BtnAssignToSearch => page.Locator(selector: "button[aria-label='Search records for User or team, Lookup field']");

        public ILocator PnlAsigneeSearch => page.Locator(selector: "//div[@data-id='systemuserview_id.fieldControl-LookupResultsDropdown_systemuserview_id_all_heading']/parent::div");

        public ILocator BtnAssignSearchedUser => page.Locator(selector: "button[data-id='ok_id']");

        public ILocator TxtOwnerValue => page.Locator(selector: "//div[text()='Owner']/preceding-sibling::div/child::a");

        //Implementation

        public async Task ClickNewCase() => await BtnAddNewCase.ClickAsync();

        public async Task ClickSubjectDrpDwn() => await DrpDwnSubject.ClickAsync();

        public async Task EnterCaseTitle(String caseTitle)
        {
            await TxtCaseTitle.FillAsync(caseTitle);
        }

        public ILocator Expand() => PnlSubject;

        public async Task ExpandSubjectValues()
        {
            await TxtDefaultSlt.WaitForAsync(new() { Timeout = 5000 });
            int count = await ExpandSubjectParent.CountAsync();
            Console.WriteLine("Writing value of count - " + count);
            //for (int i = 1; i <=count; ++i)
            //{
            //    await ExpandSubjectParent.Nth(i).WaitForAsync(new() { State = WaitForSelectorState.Visible });
            //    await ExpandSubjectParent.Nth(i).ClickAsync();
            //}
            await TxtQuerySlt.ClickAsync();
            await TxtServiceSlt.ClickAsync();
        }

        public async Task SelectSubject(String subject)
        {
            await PnlSubject.Filter(new() { HasText = $"{subject}" }).ClickAsync();
        }

        public async Task ClickCustomerSearch() => await TxtSearchCustomer.ClickAsync();

        public async Task WaitCustomerPnlVisiblity() => await PnlCustomer.IsVisibleAsync();

        public async Task GetCustomerByText(String customerName)
        {
            await page.Locator(selector: $"//span[contains(@data-id,'customerid.fieldControl')]/child::span[text()='{customerName}']").First.ClickAsync();
        }

        public async Task GetContactByText(String contactEmail)
        {
            await page.Locator(selector: $"//span[contains(@data-id,'primarycontactid.fieldControl-emailaddress')]/child::span[text()='{contactEmail}']").First.ClickAsync();
        }

        public async Task GetProductByText(String productName)
        {
            await page.GetByText(productName).ClickAsync();
        }

        public async Task SelectOrigin(String origin)
        {
            await DrpDwnOrigin.SelectOptionAsync($"{origin}");
        }

        public async Task EnterContact(String contact)
        {
            await TxtSearchContact.FillAsync($"{contact}");
        }

        public async Task ClickContactSearch() => await BtnSearchContact.ClickAsync();

        public async Task SelectSatisfaction(String satisfaction)
        {
            await DrpDwnSatisfaction.SelectOptionAsync($"{satisfaction}");
        }

        public async Task EnterProduct(String product)
        {
            await TxtProduct.FillAsync($"{product}");
        }

        public async Task ClickProductSearch() => await BtnProduct.ClickAsync();

        public async Task EnterDescription(String description)
        {
            await TxtDescription.FillAsync(description);
        }
        public async Task<String?> GetCaseId()
        {
            await TxtCaseID.WaitForAsync();
            await TxtCaseID.ClickAsync();
            Console.WriteLine(await TxtCaseID.GetAttributeAsync(name: "value"));
            return await TxtCaseID.GetAttributeAsync(name: "value");
        }

        public async Task SelectView(String viewName)
        {
            await ChangeFilter.ClickAsync();
            await PnlViewSelector.WaitForAsync();
            await TxtSearchFilter.FillAsync(viewName);
            await PnlViewSelector.Filter(new() { HasText = $"{viewName}" }).ClickAsync();
        }

        public async Task SelectCaseSearchResult(String caseId)
        {
            await page.DblClickAsync(selector: $"//div[text()='{caseId}' and contains(@class, 'ms-TooltipHost')]");
        }

        public async Task WaitForCaseTitle() => await LblCaseTitle.WaitForAsync();

        public async Task ClickQueueSearch() => await BtnSearchAddToQueue.ClickAsync();

        public async Task EnterQueue(String queueName)
        {
            await TxtAddToQueue.FillAsync($"{queueName}");
        }

        public async Task SelectQueueFromPanel(String queueName)
        {
            await page.Locator($"//span[text()='{queueName}']").ClickAsync();
        }

        public async Task MenuAssignCaseBtn() => await MenuAssignBtn.ClickAsync();

        public async Task SelectAssingee(String assigneeType)
        {

            await SltAssignTo.SelectOptionAsync(assigneeType);
        }

        public async Task EnterAsignee(String assigneeValue)
        {
            await TxtAssignTo.FillAsync(assigneeValue);
        }

        public async Task SearchAsignee() => await BtnAssignToSearch.ClickAsync();

        public async Task SelectSearchedTeam(String teamName)
        {
            await page.Locator(selector: $"//li[contains(@aria-label, '{teamName}')]").ClickAsync();
        }

        public async Task AddAsignedUser() => await BtnAssignSearchedUser.ClickAsync(); 
    }
}
