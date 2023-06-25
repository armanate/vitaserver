using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessEntityIdController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBusinessEntityId _db;

        public BusinessEntityIdController(IBusinessEntityId db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        // GET: api/<CountryController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.GetALl());
        }

        // GET api/<CountryController>/5
        [HttpGet("{name}")]
        [Authorize]
        public async Task<IActionResult> Get(string name)
        {
            return Ok(await _db.GetById(name));
        }

        // POST api/<CountryController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BusinessEntityId model)
        {
            var res = await _db.Insert(model);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        // PUT api/<CountryController>/5
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] BusinessEntityId model)
        {

            var res = await _db.Update(model);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{name}")]
        [Authorize]
        public async Task<IActionResult> Delete(string name)
        {
            var res = await _db.Delete(name);
            if (res)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
