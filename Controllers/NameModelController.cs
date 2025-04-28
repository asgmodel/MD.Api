using Api.SM.Models;
using Api.SM.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MD.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NameModelController : ControllerBase
    {

        private readonly INameRepository _repository;
        public NameModelController(INameRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<NameModel>>> GetAll()
        {



            var nameModels = await _repository.GetAllAsync();
            if (nameModels == null || !nameModels.Any())
            {
                return NotFound();
            }
            return Ok(nameModels);

        }


        [HttpGet("{id}")]

        public async Task<ActionResult<NameModel>> GetById(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID cannot be null or empty.");
            }
            var nameModel = await _repository.GetByIdAsync(id);
            if (nameModel == null)
            {
                return NotFound();
            }
            return Ok(nameModel);
        }
    }
}
