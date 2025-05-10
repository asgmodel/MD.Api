//using Api.SM.Models;
//using Api.SM.Repository;
//using Api.SM.VM;
//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//[ApiController]
//[Route("api/[controller]")]
//public class SchoolTeacherController : ControllerBase
//{
//    private readonly ISchoolTeacherRepository _repository;
//    private readonly IMapper _mapper;

//    public SchoolTeacherController(ISchoolTeacherRepository repository, IMapper mapper)
//    {
//        _repository = repository;
//        _mapper = mapper;
//    }


//    //private readonly ISchoolTeacherRepository _repository;
//    //private readonly AppDbContext _context;
//    [HttpGet]
//    public async Task<IActionResult> GetAll()
//    {
//        var modul = await _repository.GetAllAsync();
//        var modulVM = _mapper.Map<IEnumerable<SchoolTeacherVM>>(modul);
//        return Ok(modulVM);
//    }
//    //public SchoolTeacherController(ISchoolTeacherRepository repo, AppDbContext context)
//    //{
//    //    _repository = repo;
//    //    _context = context;
//    //}

//    //public async Task<IActionResult> Index()
//    //{
//    //    var result = await _repository.GetAllAsync();
//    //    return View(result);
//    //}

//    //public async Task<IActionResult> Create()
//    //{
//    //    var vm = new SchoolTeacherViewModel
//    //    {
//    //        Schools = await _context.Schools.ToListAsync(),
//    //        Teachers = await _context.Teachers.ToListAsync()
//    //    };
//    //    return View(vm);
//    //}


//    //[HttpPost]
//    //public async Task<IActionResult> Create(CreateSchoolTeacherVM vm)
//    //{
//    //    if (!ModelState.IsValid)
//    //    {
//    //        //vm.Schools = await _repository.ToListAsync();
//    //        //vm.Teachers = await _repository.ToListAsync();
//    //        return View(vm);
//    //    }

//    //    var entity = new SchoolTeacher
//    //    {
//    //        SchoolId = vm.SchoolId,
//    //        TeacherId = vm.TeacherId
//    //    };

//    //    await _repository.CreateAsync(entity);
//    //    return RedirectToAction(nameof(Index));
//    //}
//    [HttpPost]
//    public async Task<IActionResult> Create([FromBody] CreateSchoolTeacherVM schoolTeacherVM)
//    {
//        if (!ModelState.IsValid)
//            return BadRequest(ModelState);

//        try
//        {
//            var schoolTeacher = _mapper.Map<SchoolTeacher>(schoolTeacherVM);
//            var created = await _repository.CreateAsync(schoolTeacher);

//            if (created == null)
//                return StatusCode(500, "فشل في إنشاء العلاقة بين المدرسة والمعلم.");

//            var result = _mapper.Map<SchoolTeacherVM>(created);
//            return Ok(result);
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, $"حدث خطأ أثناء الحفظ: {ex.InnerException?.Message ?? ex.Message}");
//        }
//    }

//    //[HttpPost]
//    //public async Task<IActionResult> Create([FromBody] CreateSchoolTeacherVM rowModel)
//    //{
//    //    if (rowModel == null)
//    //    {
//    //        return BadRequest();
//    //    }

//    //    // تحقق من وجود المدرسة قبل إنشاء الصف
//    //    if (rowModel == null || string.IsNullOrWhiteSpace(rowModel.SchoolId))
//    //    {
//    //        return BadRequest("البيانات المدخلة غير صالحة.");
//    //    }

//    //    //var school = await _repository.GetByIdAsync(rowModel.SchoolId);
//    //    //if (school == null)
//    //    //{
//    //    //    return BadRequest("المدرسة غير موجودة.");
//    //    //}

//    //    var row = _mapper.Map<SchoolTeacher>(rowModel);
//    //    row.Id = Guid.NewGuid().ToString();


//    //    var createdRow = await _repository.CreateAsync(row);
//    //    if (createdRow == null)
//    //    {
//    //        return BadRequest("فشل في إنشاء الصف.");
//    //    }
//    //    var rowVM = _mapper.Map<SchoolTeacherVM>(createdRow); // استخدم createdRow وليس row


//    //    return CreatedAtAction(nameof(GetAll), new { id = createdRow.Id }, rowVM);
//    //}

//    //public async Task<IActionResult> Delete(string id)
//    //{
//    //    await _repository.DeleteAsync(id);
//    //    return RedirectToAction(nameof(Index));
//    //}
//}

//using Api.SM.Models;
//using Api.SM.Repository;
//using Api.SM.VM;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//[ApiController]
//[Route("api/[controller]")]
//public class SchoolTeacherController : ControllerBase
//{
//    private readonly ISchoolTeacherRepository _repository;
//    private readonly IMapper _mapper;

//    public SchoolTeacherController(ISchoolTeacherRepository repository, IMapper mapper)
//    {
//        _repository = repository;
//        _mapper = mapper;
//    }

//    [HttpGet]
//    public async Task<IActionResult> GetAll()
//    {
//        var entities = await _repository.GetAllAsync();
//        var viewModels = _mapper.Map<IEnumerable<SchoolTeacherVM>>(entities);
//        return Ok(viewModels);
//    }
//    [HttpPost]
//    public async Task<ActionResult<CreateSchoolTeacherVM>> Create([FromBody] CreateSchoolTeacherVM vm)
//    {
//        if (!ModelState.IsValid)
//            return BadRequest(ModelState);

//        // تحقق من صحة البيانات الأساسية
//        if (string.IsNullOrWhiteSpace(vm.SchoolModelId) || string.IsNullOrWhiteSpace(vm.TeacherId))
//            return BadRequest("يجب إدخال كل من معرف المدرس ومعرف .");

//        // تحقق من عدم التكرار
//        if (await _repository.ExistsAsync(vm.SchoolModelId, vm.TeacherId))
//            return BadRequest("المعلم مرتبط بالفعل بهذا .");


//        // إنشاء الكيان
//        var entity = _mapper.Map<SchoolTeacher>(vm);
//        entity.Id = Guid.NewGuid().ToString();

//        var created = await _repository.CreateAsync(entity);

//        if (created == null)
//            return BadRequest("فشل في إنشاء العلاقة.");

//        var result = _mapper.Map<CreateSchoolTeacherVM>(created);
//        return Ok(result);
//    }

//    // DELETE: /ModulsTeacher/{id}


////[HttpPost]
////    public async Task<IActionResult> Cureate([FromBody] CreateSchoolTeacherVM schoolTeacherVM)
////    {
////        if (!ModelState.IsValid)
////            return BadRequest(ModelState);

////        try
////        {
////            var schoolTeacher = _mapper.Map<SchoolTeacher>(schoolTeacherVM);
////            schoolTeacher.Id = Guid.NewGuid().ToString();
////          // schoolTeacher.SchoolModelId = schoolTeacher.SchoolModel.Id;

////            var created = await _repository.CreateAsync(schoolTeacher);

////            if (created == null)
////                return StatusCode(500, "فشل في إنشاء العلاقة بين المدرسة والمعلم.");

////            var result = _mapper.Map<SchoolTeacherVM>(created);
////            return Ok(result);
////        }
////        catch (Exception ex)
////        {
////            return StatusCode(500, $"حدث خطأ أثناء الحفظ: {ex.InnerException?.Message ?? ex.Message}");
////        }
////    }
//}

using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    // جلب جميع العلاقات
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var entities = await _repository.GetAllAsync();
        var viewModels = _mapper.Map<IEnumerable<SchoolTeacherVM>>(entities);
        return Ok(viewModels);
    }

    [HttpPost]
    public async Task<ActionResult<CreateSchoolTeacherVM>> Create([FromBody] CreateSchoolTeacherVM vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrWhiteSpace(vm.SchoolModelId) || string.IsNullOrWhiteSpace(vm.TeacherId))
            return BadRequest("يجب إدخال معرف المدرسة والمعلم.");

        var exists = await _repository.ExistsAsync(vm.SchoolModelId, vm.TeacherId);
        if (exists)
            return Conflict("المعلم مرتبط بالفعل بهذه المدرسة.");

        var entity = _mapper.Map<SchoolTeacher>(vm);
        entity.Id = Guid.NewGuid().ToString();
        entity.SchoolModelId = vm.SchoolModelId;
        entity.TeacherModelId = vm.TeacherId;
        var created = await _repository.CreateAsync(entity);
        if (created == null)
            return StatusCode(500, "فشل في إنشاء العلاقة. تأكد من وجود المدرسة والمعلم.");

        var result = _mapper.Map<CreateSchoolTeacherVM>(created);
        return Ok(result);
    }
    // حذف العلاقة بين معلم ومدرسة
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
            return NotFound("العلاقة غير موجودة أو فشل في الحذف.");

        return Ok("تم حذف العلاقة بنجاح.");
    }
}
