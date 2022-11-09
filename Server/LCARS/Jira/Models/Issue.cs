namespace LCARS.Jira.Models;

public record Issue
{
    public int Total { get; set; }
    public IEnumerable<IssuesModel> Issues { get; set; } = Enumerable.Empty<IssuesModel>();

    public class IssuesModel
    {
        public FieldsModel? Fields { get; set; } = default;

        public class FieldsModel
        {
            public string? Summary { get; set; } = default;
            public string? Description { get; set; } = default;
            public IssueTypeModel? IssueType { get; set; } = default;
            public StatusModel? Status { get; set; } = default;

            public class IssueTypeModel
            {
                public string? Name { get; set; } = default;
                public string? Description { get; set; } = default;
            }

            public class StatusModel
            {
                public string? Name { get; set; } = default;
            }
        }
    }
}