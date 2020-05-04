using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database
{
    public static class Helper
    {
        public static string GetConnectionString(IConfiguration configuration, string name)
        {
            return ConfigurationExtensions.GetConnectionString(configuration, name);
        }

    }
}
