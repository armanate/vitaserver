﻿using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTAPI.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountryService _db;

        public CountryController(ICountryService db, IMapper mapper)
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
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await _db.GetById(id));
        }

        // POST api/<CountryController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CountryPost model)
        {
            var cmodel = _mapper.Map<CountryPost, Country>(model);
            var res = await _db.Insert(cmodel);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        // PUT api/<CountryController>/5
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] CountryPost model)
        {
            var cmodel = _mapper.Map<CountryPost, Country>(model);

            var res = await _db.Update(cmodel);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            var res = await _db.Delete(id);
            if (res)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
