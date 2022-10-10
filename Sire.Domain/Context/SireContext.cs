using Sire.Data.Entities.Common;
using Sire.Data.Entities.Master;
using Sire.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sire.Data.Entities.UserMgt;
using Sire.Data.Entities.ShipManagement;
using Sire.Data.Entities.Training;
using Sire.Data.Entities.Question;
using Sire.Data.Entities.Inspection;

namespace Sire.Domain.Context
{
    public class SireContext : DbContext
    {

        public SireContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Test> Test { get; set; }
        public DbSet<Sire.Data.Entities.Operator.Operator> Operator { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Fleet> Fleet { get; set; }
        public DbSet<Vessel> Vessel { get; set; }
        public DbSet<User_Vessel> User_Vessel { get; set; }
        public DbSet<Piq_Hvpq> Piq_Hvpq { get; set; }
        public DbSet<PIQ_HVPQ_Response_Mapping1> PIQ_HVPQ_Response_Mapping1 { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Vessel_Response_Piq_Hvpq> Vessel_Response_Piq_Hvpq { get; set; }
        public DbSet<Training_Question> Training_Question { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuetionSection> QuetionSection { get; set; }
        public DbSet<QuetionSubSection> QuetionSubSection { get; set; }

        public DbSet<QuestionResponse> QuestionResponse { get; set; }
        public DbSet<Inspection> Inspection { get; set; }

        public DbSet<User_Rank> User_Rank { get; set; }
        public DbSet<RankGroup> RankGroup { get; set; }
        public DbSet<QuestionRoviq> QuestionRoviq { get; set; }
        public DbSet<License> License { get; set; }
        public DbSet<Inspection_Question> Inspection_Question { get; set; }
        public DbSet<TraningResponse> TraningResponse { get; set; }
        public DbSet<Training_Task> Training_Task { get; set; }
        public DbSet<InspectionResponse> InspectionResponse { get; set; }
        public DbSet<PIQ_HVPQ_Response> PIQ_HVPQ_Response { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.DefalutMappingValue();
            modelBuilder.DefalutDeleteValueFilter();
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            throw new Exception("Please provide IJwtTokenAccesser in SaveChanges() method.");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new Exception("Please provide IJwtTokenAccesser in SaveChangesAsync() method.");
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new Exception("Please provide IJwtTokenAccesser in SaveChangesAsync() method.");
        }

        public int SaveChanges(IJwtTokenAccesser jwtTokenAccesser)
        {
            SetModifiedInformation(jwtTokenAccesser);
            var result = base.SaveChanges();
            return result;
        }

        public async Task<int> SaveChangesAsync(IJwtTokenAccesser jwtTokenAccesser)
        {
            SetModifiedInformation(jwtTokenAccesser);
            var result = await base.SaveChangesAsync();
            return result;
        }

        public void DetectionAll()
        {
            var entries = ChangeTracker.Entries().Where(e =>
                    e.State == EntityState.Added ||
                    e.State == EntityState.Unchanged ||
                    e.State == EntityState.Modified ||
                    e.State == EntityState.Deleted)
                .ToList();
            entries.ForEach(r => r.State = EntityState.Detached);
        }

        private void SetModifiedInformation(IJwtTokenAccesser jwtTokenAccesser)
        {
            if (jwtTokenAccesser == null || jwtTokenAccesser.UserId <= 0) return;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = jwtTokenAccesser.UserId;
                    entry.Entity.CreatedDate = DateTime.Now.ToUniversalTime();
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.CreatedBy).IsModified = false;
                    entry.Property(x => x.CreatedDate).IsModified = false;

                    if (entry.Entity.IsDeleted)
                    {
                        entry.Entity.DeletedBy = jwtTokenAccesser.UserId;
                        entry.Entity.DeletedDate = DateTime.Now.ToUniversalTime();
                    }
                    else
                    {
                        entry.Entity.ModifiedBy = jwtTokenAccesser.UserId;
                        entry.Entity.ModifiedDate = DateTime.Now.ToUniversalTime();
                    }
                }
        }

        public void Begin()
        {
            base.Database.BeginTransaction();
        }

        public void Commit()
        {
            base.Database.CommitTransaction();
        }

        public void Rollback()
        {
            base.Database.RollbackTransaction();
        }
    }

    public static class Extensions
    {
        public static IDictionary<TKey, TValue> NullIfEmpty<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null || !dictionary.Any()) return null;
            return dictionary;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source) action(element);
            return source;
        }
    }
}