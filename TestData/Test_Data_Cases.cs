namespace SpecFlowDemo.TestData
{
    public class Test_Data_Cases
    {
        private string? caseTitle;
        private string? subject;
        private string? customerName;
        private string? origin;
        private string? contactName;
        private string? contactEmail;
        private string? satisfaction;
        private string? product;
        private string? description;
        private string? caseId;
        private string? queueName;
        private string? asigneeType;
        private string? asigneeTeamName;

        public string CaseTitle { get => caseTitle!; set => caseTitle = value; }

        public string Subject { get => subject!; set => subject = value; }

        public string CustomerName { get => customerName!; set => customerName = value; }

        public string Origin { get => origin!; set => origin = value; }

        public string ContactName { get => contactName!; set => contactName = value; }

        public string ContactEmail { get => contactEmail!; set => contactEmail = value; }

        public string Satisfaction { get => satisfaction!; set => satisfaction = value; }

        public string Product { get => product!; set => product = value; }

        public string Description { get => description!; set => description = value; }

        public string CaseId { get => caseId!; set => caseId = value; }

        public string QueueName { get => queueName!; set => queueName = value; }

        public string AssigneeType { get => asigneeType!; set => asigneeType = value; }

        public string AssigneeTeamName { get => asigneeTeamName!; set => asigneeTeamName = value; }
    }
}
