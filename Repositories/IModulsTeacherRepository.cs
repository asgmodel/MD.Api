using Api.SM.Data;
using Api.SM.Models;
using Api.SM.Repository;
using Microsoft.EntityFrameworkCore;
using Api.SM.Models;

public interface IModulsTeacherRepository : IRepsitory<ModulsTeacher>
{
    //Task<IEnumerable<ModulsTeacher>> GetAllDetailedAsync();
    Task<ModulsTeacher?> GetByIdDetailedAsync(string id);
    Task<bool> ExistsAsync(string modelId, string teacherId);
}

public class ModulsTeacherRepository : Repository<ModulsTeacher>, IModulsTeacherRepository
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IModulRepository _modulRepository;


    public ModulsTeacherRepository(DataContext context, ITeacherRepository teacherRepository, IModulRepository modulRepository = null) : base(context)
    {
        _teacherRepository = teacherRepository;
        _modulRepository = modulRepository;
    }
    public override async Task<ModulsTeacher?> CreateAsync(ModulsTeacher entity)
    {
        var modul = await _modulRepository.GetByIdAsync(entity.ModelModulslId);
        if (modul == null)
            return null;

        var teacher = await _teacherRepository.GetByIdAsync(entity.TeacherModelId);
        if (teacher == null)
            return null;

        return await base.CreateAsync(entity);
    }
    public async Task<IEnumerable<ModulsTeacher>> GetAllDetailedAsync()
    {
        return await _dbSet
            .Include(mt => mt.ModelModuls)
            .Include(mt => mt.TeacherModel)
            .ToListAsync();
    }
    
    public async Task<ModulsTeacher?> GetByIdDetailedAsync(string id)
    {
        return await _dbSet
            .Include(mt => mt.ModelModuls)
            .Include(mt => mt.TeacherModel)
            .FirstOrDefaultAsync(mt => mt.Id == id);
    }

    public async Task<bool> ExistsAsync(string modelId, string teacherId)
    {
        return await _dbSet.AnyAsync(mt => mt.ModelModulslId == modelId && mt.TeacherModelId == teacherId);
    }
}
