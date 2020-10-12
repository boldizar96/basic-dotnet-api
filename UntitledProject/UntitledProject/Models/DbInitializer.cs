using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UntitledProject.Models
{
    public class DbInitializer
    {
        public static void Initialize(UntitledProjectContext context, IServiceProvider serviceProvider)
        {
            if (!(context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                context.Database.Migrate();
            }
        }
    }
}
