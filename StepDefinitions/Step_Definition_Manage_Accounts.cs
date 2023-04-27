using SpecFlowDemo.Hooks;
using SpecFlowDemo.TestCases;

namespace SpecFlowDemo.StepDefinitions
{
    [Binding]
    public class Step_Definition_Manage_Accounts
    {
        private readonly Test_Case_Manage_Accounts test_Case_Manage_Accounts;
        private readonly Test_Case_Manage_Cases test_Case_Manage_Cases;

        public Step_Definition_Manage_Accounts(HooksLaunchApplication hooksLaunchApplication)
        {
            test_Case_Manage_Accounts = new Test_Case_Manage_Accounts(hooksLaunchApplication._driver.Page);
            test_Case_Manage_Cases = new Test_Case_Manage_Cases(hooksLaunchApplication._driver.Page);
        }

        [Given(@"User launches MS Dynamics CRM application and navigates to Sales hub Menu")]
        public async Task GivenUserLaunchesMSDynamicsCRMApplicationAndNavigatesToSalesHubMenuAsync()
        {
            Console.WriteLine("Starting to launch browser and perform reuqired operations for the module");
            await test_Case_Manage_Accounts.NavigateToSalesHubApp();
        }

        [When(@"User adds a new account to the system")]
        public async Task WhenUserAddsANewAccountToTheSystem()
        {
            Console.WriteLine("When operation");
            await test_Case_Manage_Accounts.NavigateToAnEntity("Accounts");
            await test_Case_Manage_Accounts.CreateAccount();
        }

        [Then(@"User should be able to view the newly added account")]
        public async Task ThenUserShouldBeAbleToViewTheNewlyAddedAccount()
        {
            Console.WriteLine("Then operation");
            await test_Case_Manage_Accounts.SearchAndDeleteAccount();
        }

        [Given(@"User adds a Case to the system")]
        public async Task GivenUserAddsACaseToTheSystem()
        {
            await test_Case_Manage_Cases.CreateCase();
        }

        [When(@"User validates the case attributes then adds the Case to the queue and assigns to a user")]
        public async Task WhenUserValidatesTheCaseAttributesThenAddsTheCaseToTheQueueAndAssignsToAUser()
        {
            await test_Case_Manage_Cases.SearchCase();
            await test_Case_Manage_Cases.VerifyCaseAttributeValues();
            await test_Case_Manage_Cases.AddCaseToQueue();
            await test_Case_Manage_Cases.AssignCaseToUser();
        }

        [Then(@"The case should be added to the queue and assigned to the user")]
        public async Task ThenTheCaseShouldBeAddedToTheQueueAndAssignedToTheUser()
        {
            await test_Case_Manage_Cases.VerifyCaseQueueAndOwner();
        }

        [Then(@"User deletes the case from the system")]
        public async Task ThenUserDeletesTheCaseFromTheSystem()
        {
            await test_Case_Manage_Cases.DeleteCase();
        }
    }
}