using System;
using LCARS.Models;

namespace LCARS.Domain
{
    public class Common : ICommon
    {
        private readonly Repository.ICommon _repository;

        public Common(Repository.ICommon repository)
        {
            _repository = repository;
        }

        public Boards SelectBoard()
        {
            return (Boards)new Random(Guid.NewGuid().GetHashCode()).Next(1, Enum.GetNames(typeof(Boards)).Length + 1);
        }

        public RedAlert GetRedAlert(string fileName)
        {
            return _repository.GetRedAlert(fileName);
        }

        public void UpdateRedAlert(string fileName, bool isEnabled, string targetDate)
        {
            _repository.UpdateRedAlert(fileName, isEnabled, targetDate);
        }
    }
}