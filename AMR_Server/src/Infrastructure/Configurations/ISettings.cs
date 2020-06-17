using System;
using System.Collections.Generic;
using System.Text;

namespace AMR_Server.Infrastructure.Configurations
{
    public interface ISettings
    {
        public string PrincipalContextName { get; set; }
    }
}
