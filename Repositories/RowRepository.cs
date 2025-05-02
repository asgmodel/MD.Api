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
        //private readonly ISchoolRepository _schoolRepository;
        public RowRepository(DataContext context ) : base(context)
        {
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
        //}

        public override Task<RowModel?> GetByIdAsync(string id)
        {


            return _dbSet.
                   Where(x => x.Id == id).
                   Include(p => p.School).
                   FirstOrDefaultAsync();
        }
        

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
