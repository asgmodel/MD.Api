using Api.SM.Data;
using Api.SM.Models;
using Api.SM.VM;
using Microsoft.EntityFrameworkCore;

namespace Api.SM.Repository
{
    // تعريف الواجهة الخاصة بـ RowRepository
    public interface IRowRepository : IRepsitory<RowModel>
    {
        Task<RowModel?> GetRowNameByIdAsync(string name);
        //Task AddStudent(string id, StudentModel student);
    }

    // تعريف الصف الخاص بـ RowRepository
    public class RowRepository : Repository<RowModel>, IRowRepository
    {
        //private readonly IStudentRepository _studentRepository;
        //private readonly IModulRepository _modulRepository;


        public RowRepository(DataContext context ) : base(context)
        {
           //_studentRepository = studentRepository;
           // _modulRepository = modulRepository;
            //_schoolRepository = schoolRepository;
        }

        public override async Task<RowModel?> CreateAsync(RowModel entity)
        {
            var rowExists = await _dbSet.AnyAsync(r => r.SchoolId == entity.SchoolId && r.Name == entity.Name);

            if (rowExists)
            {
                return null; 
            }

            return await base.CreateAsync(entity);
        }

        //public override async Task<RowModel?> CreateAsync(RowModel entity)
        //    {

        //        var schoolExists = await GetByIdAsync(entity.SchoolId);

        //    if (schoolExists != null)
        //    {
        //        return null; 
        //    }
        //    return await base.CreateAsync(entity); 

        //    }
        //public override async Task<RowModel?> CreateAsync(RowModel entity)
        //{
        //    var schoolExists = await _dbSet.Include(s => s.Id == entity.School.Id).FirstOrDefaultAsync();
        //    if (schoolExists !=null)
        //    {
        //        return null; // المدرسة غير موجودة
        //    }

        //    return await base.CreateAsync(entity); // أضف الصف إذا المدرسة موجودة
        //}
        //public async Task<SchoolModel> IsSchoolExistsAsync(string schoolId)
        //{
        //    return await _dbSet.AnyAsync(s => s.Id == schoolId);
       // }
        public override async Task<bool> DeleteAsync(string Id)
        {

            var rows = await _dbSet.Where(r => r.SchoolId == Id ).ToListAsync();
            if (rows.Any())
            {
                //   _dbSet.RemoveRange(rows);
                _dbSet.RemoveRange(rows);
            }
            await base.DeleteAsync(Id);
            return true;
        }
      


        //public override async Task<bool> DeleteAsync(string schoolId)
        //{
        //    var rows = await _dbSet
        //        .Where(r => r.SchoolId == schoolId || r.SchoolId == null)
        //        .ToListAsync();

        //    if (rows.Any())
        //    {
        //        _dbSet.RemoveRange(rows);
        //        // تأكد من تنفيذ الحذف فعليًا
        //    }

        //    return true;
        //}
        //public override async Task<bool> DeleteAsync(string schoolId)
        //{
        //    var rows = await _dbSet
        //        .Where(r => r.SchoolId == schoolId || r.SchoolId == null)
        //        .ToListAsync();
        //    var row = await _dbSet
        //    .Include(s => s.Students)
        //    .Include(s => s.Teachers)
        //    .Include(s => s.Moduls)
        //    .FirstOrDefaultAsync(s => s.Id == schoolId);
        //    if (rows.Any())
        //    {
        //        _dbSet.RemoveRange(rows);
        //        // تأكد من تنفيذ الحذف فعليًا
        //    }

        //    return true;
        //}


        public override Task<RowModel?> GetByIdAsync(string id)
        {
            return _dbSet
                .Where(x => x.Id == id)
                .Include(p => p.School)
                .Include(p => p.Moduls)
                .Include(p => p.Teachers)
                .Include(p => p.Students)
                .FirstOrDefaultAsync();
        }

  

        //public override Task<RowModel?> GetByIdAsync(string id)
        //{



        //    return _dbSet.
        //           Where(x => x.Id == id).
        //           Include(p => p.School).

        //           FirstOrDefaultAsync();
        //}


        public async Task<RowModel?> GetRowNameByIdAsync(string name)
        {
            return await _dbSet.
                           Where(x => x.Name == name).
                           Include(p => p.Students).
                           FirstOrDefaultAsync();
            //return await _dbSet
            //    .Include(p => p.Students)
            //        .ThenInclude(s => s.Name) // اختياري: إذا أردت تضمين اسم الطالب
            //    .FirstOrDefaultAsync(x => x.Name == name);
        }

        //public async Task AddStudent(string id, StudentModel student)
        //{
        //    var row = _dbSet.FirstOrDefault(r => r.Id == id);
        //    if (row != null)
        //    {
        //        student.Row = row;
        //        student.RowId = row.Id;
        //        row.Students.Add(student);
        //    }
        //    else
        //    {
        //        throw new Exception("Row not found");
        //    }
        //}
        // يمكنك إضافة وظائف إضافية هنا حسب الحاجة
    }
}
