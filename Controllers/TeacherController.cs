using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//[ApiController]
//[Route("api/[controller]")]
//public class TeacherController : ControllerBase
//{
//    private readonly ITeacherRepository _repository;
//    private readonly IMapper _mapper;

//    public TeacherController(ITeacherRepository repository, IMapper mapper)
//    {
//        _repository = repository;
//        _mapper = mapper;
//    }

//    [HttpGet]
//    public async Task<IActionResult> GetAll()
//    {
//        var modul = await _repository.GetAllAsync();
//        var modulVM = _mapper.Map<IEnumerable<TeacherVM>>(modul);
//        return Ok(modulVM);
//    }

//    [HttpGet("by-id/{id}")]
//    public async Task<IActionResult> GetById(string id)
//    {
//        var school = await _repository.GetByIdAsync(id);
//        if (school == null)
//            return NotFound();

//        return Ok(school);
//    }
//    [HttpPost]
//    public async Task<IActionResult> Create([FromBody] CreateTeacherVM vm)
//    {
//        if (!ModelState.IsValid)
//            return BadRequest(ModelState);

//        // تحويل ViewModel إلى Model
//        var teacher = _mapper.Map<TeacherModel>(vm);

//        //// ربط المعرفات بالكيانات الفرعية
//        //teacher.SchoolModels = vm.SchoolModels.Select(sm => new SchoolTeacher
//        //{
//        //    SchoolId = sm.SchoolId,
//        //    TeacherId = teacher.Id
//        //}).ToList();

//        //teacher.ModulsTeachers = vm.ModulsTeachers.Select(mt => new ModulsTeacher
//        //{
//        //    ModelId = mt.ModelId,
//        //    TeacherId = teacher.Id
//        //}).ToList();

//        // إنشاء المعلم
//        var created = await _repository.CreateAsync(teacher);

//        if (created == null)
//            return BadRequest("فشل في إنشاء المعلم، تحقق من صحة البيانات.");

//        var teacherVM = _mapper.Map<TeacherVM>(created);
//        return Ok(teacherVM);
//    }


//    //[HttpPost]
//    //public async Task<IActionResult> Create([FromBody] CreateTeacherVM vm)
//    //{
//    //    if (!ModelState.IsValid)
//    //        return BadRequest(ModelState);

//    //    //if (vm == null || vm.SchoolId == null || vm.ModulId == null)
//    //    //    return BadRequest();

//    //    var teacher = _mapper.Map<TeacherModel>(vm);

//    //    await _repository.CreateAsync(teacher);
//    //    var createdteacher = await _repository.CreateAsync(teacher);

//    //    if (createdteacher == null)
//    //    {


//    //        var tea = await _repository.GetByIdAsync(vm.ModulId);
//    //        if (tea == null)
//    //            return BadRequest(" غير موجود.");


//    //    }
//    //    var teacherModel = _mapper.Map<TeacherVM>(createdteacher);
//    //    return Ok(teacherModel);

//    //    //   return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);
//    //}
//    //[HttpPost]
//    //public async Task<IActionResult> Create([FromBody] CreateTeacherVM vm)
//    //{
//    //    if (vm == null) return BadRequest();

//    //    var teacher = new TeacherModel
//    //    {
//    //        Id = Guid.NewGuid().ToString(),
//    //        NameId = vm.NameId,

//    //        // إعداد العلاقة مع المدارس
//    //        SchoolModels = vm.SchoolId.Select(schoolId => new SchoolTeacher
//    //        {
//    //            Id = Guid.NewGuid().ToString(),
//    //            SchoolId = schoolId.
//    //        }).ToList(),

//    //        // إعداد العلاقة مع المواد
//    //        ModulsTeachers = vm.ModulIds.Select(modulId => new ModulsTeacher
//    //        {
//    //            Id = Guid.NewGuid().ToString(),
//    //            ModelId = modulId.ModelId
//    //        }).ToList()
//    //    };

//    //    var created = await _repository.CreateAsync(teacher);
//    //    if (created == null)
//    //        return BadRequest("تعذر إنشاء المعلم. تحقق من صحة المدارس أو المواد.");

//    //    return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
//    //}

//    //[HttpPost]
//    //public async Task<IActionResult> Create([FromBody] CreateTeacherVM vm)
//    //{
//    //    if (vm == null) return BadRequest();

//    //    var teacher = _mapper.Map<TeacherModel>(vm);
//    //    await _repository.CreateAsync(teacher);

//    //    return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);
//    //}
//}
using Api.SM.Models;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherRepository _repository;
    private readonly IMapper _mapper;

    public TeacherController(ITeacherRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("GetAllTeacher")]
    public async Task<ActionResult<IEnumerable<TeacherVM>>> GetAllTeacher()
    {
        var teachers = await _repository.GetAllAsync();
        var teacherVMs = _mapper.Map<IEnumerable<TeacherVM>>(teachers);
        return Ok(teacherVMs);
    }

    [HttpGet("GetTeacherById/{id}")]
    public async Task<IActionResult> GetTeacherById(string id)
    {
        var teacher = await _repository.GetByIdAsync(id);
        if (teacher == null)
            return NotFound();

        var teacherVM = _mapper.Map<TeacherVM>(teacher);
        return Ok(teacherVM);
    }
    [HttpPost("CreateTeacher")]
    public async Task<ActionResult<CreateTeacherVM>> CreateTeacher([FromBody] CreateTeacherVM teacherVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var teacher = _mapper.Map<TeacherModel>(teacherVM);
        var createdteacher = await _repository.CreateAsync(teacher);

        if (createdteacher == null)
        {


            var school = await _repository.GetByIdAsync(teacherVM.SchoolId);
            if (school == null)
                return BadRequest("المدرسة غير موجوده.");

            // return BadRequest("حدث خطأ أثناء إنشاء الطالب.");
        }

        var teachertViewModel = _mapper.Map<TeacherVM>(createdteacher);
        return Ok(teachertViewModel);
    }
    [HttpPut("Updateteacher/{id}")]
    public async Task<ActionResult<CreateTeacherVM>> Updateteacher(string id, [FromBody] CreateTeacherVM vm)
    {
        if (!ModelState.IsValid || vm == null)
            return BadRequest("البيانات غير صالحة.");

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return NotFound("المعلم غير موجود.");

    
        var teacher = _mapper.Map<TeacherModel>(vm);
        teacher.Id = id;

        var item = await _repository.UpdateAsync(existing);
        if (item != null)
        {
            var vmteacher = _mapper.Map<CreateTeacherVM>(item);
            return Ok(vmteacher);
        }
        return BadRequest("تم التحديث بنجاح.");
    }
    [HttpDelete("Deleteteacher/{id}")]
    public async Task<IActionResult> Deleteteacher(string id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
            return NotFound("المعلم غير موجود.");
        var Isdle = await _repository.DeleteAsync(id);
        if (!Isdle)
            return Ok("تم الحذف بنجاح.");
        return BadRequest("Nou Delete ");
    }


    //[HttpPost]
    //public async Task<IActionResult> Create([FromBody] CreateTeacherVM vm)
    //{
    //    if (!ModelState.IsValid)
    //        return BadRequest(ModelState);

    //    // التحقق من وجود المدرسة
    //    var school = await _repository.GetByIdAsync(vm.SchoolId);
    //    if (school == null)
    //        return BadRequest("المدرسة غير موجودة.");

    //    // إنشاء TeacherModel من ViewModel
    //    var teacher = _mapper.Map<TeacherModel>(vm);

    //    // ربط المعلم بالمدرسة عبر العلاقة SchoolTeacher
    //    teacher.SchoolModels = new List<SchoolTeacher>
    //{
    //    new SchoolTeacher
    //    {
    //        SchoolId = vm.SchoolId,
    //        TeacherModel = teacher
    //    }
    //};

    //    // إنشاء المعلم
    //    var createdTeacher = await _repository.CreateAsync(teacher);

    //    if (createdTeacher == null)
    //        return BadRequest("فشل في إنشاء المعلم، تحقق من صحة البيانات.");

    //    // تحويل الكائن الناتج إلى ViewModel
    //    var teacherVM = _mapper.Map<TeacherVM>(createdTeacher);
    //    return Ok(teacherVM);
    //}

}
