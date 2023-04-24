global using FluentAssertions;
global using NUnit;
global using TechTalk.SpecFlow;
using D365Demo.PageObjects;
using D365Demo.TestData;
using NUnit.Framework;
using System.Text.Json;

namespace D365Demo.Utilities
{
    public class PraseJsonTestData
    {
        public Test_Data_Accounts? accountData;
        public Test_Data_Accounts FetchAccountDetails()
        {
            string text = File.ReadAllText("C:\\Ela\\PlayWright\\SpecFlowLearn\\Utilities\\Accounts.json");
            var accountData = JsonSerializer.Deserialize<Test_Data_Accounts>(text);

            Console.WriteLine($"Account Name: {accountData.AccountName}");
            Console.WriteLine($"Parent Acccount Name: {accountData.ParentAccountName}");
            return accountData;
        }
    }
}