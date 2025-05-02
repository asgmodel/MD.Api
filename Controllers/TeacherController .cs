using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeacherVM vm)
    {
        if (vm == null) return BadRequest();

        var teacher = _mapper.Map<TeacherModel>(vm);
        await _repository.CreateAsync(teacher);

        return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, teacher);
    }
}
