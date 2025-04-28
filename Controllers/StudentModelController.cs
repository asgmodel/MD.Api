using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MD.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentModelController : ControllerBase
    {


        private readonly IStudentRepository  _repository;

        private readonly IMapper _mapper;
        public StudentModelController(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var studentModels = await _repository.GetAllAsync();
            return Ok(studentModels);
        }



        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(string id)
        {
            var studentModel = await _repository.GetByIdAsync(id);
            if (studentModel == null)
            {
                return NotFound();
            }
            return Ok(studentModel);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateStudentVM studentModel)
        {
            if (studentModel == null)
            {
                return BadRequest();
            }
            var  student = _mapper.Map<StudentModel>(studentModel);
            await _repository.CreateAsync(student);
            return CreatedAtAction(nameof(GetById), new { id = studentModel.Id }, studentModel);
        }


    }

}