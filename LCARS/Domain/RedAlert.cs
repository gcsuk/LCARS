using System;
using LCARS.ViewModels;

namespace LCARS.Domain
{
    public class RedAlert : IRedAlert
    {
        private readonly Repository.IRepository<Models.RedAlert> _repository;

        public RedAlert(Repository.IRepository<Models.RedAlert> repository)
        {
            _repository = repository;
        }

        public ViewModels.RedAlert GetRedAlert(string filePath)
        {
            var settings = _repository.Get(filePath);

            return new ViewModels.RedAlert
            {
                IsEnabled = settings.IsEnabled,
                AlertType = settings.AlertType,
                TargetDate = settings.TargetDate
            };
        }

        public void UpdateRedAlert(string filePath, ViewModels.RedAlert settings)
        {
            var model = new Models.RedAlert
            {
                IsEnabled = settings.IsEnabled,
                TargetDate = settings.TargetDate,
                AlertType = settings.AlertType
            };

            _repository.Update(filePath, model);
        }
    }
}