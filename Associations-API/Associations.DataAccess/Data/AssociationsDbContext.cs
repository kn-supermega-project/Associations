using Associations.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
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

    }
}
