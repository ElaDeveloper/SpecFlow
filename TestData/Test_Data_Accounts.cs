using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365Demo.TestData
{

    public class Test_Data_Accounts
    {
        private string? accountName;
        private string? parentAccountName;

        public string AccountName { get => accountName!; set => accountName = value; }
        public string ParentAccountName { get => parentAccountName!; set => parentAccountName = value; }

    }
}
