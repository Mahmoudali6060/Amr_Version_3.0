﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AMR_Server.Domain.Entities
{
    public class User
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
