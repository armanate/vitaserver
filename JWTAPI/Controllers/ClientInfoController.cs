using AuthorizeAttribute = JWTAPI.Authorization.AuthorizeAttribute;


namespace JWTAPI.Controllers
{
    [ApiController]
    [Route("/api/app")]
    public class ClientInfoController: ControllerBase
    {
        public readonly IClientInfoService _db;

        public ClientInfoController(IClientInfoService service)
        {
            _db= service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.GetClientInfo());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(ClientInfoPostModel clientInfo)
        {
            var result = await _db.UpdateClientInfo(clientInfo);
            if (result)
            {
                return Ok(result);
            }
            else
                return BadRequest();
        }
    }
}
