using System;
using LCARS.Models;
using LCARS.Services;

namespace LCARS.Domain
{
    public class Common : ICommon
    {
        private readonly IRepository _repository;

        public Common(IRepository repository)
        {
            _repository = repository;
        }

        public Boards SelectBoard()
        {
            return (Boards)new Random(Guid.NewGuid().GetHashCode()).Next(1, Enum.GetNames(typeof(Boards)).Length + 1);
        }

        public RedAlert GetRedAlert(string fileName)
        {
            return _repository.GetRedAlertSettings(fileName);
        }

        public void UpdateRedAlert(string fileName, bool isEnabled, string targetDate)
        {
            _repository.UpdateRedAlertSettings(fileName, isEnabled, targetDate);
        }
    }
}