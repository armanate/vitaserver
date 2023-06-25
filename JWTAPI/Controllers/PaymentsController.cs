using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using AuthorizeAttribute = JWTAPI.Authorization.AuthorizeAttribute;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentService _db;
        private readonly IUserService _userService;

        public PaymentsController(IUserService userService, IPaymentService db, IMapper mapper)
        {
            _userService = userService;
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
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentsPost model)
        {
            var paymentModel = new Payments();

            var user = await _userService.FindByPhone(model.Phone);
            if (user == null)
                return BadRequest();

            paymentModel.UserId = user.Id;
            paymentModel.PaymentDate = DateTime.UtcNow;
            paymentModel.ExpireDate = DateTime.UtcNow.AddDays(model.AccessDays);


            var res = await _db.Insert(paymentModel);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        // PUT api/<CountryController>/5
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] PaymentsPost model)
        {
            var cmodel = _mapper.Map<PaymentsPost, Payments>(model);

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
        // POST api/<CountryController>
        [HttpPost("userpayment")]
        [Authorize]
        public async Task<IActionResult> UserPayment(long id)
        {
            var res = await _db.GetPaymetByUserId(id);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        // POST api/<CountryController>
        [HttpGet("userpaymentnotexpier")]
        [Authorize]
        public async Task<IActionResult> UserPaymentٍNotExp()
        {
            User user = (User)HttpContext.Items["User"];

            var res = await _db.GetPaymetByUserIdNotExpier(user.Id);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
