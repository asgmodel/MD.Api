using Api.SM.Data;
using Api.SM.Models;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MD.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModulsTeacherController : ControllerBase
    {
        private readonly IModulsTeacherRepository _repository;
        private readonly IMapper _mapper;
       

        public ModulsTeacherController(IModulsTeacherRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
           
        }

        // GET: /ModulsTeacher
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _repository.GetAllAsync();
            var studentsVM = _mapper.Map<IEnumerable<ModulsTeacher>>(students);
            return Ok(studentsVM);
            //var list = await _repository.Set<ModulsTeacher>()
            //                         .Include(mt => mt.ModelModuls)
            //                         .Include(mt => mt.TeacherModel)
            //                         .ToListAsync();
            //return Ok(list);
        }

        // GET: /ModulsTeacher/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                return NotFound("الطالب غير موجود.");

            var studentVM = _mapper.Map<ModulsTeacher>(student); // Correct Mapping
            return Ok(studentVM);
            //var entity = await _repository.Set<ModulsTeacher>()
            //                           .Include(mt => mt.ModelModuls)
            //                           .Include(mt => mt.TeacherModel)
            //                           .FirstOrDefaultAsync(mt => mt.Id == id);
            //if (entity == null)
            //    return NotFound();

            //return Ok(entity);
        }

        //// POST: /ModulsTeacher
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] ModulsTeacherVM vm)
        //{
        //    if (vm == null || string.IsNullOrEmpty(vm.ModelId) || string.IsNullOrEmpty(vm.TeacherId))
        //        return BadRequest("Invalid input");

        //    var entity = _mapper.Map<ModulsTeacher>(vm);
        //    entity.Id = Guid.NewGuid().ToString();

        //    // Optional: تحقق من وجود المدرس والمودول
        //    var modulExists = await _repository.GetByIdAsync<ModulModel>().AnyAsync(m => m.Id == vm.ModelId);
        //    var teacherExists = await _repository.GetByIdAsync<TeacherModel>().AnyAsync(t => t.Id == vm.TeacherId);

        //    if (!modulExists || !teacherExists)
        //        return NotFound("Modul or Teacher not found");

        //    var studentViewModel = _mapper.Map<StudentVM>(createdStudent);
        //    return Ok(studentViewModel);
        //    //_repository.Add(entity);
        //    //await _repository.SaveChangesAsync();

        //    //return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        //}
        [HttpPost]
        public async Task<ActionResult<CreateModulsTeacherVM>> Create([FromBody] CreateModulsTeacherVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // تحقق من صحة البيانات الأساسية
            if (string.IsNullOrWhiteSpace(vm.ModelId) || string.IsNullOrWhiteSpace(vm.TeacherId))
                return BadRequest("يجب إدخال كل من معرف المدرس ومعرف المودول.");

            // تحقق من عدم التكرار
            if (await _repository.ExistsAsync(vm.ModelId, vm.TeacherId))
                return BadRequest("المعلم مرتبط بالفعل بهذا المودول.");

            // تحقق من وجود المدرس والمودول
            //var modul = await _modulRepository.GetByIdAsync(vm.ModelId);
            //if (modul == null)
            //    return NotFound("المودول غير موجود.");

            //var teacher = await _teacherRepository.GetByIdAsync(vm.TeacherId);
            //if (teacher == null)
            //    return NotFound("المعلم غير موجود.");

            // إنشاء الكيان
            var entity = _mapper.Map<ModulsTeacher>(vm);
            entity.Id = Guid.NewGuid().ToString();

            var created = await _repository.CreateAsync(entity);

            if (created == null)
                return BadRequest("فشل في إنشاء العلاقة.");

            var result = _mapper.Map<CreateModulsTeacherVM>(created);
            return Ok(result);
        }

        // DELETE: /ModulsTeacher/{id}
       
    }
}
