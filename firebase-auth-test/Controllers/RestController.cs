namespace Firebase.AuthTest.Controllers
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [Route("/api")]
    public class RestController : Controller
    {
        /// <summary>
        /// GET:  /api/userIdNoScheme
        /// </summary>
        [HttpGet]
        [Route("userIdNoScheme")]
        [Authorize]
        public string RetrieveUserIdNoSchemeRestriction()
        {
            return this.User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        }

        /// <summary>
        /// GET:  /api/userIdWithScheme
        /// </summary>
        [HttpGet]
        [Route("userIdWithScheme")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public string RetrieveUserIdWithSchemeRestriction()
        {
            return this.User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        }
    }
}