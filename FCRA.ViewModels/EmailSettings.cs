﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.ViewModels
{
    public class EmailSettings
    {
        public string IsThroughSMTP { get; set; } = "";
        public string From { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Host { get; set; } = "";
        public int Port { get; set; }
    }
}
