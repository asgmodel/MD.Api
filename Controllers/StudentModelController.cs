
using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.SM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repository;
       
        private readonly IMapper _mapper;

        // Constructor to inject dependencies
        public StudentController(IStudentRepository repository,
                                      
                                      IMapper mapper)
        {
            _repository = repository;
           
            _mapper = mapper;
        }
        [HttpGet("GetAllStudent")]
        public async Task<ActionResult<IEnumerable<StudentVM>>> GetAllStudent()
        {
            var students = await _repository.GetAllAsync();
            var studentsVM = _mapper.Map<IEnumerable<StudentVM>>(students);
            return Ok(studentsVM);
        }

        // POST: api/Student
        

        [HttpPost("CreateStudent")]
        public async Task<ActionResult<CreateStudentVM>> CreateStudent([FromBody] CreateStudentVM studentVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = _mapper.Map<StudentModel>(studentVM);
            var createdStudent = await _repository.CreateAsync(student);

            if (createdStudent == null)
            {               

                var row = await _repository.GetByIdAsync(studentVM.RowId);
                if (row == null)
                    return BadRequest("الصف غير موجود.");

                if (row.SchoolId != studentVM.SchoolId)
                    return BadRequest("الصف لا ينتمي إلى نفس المدرسة.");

               // return BadRequest("حدث خطأ أثناء إنشاء الطالب.");
            }
          
            var studentViewModel = _mapper.Map<StudentVM>(createdStudent);
            return Ok(studentViewModel);
        }

        // GET: api/Student/{id}
        
        [HttpGet("GetStudentById/{id}")]
        public async Task<ActionResult<StudentVM>> GetStudentById(string id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                return NotFound("الطالب غير موجود.");

            var studentVM = _mapper.Map<StudentVM>(student); // Correct Mapping
            return Ok(studentVM);
        }

        // PUT: api/Student/{id}
        [HttpPut("UpdateStudent/{id}")]
        public async Task<ActionResult<CreateStudentVM>> Update(string id, [FromBody] CreateStudentVM vm)
        {
            if (!ModelState.IsValid || vm == null)
                return BadRequest("البيانات غير صالحة.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound("الطالب غير موجود.");

            //// ✅ تحديث الخصائص مباشرة بدون استبدال الكائنات
            //existing.RowId = vm.RowId;
            //existing.SchoolId = vm.SchoolId;
            //existing.Age = vm.Age ?? existing.Age;

            //// ✅ تأكد من أن NameModel يتم تحديثه وليس استبداله (لتفادي التكرار)
            //if (existing.Name != null && vm.Name != null)
            //{
            //    existing.Name.Name = vm.Name.Name;
            //    existing.Name.Title = vm.Name.Title;
            //}
            var student = _mapper.Map<StudentModel>(vm);
            student.Id = id;

           var item = await _repository.UpdateAsync(existing);
            if(item !=null)
            {
                var vmstu = _mapper.Map<CreateStudentVM>(item);
                return Ok(vmstu);
            }
            return BadRequest("تم التحديث بنجاح.");
        }




        // DELETE: api/Student/{id}
        [HttpDelete("DELETEStudent/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound("الطالب غير موجود.");
          var Isdle=  await _repository.DeleteAsync(id);
            if(!Isdle)
            return Ok("تم الحذف بنجاح.");
            return BadRequest("Nou Delete ");
        }

        // GET: api/Student/{id}/row
        [HttpGet("{id}/row")]
        public async Task<IActionResult> GetRow(string id)
        {
            var row = await _repository.GetRowAsync(id);
            if (row == null)
                return NotFound("الصف غير موجود.");
            var rowVm = _mapper.Map<RowVM>(row); // Correct Mapping
          // if(rowVm  !=null)

            return Ok(rowVm);
        }

        // GET: api/Student/{id}/moduls
        [HttpGet("{id}/moduls")]
        public async Task<IActionResult> GetModuls(string id)
        {
            var moduls = await _repository.GetModulsAsync(id);
            return Ok(moduls);
        }

        // GET: api/Student/{id}/teachers
        [HttpGet("{id}/teachers")]
        public async Task<IActionResult> GetTeachers(string id)
        {
            var teachers = await _repository.GetTeachersAsync(id);
            return Ok(teachers);
        }

        // PUT: api/Student/{id}/card
        [HttpPut("{id}/card")]
        public async Task<IActionResult> UpdateCard(string id, [FromBody] CardModel newCard)
        {
            var result = await _repository.UpdateCardAsync(id, newCard);
            if (!result)
                return BadRequest("لم يتم تحديث البطاقة. تحقق من الطالب.");
          
        
            return Ok("تم تحديث البطاقة بنجاح.");
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest("يرجى إدخال كلمة للبحث.");

            var students = await _repository.SearchAsync(keyword);

            if (!students.Any())
                return NotFound("لا توجد نتائج.");

            var studentVMs = _mapper.Map<IEnumerable<StudentVM>>(students);
            return Ok(studentVMs);
        }


    }
}
