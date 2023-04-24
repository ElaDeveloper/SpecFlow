using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365Demo.TestData
{
    public class Test_Data_Environment_Config
    {
        private String? environmentName;

        public String EnvironmentName { get => environmentName!; set => environmentName = value; }
    }
}
