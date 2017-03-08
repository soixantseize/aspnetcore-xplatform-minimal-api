using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RoutingSample.Models
{
    public static class ApplicationDbExtensions
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.StoredValues.Any())
                {
                    context.StoredValues.AddRange(
                        new StoredValue { Value = "Google" },
                        new StoredValue { Value = "Microsoft" },
                        new StoredValue { Value = "Facebook" },
                        new StoredValue { Value = "Salesforce" },
                        new StoredValue { Value = "Oracle" });

                    context.SaveChanges();
                }
            }
        }
    }
}