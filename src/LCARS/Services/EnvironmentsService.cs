using System.Collections.Generic;
using LCARS.Repositories;
using System.Linq;
using LCARS.ViewModels.Environments;

namespace LCARS.Services
{
    public class EnvironmentsService : IEnvironmentsService
    {
        private readonly DataContext _dbContext;

        public EnvironmentsService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Site> Get()
        {
            return _dbContext.Sites.Select(t => new Site
            {
                Id = t.Id,
                Name = t.Name,
                Environments = t.Environments.Select(e => new Environment
                {
                    Name = e.Name,
                    Status = e.Status
                })
            });
        }
    }
}
