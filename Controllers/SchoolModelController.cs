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
//    public class SchoolModelController : ControllerBase
//    {
//        private readonly ISchoolRepository _repository;
//        private readonly IMapper _mapper;

//        public SchoolModelController(ISchoolRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var schoolModels = await _repository.GetAllAsync();
//            return Ok(schoolModels);
//        }



//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(string id)
//        {
//            var schoolModel = await _repository.GetByIdAsync(id);
//            if (schoolModel == null)
//            {
//                return NotFound();
//            }
//            return Ok(schoolModel);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] SchoolVM schoolModel)
//        {
//            if (schoolModel == null)
//            {
//                return BadRequest("School data is required.");
//            }

//            var school = _mapper.Map<SchoolModel>(schoolModel);
//            school.Id = Guid.NewGuid().ToString(); // إنشاء ID جديد
//            await _repository.CreateAsync(school);

//            return CreatedAtAction(nameof(GetById), new { id = school.Id }, school);
//        }

//        //[HttpPut("{id}")]
//        //public async Task<IActionResult> Update(string id, [FromBody] UpdateSchoolVM schoolModel)
//        //{
//        //    if (schoolModel == null || id != schoolModel.Id)
//        //    {
//        //        return BadRequest("School data is incorrect.");
//        //    }

//        //    var school = await _repository.GetByIdAsync(id);
//        //    if (school == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    _mapper.Map(schoolModel, school);
//        //    await _repository.UpdateAsync(school);

//        //    return NoContent();
//        //}

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(string id)
//        {
//            var school = await _repository.GetByIdAsync(id);
//            if (school == null)
//            {
//                return NotFound();
//            }

//            await _repository.DeleteAsync(id);
//            return NoContent();
//        }

//        [HttpPost("{schoolId}/AddRow/{rowId}")]
//        public async Task<IActionResult> AddRowToSchool(string schoolId, string rowId)
//        {
//            try
//            {
//                await _repository.AddRowAsync(rowId, schoolId);
//                return Ok(new { message = "Row added to school successfully." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { error = ex.Message });
//            }
//        }
//        // GET: /SchoolModel/{schoolId}/Rows
//        [HttpGet("{schoolId}/Rows")]
//        public async Task<IActionResult> GetRowsBySchoolId(string schoolId)
//        {
//            try
//            {
//                var rows = await _repository.GetRowsBySchoolIdAsync(schoolId);
//                return Ok(rows);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { error = ex.Message });
//            }
//        }

//        //[HttpDelete("{schoolId}/RemoveRow/{rowId}")]
//        //public async Task<IActionResult> RemoveRowFromSchool(string schoolId, string rowId)
//        //{
//        //    try
//        //    {
//        //        await _repository.RemoveRowAsync(rowId, schoolId);
//        //        return Ok(new { message = "Row removed from school successfully." });
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return BadRequest(new { error = ex.Message });
//        //    }
//        //}
//        [HttpPost("{schoolId}/AddStudents/{studentId}")]
//        public async Task<IActionResult> AddStudentToSchool(string schoolId, string studentId)
//        {
//            try
//            {
//                await _repository.AddStudent(studentId, schoolId);
//                return Ok(new { message = "Studnt added to school successfully." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { error = ex.Message });
//            }
//        }
//    }
//}
using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolModelController : ControllerBase
    {
        private readonly ISchoolRepository _repository;
        private readonly IMapper _mapper;

        public SchoolModelController(ISchoolRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("GetAllSchools")]
        public async Task<ActionResult<IEnumerable<SchoolVM>>> GetAllSchools()
        {
            var schools = await _repository.GetAllAsync();
            var schoolsVM = _mapper.Map<IEnumerable<SchoolVM>>(schools);
            return Ok(schoolsVM);
        }
        //[HttpGet("all")]

        //public async Task<IActionResult> GetAll()
        //{
        //    var schools = await _repository.GetAllAsync();
        //    return Ok(schools);
        //}

        [HttpGet("GetByIdSchool/{id}")]
        public async Task<IActionResult> GetByIdSchool(string id)
        {
            var school = await _repository.GetByIdAsync(id);
            if (school == null)
                return NotFound();

            return Ok(school);
        }
      
        //[HttpPost("CreateSchool")]
        //public async Task<ActionResult<SchoolVM>> CreateSchool([FromBody] SchoolVM schoolVM)
        //{

        //    if (schoolVM == null)
        //        return BadRequest("School data is required.");

        //    var school = _mapper.Map<SchoolModel>(schoolVM);
        //    school.Id = Guid.NewGuid().ToString();
        //    var createdschool = await _repository.CreateAsync(school);
        //    if (createdschool == null)
        //        return BadRequest("School data is required.");
        //    var schooltViewModel = _mapper.Map<StudentVM>(createdschool);
        //    return Ok(schooltViewModel);
        //    return CreatedAtAction(nameof(GetByIdSchool), new { id = school.Id }, school);
        //}
        [HttpPost("CreateSchool")]
        public async Task<ActionResult<CreateSchoolVM>> CreateSchool([FromBody] CreateSchoolVM schoolVM)
        {

            if (schoolVM == null)
                return BadRequest("School data is required.");

            var school = _mapper.Map<SchoolModel>(schoolVM);
            school.Id = Guid.NewGuid().ToString();
            var createdschool = await _repository.CreateAsync(school);
            if (createdschool == null)
                return BadRequest("School data is required.");
            var schooltViewModel = _mapper.Map<CreateSchoolVM>(createdschool);
            return Ok(schooltViewModel);
            //return CreatedAtAction(nameof(GetByIdSchool), new { id = school.Id }, school);
        }
        //[HttpPost("CreateSchool")]
        //public async Task<ActionResult<SchoolVM>> CreateSchool([FromBody] SchoolVM schoolVM)
        //{
        //    if (schoolVM == null)
        //        return BadRequest("School data is required.");

        //    var school = _mapper.Map<SchoolModel>(schoolVM);
        //    school.Id = Guid.NewGuid().ToString();

        //    try
        //    {
        //        var createdSchool = await _repository.CreateAsync(school);
        //        var schoolViewModel = _mapper.Map<SchoolVM>(createdSchool);
        //        return Ok(schoolViewModel);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return Conflict(new { message = ex.Message }); // 409 Conflict
        //    }
        //}
        [HttpPut("UpdateSchool/{id}")]
        public async Task<ActionResult<CreateSchoolVM>> UpdateSchool(string id, [FromBody] CreateSchoolVM vm)
        {
            //if (vm == null || id != vm.Id)
            //{
            //    return BadRequest("School data is incorrect.");
            //}
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null || id != existing.Id)
                return NotFound("School data is incorrect.");

        
            var school = _mapper.Map<SchoolModel>(vm);
            school.Id = id;

            var item = await _repository.UpdateAsync(existing);
            if (item != null)
            {
                var schoolvm = _mapper.Map<CreateSchoolVM>(item);
                return Ok(schoolvm);
            }
            return BadRequest("تم التحديث بنجاح.");
        }

        //[HttpDelete("Delete/{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var school = await _repository.GetByIdAsync(id);
        //    if (school == null)
        //        return NotFound();

        //    await _repository.DeleteAsync(id);
        //    return NoContent();
        //}
        [HttpDelete("DeleteSchool/{id}")]
        public async Task<IActionResult> DeleteSchool(string id)
        {
           
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound("المدرسة غير موجود.");
            var Isdle = await _repository.DeleteAsync(id);
            if (!Isdle)
                return Ok("تم الحذف بنجاح.");
            return BadRequest("Nou Delete ");
        }
        [HttpDelete("{schoolId}/rows/{rowId}")]
        public async Task<IActionResult> DeleteRowSchool(string schoolId, string rowId)
        {
            try
            {
                var rowDeleted = await _repository.DeleteRowsAsync(rowId, schoolId);
                if (!rowDeleted)
                    return BadRequest("فشل في حذف الصف أو أنه لا يتبع المدرسة.");

                return Ok(new { message = "تم حذف الصف بنجاح." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("{schoolId}/rows/{rowId}")]
        public async Task<IActionResult> AddRowToSchool(string schoolId, string rowId)
        {
            try
            {
                await _repository.AddRowAsync(rowId, schoolId);
                return Ok(new { message = "Row added to school successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{schoolId}/rows")]
        public async Task<IActionResult> GetRowsBySchoolId(string schoolId)
        {
            try
            {
                var rows = await _repository.GetRowsBySchoolIdAsync(schoolId);
                return Ok(rows);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{schoolId}/students/{studentId}")]
        public async Task<IActionResult> AddStudentToSchool(string schoolId, string studentId)
        {
            try
            {
                await _repository.AddStudent(studentId, schoolId);
                return Ok(new { message = "Student added to school successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpDelete("{schoolId}/DeleteStudentSchool/{studentId}")]
        public async Task<IActionResult> DeleteStudentSchool(string schoolId, string studentId)
        {
            try
            {
                var rowDeleted = await _repository.DeleteStudentAsync(schoolId, studentId);
                if (!rowDeleted)
                    return BadRequest("فشل في حذف طالب أو أنه لا يتبع المدرسة.");

                return Ok(new { message = "تم حذف طالب بنجاح." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
          
        }

        // POST: api/Card/issue
        [HttpPost("issue")]
        public async Task<IActionResult> IssueCard([FromBody] CardModel card)
        {
            if (card == null)
                return BadRequest("البيانات غير صالحة");

            var result = await _repository.IssuingCardStudent(card);

            if (result == null)
                return BadRequest("فشل في إصدار البطاقة: تحقق من تطابق الصف والمدرسة");

            return Ok(result);
        }
    }
    // PUT: api/Student/{id}
    
    //[HttpPut("{id}")]
    //public async Task<IActionResult> Update(string id, [FromBody] UpdateSchoolVM schoolModel)
    //{
    //    if (schoolModel == null || id != schoolModel.Id)
    //    {
    //        return BadRequest("School data is incorrect.");
    //    }

    //    var school = await _repository.GetByIdAsync(id);
    //    if (school == null)
    //    {
    //        return NotFound();
    //    }

    //    _mapper.Map(schoolModel, school);
    //    await _repository.UpdateAsync(school);

    //    return NoContent();
    //}

}
