﻿using System.ComponentModel;

namespace LCARS.Models
{
    public enum Boards
    {
        [Description("Home")] Environment = 1,
        [Description("Builds")] Build = 2,
        Git = 3,
        Issues = 4
    }
}