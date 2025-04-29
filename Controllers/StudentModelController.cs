//using Api.SM.Models;
//using Api.SM.Repository;
//using Api.SM.VM;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace MD.Api.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class StudentModelController : ControllerBase
//    {


//        private readonly IStudentRepository  _repository;

//        private readonly IMapper _mapper;
//        public StudentModelController(IStudentRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }


//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var studentModels = await _repository.GetAllAsync();
//            return Ok(studentModels);
//        }



//        [HttpGet("{id}")]

//        public async Task<IActionResult> GetById(string id)
//        {
//            var studentModel = await _repository.GetByIdAsync(id);
//            if (studentModel == null)
//            {
//                return NotFound();
//            }
//            return Ok(studentModel);
//        }

//        //[HttpPost]

//        //public async Task<IActionResult> Create([FromBody] CreateStudentVM studentModel)
//        //{
//        //    if (studentModel == null)
//        //    {
//        //        return BadRequest();
//        //    }
//        //    var  student = _mapper.Map<StudentModel>(studentModel);
//        //    await _repository.CreateAsync(student);
//        //    return CreatedAtAction(nameof(GetById), new { id = studentModel.Id }, studentModel);
//        //}

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] CreateStudentVM studentVM)
//        {
//            if (studentVM == null)
//                return BadRequest();

//            var student = _mapper.Map<StudentModel>(studentVM);
//            student.Id = new IDgenerate().GenvetId("Student", '-', 10, 4, 3);

//            await _repository.CreateAsync(student);

//            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(string id, [FromBody] UpdateStudentVM studentVM)
//        {
//            if (studentVM == null || id != studentVM.Id)
//                return BadRequest();

//            var existingStudent = await _repository.GetByIdAsync(id);
//            if (existingStudent == null)
//                return NotFound();

//            _mapper.Map(studentVM, existingStudent);
//            await _repository.UpdateAsync(existingStudent);

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(string id)
//        {
//            var student = await _repository.GetByIdAsync(id);
//            if (student == null)
//                return NotFound();

//            await _repository.DeleteAsync(id);
//            return NoContent();
//        }
//    }

////}
//using Api.SM.Models;
//using Api.SM.Repository;
//using Api.SM.VM;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;

//namespace Api.SM.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class StudentModelController : ControllerBase
//    {
//        private readonly IStudentRepository _repository;
//        private readonly IMapper _mapper;

//        public StudentModelController(IStudentRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var students = await _repository.GetAllAsync();
//            var studentsVM = _mapper.Map<IEnumerable<StudentVM>>(students);
//            return Ok(studentsVM);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(string id)
//        {
//            var student = await _repository.GetByIdAsync(id);
//            if (student == null)
//                return NotFound();

//            var studentVM = _mapper.Map<StudentVM>(student);
//            return Ok(studentVM);
//        }

//        //[HttpPost]
//        //public async Task<IActionResult> Create([FromBody] StudentVM vm)
//        //{
//        //    if (vm == null)
//        //        return BadRequest();

//        //    var student = _mapper.Map<StudentModel>(vm);
//        //    student.Id = Guid.NewGuid().ToString();

//        //    await _repository.CreateAsync(student);
//        //    return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
//        //}
//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] StudentVM vm)
//        {
//            if (vm == null)
//                return BadRequest();

//            var student = _mapper.Map<StudentModel>(vm);
//            student.Id = Guid.NewGuid().ToString(); // توليد ID جديد

//            // تأكد من عدم إضافة كائنات تم تحميلها مسبقًا.
//            student.Card = null;  // إذا كنت تربط عبر ID فقط، فلا حاجة لتحميل الكائنات.

//            await _repository.CreateAsync(student);
//            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
//        }

//        //[HttpPut("{id}")]
//        //public async Task<IActionResult> Update(string id, [FromBody] UpdateStudentVM vm)
//        //{
//        //    if (vm == null || id != vm.Id)
//        //        return BadRequest();

//        //    var student = await _repository.GetByIdAsync(id);
//        //    if (student == null)
//        //        return NotFound();

//        //    _mapper.Map(vm, student);
//        //    await _repository.UpdateAsync(student);
//        //    return NoContent();
//        //}

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(string id)
//        {
//            var student = await _repository.GetByIdAsync(id);
//            if (student == null)
//                return NotFound();

//            await _repository.DeleteAsync(id);
//            return NoContent();
//        }
//    }
//}
using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.SM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentModelController : ControllerBase
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;

        public StudentModelController(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        // GET: /StudentModel
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _repository.GetAllAsync();
            var studentsVM = _mapper.Map<IEnumerable<StudentVM>>(students);
            return Ok(studentsVM);
        }

        // GET: /StudentModel/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            var studentVM = _mapper.Map<StudentVM>(student);
            return Ok(studentVM);
        }
        // POST: /StudentModel
        [HttpPost("StudentModel")]
        public async Task<IActionResult> Create([FromBody] CreateStudentVM studentVM)
        {
            if (studentVM == null)
                return BadRequest();

            var student = _mapper.Map<StudentModel>(studentVM);
            student.Id = new IDgenerate().GenvetId("Student", '-', 10, 4, 3);
            //   student.Name = student.Card.Name.FullName;
            await _repository.CreateAsync(student);

            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }
        // POST: /StudentModel
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentVM vm)
        {
            if (vm == null)
                return BadRequest();

            var student = _mapper.Map<StudentModel>(vm);
            student.Id = Guid.NewGuid().ToString(); // توليد ID جديد للطالب

            // تحقق من الكائنات المرتبطة مثل "Card" و "Row"
            if (student.Card != null)
            {
                _repository.FindAsync(student.Card);  // تجنب إضافة الكائن مرة أخرى
            }

            await _repository.CreateAsync(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        // DELETE: /StudentModel/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
