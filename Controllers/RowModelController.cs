//using Api.SM.Models;
//using Api.SM.Repository;
//using Api.SM.VM;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;

//namespace MD.Api.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class RowModelController : ControllerBase
//    {
//        private readonly IRowRepository _repository;
//        private readonly IMapper _mapper;

//        public RowModelController(IRowRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        // GET: /RowModel
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var rowModels = await _repository.GetAllAsync();
//            return Ok(rowModels);
//        }

//        // GET: /RowModel/{id}
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(string id)
//        {
//            var rowModel = await _repository.GetByIdAsync(id);
//            if (rowModel == null)
//            {
//                return NotFound();
//            }
//            return Ok(rowModel);
//        }

//        // POST: /RowModel
//        [HttpPost("POSTRowModel")]
//        public async Task<IActionResult> Create([FromBody] RowVM rowModel)
//        {
//            if (rowModel == null)
//            {
//                return BadRequest("Row data is required.");
//            }

//            var row = _mapper.Map<RowModel>(rowModel);

//            row.Id = new IDgenerate().GenvetId("Row", '-', 10, 4, 3); // استخدم IDgenerate هنا كما هو في الموديل.

//            await _repository.CreateAsync(row);
//            return CreatedAtAction(nameof(GetById), new { id = row.Id }, row);
//        }

//        // PUT: /RowModel/{id}
//        //[HttpPut("{id}")]
//        //public async Task<IActionResult> Update(string id, [FromBody] UpdateRowVM rowModel)
//        //{
//        //    if (rowModel == null || id != rowModel.Id)
//        //    {
//        //        return BadRequest("Row data is incorrect.");
//        //    }

//        //    var existingRow = await _repository.GetByIdAsync(id);
//        //    if (existingRow == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    _mapper.Map(rowModel, existingRow);
//        //    await _repository.UpdateAsync(existingRow);

//        //    return NoContent();
//        //}

//        // DELETE: /RowModel/{id}
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(string id)
//        {
//            var existingRow = await _repository.GetByIdAsync(id);
//            if (existingRow == null)
//            {
//                return NotFound();
//            }

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

namespace MD.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RowModelController : ControllerBase
    {
        private readonly IRowRepository _repository;
        private readonly IMapper _mapper;

        public RowModelController(IRowRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: /RowModel
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rowModels = await _repository.GetAllAsync();
            return Ok(rowModels);
        }

        // GET: /RowModel/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var rowModel = await _repository.GetByIdAsync(id);
            if (rowModel == null)
            {
                return NotFound();
            }
            return Ok(rowModel);
        }

        // POST: /RowModel
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRowVM rowModel)
        {
            if (rowModel == null)
            {
                return BadRequest();
            }

            var row = _mapper.Map<RowModel>(rowModel);

           
           row.Id = new IDgenerate().GenvetId("Row", '-', 10, 4, 3); // استخدم IDgenerate هنا كما هو في الموديل.


            await _repository.CreateAsync(row);
            return CreatedAtAction(nameof(GetById), new { id = row.Id }, rowModel);
        }

        // PUT: /RowModel/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(string id, [FromBody] UpdateRowVM rowModel)
        //{
        //    if (rowModel == null || id != rowModel.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var existingRow = await _repository.GetByIdAsync(id);
        //    if (existingRow == null)
        //    {
        //        return NotFound();
        //    }

        //    _mapper.Map(rowModel, existingRow);
        //    await _repository.UpdateAsync(existingRow);

        //    return NoContent();
        //}

        // DELETE: /RowModel/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingRow = await _repository.GetByIdAsync(id);
            if (existingRow == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
