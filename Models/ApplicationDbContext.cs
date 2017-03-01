using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RoutingSample.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {

        }

        private DbSet<StoredValue> StoredValues{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string dbName = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "localhost";
            string dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "password";
            string pgConStr = string.Format("Host={0};Port=5432;Database=myDataBase;User ID=postgres;Password={1}", dbName, dbPassword);
            builder.UseNpgsql(pgConStr);
            base.OnConfiguring(builder);
        }

        public Task<IEnumerable<StoredValue>> GetAll()
        {
            return Task.FromResult(StoredValues.AsEnumerable());
        }

        public Task<StoredValue> Get(int id)
        {
            return Task.FromResult(StoredValues.FirstOrDefault(v => v.Id == id));
        }

        public async Task<string> Add(StoredValue value)
        {
            var valueExists = await StoredValues.AnyAsync(v => v.Value == value.Value);

            if (valueExists)
            {
                return "Exists";
            }

            StoredValues.Add(new StoredValue { Value = value.Value});
            await SaveChangesAsync();
            return "Success";
        }

        public async Task<string> Update(StoredValue updatedValue)
        {
            var value = StoredValues.FirstOrDefault(v => v.Id == updatedValue.Id);
            if (value == null)
            {
                //throw new InvalidOperationException(string.Format("Contact with id '{0}' does not exists", updatedContact.ContactId));
                return "Not Found";
            }

            value.Value = updatedValue.Value;
            StoredValues.Update(value);
            await SaveChangesAsync();
            return "Success";
        }

        public async Task Delete(int id)
        {
            var value = StoredValues.FirstOrDefault(v => v.Id == id);

            StoredValues.Remove(value);
            await SaveChangesAsync();
        }
    }
}