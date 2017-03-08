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
        { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<StoredValue> StoredValues{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string dbName = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "localhost";
            string dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
            string dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "password";
            string pgConStr = string.Format("Host={0};Port=5432;Database=myDataBase;User ID={1};Password={2}", dbName, dbUser, dbPassword);
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

        public async Task<StoredValue> Add(StoredValue value)
        {
            var valueExists = await StoredValues.AnyAsync(v => v.Value == value.Value);

            if (valueExists)
                return null;

            var ret = StoredValues.Add(new StoredValue { Value = value.Value});
            await SaveChangesAsync();
            return ret.Entity;
        }

        public async Task<StoredValue> Update(StoredValue updatedValue)
        {
            var value = StoredValues.FirstOrDefault(v => v.Id == updatedValue.Id);
            if (value == null)
                return null;

            value.Value = updatedValue.Value;
            var ret = StoredValues.Update(value);
            await SaveChangesAsync();
            return ret.Entity;
        }

        public async Task<StoredValue> Delete(int id)
        {
            var value = StoredValues.FirstOrDefault(v => v.Id == id);
            if(value == null)
            {
                return null;
            }

            var ret = StoredValues.Remove(value);
            await SaveChangesAsync();
            return ret.Entity;
        }
    }
}
