﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plate.Domain.Models
{
    public class DatabaseConfig
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        
        public string ConnectionString => $"Server={Host};Port={Port};Database={Database};Uid={User};Pwd={Password};"; 
    }
}
