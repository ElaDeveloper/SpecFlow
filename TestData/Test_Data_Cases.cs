using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365Demo.TestData
{
    public class Test_Data_Cases
    {
        private String? caseTitle;
        private String? subject;
        private String? customerName;
        private String? origin;
        private String? contactName;
        private String? contactEmail;
        private String? satisfaction;
        private String? product;
        private String? description;
        private String? caseId;
        private String? queueName;
        private String? asigneeType; 
        private String? asigneeTeamName;

        public String CaseTitle { get => caseTitle!; set => caseTitle = value; }

        public String Subject { get => subject!; set => subject = value; }

        public String CustomerName { get => customerName!; set => customerName = value; }

        public String Origin { get => origin!; set => origin = value; }

        public String ContactName { get => contactName!; set => contactName = value; }

        public String ContactEmail { get => contactEmail!; set => contactEmail = value; }

        public String Satisfaction { get => satisfaction!; set => satisfaction = value; }

        public String Product { get => product!; set => product = value; }

        public String Description { get => description!; set => description = value; }

        public String CaseId { get => caseId!; set => caseId = value; }

        public String QueueName { get => queueName!; set => queueName = value; }

        public String AssigneeType { get => asigneeType!; set => asigneeType = value; }        

        public String AssigneeTeamName { get => asigneeTeamName!; set => asigneeTeamName = value; }
    }
}
