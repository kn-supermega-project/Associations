using Associations.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Associations.DataAccess.Data
{
    public class AssociationsDbContext: DbContext
    {
        public AssociationsDbContext()
        {
        }
        public AssociationsDbContext(DbContextOptions<AssociationsDbContext> options)
            :base(options)
        {
        }
        public virtual DbSet<Words> Words { get; set; }
        public virtual DbSet<WordRels> WordRels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordRels>(entity =>
            {
                entity.HasOne(o => o.word)
                .WithMany(p => p.Rels)
                .HasForeignKey(d => d.WordId)
                .HasConstraintName("FK_MainWords_To_Words")
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(o => o.RelWord)
                .WithMany(p => p.RelsWord)
                .HasForeignKey(d => d.WordRelId)
                .HasConstraintName("FK_RelWords_TO_Words")
                .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
