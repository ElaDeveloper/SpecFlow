global using TechTalk.SpecFlow;
using SpecFlowDemo.TestData;
using System.Text.Json;

namespace SpecFlowDemo.Utilities
{
    public class PraseJsonTestData
    {
        public Test_Data_Accounts? accountData;
        public Test_Data_Accounts FetchAccountDetails()
        {
            string text = File.ReadAllText("C:\\Ela\\PlayWright\\SpecFlowLearn\\Utilities\\Accounts.json");
            var accountData = JsonSerializer.Deserialize<Test_Data_Accounts>(text);

            Console.WriteLine($"Account Name: {accountData!.AccountName}");
            Console.WriteLine($"Parent Acccount Name: {accountData.ParentAccountName}");
            return accountData;
        }
    }
}