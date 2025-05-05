using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
[ApiController]
[Route("api/[controller]")]
public class SchoolTeacherController : ControllerBase
{
    private readonly ISchoolTeacherRepository _repository;
    private readonly IMapper _mapper;

    public SchoolTeacherController(ISchoolTeacherRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    //private readonly ISchoolTeacherRepository _repository;
    //private readonly AppDbContext _context;
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var modul = await _repository.GetAllAsync();
        var modulVM = _mapper.Map<IEnumerable<SchoolTeacherVM>>(modul);
        return Ok(modulVM);
    }
    //public SchoolTeacherController(ISchoolTeacherRepository repo, AppDbContext context)
    //{
    //    _repository = repo;
    //    _context = context;
    //}

    //public async Task<IActionResult> Index()
    //{
    //    var result = await _repository.GetAllAsync();
    //    return View(result);
    //}

    //public async Task<IActionResult> Create()
    //{
    //    var vm = new SchoolTeacherViewModel
    //    {
    //        Schools = await _context.Schools.ToListAsync(),
    //        Teachers = await _context.Teachers.ToListAsync()
    //    };
    //    return View(vm);
    //}


    //[HttpPost]
    //public async Task<IActionResult> Create(CreateSchoolTeacherVM vm)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        //vm.Schools = await _repository.ToListAsync();
    //        //vm.Teachers = await _repository.ToListAsync();
    //        return View(vm);
    //    }

    //    var entity = new SchoolTeacher
    //    {
    //        SchoolId = vm.SchoolId,
    //        TeacherId = vm.TeacherId
    //    };

    //    await _repository.CreateAsync(entity);
    //    return RedirectToAction(nameof(Index));
    //}
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSchoolTeacherVM schoolTeacherVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            

            // تحويل ViewModel إلى Model
            var schoolTeacher = _mapper.Map<SchoolTeacher>(schoolTeacherVM);

            // الإنشاء
            var created = await _repository.CreateAsync(schoolTeacher);

            if (created == null)
                return StatusCode(500, "فشل في إنشاء العلاقة بين المدرسة والمعلم.");

            // التحويل إلى ViewModel
            var result = _mapper.Map<SchoolTeacherVM>(created);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"حدث خطأ أثناء الحفظ: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    //public async Task<IActionResult> Delete(string id)
    //{
    //    await _repository.DeleteAsync(id);
    //    return RedirectToAction(nameof(Index));
    //}
}
