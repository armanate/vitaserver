using JWTAPI.Authorization;
using System.Net;
using System.Threading.Tasks;
using AuthorizeAttribute = JWTAPI.Authorization.AuthorizeAttribute;

namespace JWTAPI.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IAuthenticationService _auth;

    public UsersController(IUserService userService, IAuthenticationService aut, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
        _auth = aut;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(
        [FromBody] UserCredentialsResource userCredentials)
    {
        var user = _mapper.Map<UserCredentialsResource, User>(userCredentials);

        var response = await _userService.CreateUserAsync(user, ApplicationRole.Common);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        var userResource = _mapper.Map<User, UserResource>(response.User);

        return Ok(userResource);
    }
    [HttpPost("forget")]
    public async Task<IActionResult> ForgetAsync(
        [FromBody] ForgetPassword userCredentials)
    {
        var response = await _userService.UpdatePassword(userCredentials);

        if (response)
        {
            return Ok();
        }
        return BadRequest();

    }
    [HttpPost("sms")]
    public async Task<IActionResult> Sms(string phonenumber)
    {
        string respons = await _auth.SendSmsOtp(phonenumber);
        if (respons == "0")
        {
            return BadRequest("Error in Send Message Plz Call manager");
        }
        else if (respons == "1")
        {
            return StatusCode(429, "You Are Get Otp!!!");
        }
        if (!string.IsNullOrEmpty(respons))
        {
            return Ok(respons);
        }
        return BadRequest();

    }
    [HttpPost("chekingotp")]
    public async Task<IActionResult> ChekingExpireOtp(string phonenumber, int otp)
    {
        bool respons = await _auth.ChekingOtp(phonenumber, otp);

        if (respons)
        {
            return Ok(respons);
        }
        return BadRequest(respons);

    }

    [Authorize]
    [HttpGet("has_free_time")]
    public async Task<IActionResult> HasFreeTime()
    {
        User user = (User)HttpContext.Items["User"];
        var hoursSpended = (DateTime.Now - user.DateCreated).TotalMinutes;
        if (hoursSpended <= 60)
        {
            return Ok(new NetworkResponse(true, "You has free time"));
        }
        else
        {
            return Ok(new NetworkResponse(false, "You got no more free time"));
        }
    }

    [Authorize]
    [HttpGet("get_user")]
    public async Task<IActionResult> getUser()
    {
        User user = (User)HttpContext.Items["User"];
        return Ok(user);
    }


    [HttpPost("phoneLogin")]
    public async Task<IActionResult> PhoneLogin([FromBody] PhoneLoginCredentials credentials)
    {
        if (credentials.OtpCode.HasValue && credentials.OtpCode != 0)
        {
            bool otpResponse = await _auth.ChekingOtp(credentials.Phone, credentials.OtpCode.Value);

            if (otpResponse)
            {
                if (await _userService.FindByPhone(credentials.Phone) == null) //not found, create user than get token
                {
                    var createUserResponse = await _userService.CreateUserByPhone(credentials.Username, credentials.Phone, ApplicationRole.Common);

                    if (!createUserResponse.Success)
                    {
                        return BadRequest(new TokenResponse(false, createUserResponse.Message, null));
                    }

                    return await getToken(credentials.Phone);

                }
                else //user found, return token
                {
                    return await getToken(credentials.Phone);
                }
            }
            else
            {
                var tokenResponse = new TokenResponse(false, "Code is wrong!", null);
                return BadRequest(tokenResponse);
            }
        }
        else //send code to user phone
        {
            string respons = await _auth.SendSmsOtp(credentials.Phone);
            if (respons == "0")
            {
                return BadRequest(new TokenResponse(true, "Error in sending sms, call manager please.",null));
            }
            else if (respons == "1")
            {
                return Ok(new TokenResponse(true, "You got confirmation code", null));
            }
            if (!string.IsNullOrEmpty(respons))
            {
                return Ok(new TokenResponse(true, "Confirmation code sent to " + credentials.Phone,null));
            }
            return BadRequest();
        }
       

    }

    private async Task<IActionResult> getToken(string phone)
    {
        var tokenResponse = await _auth.CreateAccessTokenByPhone(phone);

        if (!tokenResponse.Success)
        {
            return BadRequest(tokenResponse);
        }
        else
        {
            return Ok(tokenResponse);
        }

    }

}