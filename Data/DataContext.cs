
using Api.SM.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.SM.Data
{
public class DataContext :DbContext
{
    // Add properties like DbSet for your models
   

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<NameModel> NameModels { get; set; }



    public DbSet<StudentModel> StudentModels { get; set; }


    public DbSet<RowModel> RowModels { get; set; }
    public DbSet<SchoolModel> SchoolModels { get; set; }
    public DbSet<ModulModel> ModulModels { get; set; }
    public DbSet<TeacherModel> TeacherModels { get; set; }
    public DbSet<CardModel> CardModels { get; set; }
    public DbSet<SchoolModel> Schools { get; set; }
    public DbSet<ModulModel> Moduls { get; set; }
   // public DbSet<TeacherModel> Teachers { get; set; }
    public DbSet<StudentModel> Students { get; set; }
    public DbSet<ModulsTeacher> ModulsTeachers { get; set; }
    public DbSet<SchoolTeacher> SchoolTeachers { get; set; }

        // You can add any custom functions for DbContext here
    }

}
