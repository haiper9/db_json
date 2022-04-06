using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;


namespace ConsoleApp2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string path = "table_json.json";
            string jsonStrong = File.ReadAllText(path);
            var cities = JsonSerializer.Deserialize<IEnumerable<city>>(jsonStrong);
            Console.WriteLine($"  ");
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Cities.AddRange(cities);

                db.SaveChanges();
            }
        }
    }
   public class city
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string district { get; set; }
        public int population { get; set; }
        public string subject { get; set; }
        public coords coords { get; set; }


    }
   public class coords
    {
        public int Id { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }

        public int cityID { get; set; }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<city> Cities { get; set; }
        public DbSet<coords> Coords { get; set; }
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }


    }

}
