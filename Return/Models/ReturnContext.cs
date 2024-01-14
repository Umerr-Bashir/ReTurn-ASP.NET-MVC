using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Return.Models
{
    public class ReturnContext:DbContext
    {
        public ReturnContext():base("ConnectionString")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            { 
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ClassIncharge> ClassIncharges { get; set; }
        public DbSet<StudentClassEnrollment> StudentClassEnrollments { get; set; }

    }


}