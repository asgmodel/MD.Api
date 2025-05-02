//// Ê«ÃÂ… ISchoolRepository
//using Api.SM.Data;
//using Api.SM.Models;
//using Api.SM.Repository;
//using Api.SM.VM;
//using Microsoft.EntityFrameworkCore;

//public interface ISchoolRepository : IRepsitory<SchoolModel>
//{
//    Task AddRowAsync(string rowId, string schoolId);
//    Task<IEnumerable<RowModel>> GetRowsBySchoolIdAsync(string schoolId);
//    Task AddStudent(string studentid, string schoolId);

//}

////  ‰›Ì– SchoolRepository
//public class SchoolRepository : Repository<SchoolModel>, ISchoolRepository
//{
//    private readonly IRowRepository _rowRepository;
//    private readonly IStudentRepository _studentRepository;

//    public SchoolRepository(DataContext context, IRowRepository rowRepository, IStudentRepository studentRepository) : base(context)
//    {
//        _rowRepository = rowRepository;
//        _studentRepository = studentRepository;
//    }

//    public override async Task CreateAsync(SchoolModel entity)
//    {
//        bool isNameTaken = _dbSet.Any(s => s.Name == entity.Name);
//        if (isNameTaken)
//        {
//            throw new InvalidOperationException("A school with the same name already exists.");
//        }

//        await base.CreateAsync(entity);
//    }

//    public async Task AddRowAsync(string rowId, string schoolId)
//    {
//        var row = await _rowRepository.GetByIdAsync(rowId);
//        if (row == null)
//        {
//            throw new InvalidOperationException("Row not found.");
//        }

//        var school = await GetByIdAsync(schoolId);
//        if (school == null)
//        {
//            throw new InvalidOperationException("School not found.");
//        }

//        if (school.Rows.Any(r => r.Id == row.Id))
//        {
//            throw new InvalidOperationException("Row already assigned to this school.");
//        }

//        school.Rows.Add(row);
//        row.SchoolId = school.Id;

//        await UpdateAsync(school); //  Õ›Ÿ «· ⁄œÌ·« 
//    }
//    public async Task AddStudent(string studentid, string schoolId)
//    {

//        var studen = await _studentRepository.GetByIdAsync(studentid);
//        if (studen == null)
//        {
//            throw new InvalidOperationException("Students not found.");
//        }

//         var school = await GetByIdAsync(schoolId);
//        if (school == null)
//        {
//            throw new InvalidOperationException("School not found.");
//        }

//        if (school.Students.Any(r => r.Id == studen.Id))
//        {
//            throw new InvalidOperationException("Students already assigned to this school.");
//        }
//        //studen.Name = studen.card.Name.FullName;
//        school.Students.Add(studen);
//        studen.SchoolId = school.Id;

//        await UpdateAsync(school);

//    }
//    //public async Task AddStudent(StudentModel student, string rowId)
//    //{

//    //    var row = await _rowRepository.GetByIdAsync(rowId);

//    //    if (row == null)
//    //    {
//    //        throw new Exception("Row not found");
//    //    }
//    //    if (Validator.Validate(row.Students, student))
//    //    {

//    //        student.Row = row;
//    //        student.RowId = row.Id;
//    //        //student.Name = student.Card.Name.FullName;
//    //        _rowRepository.AddStudent(rowId, student);

//    //        // _studentRepository.Add(student);
//    //        // row.Students.Add(student);
//    //    }
//    //}

//    public async Task<IEnumerable<RowModel>> GetRowsBySchoolIdAsync(string schoolId)
//    {
//        var school = await _dbSet
//            .Include(s => s.Rows)
//            .FirstOrDefaultAsync(s => s.Id == schoolId);

//        if (school == null)
//        {
//            throw new InvalidOperationException("School not found.");
//        }

//        return school.Rows;
//    }
//    public IEnumerable<SchoolModel> GetAllAsync()
//    {
//        return _dbSet.ToList();
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
// Ê«ÃÂ… ISchoolRepository
using Api.SM.Data;
using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using Microsoft.EntityFrameworkCore;

public interface ISchoolRepository : IRepsitory<SchoolModel>
{
    Task AddRowAsync(string rowId, string schoolId);
    Task<IEnumerable<RowModel>> GetRowsBySchoolIdAsync(string schoolId);
    Task AddStudent(string studentid, string schoolId);
    Task<CardModel> IssuingCardStudent(CardModel card);
    //Task<CardModel> IssuingCardStudent(CardModel card);

}

//  ‰›Ì– SchoolRepository
public class SchoolRepository : Repository<SchoolModel>, ISchoolRepository
{
    private readonly IRowRepository _rowRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICardRepository _cardRepository;

    public SchoolRepository(DataContext context, IRowRepository rowRepository, IStudentRepository studentRepository, ICardRepository cardRepository) : base(context)
    {
        _rowRepository = rowRepository;
        _studentRepository = studentRepository;
        _cardRepository = cardRepository;
    }


    //public override async Task<SchoolModel?> GetByIdAsync(string schoolId)
    //{
    //    return await _dbSet.FirstOrDefaultAsync(s => s.Id == schoolId);
    //}

    public async Task AddRowAsync(string rowId, string schoolId)
    {
        var row = await _rowRepository.GetByIdAsync(rowId)
            ?? throw new InvalidOperationException("Row not found.");

        var school = await GetByIdAsync(schoolId)
            ?? throw new InvalidOperationException("School not found.");

        if (school.Rows.Any(r => r.Id == row.Id))
            throw new InvalidOperationException("Row already assigned to this school.");

        school.Rows.Add(row);
        row.SchoolId = school.Id;

        await UpdateAsync(school);
    }

    public async Task AddStudent(string studentId, string schoolId)
    {
        var student = await _studentRepository.GetByIdAsync(studentId)
            ?? throw new InvalidOperationException("Student not found.");

        var school = await GetByIdAsync(schoolId)
            ?? throw new InvalidOperationException("School not found.");

        await  _studentRepository.CreateAsync(student);
        //school.Students.Add(student);
        student.SchoolId = school.Id;

        await UpdateAsync(school);
    }

    

    public async Task<IEnumerable<RowModel>> GetRowsBySchoolIdAsync(string schoolId)
    {
        var school = await _dbSet
            .Include(s => s.Rows)
            .FirstOrDefaultAsync(s => s.Id == schoolId);

        if (school == null)
        {
            throw new InvalidOperationException("School not found.");
        }

        return school.Rows;
    }
    public IEnumerable<SchoolModel> GetAllAsync()
    {
        return _dbSet.ToList();
    }

    //    public async Task<CardModel> IssuingCardStudent(CardModel card)
    //    {


    //        var student =await _studentRepository.GetByIdAsync(card.StudentId);
    //        if (student == null&&student.SchoolId!=card.SchoolId&&student.RowId!=card.RowId)
    //        {
    //            return null;
    //        }

    //        return await _cardRepository.CreateAsync(card);
    //    }
    public async Task<CardModel> IssuingCardStudent(CardModel card)
    {
        var student = await _studentRepository.GetByIdAsync(card.StudentId);


        if (student?.SchoolId != card.SchoolId || student?.RowId != card.RowId)
        {
            return null;
        }

        return await _cardRepository.CreateAsync(card);
    }

}

public abstract class Validator
{
    public static bool Validate<T>(ICollection<T> items, T item) where T : class
    {

        foreach (var ob in items)
        {
            if (ob == item)
            {
                Console.WriteLine("Error: Null item found in the list.");
                return false;
            }

        }

        return true;

    }

}
