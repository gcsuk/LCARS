﻿using System;

namespace LCARS.ViewModels
{
    public class RedAlert
    {
        public bool IsEnabled { get; set; }

        public DateTime? TargetDate { get; set; }

        public string AlertType { get; set; }
    }
}