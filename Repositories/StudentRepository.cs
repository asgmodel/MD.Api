
////using Api.SM.Data;
////using Api.SM.Models;

////namespace Api.SM.Repository;

////public interface IStudentRepository : IRepsitory<StudentModel>
////{
////    RowModel GetRow(string id);

////    ICollection<ModulModel> GetModuls(string id);

////    ICollection<TeacherModel> GetTeachers(string id);

////    bool UpdateCard(CardModel card);
////}
////public class StudentRepository : Repository<StudentModel>, IStudentRepository
////{
////    private readonly INameRepository nameRepository;
////    public StudentRepository(DataContext context,INameRepository nameReposi) : base(context)
////    {
////        nameRepository = nameReposi ;


////    }

////    public ICollection<ModulModel> GetModuls(string id)
////    {
////        throw new NotImplementedException();
////    }

////    public RowModel GetRow(string id)
////    {
////        throw new NotImplementedException();
////    }

////    public ICollection<TeacherModel> GetTeachers(string id)
////    {
////        throw new NotImplementedException();
////    }

////    public bool UpdateCard(CardModel card)
////    {
////        throw new NotImplementedException();
////    }
////    public void Info()
////    {
////        foreach (var student in GetAllAsync())
////        {

////            // Console.WriteLine($" Student: {student.card.Name.FullName} - Row: {student.Row?.Name}");
////            Console.WriteLine($" Student kk: {student.Name} - Row: {student.Row?.Name}");

////        }
////    }

////    public  IEnumerable<StudentModel> GetAllAsync()
////    {
////        return  ;
////    }

//////}
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
using Api.SM.Models;

using Api.SM.Data;
using Api.SM.Models;
using Api.SM.Repository;
using Microsoft.EntityFrameworkCore;


    public interface IStudentRepository : IRepsitory<StudentModel>
{
    Task<RowModel?> GetRowAsync(string Id);
    Task<ICollection<ModulModel>> GetModulsAsync(string studentId);
    Task<ICollection<TeacherModel>> GetTeachersAsync(string studentId);
    Task<bool> UpdateCardAsync(string studentId, CardModel newCard);
    Task InfoAsync();
    //Task IEnumerable<StudentModel> GetAll();
}

public class StudentRepository : Repository<StudentModel>, IStudentRepository
{
    private readonly INameRepository _nameRepository;
    private readonly IRowRepository _rowRepository;
 //   private readonly ICardRepository _cardRepository;

    public StudentRepository(DataContext context,
        INameRepository nameRepository,
     //   ICardRepository cardRepository,
        IRowRepository rowRepository)
        : base(context)
    {
        _nameRepository = nameRepository;
        _rowRepository = rowRepository;
       // _cardRepository = cardRepository;
    }

    public async Task<RowModel?> GetRowAsync(string Id)
    {
        // تحميل بيانات الطلاب مع Row باستخدام Include.
        var student = await _dbSet.Include(s => s.Row).FirstOrDefaultAsync(s => s.Id == Id);
        return student?.Row;
    }

    public async Task<ICollection<ModulModel>> GetModulsAsync(string studentId)
    {
        // تحميل بيانات الطلاب مع Moduls باستخدام Include.
        var student = await _dbSet.Include(s => s.Moduls).FirstOrDefaultAsync(s => s.Id == studentId);
        return student?.Moduls ?? new List<ModulModel>();
    }

    public async Task<ICollection<TeacherModel>> GetTeachersAsync(string studentId)
    {
        // تحميل بيانات الطلاب مع Teachers باستخدام Include.
        var student = await _dbSet.Include(s => s.Teachers).FirstOrDefaultAsync(s => s.Id == studentId);
        return student?.Teachers ?? new List<TeacherModel>();
    }

    public async Task<bool> UpdateCardAsync(string studentId, CardModel newCard)
    {
        var student = await _dbSet.Include(s => s.Card).FirstOrDefaultAsync(s => s.Id == studentId);
        if (student == null)
            return false;

        student.Card = newCard; // تحديث بطاقة الطالب
                                // await _context.SaveChangesAsync(); // حفظ التغييرات في DbContext
        return true;
    }

    public async Task InfoAsync()
    {
        // استعراض معلومات الطلاب مع Row و Card.
        var students = await _dbSet
            .Include(s => s.Row)
            .Include(s => s.Card)
            .ToListAsync();

        foreach (var student in GetAllAsync())
        {
            var studentName = student.Card?.Name?.FullName ?? "Unknown";
            var rowName = student.Row?.Name ?? "No Row";

            Console.WriteLine($"Student: {studentName} - Row: {rowName}");
        }
    }
    public IEnumerable<StudentModel> GetAllAsync()
    {
        return _dbSet.ToList();
    }
}

