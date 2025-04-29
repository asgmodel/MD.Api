//using Api.SM.Models;
//using Api.SM.Repository;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace MD.Api.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class NameModelController : ControllerBase
//    {

//        private readonly INameRepository _repository;
//        public NameModelController(INameRepository repository)
//        {
//            _repository = repository;
//        }

//        [HttpGet]

//        public async Task<ActionResult<IEnumerable<NameModel>>> GetAll()
//        {



//            var nameModels = await _repository.GetAllAsync();
//            if (nameModels == null || !nameModels.Any())
//            {
//                return NotFound();
//            }
//            return Ok(nameModels);

//        }


//        [HttpGet("{id}")]

//        public async Task<ActionResult<NameModel>> GetById(string id)
//        {

//            if (string.IsNullOrEmpty(id))
//            {
//                return BadRequest("ID cannot be null or empty.");
//            }
//            var nameModel = await _repository.GetByIdAsync(id);
//            if (nameModel == null)
//            {
//                return NotFound();
//            }
//            return Ok(nameModel);
//        }
//        [HttpPost]
//        public async Task<ActionResult<NameModel>> Create([FromBody] NameModel nameModel)
//        {
//            if (nameModel == null)
//            {
//                return BadRequest("Invalid data.");
//            }

//            await _repository.CreateAsync(nameModel);
//            return CreatedAtAction(nameof(GetById), new { id = nameModel.Name }, nameModel);
//        }

//    }
//}
using Api.SM.Models;
using Api.SM.Repository;
using Api.SM.VM;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace Api.SM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NameModelController : ControllerBase
    {
        private readonly INameRepository _repository;
        private readonly IMapper _mapper;

        public NameModelController(INameRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var names = await _repository.GetAllAsync();
            var nameVMs = _mapper.Map<IEnumerable<NameVM>>(names);
            return Ok(nameVMs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var name = await _repository.GetByIdAsync(id);
            if (name == null)
                return NotFound();

            var nameVM = _mapper.Map<NameVM>(name);
            return Ok(nameVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NameVM vm)
        {
            if (vm == null)
                return BadRequest();

            var name = new NameModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = vm.Name,
                Title = vm.Title
            };

            await _repository.CreateAsync(name);
            return CreatedAtAction(nameof(GetById), new { id = name.Id }, name);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(string id, [FromBody] UpdateNameVM vm)
        //{
        //    if (vm == null || id != vm.Id)
        //        return BadRequest();

        //    var name = await _repository.GetByIdAsync(id);
        //    if (name == null)
        //        return NotFound();

        //    name.Name = vm.Name;
        //    name.Title = vm.Title;

        //    await _repository.UpdateAsync(name);
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var name = await _repository.GetByIdAsync(id);
            if (name == null)
                return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
