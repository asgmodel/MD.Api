
//using Api.SM.Models;
//using Api.SM.Repository;


//public interface IInfo
//{
//    void Info();
//}

//public interface ISchoolRepository : IRepsitory<SchoolModel>, IInfo
//{
//    void AddRow(string studentid, string schoolId);
//    void AddRow(RowModel row);
//    void AddTeacher(string teacherId, string schoolId);
//    void AddStudent(StudentModel student, string rowId);
//    void AddStudent(string studentid,string schoolId);
//    void AddStudent(string teacherId,string schoolId, string rowId);
//    void UpdateSchoolName(string schoolId, string newName);
//    void RemoveStudent(string studentId, string schoolId);
//    void RemoveTeacher(string teacherId, string schoolId);
//    void AddModul(ModulModel modul, string rowId);
//    void AddTeacherToModul(TeacherModel teacher, string modulId);
//    void ShowAll();
//    void SerachStudent(StudentModel name);
//    void SearchStudentByRowName(string rowName);
//}








//public class SchoolRepository : Repository<SchoolModel>, ISchoolRepository
//{

//    private readonly ISchoolRepository _ISchoolRepository;
//    private readonly IRowRepository _rowRepository;
//  // private readonly ITeacherRepository _teacherRepository;
//    private readonly IStudentRepository _studentRepository;
//   // private readonly IModulRepository _modulRepository;
//    private readonly INameRepository _nameRepository;
//    //private readonly ICardRepository _cardRepository;


//    public SchoolRepository()
//    {
//        _rowRepository = new RowRepository();
//        _teacherRepository = new TeacherRepository();
//        _nameRepository = new NameRepository(); 
//        _modulRepository = new ModulRepository();
//        _studentRepository = new StudentRepository();



//    }
//    //public void GetRows()
//    //{

//    //}
//    public void UpdateSchoolName(string schoolId, string newName)
//    {
//        var school = GetById(schoolId);

//        if (school == null)
//        {
//            Console.WriteLine("Error: School not found.");
//            return;
//        }

//        if (string.IsNullOrWhiteSpace(newName))
//        {
//            Console.WriteLine("Error: New name is invalid.");
//            return;
//        }

//        if (school.Name == newName)
//        {
//            Console.WriteLine("Warning: The new name is the same as the current one.");
//            return;
//        }

//           school.Name = newName;
//           Update(school);
//        Console.WriteLine($"School name updated to: {newName}");
//    }
//    public void RemoveStudent(string studentId, string schoolId)
//    {
//        var school = GetById(schoolId);
//        if (school == null)
//        {
//            Console.WriteLine("Error: School not found.");
//            return;
//        }

//        var student = _studentRepository.GetById(studentId);
//        if (student == null)
//        {
//            Console.WriteLine("Error: Student not found.");
//            return;
//        }

//        if (student.Row != null)
//        {
//            var row = _rowRepository.GetById(student.RowId);
//            if (row != null && row.Students.Contains(student))
//            {
//                row.Students.Remove(student);
//            }
//        }

//        if (school.Students.Contains(student))
//        {
//            school.Students.Remove(student);
//        }

//         _studentRepository.Remove(student);

//        Console.WriteLine($"Student with ID {studentId} removed successfully.");
//    }

//    public void RemoveTeacher(string teacherId, string schoolId)
//    {
//        var school = GetById(schoolId);
//        if (school == null)
//        {
//            Console.WriteLine("Error: School not found.");
//            return;
//        }

//        var teacher = _teacherRepository.GetById(teacherId);
//        if (teacher == null)
//        {
//            Console.WriteLine("Error: Teacher not found.");
//            return;
//        }

//        if (school.Teachers.Contains(teacher))
//        {
//            school.Teachers.Remove(teacher);
//        }

//        if (teacher.SchoolModels != null && teacher.SchoolModels.Contains(school))
//        {
//            teacher.SchoolModels.Remove(school);
//        }

//        if (teacher.Moduls != null)
//        {
//            foreach (var modul in teacher.Moduls)
//            {
//                var fullModul = _modulRepository.GetById(modul.Id);
//                if (fullModul?.Teachers != null && fullModul.Teachers.Contains(teacher))
//                {
//                    fullModul.Teachers.Remove(teacher);
//                }
//            }

//            teacher.Moduls.Clear();
//        }
//        _teacherRepository.Remove(teacher);

//        Console.WriteLine($"Teacher with ID {teacherId} removed successfully.");

//    }
//    public void SerachStudent(StudentModel name)
//    {
//        var student = _studentRepository.Serach(name);
//      //  var student = _studentRepository.Serach(name.);
//        if(student == null)
//        {
//            Console.WriteLine("Error: SerachStudent not found.");
//            return;
//        }

//        _studentRepository.Serach(student);
//        Console.WriteLine($"SerachStudent the is {student.Name} removed successfully.");

//    }
//    public void SearchStudentByRowName(string rowName)
//    {
//        var rows = _rowRepository.Search(r =>
//            r.Name != null && r.Name.Contains(rowName, StringComparison.OrdinalIgnoreCase));

//        if (!rows.Any())
//        {
//            Console.WriteLine("  No rows found for this shcool.");
//            return;
//        }

//        foreach (var row in rows)
//        {
//            Console.WriteLine($" rows: {row.Name}");
//            if (row.Students != null && row.Students.Any())
//            {
//                foreach (var student in row.Students)
//                {
//                    Console.WriteLine($"• Students: {student.Name}");
//                }
//            }
//            else
//            {
//                Console.WriteLine("No students found matching the Rows.");

//            }
//        }
//    }


//    public void AddStudent(string studentid, string schoolId)
//    {
//        var studen = _studentRepository.GetById(studentid);
//        var shcool = GetById(schoolId);
//        if (shcool.Students.Contains(studen))
//        {
//            Console.WriteLine("Error: Students already exists in the school.");

//            return;
//        }
//        //var row = _rowRepository.GetById(shcool.Id);
//        //var row = ;

//        //if (row == null)
//        //{
//        //    throw new Exception("Row not found");
//        //}
//        var row = Items.FirstOrDefault(r => r.Id == schoolId);

//        //var row = Items.FirstOrDefault(r => r.Id == id);
//        if (row != null)
//        {
//            //studen.Row.Name = row.Name;
//            // studen.RowId = row.Id;
//            //row.Rows.Add(studen.Row);
//            row.Students.Add(studen);
//        }
//        else
//        {
//            throw new Exception("Row not found");
//        }
//        //studen.Name = studen.card.Name.FullName;
//        studen.Name = studen.Name;
//        //studen.RowId =studen.Row.Id ;

//        //studen.Row.Name = studen.Row.Name;

//       // shcool.Students.Add(studen);
//        //studen.School.Add(studen.Row);
//      // row.Students.Add(studen);
//        studen.SchoolId = shcool.Id;

//    }
//    public void AddStudent(StudentModel student, string rowId)
//    {

//        var row = _rowRepository.GetById(rowId);

//        if (row == null)
//        {
//            throw new Exception("Row not found");
//        }
//        if (Validator.Validate(row.Students, student))
//        {

//            student.Row = row;
//            student.RowId = row.Id;
//            student.Name= student.card.Name.FullName;
//            _rowRepository.AddStudent(rowId, student);

//          //  _studentRepository.Add(student);
//            // row.Students.Add(student);
//        }
//    }



//    public void AddStudent(string studentId, string schoolId,string rowId)
//    {
//        var student = _studentRepository.GetById(studentId);
//        var school = GetById(schoolId);

//        if (school.Students.Contains(student))
//        {
//            Console.WriteLine("Error: Student already exists in the school.");
//            return;
//        }
//        student.Name = student.card.Name.FullName;
//        school.Students.Add(student);
//        student.SchoolId = school.Id;

//        var row = _rowRepository.GetById(rowId);
//        if (row == null)
//            throw new Exception("Row not found");

//        if (!row.Students.Contains(student))
//        {
//            student.RowId = row.Id;
//            student.Row = row;
//            row.Students.Add(student);
//        }


//    }
//    public void AddModul(ModulModel modul, string rowId)
//    {
//        var row = _rowRepository.GetById(rowId);
//        if (row == null) throw new Exception("Row not found");
//        if (Validator.Validate(row.Moduls, modul))
//        {
//            modul.RowId = row.Id;
//            modul.RowName = row.Name;
//            _rowRepository.AddModul(rowId, modul);
//        }
//    }

//    //public void AddModul(ModulModel modul, string rowId)
//    //{
//    //    var row = _rowRepository.GetById(rowId);
//    //    if (row == null) throw new Exception("Row not found");
//    //    if (Validator.Validate(row.Moduls, modul))
//    //    {

//    //        modul.RowId = row.Id;
//    //        modul.RowName = row.Name;

//    //         _rowRepository.AddModul(rowId, modul);
//    //        row.Moduls.Add(modul);
//    //      //  _modulRepository.Add(modul);
//    //        // _modulRepository.AddModulesToRow(row);
//    //     //   _modulRepository.Add(modul);

//    //    }
//    //}
//    public void AddRow(RowModel row)
//    {

//        var shcool = GetById(row.SchoolId);

//        if(shcool == null)
//        {
//            return;
//        }
//         row.School=shcool;


//        //  _rowRepository.Add(row);


//    }

//    public void AddRow(string rowid, string schoolId)
//    {
//        var row = _rowRepository.GetById(rowid);
//        var shcool = GetById(schoolId);
//        if (shcool.Rows.Contains(row))
//        {
//            Console.WriteLine("Error: Teacher already exists in the school.");

//            return;
//        }
//        shcool.Rows.Add(row);
//        row.SchoolId = shcool.Id;
//        //studen.SchoolModel.Add(shcool);

//        //  studen.SchoolId.Add(shcool.Id);  
//    }

//    public void AddTeacher(string teacherId,string schoolId)
//    {
//        var  teache = _teacherRepository.GetById(teacherId);
//        var shcool = GetById(schoolId);
//        if(shcool.Teachers.Contains(teache))
//        {
//            Console.WriteLine("Error: Teacher already exists in the school.");

//            return;
//        }
//        shcool.Teachers.Add(teache);

//        teache.SchoolModels.Add(shcool);




//    }
//    public void AddTeacherToModul(TeacherModel teacher, string modulId)
//    {
//        var modul = _modulRepository.GetById(modulId); 
//        if (modul == null)
//            throw new Exception("Modul not found");

//        modul.Teachers ??= new List<TeacherModel>();  

//        if (!modul.Teachers.Contains(teacher))
//        {
//            modul.Teachers.Add(teacher);  
//        }

//        teacher.Moduls ??= new List<ModulModel>();  
//        if (!teacher.Moduls.Contains(modul))
//        {
//            teacher.Moduls.Add(modul);  
//        }
//    }


//    //public void AddTeacherToModul(TeacherModel teacher, string modulId)
//    //{
//    //        var modul = _modulRepository.GetById(modulId);
//    //        if (modul == null)
//    //            throw new Exception("Module not found");
//    //    if (Validator.Validate(modul.Teachers, teacher))
//    //    {

//    //        if (modul.Teachers == null)
//    //            modul.Teachers = new List<TeacherModel>();



//    //        if (!modul.Teachers.Contains(teacher))
//    //            modul.Teachers.Add(teacher);


//    //        //_rowRepository.AddTeacherToModul(teacher, modulId);

//    //    }


//    //}


//    public void Info()
//    {
//        foreach (var school in GetAll())
//        {
//            Console.WriteLine($" School: {school.Name},Row: {school.Rows?.Count}, Students: {school.Students?.Count}, Modules: {school.Moduls?.Count}, Teachers : {school.Teachers?.Count}");

//        }
//    }
//    public IEnumerable<SchoolModel> GetAll()
//    {
//        return Items.ToList();
//    }


//    public void ShowAll()
//    {
//        Console.WriteLine("===== School Info =====");

//        _teacherRepository.Info();
//        _studentRepository.Info();
//        _modulRepository.Info();
//        _rowRepository.Info();
//    }



//}
//public abstract class Printer
//{
//    public static void PrintAll(params IEnumerable<IInfo>[] collections)
//    {
//        foreach (var collection in collections)
//        {
//            foreach (var item in collection)
//            {
//                item.Info();
//            }

//        }
//    }

//}

//public abstract class Validator
//{
//    public static bool Validate<T>(ICollection<T> items, T item) where T : class
//    {

//        foreach (var ob in items)
//        {
//            if (ob == item)
//            {
//                Console.WriteLine("Error: Null item found in the list.");
//                return false;
//            }

//        }

//        return true;

//    }
//}







////public void ShowAll()
////{
////    Console.WriteLine("===== School Info =====");

////    Console.WriteLine("== Teachers ==");
////    foreach (var teacher in _teacherRepository.GetAll())
////    {
////        Console.WriteLine($" Teacher: {teacher.Name}");
////    }

////    Console.WriteLine("\n== Students ==");
////    foreach (var student in _studentRepository.GetAll())
////    {
////        Console.WriteLine($"Student: {student.card.Name.FullName}, Row: {student.Row?.Name}");
////    }

////    Console.WriteLine("\n== Modules ==");
////    foreach (var modul in _modulRepository.GetAll())
////    {
////        Console.WriteLine($" Module: {modul.Name}, Row: {modul.RowName}, Teacher:");
////    }

////    Console.WriteLine("\n== Rows ==");
////    foreach (var row in _rowRepository.GetAll())
////    {
////        Console.WriteLine($" Row: {row.Name}, Students: {row.Students?.Count}, Modules: {row.Moduls?.Count}");
////    }
////}    
///
