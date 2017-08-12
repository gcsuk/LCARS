﻿namespace LCARS.ViewModels.Builds
{
    public class Build
    {
        public string TypeId { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        private string _status;

        public string Status
        {
            get => Progress == null ? _status : $"{Progress.Percentage}% - {Progress.Status}";
            set => _status = value;
        }

        public BuildProgress Progress { get; set; }
    }
}