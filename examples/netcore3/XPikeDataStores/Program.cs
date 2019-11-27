﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using XPike.Configuration.Microsoft.AspNetCore;
using XPike.Logging.Microsoft.AspNetCore;

namespace XPikeDataStores
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(builder => { builder.UseXPikeLogging(); })
                .AddXPikeConfiguration(config => { })
                .UseStartup<Startup>();
    }
}
