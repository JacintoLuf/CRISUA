using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Publication> Publication { get; set; }
        public virtual DbSet<Publication_Identifier> Publication_Identifier { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Identifier> Identifier{ get; set; }
        public virtual DbSet<OrgUnit> OrgUnit { get; set; }
        public virtual DbSet<OrgUnit_Classification> OrgUnit_Classification { get; set; }
        public virtual DbSet<OrgUnit_Publication> OrgUnit_Publication { get; set; }
        public virtual DbSet<OrgUnit_OrgUnit> OrgUnit_OrgUnit { get; set; }
        public virtual DbSet<OrgUnit_Identifier> OrgUnit_Identifier { get; set; }
        public virtual DbSet<OrgUnitActivity> OrgUnitActivity { get; set; }
        public virtual DbSet<OrgUnitName> OrgUnitName { get; set; }
        public virtual DbSet<OrgUnit_PAddress> OrgUnit_PAddress { get; set; }
        public virtual DbSet<Person_Identifier> Person_Identifier { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrgUnit_Publication>().HasKey(c => new { c.PublicationId, c.OrgUnitId });
            modelBuilder.Entity<Publication_Identifier>().HasKey(c => new { c.PublicationId, c.IdentifierId });
            modelBuilder.Entity<Person_Publication>().HasKey(c => new { c.PersonId, c.PublicationId});
            modelBuilder.Entity<Person_Identifier>().HasKey(c => new { c.PersonID, c.IdentifierId});
            modelBuilder.Entity<OrgUnit_Classification>().HasKey(c => new { c.OrgUnitId, c.ClassificationID});
            modelBuilder.Entity<OrgUnit_Identifier>().HasKey(c => new { c.OrgUnitId, c.IdentifierId});
            modelBuilder.Entity<OrgUnit_OrgUnit>().HasKey(c => new { c.OrgUnitId1, c.OrgUnitId2, c.ClassificationID});
            modelBuilder.Entity<OrgUnit_PAddress>().HasKey(c => new { c.OrgUnitId, c.PAddressId});
            modelBuilder.Entity<OrgUnitActivity>().HasKey(c => new { c.OrgUnitId, c.LanguageId});
            modelBuilder.Entity<OrgUnitName>().HasKey(c => new { c.OrgUnitId, c.LanguageId});

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        public virtual DbSet<PersonName> PersonName { get; set; }
        public virtual DbSet<PAddress> PAddress { get; set; }
        public virtual DbSet<Person_Publication> Person_Publication { get; set; }
        public virtual DbSet<PublicationAbstract> PublicationAbstract { get; set; }
        public virtual DbSet<PublicationDetail> PublicationDetail { get; set; }
        public virtual DbSet<PublicationTitle> PublicationTitle { get; set; }
        public virtual DbSet<Visibility> Visibility { get; set; }
        public virtual DbSet<Nomes_Portal_ORCID> Nomes_Portal_ORCID { get; set; }
        public virtual DbSet<PortalIdentifier> PortalIdentifier { get; set; }

    }
}
