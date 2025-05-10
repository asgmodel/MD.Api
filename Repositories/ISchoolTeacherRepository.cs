using Api.SM.Data;
using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



public interface ISchoolTeacherRepository : IRepsitory<SchoolTeacher>
{
    //Task<List<SchoolTeacher>> GetAllAsync();
    //Task<SchoolTeacher?> GetByIdAsync(string id);
    //Task CreateAsync(SchoolTeacher entity);
    //Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string schoolId, string teacherId);

}


public class SchoolTeacherRepository : Repository<SchoolTeacher>, ISchoolTeacherRepository
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ISchoolRepository _schoolRepository;


    public SchoolTeacherRepository(DataContext context, ITeacherRepository teacherRepository, ISchoolRepository schoolRepository) : base(context)
    {
        _teacherRepository = teacherRepository;
        _schoolRepository = schoolRepository;
    }



    public async Task<IEnumerable<SchoolTeacher>> GetAllAsync()
    {
        return await _dbSet
            .Include(st => st.SchoolModel)
            .Include(st => st.TeacherModel)
            .ToListAsync();
    }

    public override async Task<SchoolTeacher ?> GetByIdAsync(string id)
    {
        return await _dbSet
            .Include(st => st.SchoolModel)
            .Include(st => st.TeacherModel)
            .FirstOrDefaultAsync(st => st.Id == id);
    }
    public override async Task<SchoolTeacher?> CreateAsync(SchoolTeacher entity)
    {
        var schoolExists = await _schoolRepository.GetByIdAsync(entity.SchoolModelId);
        var teacherExists = await _teacherRepository.GetByIdAsync(entity.TeacherModelId);

        if (schoolExists == null || teacherExists == null)
            return null;

        return await base.CreateAsync(entity);
    }


    public async Task<bool> ExistsAsync(string schoolId, string teacherId)
    {
        return await _dbSet.AnyAsync(mt => mt.SchoolModelId == schoolId && mt.TeacherModelId == teacherId);
    }
    







    //public override Task<SchoolTeacher?> CreateAsync(SchoolTeacher entity)
    //{
    //    return base.CreateAsync(entity);
    //}
    //public async Task<List<SchoolTeacher>> GetAllAsync()
    //{
    //    return await _dbSet
    //        .Include(st => st.TeacherModel)
    //            .ThenInclude(t => t.Name) // إذا أردت اسم المعلم
    //        .Include(st => st.SchoolModel)
    //        .ToListAsync();
    //}

    ////public async Task<List<SchoolTeacher>> GetAllAsync()
    ////{
    ////    return await _dbSet
    ////        .Include(st => st.SchoolModel)
    ////        .Include(st => st.TeacherModel)
    ////        .ToListAsync();
    ////}

    //public async Task<SchoolTeacher?> GetByIdAsync(string id)
    //{
    //    return await _dbSet
    //        .Include(st => st.SchoolModel)
    //        .Include(st => st.TeacherModel)
    //        .FirstOrDefaultAsync(st => st.Id == id);
    //}



}
//override 
//    public async Task CreateAsync(SchoolTeacher entity)
//    {
//        entity.Id = Guid.NewGuid().ToString();
//        await _context.SchoolTeachers.AddAsync(entity);
//        await _context.SaveChangesAsync();
//    }