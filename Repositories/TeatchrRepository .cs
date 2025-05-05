//using Api.SM.Data;
//using Api.SM.Models;
//using Api.SM.Repository;
//using Api.SM.VM;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//public interface ITeacherRepository : IRepsitory<TeacherModel>
//{
//  //  Task<TeacherModel?> GetTeacherWithRelationsAsync(string id);
//}
//public class TeacherRepository : Repository<TeacherModel>, ITeacherRepository
//{
//    private readonly IModulRepository _IModulRepository;

//    public TeacherRepository(DataContext context, IModulRepository modulRepository) : base(context)
//    {
//        _IModulRepository = modulRepository;

//    }


//    //public async Task<List<TeacherModel>> GetAllAsync()
//    //{
//    //    return await _dbSet.Teachers
//    //        .Include(t => t.Name)
//    //        .Include(t => t.SchoolModels)
//    //        .ThenInclude(st => st.SchoolModel)
//    //        .Include(t => t.ModulsTeachers)
//    //        .ThenInclude(mt => mt.ModelModuls)
//    //        .ToListAsync();
//    //}
//    public override Task<TeacherModel?> CreateAsync(TeacherModel entity)
//    {

//        return base.CreateAsync(entity);
//    }
//    //public async Task<TeacherModel?> GetByIdAsync(string id)
//    //{
//    //    return await _context.Teachers
//    //        .Include(t => t.Name)
//    //        .Include(t => t.SchoolModels)
//    //        .ThenInclude(st => st.SchoolModel)
//    //        .Include(t => t.ModulsTeachers)
//    //        .ThenInclude(mt => mt.ModelModuls)
//    //        .FirstOrDefaultAsync(t => t.Id == id);
//    //}

//    //public async Task CreateAsync(TeacherModel teacher, List<string> schoolIds, List<string> modulIds)
//    //{
//    //    teacher.Id = Guid.NewGuid().ToString();

//    //    // ربط المدارس
//    //    teacher.SchoolModels = schoolIds.Select(sid => new SchoolTeacher
//    //    {
//    //        Id = Guid.NewGuid().ToString(),
//    //        SchoolId = sid,
//    //        TeacherId = teacher.Id
//    //    }).ToList();

//    //    // ربط المواد
//    //    teacher.ModulsTeachers = modulIds.Select(mid => new ModulsTeacher
//    //    {
//    //        Id = Guid.NewGuid().ToString(),
//    //        ModelId = mid,
//    //        TeacherId = teacher.Id
//    //    }).ToList();


//    //}




//    //    public override async Task<TeacherModel?> CreateAsync(TeacherModel entity)
//    //    {


//    //        // تحقق من وجود الصف
//    //        if (string.IsNullOrWhiteSpace(entity.RowId))
//    //            return null;


//    //        var row = await _rowRepository.GetByIdAsync(entity.RowId);
//    //        if (row == null || row.SchoolId != entity.SchoolId)
//    //            return null;
//    //        bool exists = await _dbSet.AnyAsync(s =>
//    //    s.Name == entity.Name
//    //);
//    //        if (exists)
//    //            return null;
//    //        //throw new InvalidOperationException("هذا الطالب مسجل مسبقاً في نفس الصف والمدرسة.");

//    //        return await base.CreateAsync(entity);
//    //    }

//}

////public async Task<TeacherModel?> GetTeacherWithRelationsAsync(string id)
////{
////    return await _dbSet
////        .Include(t => t.Name)
////        .Include(t => t.Rows)
////        .Include(t => t.Moduls)
////        .Include(t => t.Students)
////        .Include(t => t.SchoolModels)

////        .FirstOrDefaultAsync(t => t.Id == id);
////}
///
using Api.SM.Data;
using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface ITeacherRepository : IRepsitory<TeacherModel>
{
    //Task<TeacherModel?> CreateWithRelationsAsync(TeacherModel teacher, List<string> schoolIds, List<string> modulIds);
}

public class TeacherRepository : Repository<TeacherModel>, ITeacherRepository
{
    public TeacherRepository(DataContext context) : base(context) { }
    public override async Task<TeacherModel?> CreateAsync(TeacherModel entity)
    {
        // التحقق من وجود اسم
        //if (entity.Name == null)
        //    return null;

        //// التحقق من وجود مدارس
        //if (entity.SchoolModels == null || !entity.SchoolModels.Any())
        //    return null;

        //// التحقق من وجود مواد
        //if (entity.ModulsTeachers == null || !entity.ModulsTeachers.Any())
        //    return null;

        //// التحقق من صحة معرفات المدارس
        //var schoolIds = entity.SchoolModels.Select(sm => sm.SchoolId).Distinct().ToList();
        //var validSchoolIds = await _dbSet
        //    .Where(s => schoolIds.Contains(s.Id))
        //    .Select(s => s.Id)
        //    .ToListAsync();

        //if (validSchoolIds.Count != schoolIds.Count)
        //    return null; // بعض المدارس غير موجودة

        //// التحقق من صحة معرفات المواد
        //var modulIds = entity.ModulsTeachers.Select(mt => mt.ModelId).Distinct().ToList();
        //var validModulIds = await _dbSet
        //    .Where(m => modulIds.Contains(m.Id))
        //    .Select(m => m.Id)
        //    .ToListAsync();

        //if (validModulIds.Count != modulIds.Count)
        //    return null; // بعض المواد غير موجودة

        // حفظ الكيان الأساسي
       
        var createdTeacher = await base.CreateAsync(entity);

        return createdTeacher;
    }

    ////public async Task<RowModel?> GetRowAsync(string studentId)
    ////{
    ////    var student = await _dbSet
    ////        .Include(s => s.Row)
    ////        .FirstOrDefaultAsync(s => s.Id == studentId);


    ////    return student?.Row;
    ////}
    //public override async Task<TeacherModel?> CreateAsync(TeacherModel entity)
    //{
    //    // تحقق من أن هناك مدارس مرتبطة
    //    if (entity.SchoolModels == null || !entity.SchoolModels.Any())
    //        return null;

    //    // تحقق من أن جميع المدارس موجودة
    //    var schoolIds = entity.SchoolModels.Select(sm => sm.SchoolId).ToList();
    //    var validSchoolIds = await _dbSet
    //        .Where(s => schoolIds.Contains(s.Id))
    //        .Select(s => s.Id)
    //        .ToListAsync();

    //    if (validSchoolIds.Count != schoolIds.Count)
    //        return null; // هناك مدرسة غير موجودة

    //    // تحقق من وجود المواد المرتبطة
    //    if (entity.ModulsTeachers == null || !entity.ModulsTeachers.Any())
    //        return null;

    //    var modulIds = entity.ModulsTeachers.Select(mt => mt.ModelId).ToList();
    //    var validModulIds = await _dbSet
    //        .Where(m => modulIds.Contains(m.Id))
    //        .Select(m => m.Id)
    //        .ToListAsync();

    //    if (validModulIds.Count != modulIds.Count)
    //        return null; // هناك مادة غير موجودة

    //    return await base.CreateAsync(entity);
    //}

    //public override async Task<TeacherModel?> CreateAsync(TeacherModel entity)
    //{
    //  //  var schol= GetByIdAsync(entity.);
    //    // تحقق من وجود المدارس
    //    if (string.IsNullOrWhiteSpace(entity.SchoolId))
    //        return null;

    //    var school = await _dbSet.FirstOrDefaultAsync(s => s.Id == entity.SchoolId);
    //    if (school == null)
    //        return null; // إذا لم توجد المدرسة، إرجاع null

    //    // تحقق من وجود المواد
    //    if (entity.ModulsTeachers == null || !entity.ModulsTeachers.Any())
    //        return null;

    //    var validModulIds = await _dbSet
    //        .Where(m => entity.ModulsTeachers.Select(mt => mt.ModelId).Contains(m.Id))
    //        .Select(m => m.Id)
    //        .ToListAsync();

    //    if (validModulIds.Count != entity.ModulsTeachers.Count)
    //        return null; // بعض الـ ModulIds غير موجودة


    //    return await base.CreateAsync(entity);
    //}


}

//public async Task<TeacherModel?> CreateWithRelationsAsync(TeacherModel teacher, List<string> schoolIds, List<string> modulIds)
//{
//    teacher.Id = Guid.NewGuid().ToString();

//    // ربط المدارس
//    teacher.SchoolModels = schoolIds.Select(sid => new SchoolTeacher
//    {
//        Id = Guid.NewGuid().ToString(),
//        SchoolId = sid,
//        TeacherId = teacher.Id
//    }).ToList();

//    // ربط المواد
//    teacher.ModulsTeachers = modulIds.Select(mid => new ModulsTeacher
//    {
//        Id = Guid.NewGuid().ToString(),
//        ModelId = mid,
//        TeacherId = teacher.Id
//    }).ToList();

//    //_dbSet.Teachers.Add(teacher);
//    //await _context.SaveChangesAsync();

//    return teacher;
//}