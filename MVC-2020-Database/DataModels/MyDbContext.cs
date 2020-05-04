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
    }
}
