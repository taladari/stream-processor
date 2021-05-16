using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessor.BL.Data
{
    public class EventsDbContext : DbContext
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventTypeCount>(e =>
            {
                e.HasKey(x => x.EventType);
            });

            modelBuilder.Entity<WordAppearancesCount>(e =>
            {
                e.HasKey(x => x.Word);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EventTypeCount> EventTypesCount { get; set; }
        public DbSet<WordAppearancesCount> WordAppearancesCount { get; set; }
    }
}
