using System.Linq;
using System.Threading.Tasks;
using LCARS.Models.AlertCondition;
using LCARS.Repositories;

namespace LCARS.Services
{
    public class AlertConditionService : IAlertConditionService
    {
        private readonly IRepository<AlertCondition> _repository;

        public AlertConditionService(IRepository<AlertCondition> repository)
        {
            _repository = repository;
        }

        public async Task<AlertCondition> GetAlertCondition() => (await _repository.GetAll()).Single();
    }
}
