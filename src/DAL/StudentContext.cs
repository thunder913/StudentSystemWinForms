using System;
using System.Data.Entity;
using StudentSystemWinForms.Migrations;
using StudentSystemWinForms.MVVM.Model.DB;

namespace StudentSystemWinForms.DAL
{
    public class StudentContext : DbContext
    {
        public StudentContext() : base("StudentSystemDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}
