﻿using System.Collections.Generic;

namespace LCARS.ViewModels.Builds
{
    public class Settings
    {
        public int Id { get; set; }
        public string ServerUrl { get; set; }
        public string ServerUsername { get; set; }
        public string ServerPassword { get; set; }
        public IEnumerable<string> ProjectIds { get; set; }
    }
}