using Microsoft.Playwright;

namespace SpecFlowDemo.PageObjects
{
    public class PageObjectAccountEntity
    {
        private readonly IPage page;

        public PageObjectAccountEntity(IPage page) => this.page = page;

        private ILocator TxtUserName => page.Locator(selector: "input[type='email']");
        private ILocator TxtPassword => page.Locator(selector: "input[type='password']");
        private ILocator BtnNext => page.Locator(selector: "input[type='submit'][class*='primary']");

        private ILocator StaySignedPopup => page.Locator(selector: "#lightbox");

        private ILocator StayLabel => page.GetByLabel("Don't show this again");

        private ILocator StaySignedBtn => page.Locator(selector: "input[type='submit']");

        private IFrameLocator ApplicationFrame => page.FrameLocator(selector: "input[type='submit']");

        private ILocator AccountLnk => page.Locator(selector: "div[title='Accounts']");

        private ILocator CasesLnk => page.Locator(selector: "li[data-text='Cases']");

        private ILocator AddAccountBtn => page.Locator(selector: "button[aria-label='New']");

        private ILocator TxtAccountName => page.Locator(selector: "input[aria-label= 'Account Name']");

        private ILocator TxtParentAccountName => page.Locator(selector: "input[aria-label='Parent Account, Lookup']");

        private ILocator SearchParentAccount => page.Locator(selector: "button[data-id='parentaccountid.fieldControl-LookupResultsDropdown_parentaccountid_search']");

        private ILocator PnlParentAccount => page.Locator(selector: "div[aria-label='Dropdown panel']");

        public ILocator BtnSave => page.Locator(selector: "button[aria-label='Save (CTRL+S)']");

        private ILocator TxtAccountFieldSearch => page.Locator(selector: "input[aria-label*='Filter by keyword']");

        private ILocator TxtSearchAccountName => page.Locator(selector: "input[aria-label*='Filter by keyword']");

        private ILocator BtnSearchAccount => page.Locator(selector: "button[aria-label='Start search']");

        private ILocator BtnMoreCommands => page.Locator(selector: "button[aria-label*='More commands for']");

        private ILocator BtnDelete => page.Locator(selector: "button[aria-label='Delete']");

        private ILocator BtnConfirmDelete => page.Locator(selector: "button[data-id='confirmButton']");

        //Implementation
        public async Task Login(string userName, string password)
        {
            await TxtUserName.WaitForAsync();
            await TxtUserName.FillAsync(userName);
            await ClickEnter();
            await TxtPassword.WaitForAsync();
            await TxtPassword.ClickAsync();
            await TxtPassword.FillAsync(password);
            await ClickEnter();

        }

        public async Task ClickEnter()
        {
            await BtnNext.WaitForAsync();
            await BtnNext.ClickAsync();
        }

        public async Task StaySigned() => await StaySignedPopup.IsVisibleAsync();

        public async Task StaysignedLabel() => await StayLabel.CheckAsync();

        public async Task StaySignedClickBtn() => await StaySignedBtn.ClickAsync();

        public async Task NavigateSalesHub()
        {
            var locator = page.FrameLocator(selector: "#AppLandingPage");
            await locator.Locator("div[data-lp-id*='saleshub'][role='listitem']").ClickAsync();
        }

        public async Task NavigateAccounts() => await AccountLnk.ClickAsync();

        public async Task AddNewAccount() => await AddAccountBtn.ClickAsync();

        public async Task EnterAccountName(string accountName)
        {
            await TxtAccountName.FillAsync(accountName);
        }

        public async Task EnterParentAccountName(string accountName)
        {
            await TxtParentAccountName.FillAsync(accountName);
        }

        public async Task ClickParentSearch() => await SearchParentAccount.ClickAsync();

        public async Task WaitParentPnlVisiblity() => await PnlParentAccount.IsVisibleAsync();

        public async Task GetParentAccountByText(string parentAccountName)
        {
            await page.GetByText(parentAccountName).ClickAsync();
        }

        public async Task SaveForm() => await BtnSave.ClickAsync();

        public async Task ClickAccountSearchField() => await TxtAccountFieldSearch.ClickAsync();

        public async Task SearchAccount(string accountName)
        {
            await TxtSearchAccountName.FillAsync(accountName);
        }

        public async Task ClickAccountSearch() => await BtnSearchAccount.ClickAsync();

        public async Task SelectAccountSearch(string accountName)
        {
            await page.ClickAsync(selector: $"//span[text()='{accountName}']");
        }

        public ILocator GetAccountName()
        {
            return page.Locator(selector: "input[aria-label='Account Name']");
        }

        public async Task ClickMoreCommands() => await BtnMoreCommands.ClickAsync();

        public async Task ClickDelete() => await BtnDelete.ClickAsync();

        public async Task ClickConfirm() => await BtnConfirmDelete.ClickAsync();

        public async Task NavigateCases() => await CasesLnk.ClickAsync();

        public async Task searchEntity(string entityId)
        {
            await TxtAccountFieldSearch.WaitForAsync();
            await TxtAccountFieldSearch.ClickAsync();
            await SearchAccount(entityId);
            await ClickAccountSearch();
        }

    }
}
