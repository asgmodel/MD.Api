
using Api.SM.Data;
using Api.SM.Models;
using Api.SM.VM;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Api.SM.VM;

//public class CreateNameVM
//{
//    public string Name { get; set; }
//    public string Title { get; set; }
//}
//public class UpdateNameVM
//{
//    public string Id { get; set; }
//    public string Name { get; set; }
//    public string Title { get; set; }
//}
//public class NameVM
//{
//    public string Id { get; set; }
//    public string FullName { get; set; }
//}
//public class CreateCardVM
//{
//    public CreateNameVM Name { get; set; }
//    public DateTime Date { get; set; }
//    public SexType? SexType { get; set; }
//}
//public class CardVM
//{
//    public string Id { get; set; }
//    public string FullName { get; set; }
//    public DateTime Date { get; set; }
//    public string SexType { get; set; }
//}
//public class UpdateCardVM
//{
//    public string Id { get; set; }
//    public CreateNameVM FullName { get; set; }
//    public DateTime Date { get; set; }
//    public SexType? SexType { get; set; }
//}
//public class CreateStudentVM
//    {
//        public string? RowId { get; set; }
//        public string? SchoolId { get; set; }
//        public string? CardId { get; set; } // نفترض أن الكارت موجود
//        public List<string>? ModulIds { get; set; } = new();
//        public List<string>? TeacherIds { get; set; } = new();
//    }


//public class UpdateStudentVM
//{
//    public string Id { get; set; }
//    public string? RowId { get; set; }
//    public string? SchoolId { get; set; }
//    public string? CardId { get; set; }
//    public List<string>? ModulIds { get; set; } = new();
//    public List<string>? TeacherIds { get; set; } = new();
//}

//public class StudentVM
//{
//    public string Id { get; set; }
//    public CreateRowsVM? Row { get; set; }
//    public string? SchoolName { get; set; }
//    public string? CardName { get; set; }
//   // public List<string>? ModulNames { get; set; }
//   // public List<string>? TeacherNames { get; set; }
//}
//public class CreateSchoolVM
//{
//    public string Id { get; set; } = string.Empty;

//    public string Name { get; set; }
//    public ICollection<RowModel> Rows { get; set; } = new List<RowModel>();

//    // أي خصائص أخرى تحتاجها
//}

//// نموذج لتحديث بيانات المدرسة
//public class UpdateSchoolVM
//{
//    public string Id { get; set; }
//    public string Name { get; set; }
//    // أي خصائص أخرى تحتاجها
//}Required.AllowNull

public enum SexTypeVM
{
    Male,
    Famle,
}
public class CreateNameVM
{
    public string Name { get; set; }
    public string Title { get; set; }
}
public class NameVM
{
   public string Id { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
    public string FullName => $"{Name} {Title}";
}
public class CardVM
{
    public string? Id { get; set; }// = Guid.NewGuid().ToString();

    public DateTime Date { get; set; }

    public string? SchoolId { get; set; }


    public string? StudentId { get; set; }

    public string? RowId { get; set; }

    public string? Academic { get; set; } //
    public string? Stage { get; set; }

}
public class CreateCardVM
{

    public DateTime Date { get; set; }

    public string? SchoolId { get; set; }


    public string? StudentId { get; set; }

    public string? RowId { get; set; }

    public string? Academic { get; set; } //
    public string? Stage { get; set; }

}
public class CreateSchoolVM
{
    public string? Name { get; set; }
}
public class SchoolVM
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<RowVM> Rows { get; set; } = new List<RowVM>();
    public ICollection<ModulVM> Moduls { get; set; } = new List<ModulVM>();
    public ICollection<TeacherVM> Teachers { get; set; } = new List<TeacherVM>();
    public ICollection<StudentVM> StudentsVM { get; set; } = new List<StudentVM>();
}
public class CreateRowVM
{
  
    public string Name { get; set; }
    public string? SchoolId { get; set; }
}

public class RowVM
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? SchoolId { get; set; }
    public CreateSchoolVM? School { get; set; }
    public ICollection<ModulVM> Moduls { get; set; } = new List<ModulVM>();
    public ICollection<TeacherVM> Teachers { get; set; } = new List<TeacherVM>();
    public ICollection<StudentVM> StudentsVM { get; set; } = new List<StudentVM>();

}

//public class ModulVM
//{
//    public string Id { get; set; }
//    public string Name { get; set; }
//    public string RowId { get; set; }
//    public string RowName { get; set; }
//}
//public enum SexTypeVM
//{
//    Male = 0,     // ذكر
//    Female = 1    // أنثى
//}

public class CreateStudentVM
{
 
    public CreateNameVM? Name { get; set; }
    public string? RowId { get; set; }
    public string? SchoolId { get; set; }
    public int? Age { get; set; } // نفترض أن الكارت موجود
}

public class StudentVM
{
    public string Id { get; set; }
    public string? RowId { get; set; }
    public NameVM? Name { get; set; }
    public int? Age { get; set; } // نفترض أن الكارت موجود

    public RowVM? Row { get; set; }
    public CreateCardVM? Card { get; set; }
    public string? SchoolId { get; set; }
    public SchoolVM? School { get; set; }
    public ICollection<ModulVM> Moduls { get; set; } = new List<ModulVM>();
    public ICollection<TeacherVM> Teachers { get; set; } = new List<TeacherVM>();
}

//public class TeacherVM
//{
   
//    public string Name { get; set; }
//    public string RowId { get; set; }
//    public string RowName { get; set; }
//}
//public class CreateTeacherVM
//{
//    public NameVM? Name { get; set; }
//    public string? ModulId { get; set; }
//  //  public List<ModulVM>? ModulIds { get; set; }
//   // public List<StudentVM>? StudentIds { get; set; }
//    public string? SchoolId { get; set; }
//}

public class TeacherVM
{
    public string Id { get; set; }

    public string? NameId { get; set; }
    public NameVM? Name { get; set; }

    public List<RowVM> Rows { get; set; } = new();
    public List<ModulVM> Moduls { get; set; } = new();
    public List<StudentVM> Students { get; set; } = new();
    public List<SchoolVM> Schools { get; set; } = new();
}
public class CreateModulVM
{
    public string? Name { get; set; }
    public string RowId { get; set; }
   // public string? TeacherId { get; set; }
}

public class ModulVM
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string RowId { get; set; }
  //  public RowVM? Row { get; set; }
   // public List<TeacherVM> Teachers { get; set; } = new();
   // public List<StudentVM> Students { get; set; } = new();
  //  public ICollection<SchoolTeacherVM> SchoolModels { get; set; } = new List<SchoolTeacherVM>();
    //public ICollection<ModulsTeacherVM> ModulsTeachers { get; set; } = new List<ModulsTeacherVM>();
}

public class CreateTeacherVM
{
    public CreateNameVM? Name { get; set; }
    public string? SchoolId { get; set; }
    public string? ModelId { get; set; }

    // public ICollection<SchoolTeacherVM> SchoolModels { get; set; } = new List<SchoolTeacherVM>();
    //public ICollection<ModulsTeacherVM> ModulsTeachers { get; set; } = new List<ModulsTeacherVM>();
    //public ICollection<SchoolVM> SchoolModels { get; set; } = new List<SchoolVM>();
    // public ICollection<CreateModulVM> ModulsTeachers { get; set; } = new List<CreateModulVM>();


}
public class CreateModulsTeacherVM
{
    public string? ModelId { get; set; }
    public string? TeacherId { get; set; }
}

public class ModulsTeacherVM
{
    public string? Id { get; set; }

    public string? ModelId { get; set; }
    public ModulVM? ModulsVM { set; get; }

    public string? TeacherId { set; get; }
    public TeacherVM? TeacherVM { set; get; }
}

public class SchoolTeacherVM
{
    public string? Id { get; set; }

    public string? SchoolId { get; set; }
    public SchoolVM SchoolVM { set; get; }

    public string TeacherId { set; get; }
    public TeacherVM? TeacherVM { set; get; }
}

public class CreateSchoolTeacherVM
{

    public string? SchoolModelId { get; set; }


    public string TeacherId { get; set; }
}

//public class CreateSchoolTeacherVM
//{
//    public string SchoolId { get; set; }
//    public string TeacherId { get; set; }
//}
//public class CreateSchoolTeacherVM
//{
//    public string SchoolModelId { get; set; }   // ✅ ليس فقط SchoolId
//    public string TeacherId { get; set; }
//}
