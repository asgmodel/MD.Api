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

        [AllowAnonymous]

        [HttpGet("all")]

        public async Task<IActionResult> GetAll()
        {
            var schools = await _repository.GetAllAsync();
            return Ok(schools);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var school = await _repository.GetByIdAsync(id);
            if (school == null)
                return NotFound();

            return Ok(school);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SchoolVM schoolVM)
        {
            if (schoolVM == null)
                return BadRequest("School data is required.");

            var school = _mapper.Map<SchoolModel>(schoolVM);
            school.Id = Guid.NewGuid().ToString();
            await _repository.CreateAsync(school);

            return CreatedAtAction(nameof(GetById), new { id = school.Id }, school);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var school = await _repository.GetByIdAsync(id);
            if (school == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
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
    }
}
