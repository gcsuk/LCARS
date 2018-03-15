using System.Threading.Tasks;
using LCARS.Models.AlertCondition;

namespace LCARS.Services
{
    public interface IAlertConditionService
    {
        Task<AlertCondition> GetAlertCondition();
    }
}
