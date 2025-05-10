
////using Api.SM.Data;
////using Api.SM.Models;
////using Microsoft.EntityFrameworkCore;

////namespace Api.SM.Repository;

////public interface IStudentRepository : IRepsitory<StudentModel>
////{
////    Task<RowModel> GetRowAsync(string id);   // تعديل دالة GetRow لجعلها غير متزامنة.
////    //Task<ICollection<ModulModel>> GetModulsAsync(string id);  // تعديل دالة GetModuls لجعلها غير متزامنة.
////    //Task<ICollection<TeacherModel>> GetTeachersAsync(string id);  // تعديل دالة GetTeachers لجعلها غير متزامنة.
////    //Task<bool> UpdateCardAsync(CardModel card);   // تعديل UpdateCard لتكون غير متزامنة.
////}

////public class StudentRepository : Repository<StudentModel>, IStudentRepository
////{
////    private readonly INameRepository _nameRepository;
////    private readonly IRowRepository _rowRepository;

////    public StudentRepository(DataContext context, INameRepository nameRepository, IRowRepository rowRepository) : base(context)
////    {
////        _nameRepository = nameRepository;
////        _rowRepository = rowRepository;
////    }



////    public async Task InfoAsync()
////    {
////        var students = await GetAllAsync();
////        foreach (var student in students)
////        {
////            Console.WriteLine($"Student: {student.Card.Name} - Row: {student.Row?.Name}");
////        }
////    }

////    // تنفيذ GetAllAsync لاسترجاع جميع الطلاب
////    public async Task<IEnumerable<StudentModel>> GetAllAsync()
////    {
////        return await _dbSet.ToListAsync();
////    }

////    public Task<RowModel> GetRowAsync(string id)
////    {
////        throw new NotImplementedException();
////    }
////}



////using Api.SM.Data;
////using Api.SM.Models;
////using Api.SM.Repository;
////using Microsoft.EntityFrameworkCore;
////public interface IStudentRepository : IRepsitory<StudentModel>
////{
////    Task<RowModel?> GetRowAsync(string studentId);
////    Task<ICollection<ModulModel>> GetModulsAsync(string studentId);
////    Task<ICollection<TeacherModel>> GetTeachersAsync(string studentId);
////    Task<bool> UpdateCardAsync(string studentId, CardModel newCard);
////    Task InfoAsync();
////}

////public class StudentRepository : Repository<StudentModel>, IStudentRepository
////{
////    private readonly INameRepository _nameRepository;
////    private readonly IRowRepository _rowRepository;
////    private readonly ICardRepository _CardRepository;

////    public StudentRepository(DataContext context, INameRepository nameRepository, ICardRepository CardRepository, IRowRepository rowRepository)
////        : base(context)
////    {
////        _nameRepository = nameRepository;
////        _rowRepository = rowRepository;
////        _CardRepository = CardRepository;
////    }

////    public async Task<RowModel?> GetRowAsync(string studentId)
////    {
////        var student = await _dbSet.Include(s => s.Row).FirstOrDefaultAsync(s => s.Id == studentId);
////        return student?.Row;
////    }

////    public async Task<ICollection<ModulModel>> GetModulsAsync(string studentId)
////    {
////        var student = await _dbSet.Include(s => s.Moduls).FirstOrDefaultAsync(s => s.Id == studentId);
////        return student?.Moduls ?? new List<ModulModel>();
////    }

////    public async Task<ICollection<TeacherModel>> GetTeachersAsync(string studentId)
////    {
////        var student = await _dbSet.Include(s => s.Teachers).FirstOrDefaultAsync(s => s.Id == studentId);
////        return student?.Teachers ?? new List<TeacherModel>();
////    }

////    public async Task<bool> UpdateCardAsync(string studentId, CardModel newCard)
////    {
////        var student = await _dbSet.Include(s => s.Card).FirstOrDefaultAsync(s => s.Id == studentId);
////        if (student == null)
////            return false;

////        student.Card = newCard;
////      //  await _context.SaveChangesAsync();
////        return true;
////    }

////    public async Task InfoAsync()
////    {
////        var students = await _dbSet
////            .Include(s => s.Row)
////            .Include(s => s.Card)
////            .ToListAsync();

////        foreach (var student in students)
////        {
////            var studentName = student.Card?.Name?.FullName ?? "Unknown";
////            var rowName = student.Row?.Name ?? "No Row";

////            Console.WriteLine($"Student: {studentName} - Row: {rowName}");
////        }
////    }
////}
///


//using Api.SM.Models;

//using Api.SM.Data;

//using Api.SM.Repository;
//using Microsoft.EntityFrameworkCore;


//    public interface IStudentRepository : IRepsitory<StudentModel>
//{
//    Task<RowModel?> GetRowAsync(string Id);
//    Task<ICollection<ModulModel>> GetModulsAsync(string studentId);
//    Task<ICollection<TeacherModel>> GetTeachersAsync(string studentId);
//    Task<bool> UpdateCardAsync(string studentId, CardModel newCard);
//    Task InfoAsync();


//    //Task IEnumerable<StudentModel> GetAll();
//}

//public class StudentRepository : Repository<StudentModel>, IStudentRepository
//{
//    private readonly INameRepository _nameRepository;
//    private readonly IRowRepository _rowRepository;
// //   private readonly ICardRepository _cardRepository;

//    public StudentRepository(DataContext context,
//        INameRepository nameRepository,
//     //   ICardRepository cardRepository,
//        IRowRepository rowRepository)
//        : base(context)
//    {
//        _nameRepository = nameRepository;
//        _rowRepository = rowRepository;
//       // _cardRepository = cardRepository;
//    }

//    public override async Task<StudentModel?> GetByIdAsync(string id)
//    {
//        var student =  await base.GetByIdAsync(id);

//        if (student == null)
//        {
//            return null;
//        }
//        var row = await _rowRepository.GetByIdAsync(student.RowId);

//        student.School = row?.School;
//        student.Row = row;
//        return student;
//    }
//    //public override async Task<StudentModel?> CreateAsync(StudentModel entity)
//    //{


//    //    var row =await _rowRepository.GetByIdAsync(entity.RowId);

//    //    if (row == null||row.SchoolId==entity.SchoolId)
//    //    {
//    //       return null;
//    //    }



//    //  return  await base.CreateAsync(entity);

//    //}



//    public async Task<RowModel?> GetRowAsync(string Id)
//    {
//        // تحميل بيانات الطلاب مع Row باستخدام Include.
//        var student = await _dbSet.Include(s => s.Row).FirstOrDefaultAsync(s => s.Id == Id);
//        return student?.Row;
//    }

//    public async Task<ICollection<ModulModel>> GetModulsAsync(string studentId)
//    {
//        // تحميل بيانات الطلاب مع Moduls باستخدام Include.
//        var student = await _dbSet.Include(s => s.Moduls).FirstOrDefaultAsync(s => s.Id == studentId);
//        return student?.Moduls ?? new List<ModulModel>();
//    }

//    public async Task<ICollection<TeacherModel>> GetTeachersAsync(string studentId)
//    {
//        // تحميل بيانات الطلاب مع Teachers باستخدام Include.
//        var student = await _dbSet.Include(s => s.Teachers).FirstOrDefaultAsync(s => s.Id == studentId);
//        return student?.Teachers ?? new List<TeacherModel>();
//    }

//    public async Task<bool> UpdateCardAsync(string studentId, CardModel newCard)
//    {
//        var student = await _dbSet.Include(s => s.Card).FirstOrDefaultAsync(s => s.Id == studentId);
//        if (student == null)
//            return false;

//        student.Card = newCard; // تحديث بطاقة الطالب
//                                // await _context.SaveChangesAsync(); // حفظ التغييرات في DbContext
//        return true;
//    }

//    public async Task InfoAsync()
//    {
//        // استعراض معلومات الطلاب مع Row و Card.
//        var students = await _dbSet
//            .Include(s => s.Row)
//            .Include(s => s.Card)
//            .ToListAsync();

//        foreach (var student in GetAllAsync())
//        {
//            var studentName = student?.Name?.FullName ?? "Unknown";
//            var rowName = student.Row?.Name ?? "No Row";

//            Console.WriteLine($"Student: {studentName} - Row: {rowName}");
//        }
//    }
//    public IEnumerable<StudentModel> GetAllAsync()
//    {
//        return _dbSet.ToList();
//    }
//}

using Api.SM.Data;
using Api.SM.Models;
using Api.SM.VM;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.SM.Repository
{
    public interface IStudentRepository : IRepsitory<StudentModel>
    {
        Task<RowModel?> GetRowAsync(string studentId);
        Task<ICollection<ModulModel>> GetModulsAsync(string studentId);
        Task<ICollection<TeacherModel>> GetTeachersAsync(string studentId);
        Task<bool> UpdateCardAsync(string studentId, CardModel newCard);
        Task<IEnumerable<StudentModel>> SearchByNameAsync(string name);
        Task<IEnumerable<StudentModel>> SearchAsync(string keyword);
    }

    public class StudentRepository : Repository<StudentModel>, IStudentRepository
    {
        private readonly INameRepository _nameRepository;
        private readonly IRowRepository _rowRepository;
     

        public StudentRepository(
            DataContext context,
            INameRepository nameRepository,
            IRowRepository rowRepository
        ) : base(context)
        {
            _nameRepository = nameRepository;
            _rowRepository = rowRepository;
           
        }

        public override async Task<StudentModel?> GetByIdAsync(string id)
        {
            var student = await _dbSet
                .Include(s => s.Name)
                .Include(s => s.Card)
                .Include(s => s.Row)
                .ThenInclude(r => r.School)
                .FirstOrDefaultAsync(s => s.Id == id);

            return student;
        }
        public override async Task<IEnumerable<StudentModel>?> GetAllAsync()
        {
            return await _dbSet
                .Include(s => s.Name) // ✅ تضمين Name بالكامل
               // .Include(s => s.Row)
                .ToListAsync();
        }

        public override async Task<StudentModel?> CreateAsync(StudentModel entity)
        {
            
           
            // تحقق من وجود الصف
            if (string.IsNullOrWhiteSpace(entity.RowId))
                return null;
            

            var row = await _rowRepository.GetByIdAsync(entity.RowId);
            if (row == null || row.SchoolId != entity.SchoolId)
                return null;
            bool exists = await _dbSet.AnyAsync(s => s.Name.Id == entity.Name.Id );
            if (exists)
                return null;

            return await base.CreateAsync(entity);
        }
      
        public async Task<IEnumerable<StudentModel>> SearchByNameAsync(string name)
        {
            return await _dbSet
                .Include(s => s.Name)
                .Where(s => s.Name.Name.Contains(name) || s.Name.Title.Contains(name))
                .ToListAsync();
        }
        public async Task<IEnumerable<StudentModel>> SearchAsync(string keyword)
        {
            return await _dbSet
                .Include(s => s.Name)
                .Where(s =>
                    s.Name.Name.Contains(keyword) ||
                    s.Name.Title.Contains(keyword) ||
                    (s.Name.Name + " " + s.Name.Title).Contains(keyword)
                )
                .ToListAsync();
        }

        public async Task<RowModel?> GetRowAsync(string studentId)
        {
            var student = await _dbSet
                .Include(s => s.Row)
                .FirstOrDefaultAsync(s => s.Id == studentId);


            return student?.Row;
        }
        public  async Task<bool> DeleteAsync(string Id)
        {
            var students = await _dbSet.Where(s => s.SchoolId == Id || s.RowId == Id).ToListAsync();
            if (students.Any())
            {
                _dbSet.RemoveRange(students);
            }
            await base.DeleteAsync(Id);
            return true;
        }

        public async Task<ICollection<ModulModel>> GetModulsAsync(string studentId)
        {
            var student = await _dbSet
                .Include(s => s.Moduls)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            return student?.Moduls ?? new List<ModulModel>();
        }

        public async Task<ICollection<TeacherModel>> GetTeachersAsync(string studentId)
        {
            var student = await _dbSet
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            return student?.Teachers ?? new List<TeacherModel>();
        }

        public async Task<bool> UpdateCardAsync(string studentId, CardModel newCard)
        {
            var student = await _dbSet
                .Include(s => s.Card)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return false;

            student.Card = newCard;
           // await _dbSet.SaveChangesAsync();
            return true;
        }

       
    }
}
