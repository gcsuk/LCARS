using System.ComponentModel;

namespace LCARS.ViewModels.Issues
{
    public enum IssueSet
    {
        Random = 0,
        Blockers = 1,
        [Description("CMS")] CmsBugs = 2,
        [Description("Web")] WebBugs = 3
    }
}