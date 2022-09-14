using Sire.Data.Entities.Master;
using Microsoft.EntityFrameworkCore;

namespace Sire.Domain
{
    public static class DefaultEntityMappingExtension
    {
        public static void DefalutMappingValue(this ModelBuilder modelBuilder)
        {
            
            // modelBuilder.Entity<AppUser>()
            //   .Property(b => b.CreatedDate)
            //   .HasDefaultValueSql("getdate()");

          
        }

        public static void DefalutDeleteValueFilter(this ModelBuilder modelBuilder)
        {
            ////modelBuilder.Entity<Country>()
            ////    .HasQueryFilter(p => !p.IsDeleted);

            ////modelBuilder.Entity<State>()
            ////    .HasQueryFilter(p => !p.IsDeleted);

            ////modelBuilder.Entity<City>()
            ////    .HasQueryFilter(p => !p.IsDeleted);

            ////modelBuilder.Entity<Language>()
            ////    .HasQueryFilter(p => !p.IsDeleted);

            ////modelBuilder.Entity<ScopeName>()
            ////    .HasQueryFilter(p => !p.IsDeleted);
        }
    }
}