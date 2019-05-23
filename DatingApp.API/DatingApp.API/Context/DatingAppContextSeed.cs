using DatingApp.API.Enums;
using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using DatingApp.API.Helpers;

namespace DatingApp.API.Context
{

    public static class DatingAppContextSeed
    {
        private static readonly string SeedPath = Path.Combine(Hosting.ContentRootPath, "Seed");

        public static void SeedData(this DatingAppContext context)
        {
            SeedGenders(context);
             
            //update-database
            context.SaveChanges();
        }

        private static void SeedGenders(DatingAppContext context)
        {
            if (!context.Genders.Any())
            {
                string seedGendersPath = Path.Combine(SeedPath, "SeedGender.json");
                if (File.Exists(seedGendersPath))
                {
                    context.Genders.AddRange(JsonConvert.DeserializeObject<List<Gender>>(File.ReadAllText(seedGendersPath)));
                }
            }
        }

    }
}
