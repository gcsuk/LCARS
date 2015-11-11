using LCARS.Models.Issues;

namespace LCARS.Services
{
    public interface IIssues
    {
        Parent Get(string query);
    }
}