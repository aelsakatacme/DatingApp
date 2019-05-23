using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
 
namespace DatingApp.API.Helpers
{
    public static class Hosting
    {
        public static IHostingEnvironment hosting { get; set; }

        public static string ContentRootPath => hosting.ContentRootPath;
    }
}
