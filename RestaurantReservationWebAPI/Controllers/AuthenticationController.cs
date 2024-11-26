using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationWebAPI.TokenGenerators;

namespace RestaurantReservationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IConfiguration _configuration;
        ITokenGenerator _jwtTokenGenerator;
        public AuthenticationController(IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            this._configuration = configuration;
            this._jwtTokenGenerator = tokenGenerator;
        }

        [HttpPost("authenticate")]

        public ActionResult<string> Authenticate(Admin admin)
        {

            string token = _jwtTokenGenerator.GenerateToken(admin.Username, admin.Password);
            return Ok(token);
        }

        [HttpGet("validate-token")]
        public ActionResult<bool> ValidateToken([FromQuery] string token)
        {
            bool isValid = _jwtTokenGenerator.ValidateToken(token);
            return Ok(isValid);
        }

    }
}
