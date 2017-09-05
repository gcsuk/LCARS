using System;
using System.Linq;
using LCARS.Models;
using LCARS.Repositories;

namespace LCARS.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _repository;

        public SettingsService(ISettingsRepository repository)
        {
            _repository = repository;
        }

        public Settings GetSettings()
        {
            var settings = _repository.GetAll().SingleOrDefault();

            if (settings == null)
            {
                throw new InvalidOperationException("Invalid settings");
            }

            return settings;
        }
    }
}