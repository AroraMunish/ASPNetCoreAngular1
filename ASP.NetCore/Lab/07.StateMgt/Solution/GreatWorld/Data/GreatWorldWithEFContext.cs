using System;
using System.Collections.Generic;
using GreatWorld.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GreatWorld.Data
{
    public partial class GreatWorldWithEFContext : IdentityDbContext<WorldUser>
    {
        public GreatWorldWithEFContext()
        {
        }

        public GreatWorldWithEFContext(DbContextOptions<GreatWorldWithEFContext> options)
            : base(options)
        {
        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DATA-AI;Initial Catalog=GreatWorldWithEF;Integrated Security=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        //virtual keyword is used when you want to load data with lazy loading, meaning that load it when requested.
        public virtual DbSet<Stop> Stops { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
