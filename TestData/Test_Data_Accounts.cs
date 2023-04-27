namespace SpecFlowDemo.TestData
{

    public class Test_Data_Accounts
    {
        private string? accountName;
        private string? parentAccountName;

        public string AccountName { get => accountName!; set => accountName = value; }
        public string ParentAccountName { get => parentAccountName!; set => parentAccountName = value; }

    }
}
